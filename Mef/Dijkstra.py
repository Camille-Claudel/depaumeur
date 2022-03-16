class DijkstraTable:
    """ A table containing information to navigate the graph from a starting node to any node in the shortest distance """
    __slots__ = ("routes", "start_vertex")
    
    def __init__(self, graph, start_vertex):
        """ Creates a table,  """
        self.routes = [(-1, -1)] * graph.get_vertex_count()
        self.start_vertex = start_vertex
        visited = []
        unvisited = list(range(graph.get_vertex_count()))
        current = start_vertex
        total_distance = 0
        while unvisited:
            neighbours = graph.get_vertex(current)
            for v,w in neighbours.items():
                if v in unvisited:
                    self[v] = (total_distance + w, current)
            unvisited.remove(current)
            visited.append(current)
            closest_vertex = None
            closest_distance = None                                                                                                                                                                            
            for v in unvisited:
                distance = self[v][0]
                if ((closest_distance is None) or (distance < closest_distance)) and (distance > 0):
                    closest_distance = distance
                    closest_vertex = v
            current = closest_vertex
            total_distance = closest_distance
        self[start_vertex] = (0, start_vertex) # Setting the start vertex to a distance of 0, and we can get to it from itself

    def __setitem__(self, index: int, value: tuple):
        """ Sets an item of the table : tuple(distance, prev_vertex), sets only if the distance is shorter """
        if self.routes[index][0] > value[0] or self.routes[index][0] < 0 :
            self.routes[index] = value

    def __getitem__(self, index: int) -> tuple:
        """ Returns a tuple(distance, prev_vertex) of the shortest distance, and the vertex to come from """
        return self.routes[index]

    def __str__(self):
        return str(self.routes)

    def get_path(self, end_vertex: int) -> tuple:
        """ Returns 2 lists -> The list of vertex from start to end (size n), a list of the distances from vertex to vertex (size n-1) """
        vertex_path = [end_vertex]
        distances = []

        current_vertex = end_vertex
        prev_dist = 0
        while current_vertex != self.start_vertex:
            d, v = self[current_vertex]
            prev_dist, _ = self[v]
            vertex_path.insert(0, v)
            distances.insert(0, d - prev_dist)
            current_vertex = v

        return vertex_path, distances

if __name__ == "__main__":
    from WeightedListGraph import WeightedListGraph
    graph = WeightedListGraph(False)
    graph.add_vertex({})
    graph.add_vertex({0:2})
    graph.add_vertex({1:5})
    graph.add_vertex({2:42})
    graph.add_vertex({0:4,2:14,3:7})

    print(graph)
    table = DijkstraTable(graph, 2)
    path, distances = table.get_path(4)
    print(path)
    print(distances)