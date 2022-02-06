from scapy.all import *
Conf.verb = 0
import numpy as np
import json
import os
from random import randint, sample
import time

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
    if pkt.haslayer(Dot11Beacon) or pkt.haslayer(Dot11ProbeResp):
        if not pkt.addr2 in data:
            data[pkt.addr2] = []
        # add a measure of signal strength to data[pkt.addr2] where pkt.addr2 is the sender of the packet
        data[pkt.addr2].append(dBmTomW(pkt.dBm_AntSignal))

# acquire data, just listening to beacons
def sniffDataPassive():
    print('starting sniffing')
    sniff(iface='mon0',prn=callback,count=nbMeasures)
    print('finished sniffing')

# acquire data, sending probe requests and listening to responses
def sniffDataActive(n):
    channel = chr(11)
    recipients_mac_adress = 'ff:ff:ff:ff:ff:ff'
    your_mac_adress = 'b8:27:eb:d0:01:c0'
    ssids = ['Eleves','Professeurs','Invites','']
    frames = [
    RadioTap()\
      /Dot11(type=0, subtype=4, addr1=recipients_mac_adress, addr2=your_mac_adress, addr3= recipients_mac_adress)\
      /Dot11ProbeReq()\
      /Dot11Elt(ID='SSID', info=i)\
      /Dot11Elt(ID='Rates', info='\x82\x84\x8b\x96\x0c\x12\x18')\
      /Dot11Elt(ID='ESRates', info='\x30\x48\x60\x6c')\
      /Dot11Elt(ID='DSset', info=channel)
    for i in ssids]

    t = AsyncSniffer(iface='mon0',filter='subtype probe-resp or subtype beacon',count=n)
    t.start()
    print('starting sniffing')
    pts = 0
    while t.running:
        pts = (pts + 1) % 4
        for i in frames:
            sendp(i,iface='mon0')
        print('.' * pts + '   ',end='\r')
        time.sleep(0.1)
    t.join()
    print('finished sniffing, formatting')
    res = {}
    for pkt in t.results:
        if pkt.haslayer(Dot11Beacon) or pkt.haslayer(Dot11ProbeResp):
            if not pkt.addr2 in data:
                res[pkt.addr2] = []
            # add a measure of signal strength to data[pkt.addr2] where pkt.addr2 is the sender of the packet
            res[pkt.addr2].append(dBmTomW(pkt.dBm_AntSignal))
    print('finished formatting')
    return res

# generate random data, for testing purposes
def generateDummyData():
    addrList = ['15:dd:be:cc:f8:de', 'e8:42:72:bf:c:53', '34:c1:a4:72:37:d3', '1f:3:d7:f1:2a:2a', '3c:96:a0:72:a5:5a', 'b5:59:59:1f:60:f9']
    for i in sample(addrList, randint(2,6)):
        data[i] = [dBmTomW(-randint(20,100)) for _ in range(randint(3,8))]

if __name__ == '__main__':
    data = sniffDataActive(nbMeasures)

    output = [{'coords':[x,y],'data':data}]

    # add the calibration point to the calibration json file
    currentContent = []
    if os.path.isfile(filename):
        with open(filename,'r') as f:
            currentContent = json.load(f)
    with open(filename,'w') as f:
        json.dump(currentContent + output,f)