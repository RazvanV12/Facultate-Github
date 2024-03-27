# By Me:
# class NodArbore:
#     def __init__(self, informatie, parinte=None):
#         self.informatie = informatie
#         self.parinte = parinte
#
#     def drumRadacina(self):
#         result = []
#         currentNode = self
#         while currentNode.parinte is not None:
#             result.append(currentNode)
#             currentNode = currentNode.parinte
#         result.append(currentNode)
#         return result.reverse()
#
#     def inDrum(self, infoNod):
#         informatiiVizitate = [x.informatie for x in self.parinte.drumRadacina()]
#         if infoNod in informatiiVizitate:
#             return True
#         return False
#
#     def __str__(self):
#         return str(self.informatie)
#
#     def __repr__(self):
#         result = str(self.informatie) + " ("
#         informatiiVizitate = [x.informatie for x in self.parinte.drumRadacina()]
#         for informatie in informatiiVizitate:
#             result += str(informatie) + "->"
#         result += self.informatie + ")"
#         return result
#
# class Graf:
#     def __init__(self, matrice, nodStart, noduriScop):
#         self.matrice = matrice
#         self.nodStart = nodStart
#         self.noduriScop = noduriScop
#
#     def scop(self, informatieNod):
#         if informatieNod in self.noduriScop:
#             return True
#         return False
#
#     def succesori(self, nod):
#         result = []
#         for index, vecin in enumerate(self.matrice[nod.informatie]):
#             if vecin and index not in nod.drumRadacina():
#                 result.append(index)
#         return result


# By Prof:
class NodArbore:
    def __init__(self, informatie, parinte=None):
        self.informatie =informatie
        self.parinte=parinte

    def drumRadacina(self):
        nod=self
        lDrum=[]
        while nod:
            lDrum.append(nod)
            nod=nod.parinte
        return lDrum[::-1]

    def inDrum(self,infonod):
        nod=self
        while nod:
            if nod.informatie==infonod:
                return True
            nod=nod.parinte
        return False

    def __str__(self):
        return str(self.informatie)

    #c (a->b->c)
    def __repr__(self):
        return "{}, ({})".format(str(self.informatie), "->".join([str(x)   for x in self.drumRadacina()]))


class Graf:
    def __init__(self, matr, start, scopuri):
        self.matr=matr
        self.start=start
        self.scopuri=scopuri

    def scop(self, informatieNod):
        return informatieNod in self.scopuri

    def succesori(self, nod):
        lSuccesori=[]
        for infoSuccesor in range(len(self.matr)):
            if self.matr[nod.informatie][infoSuccesor]==1 and not nod.inDrum(infoSuccesor):
                lSuccesori.append(NodArbore(infoSuccesor,nod))
        return lSuccesori

def breadthFirst(gr, nsol=2):
    coada=[NodArbore(gr.start)]
    while coada:
        nodCurent=coada.pop(0)
        if gr.scop(nodCurent.informatie):
            print(repr(nodCurent))
            nsol-=1
            if nsol==0:
                return
        coada+=gr.succesori(nodCurent)

def breadthFirstVar2(gr, nsol=2):
    coada=[NodArbore(gr.start)]
    while coada:
        nodCurent=coada.pop(0)
        coada+=gr.succesori(nodCurent)
        for nod in gr.succesori(nodCurent):
            if gr.scop(nod.informatie):
                print(repr(nod))
                nsol-=1
                if nsol==0:
                    return

def depthfirstNerecursiv(graf, n):
    stiva = [NodArbore(graf.start)]
    solutii = []  # Lista pentru a colecta soluțiile găsite

    while len(stiva) > 0 and len(solutii) < n:
        nod_curent = stiva.pop()

        if graf.scop(nod_curent.informatie):
            solutii.append(nod_curent.drumRadacina())
            if len(solutii) == n:
                break  # Întrerupem căutarea dacă am găsit n soluții
            continue

        succesorii = graf.succesori(nod_curent)
        for succesor in succesorii:
            if not nod_curent.inDrum(succesor.informatie):
                stiva.append(succesor)

    return solutii

solutii = []

def dfs_recursiv(graf, nod_curent, nsol):
    if graf.scop(nod_curent.informatie):
        solutii.append(nod_curent.drumRadacina())
        if len(solutii) >= nsol:
            return True  # Dacă am găsit suficiente soluții, oprim căutarea

    for succesor in graf.succesori(nod_curent):
        if not nod_curent.inDrum(succesor.informatie):
            if dfs_recursiv(graf, succesor, nsol):
                return True  # Oprim căutarea dacă am găsit suficiente soluții
    return False

m = [
    [0, 1, 0, 1, 1, 0, 0, 0, 0, 0],
    [1, 0, 1, 0, 0, 1, 0, 0, 0, 0],
    [0, 1, 0, 0, 0, 1, 0, 1, 0, 0],
    [1, 0, 0, 0, 0, 0, 1, 0, 0, 0],
    [1, 0, 0, 0, 0, 0, 0, 1, 0, 0],
    [0, 1, 1, 0, 0, 0, 0, 0, 0, 0],
    [0, 0, 0, 1, 0, 0, 0, 0, 0, 0],
    [0, 0, 1, 0, 1, 0, 0, 0, 1, 1],
    [0, 0, 0, 0, 0, 0, 0, 1, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 1, 0, 0]
]


start = 0
scopuri = [5, 9]


gr=Graf(m,start,scopuri)
breadthFirst(gr, nsol=3)
solutii = depthfirstNerecursiv(gr, 3)

if solutii:
    for i, solutie in enumerate(solutii, start=1):
        print(f"Soluția {i}:", '->'.join(str(nod) for nod in solutie))
else:
    print("Nu există soluții.")

nod_start = NodArbore(gr.start)
dfs_recursiv(gr, nod_start, 2)
if solutii:
    print(f"Primele {3} soluții găsite sunt:")
    for i, solutie in enumerate(solutii, start=1):
        print(f"Soluția {i}: {'->'.join(str(nod) for nod in solutie)}")
else:
    print("Nu au fost găsite soluții.")

gr=Graf(m,start,scopuri)
breadthFirstVar2(gr, nsol=3)