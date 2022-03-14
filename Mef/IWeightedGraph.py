from abc import ABC, abstractmethod
from Matrix import Matrix

class IWeightedGraph(ABC): # Interface for a Weighted Graph
    __slots__ = (
        "_directional" # Protected readonly interface element
    )

    def __init__(self, directional):
        self._directional = directional # Determines if the graph has directional edges or not

    @abstractmethod
    def add_vertex(self, links: dict) -> int:
        """ Adds a vertex to the graph, using the `links` dict (with key:vertex_index, value:link_weight), returns the index (name) of the vertex """
        pass
    
    @abstractmethod
    def get_vertex(self, index: int) -> dict:
        """ Returns a `links` dict (with key:vertex_index, value:link_weight) from the index (name) of the vertex you want to get """
        pass

    @abstractmethod
    def set_vertex(self, index: int, links: dict):
        """ Sets the vertex `index` edges using the `links` dict (with key:vertex_index, value:link_weight) """
        pass

    @abstractmethod
    def get_matrix(self) -> Matrix:
        """ Returns an adjacency matrix representing the current graph """
        pass

    @abstractmethod
    def get_vertex_count(self) -> int:
        """ Returns the amount of vertices in said graph """
        pass

    @abstractmethod # Also static but i don't know how to make this work with python, there is also no support for multiple constructors
    def from_matrix(matrix: Matrix, directional: bool = False):
        """ Returns a graph made from a matrix """
        pass
    
    @abstractmethod
    def copy(self):
        """ Returns a copy of the graph """
        pass

    def __str__(self) -> str:
        s = ""
        for i in range(self.get_vertex_count()):
            sub_s = ""
            for k in self.get_vertex(i):
                sub_s += ' ' + str(k)
            s += '\n' + str(i) + " ->" + sub_s
        return s

    def get_edges_count(self) -> int:
        """ Returns the amount of edges """
        if self._directional:
            edges = []
            for i in range(self.get_vertex_count()):
                for k in self.get_vertex(i):
                    edge = (i, k)
                    if edge not in edges:
                        edges.append(edge)
            return len(edges)
        else:
            raise NotImplementedError("Not implemented")

    def get_degree(self, index: int) -> int:
        """ Returns the outdegrees of a vertex """
        return len(self.get_vertex())

    def is_eulerian(self):
        """ Returns if the graph is eulerian (We suppose the graph is connected) """
        for i in range(self.get_vertex_count()):
            if self.get_degree(i) % 2: # Is odd (See math graph wiki)
                return False
        return True

    