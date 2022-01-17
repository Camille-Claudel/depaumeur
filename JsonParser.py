import json
import numpy as np
import Utils

def parse_raw_calibrations(json_string: str, mean_function = np.average):
    """ Returns a Tuple with 1st element : a list of all existing addresses and 2nd element : a dictionnary coords (key) gives a vector of signals (value) """
    data = json.loads(json_string)
    addresses = []
    coords_dict = {}
    
    # First pass over elements to get the final array and coordinates
    for element in data: # Unpacks data according to the raw_calibration_template.json
        for address in element["data"]: # Going over all addresses (same as .keys())
            if address not in addresses:
                addresses.append(address) # Adding the address if not already in the vector
    
    for element in data:
        vector = Utils.create_empty_vector(addresses)
        for address in element["data"]:
            Utils.insert_coord_in_vector(
                vector, 
                address, 
                mean_function(element["data"][address]), # Getting average of the addresses signal points
                addresses)
        coords_dict[tuple(element["coords"])] = vector

    return (addresses, coords_dict)

if __name__ == "__main__":
    # Not a real test, it prints values, sees if anything crashes, and you just check manually if things are printed correctly
    f = open("raw_calibration_template.json", "r")
    s = f.read()
    f.close()
    a, c = parse_raw_calibrations(s)
    print("\n".join(a))
    for k in c:
        print(k, c[k])