class DijkstraTable:
    """ A table containing information to navigate the graph from a starting node to any node in the shortest distance """
    __slots__ = ()
    
    def __init__(self, size: int):
        """ Creates a table, and sets the shortest_distance to Inf """
        # Create an empty array of `size` elements  
        pass

    def __setitem__(self, index: int, value: tuple):
        """ Sets an item of the table : tuple(distance, prev_vertex), sets only if the distance is shorter """
        # Compare with the current value of the table
        # Set if the new distance is shorter
        pass

    def __getitem__(self, index: int) -> tuple:
        """ Returns a tuple(distance, prev_vertex) of the shortest distance, and the vertex to come from """
        # Return the tuple at index
        pass

def get_dijkstra_table(graph, start_vertex):
    """ Returns a table of the shortest paths to all vertices in the graph from start_vertex """
    pass