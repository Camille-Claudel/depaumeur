from IWeightedGraph import IWeightedGraph
from Matrix import Matrix

class WeightedListGraph(IWeightedGraph):
    __slots__ = (
        "_vertices"
    )
    def __init__(self, directional):
        super().__init__(directional)
        self._vertices = [] # List of dictionnaries being links

    def add_vertex(self, links: dict) -> int:
        """ Adds a vertex to the graph, using the `links` dict (with key:vertex_index, value:link_weight), returns the index (name) of the vertex """
        self._vertices.append(dict(links))
        if not self._directional:
            for i, v in links.items():
                self._vertices[i][-1] = v # If the graph isn't directional, we update other vertices to share the links
        return len(self._vertices) - 1
        
    def get_vertex(self, index: int) -> dict:
        """ Returns a `links` dict (with key:vertex_index, value:link_weight) from the index (name) of the vertex you want to get """
        return dict(self._vertices[index]) # Using "dict" to copy the dictionnary

    def set_vertex(self, index: int, links: dict):
        """ Sets the vertex `index` edges using the `links` dict (with key:vertex_index, value:link_weight) """
        if not self._directional: # Preemtively remove every link to that vertex
            for i, v in self._vertices[index].items:
                self._vertices[i].pop(index)

        # Reset the vertex
        self._vertices[index] = dict(links)

        if not self._directional: # Recreate links if needed
            for i, v in links.items():
                self._vertices[i][index] = v

    def get_matrix(self):
        m = Matrix(len(self._vertices), len(self._vertices))
        
        for i, v in enumerate(self._vertices):
            for k, j in v.items():
                m.set(i, k, j) # j is the weight
        
        return m

if __name__ == "__main__":
    graph = WeightedListGraph(False)
    graph.add_vertex({})
    graph.add_vertex({0:42})
    graph.add_vertex({0:69})
    graph.add_vertex({0:666, 1:420})
    assert str(graph.get_matrix()) == "[0, 42, 69, 666]\n[0, 0, 0, 420]\n[0, 0, 0, 0]\n[666, 420, 0, 0]"
    print("Test(s) passed")