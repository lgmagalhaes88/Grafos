using System.Collections.Generic;
using listaPraticaGrafo.Arquitetura.Interfaces;
using listaPraticaGrafo.Arquitetura.Estrutura;
using listaPraticaGrafo.Arquitetura.Enum;
using System;
using System.Text;

namespace listaPraticaGrafo
{
    public class Grafo : IGrafo
    {
        protected List<Vertice> vertices;
        protected int num_arestas;
        /// <summary>
        /// Número de componentes do grafo
        /// </summary>
        protected int componentes;
        /// <summary>
        /// Usado na contagem do tempo de descoberta dos vértices
        /// </summary>
        protected int tempo;
        public int Numero_vertices { get { return this.vertices.Count; } }
        public int Numero_arestas { get { return this.num_arestas; } }

        public Grafo()
        {
            this.Init();
            this.vertices = new List<Vertice>();
        }

        public Grafo(int numero_vertices)
        {
            for (int i = 0; i < numero_vertices; i++)
            {
                this.vertices = new List<Vertice>();
            }
        }

        public Grafo(List<Vertice> lstVertices)
        {
            this.Init();
            this.vertices = lstVertices;
            this.CalcularArestas();
        }

        /// <summary>
        /// Define a variável "visitado" de cada vértice como false
        /// </summary>
        public void LimpaVisitaVertices()
        {
            this.Init();
            foreach (Vertice vAux in vertices)
            {
                vAux.SetVisitado(false);
                this.CalcularArestas(vAux);
            }
        }

        private void Init()
        {
            this.num_arestas = 0;
            this.componentes = 0;
        }

        /// <summary>
        /// Aumenta o números de arestas que o grafo possui contando quantas arestas 
        /// o vértice passado no parâmetro possui
        /// </summary>
        /// <param name="vertice"></param>
        private void CalcularArestas(IVertice vertice)
        {
            this.num_arestas += vertice.GetArestas().Count;
        }

        /// <summary>
        /// Aumenta o números de arestas que o grafo possui contando quantas arestas 
        /// a lista de vértices passado no parâmetro possui
        /// </summary>
        /// <param name="vertice"></param>
        private void CalcularArestas(List<Vertice> vertices)
        {
            foreach (Vertice vertice in vertices)
            {
                this.num_arestas += vertice.GetArestas().Count;
            }
        }

        /// <summary>
        /// Aumenta o números de arestas que o grafo possui contando quantas arestas 
        /// a lista de vértices o grafo possui
        /// </summary>
        /// <param name="vertice"></param>
        private void CalcularArestas()
        {
            this.num_arestas = 0;
            foreach (Vertice vertice in this.vertices)
            {
                this.num_arestas += vertice.GetArestas().Count;
            }
        }

        /// <summary>
        /// Diminui o números de arestas que o grafo possui contando quantas arestas 
        /// a lista de vértices passado no parâmetro possui
        /// </summary>
        /// <param name="vertice"></param>
        private void DiminuirArestas(List<Vertice> vertices)
        {
            foreach (Vertice vertice in vertices)
            {
                if (this.num_arestas > 0)
                {
                    this.num_arestas -= vertice.GetArestas().Count;
                }
            }
        }

        /// <summary>
        /// Diminui o números de arestas que o grafo possui contando quantas arestas 
        /// o vértice passado no parâmetro possui
        /// </summary>
        /// <param name="vertice"></param>
        private void DiminuirArestas(Vertice vertice)
        {
            if (this.num_arestas > 0)
            {
                this.num_arestas -= vertice.GetArestas().Count;
            }
        }

        /// <summary>
        /// Recebe um vetor string no qual deve estar com os itens separados por ';',
        /// a primeira linha deve dizer a quantidade de vértices que o grafo terá,
        /// e as linhas seguintes devem estar no seguinte formado:
        /// 
        /// v1;v2;p;d
        /// 
        /// v1 e v2 = Vértices que compoem a aresta
        /// p = peso da aresta do grafo caso exista
        /// d = direção da aresta caso exista
        /// 
        /// </summary>
        /// <param name="arquivo"></param>
        public void GerarGrafo(string[] arquivo)
        {
            if (arquivo != null)
            {
                Informacao conteudo;
                Vertice vertice, novoVertice;
                Aresta aresta;
                string[] lineSplit;

                for (int i = 1; i < arquivo.Length; i++)
                {
                    try
                    {
                        lineSplit = arquivo[i].Split(';');
                        conteudo = new Informacao(int.Parse(lineSplit[0]));

                        if (this.Contem(conteudo)) vertice = this.GetVertice(conteudo);
                        else
                        {
                            vertice = new Vertice(new Informacao(int.Parse(lineSplit[0])));
                            this.vertices.Add(vertice);
                        }

                        conteudo = new Informacao(int.Parse(lineSplit[1]));

                        if (this.Contem(conteudo))
                        {
                            novoVertice = this.GetVertice(conteudo);
                            aresta = new Aresta(vertice, novoVertice, int.Parse(lineSplit[2]));
                        }
                        else
                        {
                            novoVertice = new Vertice(conteudo);
                            aresta = new Aresta(vertice, novoVertice, int.Parse(lineSplit[2]));
                            this.vertices.Add(novoVertice);
                        }

                        vertice.AddAresta(aresta);
                        novoVertice.AddAresta(aresta);
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Arquivo possui conteúdo inválido para leitura " + e.Message);
                    }
                }
            }
            this.CalcularArestas();
        }

        /// <summary>
        /// Procura e retorna pelo primeiro vértice cujo dado é igual aquele passado no parâmetro.
        /// Retorna null se nenhum for encontrado
        /// </summary>
        /// <param name="dado"></param>
        /// <returns></returns>
        public Vertice GetVertice(IDado dado)
        {
            if (dado != null)
            {
                foreach (Vertice vertice in this.vertices)
                {
                    if (vertice.GetDado().Equals(dado))
                    {
                        return vertice;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Verifica se existe o vértice no grafo
        /// </summary>
        /// <param name="vertice"></param>
        /// <returns></returns>
        public bool Contem(IDado vertice)
        {
            if (vertice != null)
            {
                foreach (Vertice verticeLocal in this.vertices)
                {
                    if (verticeLocal.GetDado().Equals(vertice))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Verifica se existe o vértice no grafo
        /// </summary>
        /// <param name="vertice"></param>
        /// <returns></returns>
        public bool Contem(IVertice vertice)
        {
            if (vertice != null)
            {
                foreach (Vertice verticeLocal in this.vertices)
                {
                    if (vertice.GetDado().Equals(verticeLocal.GetDado()))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Insere um novo vértice ao grafo
        /// </summary>
        /// <param name="v1"></param>
        public void AddVertice(Vertice v1)
        {
            if (v1 != null)
            {
                this.vertices.Add(v1);
                this.CalcularArestas(v1);
            }
        }

        /// <summary>
        /// Retira um vertice do grafo e define o objeto como nulo
        /// </summary>
        /// <param name="v1"></param>
        public void RemoverVertice(Vertice v1)
        {
            if (v1 != null && this.vertices.Contains(v1))
            {
                this.vertices.Remove(v1);
                this.LimparArestas(v1);
                v1 = null;
            }
        }


        /// <summary>
        /// Remove os valores nulos da lista de arestas dos vértices que fazem ligação com aquele passado no parâmetro.
        /// </summary>
        /// <param name="v1"></param>
        private void LimparArestas(Vertice v1)
        {
            if (v1 != null)
            {
                Vertice vertice1;
                Vertice vertice2;
                List<Vertice> verticesLimpar = new List<Vertice>();

                this.DiminuirArestas(v1);
                foreach (Aresta aresta in v1.GetArestas())
                {
                    vertice1 = aresta.GetVertices()[0];
                    vertice2 = aresta.GetVertices()[1];

                    if (vertice1 != v1)
                    {
                        verticesLimpar.Add(vertice1);
                    }
                    else if (vertice2 != v1)
                    {
                        verticesLimpar.Add(vertice2);
                    }
                }
                v1.GetArestas().Clear();
                foreach (Vertice vertice in verticesLimpar)
                {
                    vertice.GetArestas().RemoveAll(arr => arr == null);
                }
            }
        }

        public IGrafo GetAGMKruskal(Vertice v1)
        {
            throw new System.NotImplementedException();
        }

        public IGrafo GetAGMPrim(Vertice v1)
        {
            throw new System.NotImplementedException();
        }

        public Grafo GetComplementar()
        {
            List<Vertice> lstVertice = new List<Vertice>(); //lista que terá os vertice que participarão do grafo complementar
            List<Vertice> lstVerticeComplementar = new List<Vertice>(); //lista de vertices que terão as arestas complementares
            foreach (Vertice vertice1 in vertices) // para cada vertice, traz a lista de arestas para comparar com a lista de arestas do proximo vertice do grafo,
                                                   //sem tem alguma aresta em comum(igual), que representa a ligação
            {
                List<Aresta> lstAuxAresta = vertice1.GetArestas();
                foreach (Vertice vertice2 in vertices)
                {
                    if (vertice1.Equals(vertice2) == false) // evita a comparação do vertice com ele mesmo e se já foi adicionado na lista
                                                            // se estiver na lista é porque não tem ligação com algum outro vertice
                    {
                        List<Aresta> lstAuxAresta2 = vertice2.GetArestas();
                        //verifica se tenho aresta em comum , se não tiver adiciona lstVertice 
                        //inicio
                        foreach (Aresta aItem in lstAuxAresta)
                        {
                            foreach (Aresta aItem2 in lstAuxAresta2)
                            {
                                if (aItem2.Equals(aItem) == false)
                                {
                                    //quando não possuir aresta em comum , não tem ligação
                                    //adiciona na lista de verificação de vertice - inicio
                                    lstVertice.Add(vertice1);
                                    lstVertice.Add(vertice2);
                                    //fim
                                    //clona os vertice   - inicio
                                    Vertice aux = (Vertice)(vertice1.Clone());
                                    Vertice aux2 = (Vertice)(vertice1.Clone());
                                    //fim
                                    // desfaz todas ligações
                                    aux.LimpaArestas();
                                    aux2.LimpaArestas();
                                    // fim
                                    //cria aresta de ligação dos 2 vertices e adiciona ela aos vertices
                                    Aresta nAresta = new Aresta(aux, aux2);
                                    aux.AddAresta(nAresta);
                                    aux2.AddAresta(nAresta);
                                    //fim , adiciona os vertices a lista de vertice do grafo complementar 
                                    lstVerticeComplementar.Add(aux);
                                    lstVerticeComplementar.Add(aux2);
                                }
                            }
                        }
                        //fim
                    }
                }
            }
            Grafo cGrafo = new Grafo(lstVerticeComplementar);
            return cGrafo;
        }

        /// <summary>
        /// Depth First Search
        /// </summary>
        /// <returns></returns>
        public int DFS()
        {
            int componentes = 0;
            this.ResetarCorDosVertices();

            foreach (Vertice vertice in this.vertices)
            {
                if (vertice.Cor == Cor.BRANCO)
                {
                    this.Visitar(vertice);
                    componentes++;
                }
            }
            return componentes;
        }

        // Função recursiva para achar os cut-vértices
        // u --> O próximo vertice para ser visitado
        // visited[] --> mantem armazeado os vértices que foram visitados
        // disc[] --> salva o tempo de descoberta de cada vértice
        // parent[] --> Salva o vértice pai para ser usado no DFS
        // ap[] --> salva os cut-vértices
        public void APUtil(int u, bool[] visited, int[] disc, int[] low, int[] parent, bool[] ap)
        {
            // Conta os filhos na árvore DFS
            int children = 0;
            int index = 0;
            // Marca o vértice atual como visitado
            this.vertices[u].SetVisitado(true);

            // Vai através de todos os vértices adjacentes a este
            List<Vertice> adj = this.vertices[u].GetAdjacentes();
            foreach (Vertice v in adj)
            {
                // Se v não foi visitado ainda, então marca ele como um filho de u
                // na DFS e recua para ele
                if (!v.FoiVisitado())
                {
                    children++;
                    index = this.vertices.IndexOf(v);

                    parent[index] = u;
                    APUtil(index, visited, disc, low, parent, ap);

                    // Verifica se a subarvore base com v tem conexão com a 
                    // primeira das ancestrais de u
                    low[u] = Math.Min(low[u], low[index]);

                    // u is an articulation point in following cases
                    // (1) u is root of DFS tree and has two or more chilren.
                    if (parent[u] == -1 && children > 1) ap[u] = true;

                    // (2) If u is not root and low value of one of its child
                    // is more than discovery value of u.
                    if (parent[u] != -1 && low[index] >= disc[u]) ap[u] = true;
                }

                // Update low value of u for parent function calls.
                else if (v.isAdjacenteDe(this.vertices[u])) low[u] = Math.Min(low[u], disc[index]);
            }
        }

        public int GetCutVertices()
        {
            int size = this.vertices.Count;
            int number = 0;
            // Marca os vértices como não visitados
            bool[] visited = new bool[size];
            int[] disc = new int[size];
            int[] low = new int[size];
            int[] parent = new int[size];
            bool[] ap = new bool[size]; // Armazenar os pontos de articulação

            //Inicializa pais(adjacentes), visitados, e os vetores de 
            //ponto de articulação
            for (int i = 0; i < this.vertices.Count; i++)
            {
                parent[i] = -1;
                visited[i] = false;
                ap[i] = false;
            }

            // Chama a função recursiva de ajuda para achar a articulação
            // Ponto na "árvore" DFS com base no vertex 'i'
            for (int i = 0; i < size; i++) if (visited[i] == false) APUtil(i, visited, disc, low, parent, ap);
           
            // Agora a ap[] contém pontos de articulação, conta os valores
            for (int i = 0; i < size; i++) if (ap[i] == true) number++;
            return number;
        }

        /// <summary>
        /// Informa o grau de um vértice (Número de arestas que ele possui)
        /// Retorna -1 se o parâmetro for nulo.
        /// </summary>
        /// <param name="v1"></param>
        /// <returns></returns>
        public int GetGrau(Vertice v1)
        {
            if (v1 != null)
            {
                return v1.GetGrau();
            }
            return -1;
        }

        /// <summary>
        /// Verifica se um vértice possui ligação com outro
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public bool IsAdjacente(Vertice v1, Vertice v2)
        {
            if (v1 != null && v2 != null)
            {
                foreach (Aresta aresta in v1.GetArestas())
                {
                    if (aresta.GetVertices()[0] == v2 || aresta.GetVertices()[1] == v2)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Verifica se todos os vértices estão conectados com todos os outros
        /// </summary>
        /// <returns></returns>
        public bool IsCompleto()
        {
            int valGrauCompleto = this.vertices.Count - 1;

            foreach (Vertice vertice in this.vertices)
            {
                if (vertice.GetGrau() != valGrauCompleto)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Verifica se existe um caminho entre os vertices
        /// </summary>
        /// <returns></returns>
        public bool IsConexo()
        {
            LimpaVisitaVertices();
            foreach (Vertice vertice in this.vertices)
            {
                if (vertice.FoiVisitado() == false)
                {
                    Visitar(vertice, vertice.GetArestas());
                    componentes++;
                }
                vertice.SetVisitado(true);
            }
            if (componentes >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Visitar(IVertice vertice)
        {
            vertice.AtualizarCor();
            foreach (Vertice vertice2 in vertice.GetAdjacentes())
            {
                if (vertice2.Cor == Cor.BRANCO)
                {
                    this.Visitar(vertice2);
                }
            }
            vertice.AtualizarCor();
        }

        private void Visitar(IVertice v, List<Aresta> a)
        {
            if (v != null && a != null)
            {
                List<Vertice> lstVAux;
                foreach (Aresta aAux in a)
                {
                    lstVAux = aAux.GetVertices();
                    foreach (Vertice vAux in lstVAux)
                    {
                        if ((v.Equals(vAux) && vAux.FoiVisitado()) == false) // não pode ser o vertice de origem e não pode estar visitado
                        {
                            Visitar(vAux, vAux.GetArestas());//vai para proximo vertice
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Verifica se todos os vertices tem grau par e é conexo, ou seja, euleriano
        /// </summary>
        /// <returns></returns>
        public bool IsEuleriano()
        {
            if (this.IsConexo())
            {
                foreach (Vertice vertice in this.vertices)
                {
                    if ((vertice.GetGrau() % 2) != 0) // todos vertices devem possuir grau par
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Verifica se um vértice não possui arestas
        /// </summary>
        /// <param name="v1"></param>
        /// <returns></returns>
        public bool IsIsolado(Vertice v1)
        {
            if (v1 != null)
            {
                return v1.GetGrau() == 0;
            }
            return false;
        }

        /// <summary>
        /// Verifica se o grafo não possui arestas
        /// </summary>
        /// <returns></returns>
        public bool IsNulo()
        {
            foreach (Vertice vertice in this.vertices)
            {
                if (vertice.GetGrau() > 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Verifica se um vértice possui grau 1
        /// </summary>
        /// <param name="v1"></param>
        /// <returns></returns>
        public bool IsPendente(Vertice v1)
        {
            if (v1 != null)
            {
                return v1.GetGrau() == 1;
            }
            return false;
        }

        /// <summary>
        /// Verifica se os vértices possuem o mesmo grau
        /// </summary>
        /// <returns></returns>
        public bool IsRegular()
        {
            int grau = this.vertices[0].GetGrau();
            foreach (Vertice vertice in this.vertices)
            {
                if (grau != vertice.GetGrau())
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Verifica se existem dois vértices de grau ímpar
        /// </summary>
        /// <returns></returns>
        public bool IsUnicursal()
        {
            int countImpares = 0;
            foreach (Vertice vertice in this.vertices)
            {
                if (vertice.GetGrau() % 2 != 0)
                {
                    countImpares++;
                    if (countImpares > 1) return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Define a cor de todos os vértices do grafo como BRANCO.
        /// </summary>
        private void ResetarCorDosVertices()
        {
            foreach (Vertice vertice in this.vertices)
            {
                vertice.ResetarCor();
            }
        }

        /// <summary>
        /// Converte todas as informações do grafo em um string no seguinte formado
        /// <para>
        /// Vertice VERTICE
        /// <para> VERTICE_1 - VERTICE_2 ; Peso PESO_ARESTA</para>
        /// </para>
        /// (Essa estrutura é repetida para cada vértice do grafo.)
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder retorno = new StringBuilder();
            foreach (Vertice vertice in this.vertices)
            {
                retorno.AppendLine("Vértice " + vertice.GetDadoValor());
                retorno.AppendLine(vertice.ToStringComArestas());
                retorno.AppendLine();
            }
            return retorno.ToString();
        }
    }
}