from IWeightedGraph import IWeightedGraph
from Matrix import Matrix

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
        for i, w in links:
            set_function(self._am, vi, i, w)

    def get_vertex(self, index: int) -> dict:
        """ Returns a `links` dict (with key:vertex_index, value:link_weight) from the index (name) of the vertex you want to get """
        assert index < self._vertices_count, "This vertex doesn't exist"
        d = {}
        for y in range(self._vertices_count):
            v = self._am.get(index, y)
            if v != 0:
                d[y] = v
        return d

    def set_vertex(self, index: int, links: dict):
        """ Sets the vertex `index` edges using the `links` dict (with key:vertex_index, value:link_weight) """
        def directional_set(matrix, x, y, value):
            matrix.set(x, y, value)

        def default_set(matrix, x, y, value):
            matrix.set(x, y, value)
            matrix.set(y, x, value)

        set_function = directional_set if self._directional else default_set

        for y in range(self._vertices_count):
            set_function(self._am, index, y, 0) # Resetting links

        for i, w in links:
            set_function(self._am, index, i, w)

    def get_matrix(self):
        return self._am.copy()

if __name__ == "__main__":
    graph = WeightedGraph(False)
