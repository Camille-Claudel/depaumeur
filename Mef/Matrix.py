class Matrix:
    __slots__=(
        "_items"
    )
    def __init__(self, x, y):
        """ Creates a matrix of 0s of size x * y """
        self._items = [[0] * x for _ in range(y)]
        
    def get(self, x, y):
        """ Returns the value at the given coordinate """
        return self._items[y][x]

    def set(self, x, y, value):
        """ Sets the item at the given coordinates """
        self._items[y][x] = value

    def __str__(self):
        return "".join([str(row) + '\n' for row in reversed(self._items)]) # Reversed so the top elements show first

if __name__ == "__main__":
    m = Matrix(4, 2) # """"""Unit testing"""""" (kinda)
    m.set(2, 1, 42)
    m.set(3, 1, 69)
    print(m)
    assert str(m) == "[0, 0, 42, 69]\n[0, 0, 0, 0]\n", "Matrix wasn't printed to string correctly"
    assert m.get(2, 1) == 42, "Matrix get failed to fetch element (42) at coords (2, 1)"
    print("All tests passed")