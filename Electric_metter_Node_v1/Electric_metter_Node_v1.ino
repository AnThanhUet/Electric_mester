#include <SoftwareSerial.h>
#include <PZEM004Tv30.h>
#include <avr/pgmspace.h>
#include "Reference.h"
#include <EEPROM.h>
#include <CRC.h>

#define M0 12
#define M1 13
#define Tx1 2
#define Rx1 3

#ifdef __arm__
// should use uinstd.h to define sbrk but Due causes a conflict
extern "C" char* sbrk(int incr);
#else  // __ARM__
extern char *__brkval;
#endif  // __arm__

PZEM004Tv30 pzem(9, 8);   //rx,tx
PZEM004Tv30 pzem1(7, 6);
PZEM004Tv30 pzem2(5, 4);
//SoftwareSerial LoRa(Rx1, Tx1);
#define LoRa Serial1


float voltage[3]  = {0.0};
float current[3]  = {0.0};
float power[3]  = {0.0};
float energy[3]  = {0.0};
float freq[3]  = {0.0};
float pf[3]  = {0.0};


boolean send = true;
boolean demo = true;
boolean CRC_flag = false;
//boolean demo = false;

unsigned long timeOld = 0;
unsigned long timeInterval = 5000; // mili second

byte mySRN[8] = {0};
byte pseudoSRN[8] = {0x00, 0x10, 0x20, 0x30, 0x40, 0x50, 0x60, 0x70};


union text {
  byte Byte;
  char Char;
} text;

union convert4byte {
  byte b_convert[4];
  char c_convert[4];
  float f_convert;
  long l_convert;
} convert4byte;

int freeMemory() {
  char top;
#ifdef __arm__
  return &top - reinterpret_cast<char*>(sbrk(0));
#elif defined(CORE_TEENSY) || (ARDUINO > 103 && ARDUINO != 151)
  return &top - __brkval;
#else  // __arm__
  return __brkval ? &top - __brkval : &top - __malloc_heap_start;
#endif  // __arm__
}

String cutString(String dataIn, int start, int end) {
  String dataOut = "";
  for (int i  = start; i < end + 1; i++) {
    dataOut += dataIn[i];
  }
  return dataOut;
}

void readData(PZEM004Tv30 pzemTemp, byte device) {
  Serial.println("----> readData");

  Serial.print("Ram free is: ");
  Serial.println(freeMemory());
  voltage[device] = pzemTemp.voltage();
  delay(10);
  current[device] = pzemTemp.current();
  delay(10);
  power[device] = pzemTemp.power();
  delay(10);
  energy[device] = pzemTemp.energy();
  delay(10);
  freq[device] = pzemTemp.frequency();
  delay(10);
  pf[device] = pzemTemp.pf();
  delay(10);

  Serial.println("<---- readData");
}

void assignData(byte amount, byte location, byte *p, char dataBlock[]) {
  for (int i_assignData = 0; i_assignData < amount; i_assignData++ ) {
    *p = dataBlock[i_assignData + location];
    p++;
  }
}

void println(char data[] , int size) {
  for (int i = 0; i < size; i++) {
    Serial.print("0x");
    Serial.print((byte)data[i], HEX);
    Serial.print(" ");
  }
  Serial.println();
}

boolean compare(byte source1[], byte source2[], byte length) {

  for ( int i = 0; i < length; i++) {
    if (source1[i] != source2[i]) {
      //Serial.println("false");
      return 0;
    }
  }
  //Serial.println("true");
  return 1;
}

//void burn(String data) {
void burn(char datablock[], int lengthOfData ) {


  //-------------------burn data----------------------//
  //int lengthOfData = data.length();
  //char datablock[lengthOfData] = {0};

  if (CRC_flag == true) {
    if (CRC::crc8(datablock, sizeof(datablock))) {
      Serial.println("ERROR packet, return!");
      return;
    }
  } else {
    Serial.println("don't check packet");
  }

  Serial.print("Data is: ");
  for (int i = 0; i < lengthOfData; i++) {
    //datablock[i] = data[i];
    Serial.print("0x");
    Serial.print((byte)datablock[i], HEX);
    Serial.print(" ");
  }
  Serial.println();

  byte MBT[2] = {0};
  byte LBT = 0;
  byte MTBG[5] = {0};
  byte MTBN[5] = {0};
  byte SRG[8] = {0};
  byte SRN[8] = {0};
  byte SLT = 0;

  assignData(2, 0, MBT, datablock);
  //assignData(1, 2, LBT, datablock);
  LBT = datablock[2];
  assignData(5, 3, MTBG, datablock);
  assignData(5, 8, MTBN, datablock);
  assignData(8, 13, SRG, datablock);
  assignData(8, 21, SRN, datablock);
  //assignData(1, 29, SLT, datablock);
  SLT = datablock[29];

  Serial.print("MBT = ");
  println(MBT, sizeof(MBT));
  Serial.print("LBT = 0x");
  Serial.println(LBT, HEX);
  Serial.print("MTBG = ");
  println(MTBG, sizeof(MTBG) );
  Serial.print("MTBN = ");
  println(MTBN, sizeof(MTBN) );
  Serial.print("SRG = ");
  println(SRG, sizeof(SRG) );
  Serial.print("SRN = ");
  println(SRN, sizeof(SRN) );
  Serial.print("SLT = 0x");
  Serial.println(SLT, HEX);

  if (SLT > 10) {
    Serial.println("Error packet data: Overflow SLT, return!");
    return;
  }

  byte TT[SLT][2];
  byte KDL[SLT] = {0};
  byte KTD[SLT] = {0};
  byte TTCB[SLT] = {0};
  byte DAT[SLT][8] = {0};

  byte m = 30;
  Serial.println("-----------");
  for (int i = 0; i < SLT; i++) {
    assignData(2, m,  TT[i], datablock);
    m += 2;
    Serial.print("TT = ");
    println(TT[i], 2);

    KDL[i] = datablock[m];
    m++;
    Serial.print("KDL = ");
    Serial.print("0x");
    Serial.println(KDL[i], HEX);

    KTD[i] = datablock[m];
    m++;
    Serial.print("KTD = ");
    Serial.print("0x");
    Serial.println(KTD[i], HEX);

    TTCB[i] = datablock[m];
    m++;
    Serial.print("TTCB = ");
    Serial.print("0x");
    Serial.println(TTCB[i], HEX);

    if (KTD[i] > 10) {
      Serial.println("Error packet data: Overflow KTD, return!");
      return;
    }

    assignData(KTD[i], m,  DAT[i], datablock);
    m += KTD[i];
    Serial.print("DAT = ");
    println(DAT[i], KTD[i]);
    Serial.println("-----------");

  }

  //-------------------handle data----------------------//
  // kiem tra MTBG -> MTBN -> SRN -> LBT -> goi tin yeu cau -> gui lai.
  if (compare(MTB_Gateway, MTBG , 5)) {
    Serial.println("Device get data is Gateway, next step.");
  } else {
    Serial.println("Devices strange, return.");
    return;
  }

  if (compare(MTB_Node, MTBN , 5)) {
    Serial.println("Device send data is Node, next step.");
  } else {
    Serial.println("Devices strange, return.");
    return;
  }

  if (compare(SRN, mySRN , 8)) {
    Serial.println("true SRN");
  } else {
    Serial.println("false SRN, return.");
    return;
  }

  if (LBT == LBT_request) {
    Serial.println("Get request!");
  }

  //-------------------create packet----------------------//
  String dataResponse = "";

  for (int i = 0; i < sizeof(MBT); i++) {               //MBT
    text.Byte = MBT[i];
    dataResponse += text.Char;
  }

  text.Byte = LBT_response;                             //LBT
  dataResponse += text.Char;

  for (int i = 0; i < sizeof(MTBN); i++) {              //MBTG
    text.Byte = MTBN[i];
    dataResponse += text.Char;
  }

  for (int i = 0; i < sizeof(MTBG); i++) {              //MBTN
    text.Byte = MTBG[i];
    dataResponse += text.Char;
  }

  for (int i = 0; i < sizeof(SRN); i++) {               //SRG
    text.Byte = SRN[i];
    dataResponse += text.Char;
  }

  for (int i = 0; i < sizeof(SRG); i++) {               //SRN
    text.Byte = SRG[i];
    dataResponse += text.Char;
  }

  text.Byte = SLT;                                      //SLT
  dataResponse += text.Char;

  for (int i = 0; i < SLT; i++) {

    for (int j = 0; j < sizeof(TT[i]); j++) {               //TT
      text.Byte = TT[i][j];
      dataResponse += text.Char;
    }

    if (compare(TT[i], TT_Vol, 2)) {                                          //-------0
      Serial.print("request vol ");                                           //-------1
      Serial.print(TTCB[i]);
      Serial.print("= ");
      Serial.print(voltage[i]);                                               //-------2
      Serial.print(", ");
      if (KDL[i] == KDL_Float) {
        text.Byte = KDL[i];
        dataResponse += text.Char;                          //KDL

        text.Byte = KDL_Float;
        dataResponse += text.Char;                          // KTD

        for (int j = 0; j < 3; j++) {
          if (TTCB[i] == j) {
            text.Byte = j;
            dataResponse += text.Char;                      // TTCB

            convert4byte.f_convert = voltage[j];                               //-------3
            for (int k = 0; k < 4; k++) {
              dataResponse += convert4byte.c_convert[k];
              Serial.print((byte)convert4byte.c_convert[k], HEX);
              Serial.print(" ");
            }
            Serial.println();
            break;
          }
        }
      } else {
        Serial.println("syntax format");
      }
    }

    if (compare(TT[i], TT_Amp, 2)) {                                          //-------0
      Serial.print("request current ");                                           //-------1
      Serial.print(TTCB[i]);
      Serial.print("= ");
      Serial.print(current[i]);                                               //-------2
      Serial.print(", ");
      if (KDL[i] == KDL_Float) {
        text.Byte = KDL[i];
        dataResponse += text.Char;                          //KDL

        text.Byte = KDL_Float;
        dataResponse += text.Char;                          // KTD

        for (int j = 0; j < 3; j++) {
          if (TTCB[i] == j) {
            text.Byte = j;
            dataResponse += text.Char;                      // TTCB

            convert4byte.f_convert = current[j];                               //-------3
            for (int k = 0; k < 4; k++) {
              dataResponse += convert4byte.c_convert[k];
              Serial.print((byte)convert4byte.c_convert[k], HEX);
              Serial.print(" ");
            }
            Serial.println();
            break;
          }
        }
      } else {
        Serial.println("syntax format");
      }
    }

    if (compare(TT[i], TT_Pow, 2)) {                                          //-------0
      Serial.print("request power ");                                           //-------1
      Serial.print(TTCB[i]);
      Serial.print("= ");
      Serial.print(power[i]);                                               //-------2
      Serial.print(", ");
      if (KDL[i] == KDL_Float) {
        text.Byte = KDL[i];
        dataResponse += text.Char;                          //KDL

        text.Byte = KDL_Float;
        dataResponse += text.Char;                          // KTD

        for (int j = 0; j < 3; j++) {
          if (TTCB[i] == j) {
            text.Byte = j;
            dataResponse += text.Char;                      // TTCB

            convert4byte.f_convert = power[j];                               //-------3
            for (int k = 0; k < 4; k++) {
              dataResponse += convert4byte.c_convert[k];
              Serial.print((byte)convert4byte.c_convert[k], HEX);
              Serial.print(" ");
            }
            Serial.println();
            break;
          }
        }
      } else {
        Serial.println("syntax format");
      }
    }

    if (compare(TT[i], TT_Eng, 2)) {                                          //-------0
      Serial.print("request energy ");                                           //-------1
      Serial.print(TTCB[i]);
      Serial.print("= ");
      Serial.print(energy[i]);                                               //-------2
      Serial.print(", ");
      if (KDL[i] == KDL_Float) {
        text.Byte = KDL[i];
        dataResponse += text.Char;                          //KDL

        text.Byte = KDL_Float;
        dataResponse += text.Char;                          // KTD

        for (int j = 0; j < 3; j++) {
          if (TTCB[i] == j) {
            text.Byte = j;
            dataResponse += text.Char;                      // TTCB

            convert4byte.f_convert = energy[j];                               //-------3
            for (int k = 0; k < 4; k++) {
              dataResponse += convert4byte.c_convert[k];
              Serial.print((byte)convert4byte.c_convert[k], HEX);
              Serial.print(" ");
            }
            Serial.println();
            break;
          }
        }
      } else {
        Serial.println("syntax format");
      }
    }

    if (compare(TT[i], TT_Freq, 2)) {                                          //-------0
      Serial.print("request freq ");                                           //-------1
      Serial.print(TTCB[i]);
      Serial.print("= ");
      Serial.print(freq[i]);                                               //-------2
      Serial.print(", ");
      if (KDL[i] == KDL_Float) {
        text.Byte = KDL[i];
        dataResponse += text.Char;                          //KDL

        text.Byte = KDL_Float;
        dataResponse += text.Char;                          // KTD

        for (int j = 0; j < 3; j++) {
          if (TTCB[i] == j) {
            text.Byte = j;
            dataResponse += text.Char;                      // TTCB

            convert4byte.f_convert = freq[j];                               //-------3
            for (int k = 0; k < 4; k++) {
              dataResponse += convert4byte.c_convert[k];
              Serial.print((byte)convert4byte.c_convert[k], HEX);
              Serial.print(" ");
            }
            Serial.println();
            break;
          }
        }
      } else {
        Serial.println("syntax format");
      }
    }

    if (compare(TT[i], TT_Pf, 2)) {                                          //-------0
      Serial.print("request pf ");                                           //-------1
      Serial.print(TTCB[i]);
      Serial.print("= ");
      Serial.print(pf[i]);                                               //-------2
      Serial.print(", ");
      if (KDL[i] == KDL_Float) {
        text.Byte = KDL[i];
        dataResponse += text.Char;                          //KDL

        text.Byte = KDL_Float;
        dataResponse += text.Char;                          // KTD

        for (int j = 0; j < 3; j++) {
          if (TTCB[i] == j) {
            text.Byte = j;
            dataResponse += text.Char;                      // TTCB

            convert4byte.f_convert = pf[j];                               //-------3
            for (int k = 0; k < 4; k++) {
              dataResponse += convert4byte.c_convert[k];
              Serial.print((byte)convert4byte.c_convert[k], HEX);
              Serial.print(" ");
            }
            Serial.println();
            break;
          }
        }
      } else {
        Serial.println("syntax format");
      }
    }
  }

  Serial.print("dataResponse after assign = ");
  Serial.println(dataResponse);
  dataResponse = "";
}



void getSerialNumber() {
  //  for (int i = 0; i < 8; i++) {
  //    EEPROM.write(i, pseudoSRN[i]);
  //    delay(5);
  //  }

  for (int i = 0; i < 8; i++) {
    mySRN[i] = EEPROM.read(i);
  }

  Serial.print("My Serial Number is: ");
  println(mySRN, 8);
}

void setup() {
  Serial.begin(9600);
  LoRa.begin(9600);
  timeOld = millis();

  pinMode(M0, OUTPUT);
  pinMode(M1, OUTPUT);

  digitalWrite(M0, 0);
  digitalWrite(M1, 0);

  getSerialNumber();

}

void loop() {

  setting();

  if (millis() >= timeOld) {
    if (millis() - timeOld > timeInterval) {

      if (demo == false) {
        readData(pzem, 0);
        readData(pzem1, 1);
        readData(pzem2, 2);
      }
      else {
        Serial.println("Demo Mode");
        for (int device = 0; device < 3; device ++) {
          voltage[device]  = 99.9;
          current[device]  = 88.8;
          power[device]  = 77.7;
          energy[device]  = 66.6;
          freq[device]  = 55.5;
          pf[device]  = 44.4;
        }
      }
      timeOld = millis();
    }
  }
  //  else {
  //    if (timeOld - millis() > timeInterval) {
  //      Serial.println("run ok");
  //      readData(pzem, 0);
  //      readData(pzem1, 1);
  //      readData(pzem2, 2);
  //      timeOld = millis();
  //    }
  //}


  if (LoRa.available()) {
    char dataBlock[100] = {0};
    int k = 0;
    delay(100);
    while (LoRa.available()) {
      dataBlock[k] = (byte)LoRa.read();
      k++;
    }
    burn(dataBlock, sizeof(dataBlock));
  }
}

void setting() {
  if (Serial.available() > 1) {
    String data;
    data = Serial.readString();
    //Serial.print("data is: ");
    //Serial.println(data);
    String first = cutString(data, 0, 6);
    if (first == "setSRN:") {
      int end = data.indexOf(";");
      if (end == -1) {
        Serial.println("- wrong syntax, data type: add [address] [phone number];\n- true example: add 0 0123456789;");
        return;
      }
    }
    String newSRN = cutString(data, 7, 14);

    for (int i = 0; i < 8; i++) {
      EEPROM.write(i, ((byte)newSRN[i] ));
      delay(5);
    }

    getSerialNumber();
  }
}
