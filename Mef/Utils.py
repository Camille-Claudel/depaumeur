def clamp(value, min_value, max_value):
    """ Returns a clamped value between min_value and max_value """    
    return max(min(value, max_value), min_value)