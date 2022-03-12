# Introduction
Ce projet a pour but la création d’une application de navigation à l’intérieur du lycée. À la manière d’un GPS, elle pourrait indiquer l’itinéraire à suivre pour se rendre dans une salle donnée.

## Objectifs
Les trois objectifs principaux sont:
* la détermination d’un itinéraire, pour aller le plus rapidement possible d’un endroit du lycée à un autre
* la localisation de l’utilisateur, pour pouvoir déterminer automatiquement le point de départ du trajet de l’utilisateur
* la création d’une interface graphique, pour avoir une application similaire à un GPS utilisable sur un téléphone Android  

Dans le futur, peut-être, si jamais nous terminons tous les objectifs précédents, existe-t-il une probabilité qui pourrait ne pas être nulle pour que nous envisagions éventuellement de considérer les objectifs suivants:
* prendre en compte la fréquentation de chaque couloir en fonction des horaires pour éviter les embouteillages, ce qui serait utile pour des raisons sanitaires et de confort, et parce que ces embouteillages ralentissent la circulation.
* Connecter l’application à pronote pour donner l’itinéraire vers la salle où l’élève a cours, et donc pouvoir déterminer automatiquement le point d’arrivée du trajet de l’utilisateur.

# principe de fonctionnement
## Calcul de l’itinéraire
Cette partie détaille les mathématiques du calcul de l’itinéraire.  
Le lycée est représenté par un graphe pondéré. Chaque nœud représente soit une intersection, soit une porte de salle. Si plusieurs portes de salle sont au même endroit, elles pourront être représentées par un seul nœud. Les arêtes représentent une portion de couloir ou un escalier, et leur poids représente la durée nécessaire pour les parcourir.  
On utilisera l’algorithme de Dijkstra pour déterminer l’itinéraire optimal.
<!---insert schéma du graphe sur le lycée]-->

## Localisation
Chaque salle du lycée comporte un répéteur wifi au-dessus de la porte. Un utilisateur peut mesurer l'intensité perçue du signal émis par chaque répéteur. Cette intensité devrait donc dépendre principalement de la position de l'utilisateur. On cherche à utiliser ces mesures d'intensité pour déduire la position de l'utilisateur.  
Dans une première phase d’étalonnage, on mesure l’intensité perçue pour chaque répéteur à différents points du lycée. Pour déterminer la position de l’utilisateur, l’application effectue cette même mesure et la compare aux données de l’étalonnage.

## Interface graphique
Sous-traitement au club informatique du lycée Fustel de coulanges, présidé par Alexandru Dragulinescu.

# Difficultés rencontrées et solutions apportées
## Algorithme de comparaison
Pour localiser un utilisateur, le programme doit comparer la mesure de l’utilisateur à des données d’étalonnage. Ce document présente l’algorithme utilisé.  
[algorithme de comparaison.pdf](https://github.com/Camille-Claudel/depaumeur/files/8238162/mathbananas.3.pdf)

## Étalonnage
Pour l’étalonnage, il est nécessaire d’avoir des mesures précises de l’intensité du signal. Pour cela, on effectue la mesure de nombreuses fois dont on conserve la moyenne. Cependant, les téléphones ne permettent pas d'effectuer ces mesures suffisamment rapidement, nous utilisons donc un dispositif alienoïde créé à base d’un raspberry pi sous un OS de “stickyfingers”, de pièces imprimées en 3D à l’arrache, d’un écran pas cher sur Amazon, de vis mal fixées parce qu’on avait pas de tournevis, et d’une batterie offerte par le comité de tennis des bouches du Rhône en 2017.

## Instabilité du signal
Malheureusement, le signal émis par les répéteurs du lycée est très peu constant. Nous n’avons donc pas encore réussi à localiser le dispositif dans le lycée avec une précision satisfaisante. La dernière partie du document détaillant l’algorithme de localisation (concernant l’interpolation) est donc complètement inutilisable, et la localisation au point d’étalonnage le plus proche ne permet pas de se localiser plus précisément qu’à 15m près.
