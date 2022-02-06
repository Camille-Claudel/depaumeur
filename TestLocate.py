import json
import Locator
import JsonParser
from probeably_bacon import sniffDataActive

AP_list_fn = 'AP_list.json'
calibration_fn = 'calibration.json'
nbMeasures = 100 # number of bacon packets to sniff

if __name__ == '__main__':
    data = sniffDataActive(nbMeasures)

    with open(AP_list_fn, 'r') as f:
        addresses = json.loads(f.read())
    with open(AP_list_fn, 'r') as f:
        calibration = json.loads(f.read())

    vector = JsonParser.parse_raw_measure(data,addresses)

    print(Locator.locate_fancy(vector,calibration))