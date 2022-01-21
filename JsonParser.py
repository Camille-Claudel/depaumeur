import json
import numpy as np
import Utils

__ADDRESS_START_STRING = "34:8a:12:c" # You can put it in some config file if you want

def parse_raw_calibrations(json_string: str, mean_function = np.average, keep_last_digit: bool = True):
    """ Returns a Tuple with 1st element : a list of all existing addresses and 2nd element : a dictionnary coords (key) gives a vector of signals (value) """
    data = json.loads(json_string)
    addresses = []
    coords_dict = {}
    
    len_ass = len(__ADDRESS_START_STRING)
    def _get_addr(full_address: str): # Embedded function i know....
        """ Returns a tuple (is it an address that we care about, the address cut of its prefix) """
        addr_split = (
            address[:len_ass], 
            address[len_ass:] if keep_last_digit else addresses[len_ass:-1]
            ) # Cutting the address in 2 -> (prefix, address)
        return (addr_split[0] == __ADDRESS_START_STRING, addr_split[1])

    # First pass over elements to get the final array and coordinates
    for element in data: # Unpacks data according to the raw_calibration_template.json
        for address in element["data"]: # Going over all addresses (same as .keys())
            addr = _get_addr(address)
            if (addr[1] not in addresses) and addr[0]: # If the address is not known, and starts with the right prefix
                addresses.append(addr[1]) # Adding the address, as it doesnt exist
    
    for element in data:
        vector = Utils.create_empty_vector(addresses)
        for address in element["data"]:
            addr = _get_addr(address)
            if not addr[0]: continue
            Utils.insert_coord_in_vector(
                vector, 
                addr[1], 
                mean_function(element["data"][address]), # Getting average of the addresses signal points
                addresses)
        coords_dict[tuple(element["coords"])] = vector

    return (addresses, coords_dict)

if __name__ == "__main__":
    # Not a real test, it prints values, sees if anything crashes, and you just check manually if things are printed correctly
    f = open("raw_calibration_2101.json", "r")
    s = f.read()
    f.close()
    a, c = parse_raw_calibrations(s)
    print("\n".join(a))
    for k in c:
        print(k, c[k])