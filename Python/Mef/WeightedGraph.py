from Mef.IWeightedGraph import IWeightedGraph
from Mef.Matrix import Matrix

class WeightedGraph(IWeightedGraph):
    __slots__ = (
        "_am",
        "_vertices_count"
    )
    def __init__(self, directional):
        """ Creates a weighted graph using an adjacency matrix """
        super().__init__(directional)
        self._am = Matrix(0, 0) # am for Adjacency Matrix
        self._vertices_count = 0

    def add_vertex(self, links: dict) -> int:
        """ Adds a vertex to the graph, using the `links` dict (with key:vertex_index, value:link_weight), returns the index (name) of the vertex """
        def directional_set(matrix, x, y, value):
            matrix.set(x, y, value)

        def default_set(matrix, x, y, value):
            matrix.set(x, y, value)
            matrix.set(y, x, value)

        set_function = directional_set if self._directional else default_set

        vi = self._vertices_count # Vertex Index
        self._vertices_count += 1
        self._am = self._am.get_resized(self._vertices_count, self._vertices_count)
        for i, w in links.items():
            set_function(self._am, i, vi, w)

    def get_vertex(self, index: int) -> dict:
        """ Returns a `links` dict (with key:vertex_index, value:link_weight) from the index (name) of the vertex you want to get """
        assert index < self._vertices_count, "This vertex doesn't exist"
        d = {}
        for y in range(self._vertices_count):
            v = self._am.get(y, index)
            if v != 0:
                d[y] = v
        return d

    def set_vertex(self, index: int, links: dict):
        """ Sets the vertex `index` edges using the `links` dict (with key:vertex_index, value:link_weight) """
        def directional_set(matrix, line, x, y, value):
            line[x] = value

        def default_set(matrix, line, x, y, value):
            line[x] = value
            matrix.set(y, x, value)

        set_function = directional_set if self._directional else default_set

        line = [0] * self._vertices_count
        
        for i, w in links.items():
            set_function(self._am, line, i, index, w)

        self._am._items[index] = line

    def get_matrix(self):
        return self._am.copy()

    def get_vertex_count(self) -> int:
        """ Returns the amount of vertices in said graph """
        return self._vertices_count

    @staticmethod
    def from_matrix(matrix: Matrix, directional: bool = False):
        """ Returns a graph made from a matrix """
        g = WeightedGraph(directional)
        g._am = matrix.copy()
        g._vertices_count = g._am.size_tuple[0]
        return g

    def copy(self):
        """ Returns a copy of the graph """
        return WeightedGraph.from_matrix(self._am, self._directional)

    def __str__(self):
        return str(self._am)

if __name__ == "__main__":
    graph = WeightedGraph(False)
    graph.add_vertex({})
    graph.add_vertex({0:42})
    graph.add_vertex({1:69})
    graph.add_vertex({0:666, 1:420})
    print(graph.get_vertex(0))
    print(graph.get_vertex(3))
    print(graph)
    graph.set_vertex(1, {0:42, 2:55})
    print(graph)