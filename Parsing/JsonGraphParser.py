import enum

from Mef.IWeightedGraph import IWeightedGraph
from Mef.WeightedGraph import WeightedGraph
from Mef.WeightedListGraph import WeightedListGraph

class _SavePattern(enum.Enum):
    UNKNOWN = -1
    MATRIX = 0
    AD_LIST = 1

def weighted_graph_to_json_format(graph: IWeightedGraph, global_format = False) -> list:
    """ Writes graph data as a json string to be saved. Global format determines if the format should be the same across all graph types """
    save = [_SavePattern.UNKNOWN, graph._directional, None] # Graph save pattern, directional, vertices
    if global_format:
        save[0] = _SavePattern.MATRIX
        save[2] = graph.get_matrix()
    elif isinstance(graph, WeightedListGraph):
        vertices = [graph.get_vertex(i) for i in range(graph.get_vertex_count())]
        save[0] = _SavePattern.AD_LIST
        save[2] = vertices
    elif isinstance(graph, WeightedGraph):
        save[0] = _SavePattern.MATRIX
        save[2] = graph.get_matrix()
    else:
        raise TypeError("Couldn't write graph data to json (Class type not recognized or supported)")
    return save

def weighted_graph_from_saved_data(save_data: list, WeightedGraphType = WeightedGraph):
    save_pattern, directional, data = save_data

    if save_pattern == _SavePattern.MATRIX:
        return WeightedGraphType.from_matrix(data, directional)
    elif save_pattern == _SavePattern.AD_LIST:
        graph = WeightedGraphType(directional)
        for vertex in data:
            graph.add_vertex(vertex)
        return graph
    else:
        raise TypeError("Coudln't parse over save_data (Not a readable graph format)")
