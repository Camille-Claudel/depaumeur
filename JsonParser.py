import json
import numpy as np
import Utils

# Prefix necessary to keep the address when parsing
__WANTED_PREFIX = "34:8a:12:c" # You can put it in some config file if you want

def parse_raw_calibrations(json_string: str, mean_function = np.average, keep_last_digit: bool = False):
    """ Returns a Tuple with 1st element : a list of all existing addresses and 2nd element : a list of dictionnaries 'coords'->list 'data'-> measure vector"""
    data = json.loads(json_string)
    addresses = []
    calibration_points = []
    
    len_ass = len(__WANTED_PREFIX)
    def _get_addr(full_address: str): # Embedded function i know....
        """ Returns a tuple (is it an address that we care about, the address cut of its prefix) """
        addr_split = (
            address[:len_ass], 
            address[len_ass:] if keep_last_digit else address[len_ass:-1]
            ) # Cutting the address in 2 -> (prefix, address)
        return (addr_split[0] == __WANTED_PREFIX, addr_split[1])

    # First pass over elements to get the final array and coordinates
    for element in data: # Unpacks data according to the raw_calibration_template.json
        for address in element["data"]: # Going over all addresses (same as .keys())
            starts_with_prefix, addr = _get_addr(address)
            if (addr not in addresses) and starts_with_prefix: # If the address is not known, and starts with the right prefix
                addresses.append(addr) # Adding the address, as it doesnt exist

    for element in data:
        temp_v = [[] for a in addresses] # Creates a vector of lists size addresses
        for address in element["data"]: # Pass over items to get their values and then calculate their averages
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
                
        calibration_points.append({
            "coords":element["coords"],
            "data":vector
        })

    return (addresses, calibration_points)

if __name__ == "__main__":
    # Not a real test, it prints values, sees if anything crashes, and you just check manually if things are printed correctly
    f = open("raw_calibration_2101.json", "r")
    s = f.read()
    f.close()
    a, c = parse_raw_calibrations(s, keep_last_digit=False)
    print("\n".join(a))
    for k in c:
        print(k["coords"], len(k["data"]))
