import numpy as np

def get_closest_point(signal,calibration):
    #give coords of the closest calibration point
    closest_point = ( 0, 0)
    closest_distance = 0
    signal_vector = np.array(signal) 
    for i in calibration:
        calibration_vector = np.array(i["data"])
        dst = np.linalg.norm(calibration_vector - signal_vector) 
        if dst < closest_distance:
            closest_distance = dst
            closest_point = i["coords"]
    return closest_point
