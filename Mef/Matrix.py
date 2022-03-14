from Utils import *

class Matrix:
    __slots__=(
        "_items",
        "size_tuple"
    )
    def __init__(self, x: int, y: int, _create_array: bool = True):
        """ Creates a matrix of 0s of size x * y (_create_array optional argument is for implementation code) """
        self.size_tuple = (x, y)
        if _create_array:
            self._items = [[0] * x for _ in range(y)]
        else:
            self._items = None
        
    def get(self, x: int, y: int) -> int:
        """ Returns the value at the given coordinate """
        return self._items[y][x]

    def set(self, x: int, y: int, value: int):
        """ Sets the item at the given coordinates """
        self._items[y][x] = value

    def copy(self):
        """ Returns a copy of the matrix (deep copy to prevent reference issues) """
        x, y = self.size_tuple
        m = Matrix(x, y, False) # Not creating array for speed reasons
        m._items = [row[:] for row in self._items]
        return m

    def get_resized(self, x: int, y: int):
        """ Returns a new matrix, copying the old matrix values and ignoring overflow values, with the new size (x, y) """
        m = Matrix(x, y, False)
        m._items = [None] * y
        ox, oy = self.size_tuple # old size
        for i in range(y):
            if i < oy:
                m._items[i] = self._items[i][:min(x, ox)] + [0] * clamp(x - ox, 0, x)
            else:
                m._items[i] = [0] * x
        return m

    def __str__(self) -> str:
        return "".join([str(row) + '\n' for row in self._items])

if __name__ == "__main__":
    m = Matrix(4, 2) # """"""Unit testing"""""" (kinda)
    m.set(2, 1, 42)
    m.set(3, 1, 69)
    print(m)
    assert str(m) == "[0, 0, 0, 0]\n[0, 0, 42, 69]\n", "Matrix wasn't printed to string correctly"
    assert m.get(2, 1) == 42, "Matrix get failed to fetch element (42) at coords (2, 1)"
    m2 = m.copy()
    m.set(1, 0, 666)
    m2.set(1, 0, 420)
    assert str(m) == "[0, 666, 0, 0]\n[0, 0, 42, 69]\n", "Matrix copy failed to deep copy"
    assert str(m2) == "[0, 420, 0, 0]\n[0, 0, 42, 69]\n", "Matrix copy failed to deep copy"
    m3 = m2.get_resized(6, 9)
    print("m2\n" + str(m2))
    print("m3\n" + str(m3)) # I know it works, no need for tests :)
    print("All tests passed")


# (A)
# BC
# D EFG
#    H