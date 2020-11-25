

#!/usr/bin/env python
import time
import serial
import array as arr
import numpy as np



class Node:
    def __init__(self, SRG, SRN, amountSensor):
        self.SRG = SRG
        self.SRN = SRN
        self.amountSensor = amountSensor
    

    def request(self,MTB, ser):
        #dataSend = hex(MBT)
        #dataSend = 0x0A
        #dataSend.append(MTB%256)
        dataSend = arr.array('b', [0x00, MTB%256, 0x0A])
        arraySRG = np.array(list(self.SRG), dtype=int)
        arraySRN = np.array(list(self.SRN), dtype=int)
        dataSend += arr.array('b', arraySRG)
        dataSend += arr.array('b', arraySRN)
#         dataSend += arr.array('b', MTB%256)        
        print(dataSend)
        ser.write(dataSend)
        ser.flush()

ser = serial.Serial(
    port='/dev/ttyUSB0',
    baudrate = 9600,
    parity=serial.PARITY_NONE,
    stopbits=serial.STOPBITS_ONE,
    bytesize=serial.EIGHTBITS,
    timeout=0.5
)
counter=0
while 1:
    x=ser.read(200)
    lenX = len(x)
    if lenX!=0:
        for i in range(0,lenX):
            y = x[i]
            print (hex(x[i]), end = ' ')
        print ()
        print ("lenngth(x) = ",lenX)
        print ("-----------------")
        node = Node("87654321", "12345678", 1)
        MTB = 2
        node.request(MTB,ser)
        
