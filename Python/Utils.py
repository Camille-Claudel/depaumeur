def create_empty_vector(model: list) -> list:
    """ Creates an empty vector according to the address model """
    return [0] * len(model)

def insert_coord_in_vector(vector: list, address: str, value: float, model: list) -> None:
    """ Inserts the `value` into the corresponding coordinate of the `vector` index `address` according to the `model` """
    vector[model.index(address)] = value

def average(l: list) -> float: # not used can be discarded
    return float(sum(l)) / len(l)