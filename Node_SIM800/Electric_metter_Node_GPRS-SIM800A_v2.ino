#define TINY_GSM_MODEM_SIM800
// #define TINY_GSM_MODEM_SIM808
// #define TINY_GSM_MODEM_SIM900
// #define TINY_GSM_MODEM_UBLOX
// #define TINY_GSM_MODEM_BG96
// #define TINY_GSM_MODEM_A6
// #define TINY_GSM_MODEM_A7
// #define TINY_GSM_MODEM_M590
// #define TINY_GSM_MODEM_ESP8266
// #define TINY_GSM_MODEM_XBEE

#include <SoftwareSerial.h>
#include <PZEM004Tv30.h>
#include <avr/pgmspace.h>
#include <TinyGsmClient.h>
#include <PubSubClient.h>

#define TOKEN "CLKK_Token"
#define SerialMon Serial
#define SerialAT Serial1
#define LED_PIN 13

// or Software Serial on Uno, Nano
//#include <SoftwareSerial.h>
//SoftwareSerial SerialAT(2, 3); // RX, TX


// Your GPRS credentials
// Leave empty, if missing user or pass
const char apn[]  = "v-internet";
const char user[] = "";
const char pass[] = "";

// MQTT details
const char* broker = "tntholdings.ddns.net";

const char* topicLed = "v1/devices/me/telemetry";
const char* topicInit = "v1/devices/me/telemetry";
const char* topicLedStatus = "v1/devices/me/telemetry";

TinyGsm modem(SerialAT);
TinyGsmClient client(modem);
PubSubClient mqtt(client);

int ledStatus = LOW;
long lastReconnectAttempt = 0;

PZEM004Tv30 pzem(9, 8);   //rx,tx
PZEM004Tv30 pzem1(7, 6);
PZEM004Tv30 pzem2(5, 4);

float voltage[3]  = {0.0};
float current[3]  = {0.0};
float power[3]  = {0.0};
float energy[3]  = {0.0};
float freq[3]  = {0.0};
float pf[3]  = {0.0};


boolean send = true;
//boolean demo = true;
boolean demo = false;

unsigned long timeOld = 0;
unsigned long timeInterval = 5000; // mili second

void setup() {
  timeOld = millis();

  pinMode(LED_PIN, OUTPUT);
  SerialMon.begin(9600);
  delay(10);
  SerialAT.begin(9600);
  delay(3000);
  SerialMon.println("Initializing modem...");
  modem.restart();

  String modemInfo = modem.getModemInfo();
  SerialMon.print("Modem: ");
  SerialMon.println(modemInfo);

  SerialMon.print("Waiting for network...");
  if (!modem.waitForNetwork()) {
    SerialMon.println(" fail");
    while (true);
  }
  SerialMon.println(" OK");

  SerialMon.print("Connecting to ");
  SerialMon.print(apn);
  if (!modem.gprsConnect(apn, user, pass)) {
    SerialMon.println(" fail");
    while (true);
  }
  SerialMon.println(" OK");

  // MQTT Broker setup
  mqtt.setServer(broker, 1883);
  mqtt.setCallback(mqttCallback);
}

boolean mqttConnect() {
  SerialMon.print("Connecting to ");
  SerialMon.print(broker);

  // Connect to MQTT Broker
  //boolean status = mqtt.connect("{\"ALO\":\"123\"}");

  // Or, if you want to authenticate MQTT:
  boolean status = mqtt.connect("SIM", TOKEN, "");

  if (status == false) {
    SerialMon.println(" fail");
    return false;
  }
  SerialMon.println(" OK");
  mqtt.publish(topicInit, "{\"ALO\":\"123\"}");
  mqtt.subscribe(topicLed);
  return mqtt.connected();
}

void readData(PZEM004Tv30 pzemTemp, int device) {
  if (demo == false) {

    voltage[device] = pzemTemp.voltage();
    Serial.println("Read 2");
    delay(10);
    current[device] = pzemTemp.current();
    Serial.println("Read 3");
    delay(10);
    power[device] = pzemTemp.power();
    Serial.println("Read 4");
    delay(10);
    energy[device] = pzemTemp.energy();
    Serial.println("Read 5");
    delay(10);
    freq[device] = pzemTemp.frequency();
    Serial.println("Read 6");
    delay(10);
    pf[device] = pzemTemp.pf();
    Serial.println("Read 7");
    //delay(10);
  } else {
    //    //Serial.println("Demo Mode, all = random");
    //    voltage  = random(190, 300);
    //    current  = random(0, 100);
    //    power  = random(0, 10000);
    //    energy  = random(0, 1000);
    //    freq  = random(50, 55);
    //    pf  = random(0, 300);
  }
}

void loop() {

  if (!mqtt.connected()) {
    SerialMon.println("=== MQTT NOT CONNECTED ===");
    // Reconnect every 10 seconds
    unsigned long t = millis();
    if (t - lastReconnectAttempt > 10000L) {
      lastReconnectAttempt = t;
      if (mqttConnect()) {
        lastReconnectAttempt = 0;
      }
    }
    delay(100);
    return;
  }

  mqtt.loop();
  //mqtt.publish(topicInit, "{\"ALO\":\"123\"}");





  if (millis() >= timeOld) {
    if (millis() - timeOld > timeInterval) {
      send  = true;
    }
    Serial.println("step 2");
  } else {
    Serial.println("step 3");
    if (timeOld - millis() > timeInterval) {
      send  = true;;
    }
  }

  if (send == true) {
    Serial.println("Send");

    readData(pzem, 0);
    readData(pzem1, 1);
    readData(pzem2, 2);

    String dataSend = "{\"vol\":\"";
    dataSend += String(voltage[0]);
    dataSend += "\",\"cur\":\"";
    dataSend += String(current[0]);
    dataSend += "\",\"pow\":\"";
    dataSend += String(power[0]);
    dataSend += "\",\"freq\":\"";
    dataSend += String(freq[0]);
    dataSend += "\",\"ene\":\"";
    dataSend += String(energy[0]);
    dataSend += "\",\"pf\":\"";
    dataSend += String(pf[0]);
    dataSend += "\"}";
    char* send = "";
    dataSend.toCharArray(send, dataSend.length());
    mqtt.publish(topicInit, send);

    dataSend = "";
    dataSend = "{\"vol1\":\"";
    dataSend += String(voltage[1]);
    dataSend += "\",\"cur1\":\"";
    dataSend += String(current[1]);
    dataSend += "\",\"pow1\":\"";
    dataSend += String(power[1]);
    dataSend += "\",\"freq1\":\"";
    dataSend += String(freq[1]);
    dataSend += "\",\"ene1\":\"";
    dataSend += String(energy[1]);
    dataSend += "\",\"pf1\":\"";
    dataSend += String(pf[1]);
    dataSend += "\"}";
    send = "";
    dataSend.toCharArray(send, dataSend.length());
    mqtt.publish(topicInit, send);

    dataSend = "";
    dataSend = "{\"vol2\":\"";
    dataSend += String(voltage[2]);
    dataSend += "\",\"cur2\":\"";
    dataSend += String(current[2]);
    dataSend += "\",\"pow2\":\"";
    dataSend += String(power[2]);
    dataSend += "\",\"freq2\":\"";
    dataSend += String(freq[2]);
    dataSend += "\",\"ene2\":\"";
    dataSend += String(energy[2]);
    dataSend += "\",\"pf2\":\"";
    dataSend += String(pf[2]);
    send = "";
    dataSend.toCharArray(send, dataSend.length());
    mqtt.publish(topicInit, send);

    dataSend = "";
    timeOld = millis();
    Serial.println("Send done!");
  }
  send  = false;

}

void mqttCallback(char* topic, byte* payload, unsigned int len) {
  SerialMon.print("Message arrived [");
  SerialMon.print(topic);
  SerialMon.print("]: ");
  SerialMon.write(payload, len);
  SerialMon.println();

  // Only proceed if incoming message's topic matches
  if (String(topic) == topicLed) {
    ledStatus = !ledStatus;
    digitalWrite(LED_PIN, ledStatus);
    mqtt.publish(topicLedStatus, ledStatus ? "1" : "0");
  }
}
