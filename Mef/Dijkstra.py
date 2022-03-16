class DijkstraTable:
    """ A table containing information to navigate the graph from a starting node to any node in the shortest distance """
    __slots__ = ("routes")
    
    def __init__(self, size: int):
        """ Creates a table, and sets the shortest_distance to Inf """
        self.routes = [(-1, -1)] * size

    def __setitem__(self, index: int, value: tuple):
        """ Sets an item of the table : tuple(distance, prev_vertex), sets only if the distance is shorter """
        if self.routes[index][0] > value[0] or self.routes[index][0] < 0 :
            self.routes[index] = value

    def __getitem__(self, index: int) -> tuple:
        """ Returns a tuple(distance, prev_vertex) of the shortest distance, and the vertex to come from """
        return self.routes[index]
        
def get_dijkstra_table(graph, start_vertex):
    """ Returns a table of the shortest paths to all vertices in the graph from start_vertex """
    visited = []
    unvisited = list(range(graph.get_vertex_count()))
    table = DijkstraTable(graph.get_vertex_count())
    current = start_vertex
    total_distance = 0
    while unvisited:
        neighbours = graph.get_vertex(current)
        for v,w in neighbours.items():
            if v in unvisited:
                table[v] = (total_distance + w, current)
        unvisited.remove(current)
        visited.append(current)
        closest_vertex = None
        closest_distance = None                                                                                                                                                                            
        for v in unvisited:
            distance = table[v][0]
            if (closest_distance is None) or ((distance < closest_distance) and (distance > 0)):
                closest_distance = distance
                closest_vertex = v
        current = closest_vertex
        total_distance = closest_distance
    return table 






