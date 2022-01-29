from scapy.all import sniff, Dot11Beacon
import numpy as np
import json
import os
from random import randint, sample

filename = 'raw_calibration.json'
nbMeasures = 1000 # number of bacon packets to sniff

# coordinates of the calibration point
x = float(input('x: '))
y = float(input('y: '))

# dictionnary of addresses associated with lists of signal strengths
data = {}

# convert signal strength from dBm to mW
def dBmTomW(L):
    return 10**(L/10)

# executed for each sniffed packet
def callback(pkt):
    if pkt.haslayer(Dot11Beacon):
        if not pkt.addr2 in data:
            data[pkt.addr2] = []
        # add a measure of signal strength to data[pkt.addr2] where pkt.addr2 is the sender of the packet
        data[pkt.addr2].append(dBmTomW(pkt.dBm_AntSignal))

# acquire data
def sniffData():
    print('starting sniffing')
    sniff(iface='mon0',prn=callback,count=nbMeasures)
    print('finished sniffing')

# generate random data, for testing purposes
def generateDummyData():
    addrList = ['15:dd:be:cc:f8:de', 'e8:42:72:bf:c:53', '34:c1:a4:72:37:d3', '1f:3:d7:f1:2a:2a', '3c:96:a0:72:a5:5a', 'b5:59:59:1f:60:f9']
    for i in sample(addrList, randint(2,6)):
        data[i] = [dBmTomW(-randint(20,100)) for _ in range(randint(3,8))]

if __name__ == '__main__':
    sniffData()

    output = [{'coords':[x,y],'data':data}]

    # add the calibration point to the calibration json file
    currentContent = []
    if os.path.isfile(filename):
        with open(filename,'r') as f:
            currentContent = json.load(f)
    with open(filename,'w') as f:
        json.dump(currentContent + output,f)

