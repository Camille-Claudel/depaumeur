import enum

from Mef.IWeightedGraph import IWeightedGraph
from Mef.WeightedGraph import WeightedGraph
from Mef.WeightedListGraph import WeightedListGraph
from Mef.Matrix import Matrix

class _SavePattern(enum.Enum):
    UNKNOWN = -1
    MATRIX = 0
    AD_LIST = 1

def weighted_graph_to_json_format(graph: IWeightedGraph, global_format = False) -> list:
    """ Writes graph data as a json string to be saved. Global format determines if the format should be the same across all graph types """
    save = [_SavePattern.UNKNOWN.value, graph._directional, None] # Graph save pattern, directional, vertices
    if global_format:
        save[0] = _SavePattern.MATRIX.value
        save[2] = graph.get_matrix()._items
    elif isinstance(graph, WeightedListGraph):
        vertices = [graph.get_vertex(i) for i in range(graph.get_vertex_count())]
        save[0] = _SavePattern.AD_LIST.value
        save[2] = vertices
    elif isinstance(graph, WeightedGraph):
        save[0] = _SavePattern.MATRIX.value
        save[2] = graph.get_matrix()._items
    else:
        raise TypeError("Couldn't write graph data to json (Class type not recognized or supported)")
    return save

def weighted_graph_from_saved_data(save_data: list, WeightedGraphType = WeightedGraph):
    save_pattern, directional, data = save_data

    if save_pattern == _SavePattern.MATRIX.value:
        x, y = 0 if len(data) == 0 else len(data[0]), len(data)
        m = Matrix(x, y, False)
        m._items = data
        return WeightedGraphType.from_matrix(m, directional)
    elif save_pattern == _SavePattern.AD_LIST.value:
        graph = WeightedGraphType(directional)
        for vertex in data:
            graph.add_vertex({})
        for i, vertex in enumerate(data): # -> setting after adding the vertex to prevent out of range exceptions
            new_dict = {}
            for edge, weight in vertex.items(): # Copying dict because keys are strings when saved to json
                new_dict[int(edge)] = weight
            graph.set_vertex(i, new_dict)
        return graph
    else:
        raise TypeError("Coudln't parse over save_data (Not a readable graph format)")
