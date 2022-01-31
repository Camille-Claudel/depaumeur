import numpy as np

def sqNorm(a):
    return np.inner(a,a)

def locate(signal,calibration):
    #give coords of the closest calibration point
    closest_point = ( 0, 0)
    closest_distance = 0
    signal_vector = np.array(signal) 
    for i in calibration:
        calibration_vector = np.array(i["data"])
        dst = sqNorm(calibration_vector - signal_vector) 
        if dst < closest_distance:
            closest_distance = dst
            closest_point = i["coords"]
    return closest_point

def locate_fancy(signal,calibration):
    # give coords of an interpolation between the 2 closest calibration points
    m = np.array(signal) 

    dsts = np.array([sqNorm(m - np.array(i["data"])) for i in calibration]) # calculate difference scores (s_i)
    # get the indexes a and b of the 2 smallest scores
    minIndexes = np.argpartition(dsts, 2)
    a = minIndexes[0]
    b = minIndexes[1]
    # get Pa, Pb, va, and vb
    pa = np.array(calibration[a]['coords'])
    pb = np.array(calibration[b]['coords'])
    va = np.array(calibration[a]['data'])
    vb = np.array(calibration[b]['data'])

    x = np.dot(vb - va, m - va) / sqNorm(vb - va)

    pu = pa + x * (pb - pa)

    return pu
