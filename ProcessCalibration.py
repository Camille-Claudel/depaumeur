import JsonParser
from StatisticFunctions import advanced_mean
import json

raw_calibration_fn = 'raw_calibration.json' # input
AP_list_fn = 'AP_list.json' # output
calibration_fn = 'calibration.json' # output


with open(raw_calibration_fn, 'r') as f:
    json_str = f.read()

addresses, calibration = JsonParser.parse_raw_calibrations(json_str, advanced_mean, False)

with open(AP_list_fn, 'w') as f:
    f.write(json.dumps(addresses))
with open(calibration_fn, 'w') as f:
    f.write(json.dumps(calibration))
