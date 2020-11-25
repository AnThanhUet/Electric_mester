
#!/usr/bin/env python
import time
import serial
import array as arr
import numpy as np
import codecs
import struct
import mysql.connector
import paho.mqtt.client as mqtt
import json

TT_vol2 = bytes([0XF1, 0x00])

TT_vol = arr.array('B', [0XF1, 0x00])
TT_cur = arr.array('B', [0xF2, 0x00])
TT_frq = arr.array('B', [0xF3, 0x00])
TT_enr = arr.array('B', [0xF4, 0x00])
TT_pow = arr.array('B', [0xF5, 0x00])
TT_pf  = arr.array('B', [0xF6, 0x00])

KDL_float = 0x0A
KTD_float = 0x04
TTCB3 = 0x03
TTCB1 = 0x01

THINGSBOARD_HOST = 'tntholdings.ddns.net'
ACCESS_TOKEN = 'GatewayF20_Token'

class Node:
    def __init__(self, SRG, SRN, amountSensor):
        self.SRG = SRG
        self.SRN = SRN
        self.amountSensor = amountSensor
    

    def request(self, MBT, ser, SLT):
        #dataSend.append(MTB%256)
        ch = 'c'
        #chOut = codecs.encode(ch, "hex")
        dataSend = arr.array('B', [int(MBT/256), int(MBT%256), 0x0A])
        dataSend += arr.array('B', [0x45, 0x4D, 0X47, 0X4C, 0X00])
        dataSend += arr.array('B', [0x45, 0x4D, 0X4E, 0X33, 0X4C])
        listSRG = list(self.SRG)
        listSRN = list(self.SRN)
        for i in range(0,7):
            dataSend.append(ord(listSRG[i]))
        for i in range(0,7):
            dataSend.append(ord(listSRN[i]))    
        if SLT == 1:
            dataSend.append(SLT)
                
            dataSend += TT_vol                
            dataSend.append(KDL_float)
            dataSend.append(KTD_float)
            dataSend.append(0)                
                
            dataSend += TT_cur
            dataSend.append(KDL_float)
            dataSend.append(KTD_float)
            dataSend.append(0)
                
            dataSend += TT_frq
            dataSend.append(KDL_float)
            dataSend.append(KTD_float)
            dataSend.append(0)
            
            dataSend += TT_enr
            dataSend.append(KDL_float)
            dataSend.append(KTD_float)
            dataSend.append(0)
            
            dataSend += TT_pow
            dataSend.append(KDL_float)
            dataSend.append(KTD_float)
            dataSend.append(0)
            
            dataSend += TT_pf
            dataSend.append(KDL_float)
            dataSend.append(KTD_float)
            dataSend.append(0)
            
        if SLT == 3:
            dataSend.append(SLT)
            for i in range(0,2):
                dataSend += TT_vol
                dataSend.append(KDL_float)
                dataSend.append(KTD_float)
                dataSend.append(i)                
            
                dataSend += TT_cur
                dataSend.append(KDL_float)
                dataSend.append(KTD_float)
                dataSend.append(i)
            
                dataSend += TT_frq
                dataSend.append(KDL_float)
                dataSend.append(KTD_float)
                dataSend.append(i)
            
                dataSend += TT_enr
                dataSend.append(KDL_float)
                dataSend.append(KTD_float)
                dataSend.append(i)
            
                dataSend += TT_pow
                dataSend.append(KDL_float)
                dataSend.append(KTD_float)
                dataSend.append(i)
            
                dataSend += TT_pf
                dataSend.append(KDL_float)
                dataSend.append(KTD_float)
                dataSend.append(i)
            
        print ("dataSend = ", end = '')
        for i in range(0,len(dataSend)):
            print (hex(dataSend[i]), end = ' ')
        print ("-----------------")
        ser.write(dataSend)
        ser.flush()

################################################################################################################################################################
def on_connect(client, userdata, rc, *extra_params):
    client.subscribe('v1/devices/me/rpc/request/+')
################################################################################################################################################################
def on_message(client, userdata, msg):
    print ('Topic: ' + msg.topic + '\nMessage: ' + str(msg.payload))
################################################################################################################################################################
def SubscribeToThingsboard():
    client = mqtt.Client()
    client.on_connect = on_connect
    client.on_message = on_message
    client.username_pw_set(ACCESS_TOKEN)
    client.connect(THINGSBOARD_HOST, 1883, 60)
    #client.loop_forever()
################################################################################################################################################################
def ConnectToMysql (host, userName, password, dataBase):
    myconn = mysql.connector.connect(host = host, user = userName, password = password, database = dataBase)
    print ("Database connected!")
    return myconn
################################################################################################################################################################
def InsertElectric1PData(sqlConnector, timestamp, deviceId, voltage, frequency, power, current, energy):
    cursor = sqlConnector.cursor()
    insertElectric1PDataSQL = "INSERT INTO electric1PData (timestamp, deviceId, voltage, frequency, power, current, energy) VALUES (%s, %s, %s, %s, %s, %s, %s)"
    cursor.execute(insertElectric1PDataSQL, (timestamp, deviceId, voltage, frequency, power, current, energy))
    sqlConnector.commit()
    sqlConnector.close()
################################################################################################################################################################
def SendDataToThingsboard ():
    THINGSBOARD_HOST = 'tntholdings.ddns.net'
    ACCESS_TOKEN = 'EM1Phase_Token'
    sensor_data = {'vol': 0, 'freq': 0, 'pow': 0, 'cur': 0, 'ene': 0}
    client = mqtt.Client()
    client.username_pw_set(ACCESS_TOKEN)
    client.connect(THINGSBOARD_HOST, 1883)
    client.loop_start()

    sensor_data['vol'] = voltage
    sensor_data['freq'] = frequency
    sensor_data['pow'] = power
    sensor_data['cur'] = current
    sensor_data['ene'] = energy
    client.publish('v1/devices/me/telemetry',json.dumps(sensor_data))
    time.sleep(2)
    client.loop_stop()
    client.disconnect()
################################################################################################################################################################
def GetListDeviceId (sqlConnector):
    GetListDeviceIdSQL = "SELECT deviceId FROM device WHERE chanelId = %d"
    chanelId = 123456
    cursor = sqlConnector.cursor()
    cursor.execute(GetListDeviceIdSQL, chanelId)
    result = cursor.fetchall()
    for x in result:
     print(x)

################################################################################################################################################################
def GetListchanelId (sqlConnector):
    GetListChanelIdSQL = "SELECT ChanelId FROM device WHERE gatewayId = %d"
    gatewayId = 123456
    cursor = sqlConnector.cursor()
    cursor.execute(GetListChanelIdSQL, gatewayId)
    result = cursor.fetchall()
    for x in result:
     print(x)

################################################################################################################################################################
ser = serial.Serial(
    port='/dev/ttyUSB0',
    baudrate = 9600,
    parity=serial.PARITY_NONE,
    stopbits=serial.STOPBITS_ONE,
    bytesize=serial.EIGHTBITS,
    timeout=0.5
)

while 1:
    SubscribeToThingsboard()
    dataReceive = ser.read(200)
    lenData = len(dataReceive)
    if lenData!=0:
        for i in range(0,lenData):
            y = dataReceive[i]
            print (hex(dataReceive[i]), end = ' ')
        print ()
        print ("lenngth(x) = ",lenData)
        print ("-----------------")
        node = Node("87654321", "12345678", 1)
        MBT = 2345
        node.request(MBT,ser, 1)
        
        MBT_N = dataReceive[0:2]
#         LBT_N = arr.array('B', [dataReceive[3]])
        LBT_N = dataReceive[2]
        MTBG_N = dataReceive[3:8]
        MTBN_N = dataReceive[8:13]
        SRG_N = dataReceive[13:21]
        SRN_N = dataReceive[21:29]
#         SLT_N = arr.array('B', [dataReceive[30]])
        SLT_N = dataReceive[29]
        
        
#         for i in range(0,2):
#             TT_N[i] = dataReceive[i]
        print("data MBT is: ", MBT_N.hex())
        print("data LBT is: ", hex(LBT_N))
        print("data MTBG is: ", MTBG_N.hex())
        print("data MTBN is: ", MTBN_N.hex())
        print("data SRG is: ", SRG_N.hex())
        print("data SRN is: ", SRN_N.hex())
        deviceId = SRN_N.decode("utf-8")
        timestamp = int(time.time())*1000
        timestamp = str(timestamp)
        print(timestamp, type(timestamp))
        print("data SLT is: ", hex(SLT_N))
        
        m = 30
        voltage = 0.0
        frequency = 0.0
        power = 0.0
        current = 0.0
        energy = 0.0
   
        for i in range(0, SLT_N):
#         for i in range(0, 1):
            TT_N = dataReceive[m:m+2]
            KDL_N = dataReceive[m+2]
            KTD_N = dataReceive[m+3]
            TTCB = dataReceive[m+4]
            DAT = dataReceive[m+5:m+5+KTD_N]
            
            
            m = m + 5 + KTD_N
            print("---------------")
            
            temp = arr.array('B', TT_N)
            
            print("data TT_N is: ", TT_N.hex(), end = '' )
            if((temp == TT_vol) and (KTD_N == 4) and (KDL_N == 0x0A)):
                print (" - DIEN AP")
                voltage = struct.unpack('f', DAT)
                voltage = voltage[0]
                print (voltage)
            if(temp == TT_cur and (KTD_N == 4) and (KDL_N == 0x0A)):
                print (" - CUONG DO")
                current = struct.unpack('f', DAT)
                current = current[0]
            if(temp == TT_frq and (KTD_N == 4) and (KDL_N == 0x0A)):
                print (" - TAN SO")
                frequency = struct.unpack('f', DAT)
                frequency = frequency[0]
            if(temp == TT_enr and (KTD_N == 4) and (KDL_N == 0x0A)):
                print (" - NANG LUONG")
                energy = struct.unpack('f', DAT)
                energy = energy[0]
            if(temp == TT_pow and (KTD_N == 4) and (KDL_N == 0x0A)):
                print (" - CONG SUAT")
                power = struct.unpack('f', DAT)
                power = power[0]
            if(temp == TT_pf and (KTD_N == 4) and (KDL_N == 0x0A)):
                print (" - HE SO CONG SUAT")
            
            print("data KDL_N is: ", hex(KDL_N), end = '' )
            if(KDL_N == 0x0A):
                print (" - float")
            print("data KTD_N is: ", hex(KTD_N))
            print("data TTCB is: ", hex(TTCB))
            print("data DAT is: ", DAT.hex())
#             print("m= ", m)            
            
            if((KTD_N == 4) and (KDL_N == 0x0A)):
                valueDAT = 0.0
                valueDAT = struct.unpack('f', DAT)
                print("Float value DAT= ", valueDAT)
            print("---------------")

        
        InsertElectric1PData(ConnectToMysql("localhost", "trung", "raspberry", "TNTHOLDINGS"), timestamp, deviceId, voltage, frequency, power, current, energy)
        #SubscribeToThingsboard()
######################################################################################################################################################

