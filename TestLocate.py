from scapy.all import *
Conf.verb = 0
import numpy as np
import json
import os
from random import randint, sample
import time
import Utils
import Locator

filename = 'raw_calibration.json'
nbMeasures = 100 # number of bacon packets to sniff

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
def sniffDataActive():
    channel = chr(11)
    recipients_mac_adress = 'ff:ff:ff:ff:ff:ff'
    your_mac_adress = 'b8:27:eb:d0:01:c0'
    ssids = ['Eleves','Profs','Invites','']
    frames = [
    RadioTap()\
      /Dot11(type=0, subtype=4, addr1=recipients_mac_adress, addr2=your_mac_adress, addr3= recipients_mac_adress)\
      /Dot11ProbeReq()\
      /Dot11Elt(ID='SSID', info=i)\
      /Dot11Elt(ID='Rates', info='\x82\x84\x8b\x96\x0c\x12\x18')\
      /Dot11Elt(ID='ESRates', info='\x30\x48\x60\x6c')\
      /Dot11Elt(ID='DSset', info=channel)
    for i in ssids]

    t = AsyncSniffer(iface='mon0',filter='subtype probe-resp or subtype beacon',count=nbMeasures)
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
    for i in t.results:
        callback(i)
    print('finished formatting')

# generate random data, for testing purposes
def generateDummyData():
    addrList = ['15:dd:be:cc:f8:de', 'e8:42:72:bf:c:53', '34:c1:a4:72:37:d3', '1f:3:d7:f1:2a:2a', '3c:96:a0:72:a5:5a', 'b5:59:59:1f:60:f9']
    for i in sample(addrList, randint(2,6)):
        data[i] = [dBmTomW(-randint(20,100)) for _ in range(randint(3,8))]

__WANTED_PREFIX = "34:8a:12:c"

len_ass = len(__WANTED_PREFIX)
def _get_addr(full_address: str): # Embedded function i know....
    """ Returns a tuple (is it an address that we care about, the address cut of its prefix) """
    addr_split = (
        address[:len_ass], 
        address[len_ass:-1]
        ) # Cutting the address in 2 -> (prefix, address)
    return (addr_split[0] == __WANTED_PREFIX, addr_split[1])

if __name__ == '__main__':
    sniffDataActive()

    with open('AP_list.json', 'r') as f:
        addresses = json.loads(f.read())
    with open('calibration.json', 'r') as f:
        calibration = json.loads(f.read())

    temp_v = [[] for a in addresses] # Creates a vector of lists size addresses
    for address in data: # Pass over items to get their values and then calculate their averages
        starts_with_prefix, addr = _get_addr(address)
        if not starts_with_prefix: continue
        temp_v[addresses.index(addr)] += element["data"][address]

    # This is the final vector for the `coords`
    vector = Utils.create_empty_vector(addresses)
    for i, intensity_values in enumerate(temp_v):
        if intensity_values: # List isn't empty
            vector[i] = mean_function(intensity_values)
        else:
            vector[i] = 0

    print(Locator.locate_fancy(vector,calibration))