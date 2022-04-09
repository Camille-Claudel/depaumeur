from Mef.WeightedListGraph import *
from Mef.WeightedGraph import *
from Parsing.JsonGraphParser import *
import json

graph = WeightedListGraph(False)
graph.add_vertex({})
graph.add_vertex({})
graph.add_vertex({0:4, 1:6})
graph.add_vertex({0:3, 2:69})
graph.add_vertex({1:42, 3:7})

print(graph)

json1 = json.dumps(weighted_graph_to_json_format(graph, False))

print(json1)

g2 = weighted_graph_from_saved_data(json.loads(json1), WeightedGraph)

print(g2)

json2 = json.dumps(weighted_graph_to_json_format(g2))

print(json2)

g3 = weighted_graph_from_saved_data(json.loads(json2), WeightedListGraph)

print(g3)