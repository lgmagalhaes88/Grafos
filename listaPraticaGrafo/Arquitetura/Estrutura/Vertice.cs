using System.Collections.Generic;
using listaPraticaGrafo.Arquitetura.Interfaces;

namespace listaPraticaGrafo.Arquitetura.Estrutura
{
    public class Vertice : VerticeBase
    {
        public Vertice(IDado dados) : base(dados) { }

        public Vertice(IDado dado, List<Aresta> arestas) : base(dado, arestas)
        {
            this.arestas = new List<ArestaBase>();
            this.arestas.AddRange(arestas);
        }

        /// <summary>
        /// N�mero de arestas que o v�rtice possui
        /// </summary>
        /// <returns></returns>
        public int GetGrau()
        {
            return base.arestas.Count;
        }

        public void RemoverAresta(Aresta aresta)
        {
            this.arestas.RemoveAll(aresta_local => aresta_local.Equals(aresta));
        }
        
        /// <summary>
        /// Para uma lista de v�rtices, verifica se o v�rtice passado no par�metro existe nessa lista,
        /// comparando pelo valor do dado do v�rtice
        /// </summary>
        /// <param name="vertices"></param>
        /// <param name="vertice"></param>
        /// <returns></returns>
        public static bool Contem(List<Vertice> vertices, Vertice vertice)
        {
            foreach (Vertice vertice_index in vertices)
            {
                if (vertice.GetDadoValor().Equals(vertice_index.GetDadoValor()))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Busca um v�rtice da lista caso ele exista nela
        /// </summary>
        /// <param name="vertice"></param>
        /// <param name="vertices"></param>
        /// <returns></returns>
        public static Vertice Get(Vertice vertice, List<Vertice> vertices)
        {
            foreach (Vertice vertice_index in vertices)
            {
                if (vertice.GetDadoValor() == vertice_index.GetDadoValor())
                {
                    return vertice_index;
                }
            }
            return null;
        }
    }
}
