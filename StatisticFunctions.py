import numpy as np
from scipy.optimize import curve_fit

def gaussian(x, mean, std, amplitude):
    return amplitude * np.exp(-0.5*((x-mean)/std)**2)

def advanced_mean(intensities):
    # x: intensities that have been measured
    # y: number of times these intensities were measured
    # f such that f(x)=y: distribution function
    # this function finds the best gaussian approximation of f and returns its mean
    x,y=np.unique(intensities, return_counts=True)
    initial_guess=(np.mean(intensities),np.std(intensities),max(y)) # starting guess for the optimization
    params, cov = curve_fit(gaussian, x, y, p0=initiali_guess) # cov: 2d array of the estimated covariance, useless for us
    return params[0]
