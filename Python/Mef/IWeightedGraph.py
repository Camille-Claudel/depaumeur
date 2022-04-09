from abc import ABC, abstractmethod
from Mef.Matrix import Matrix

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

    def __getitem__(self, index):
        return self.get_vertex(index)

    def __setitem__(self, index, value):
        self.set_vertex(index, value)

    def __len__(self):
        return self.get_vertex_count()

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

    def _find_cycle(self, start_index: int):
        """ Returns a list of vertices cycling from start_index vertex back to itself"""
        assert self.is_eulerian(), "Cannot find a cycle on a non eulerian graph"
        assert self._directional, "Cannot find a cycle in a non directional graph"
        cycle = [start_index]
        current = start_index
        while not (start_index == current and len(cycle) > 1):
            links = self.get_vertex(current)
            current = links.keys()[0] # Taking the first edge we find in the neighbours
            links.pop(current)
            self.set_vertex(start_index, links)      
            cycle.append(current)
        return cycle

    def find_eulerian_cycle(self):
        """ Returns a list of vertices cycling over the whole graph """        
        def _find_non_empty_vertex(g, vertices):
            """ Returns a vertex that has neighbours from vertices, if none exist, returns none """
            for i in vertices: # vertices is a list of vertex index (List<int>)
                if g.get_vertex(i): # If the vertex has any neighbours
                    return i
            return None

        if (not self.is_eulerian()) or self._directional:
            return None
        graph = self.copy()
        cycle = graph._find_cycle(0)
        v_index = _find_non_empty_vertex(graph, cycle)
        while v_index != None:
            new_cycle = graph._find_cycle(v_index)
            i = cycle.index(new_cycle[0])
            cycle[i:i+1] = new_cycle # Inserting new cycle at the chosen point
            v_index = _find_non_empty_vertex(graph, cycle)
        return cycle