using System.Collections.Generic;
using listaPraticaGrafo.interfaces;
using listaPraticaGrafo.estrutura;
using listaPraticaGrafo.Enum;
using System;

namespace listaPraticaGrafo
{
    public class Grafo : IGrafo
    {
        protected List<Vertice> vertices;
        protected int componente = 0;
        public Grafo()
        {
            vertices = new List<Vertice>();
        }
        public void LimpaVisitaVertices()
        {
            foreach (Vertice vAux in vertices)
            {
                vAux.SetVisitado(false);
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
                string[] lineSplit;

                for (int i = 1; i < arquivo.Length; i++)
                {
                    try
                    {
                        lineSplit = arquivo[i].Split(';');
                        conteudo = new Informacao(int.Parse(lineSplit[1]));

                        if (this.Contem(conteudo))
                        {
                            vertice = this.GetVertice(conteudo);
                        }
                        else
                        {
                            vertice = new Vertice(new Informacao(int.Parse(lineSplit[0])));
                        }

                        conteudo = new Informacao(int.Parse(lineSplit[1]));

                        if (this.Contem(conteudo))
                        {
                            vertice.AddAresta(new Aresta(vertice, this.GetVertice(conteudo),
                                int.Parse(lineSplit[2])));
                        }
                        else
                        {
                            novoVertice = new Vertice(conteudo);
                            vertice.AddAresta(new Aresta(vertice, novoVertice,
                                int.Parse(lineSplit[2])));
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Arquivo possui conteúdo inválido para leitura " + e.Message);
                    }
                    this.vertices.Add(vertice);
                }
            }
        }

        /// <summary>
        /// Procura e retorna pelo primeiro vértice cujo dado é igual aquele passado no parâmetro.
        /// Retorna null se nenhum for encontrado
        /// </summary>
        /// <param name="dado"></param>
        /// <returns></returns>
        public Vertice GetVertice(IDado dado)
        {
            foreach (Vertice vertice in this.vertices)
            {
                if (vertice.GetDado().Equals(dado))
                {
                    return vertice;
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
        public bool Contem(Vertice vertice)
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
            this.vertices.Add(v1);
        }

        /// <summary>
        /// Retira um vertice do grafo e define o objeto como nulo
        /// </summary>
        /// <param name="v1"></param>
        public void RemoverVertice(Vertice v1)
        {
            if (this.vertices.Contains(v1))
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
            Vertice vertice1;
            Vertice vertice2;
            List<Vertice> verticesLimpar = new List<Vertice>();
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

        public IGrafo GetAGMKruskal(Vertice v1)
        {
            throw new System.NotImplementedException();
        }

        public IGrafo GetAGMPrim(Vertice v1)
        {
            throw new System.NotImplementedException();
        }

        public IGrafo GetComplementar()
        {
            throw new System.NotImplementedException();
        }

        public int GetCutVertices()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Informa o grau de um vértice (Número de arestas que ele possui)
        /// </summary>
        /// <param name="v1"></param>
        /// <returns></returns>
        public int GetGrau(Vertice v1)
        {
            return v1.GetGrau();
        }

        /// <summary>
        /// Verifica se um vértice possui ligação com outro
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public bool IsAdjacente(Vertice v1, Vertice v2)
        {
            foreach (Aresta aresta in v1.GetArestas())
            {
                if (aresta.GetVertices()[0] == v2 || aresta.GetVertices()[1] == v2)
                {
                    return true;
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
            try
            {
                LimpaVisitaVertices();
                foreach (Vertice vertice in this.vertices)
                {
                    if (vertice.GetVisitado() == false)
                    {
                        Visitar(vertice, vertice.GetArestas());
                        componente++;
                    }
                    vertice.SetVisitado(true);
                }
                if (componente >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception)
            {
                throw new System.Exception("Erro IsConexo");
            }
        }
        private void Visitar(Vertice v, List<Aresta> a)
        {
            try
            {
                List<Vertice> lstVAux;
                foreach (Aresta aAux in a)
                {
                    lstVAux = aAux.GetVertices();
                    foreach (Vertice vAux in lstVAux)
                    {
                        if ((v.Equals(vAux) && vAux.GetVisitado()) == false) // não pode ser o vertice de origem e não pode estar visitado
                        {
                            Visitar(vAux, vAux.GetArestas());//vai para proximo vertice
                        }
                    }
                }
            }
            catch (System.Exception)
            {
                throw new System.Exception("Erro Visitar");
            }
        }
        /// <summary>
        /// Verifica se todos os vertices tem grau par e é conexo, ou seja, euleriano
        /// </summary>
        /// <returns></returns>
        public bool IsEuleriano()
        {
            try
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
            catch (System.Exception)
            {
                throw new System.Exception("Erro IsEuleriano");
            }
        }

        /// <summary>
        /// Verifica se um vértice não possui arestas
        /// </summary>
        /// <param name="v1"></param>
        /// <returns></returns>
        public bool IsIsolado(Vertice v1)
        {
            return v1.GetGrau() == 0;
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
            return v1.GetGrau() == 1;
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

        public bool IsUnicursal()
        {
            foreach (Vertice vertice in this.vertices)
            {
                return this.IsPercursoCompleto(vertice);
            }
            return false;
        }

        private bool IsPercursoCompleto(Vertice vertice)
        {
            Vertice proximo;
            foreach (Aresta aresta in vertice.GetArestas())
            {
                proximo = aresta.GetVerticeDiferente(vertice);
                vertice.FinalizaPercurso();

                if (proximo != null && proximo.GetCor() != Cor.PRETO)
                {
                    return this.IsPercursoCompleto(vertice);
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
    }
}
