using listaPraticaGrafo.Arquitetura.Interfaces;

namespace listaPraticaGrafo.Arquitetura.Estrutura
{
    public class Aresta : ArestaBase, IAresta
    {
        public Vertice getVertice1 { get { return this.vertice1; } }
        public Vertice getVertice2 { get { return this.vertice2; } }

        public Aresta(Vertice v1, Vertice v2) : base(v1, v2) { }
        public Aresta(Vertice v1, Vertice v2, int peso) : base(v1, v2, peso) { }

        public void SetVertice1(Vertice vertice) => this.vertice1 = vertice;

        public void SetVertice2(Vertice vertice) => this.vertice2 = vertice;

        public object getValorVertice1
        {
            get
            {
                if (this.vertice1 != null) return this.vertice1.GetDadoValor();
                else return null;
            }
        }

        public object getValorVertice2
        {
            get
            {
                if (this.vertice2 != null) return this.vertice2.GetDadoValor();
                else return null;
            }
        }

        public IDado getDadoVertice1
        {
            get

            {
                if (this.vertice1 != null) return this.vertice1.GetDado();
                else return null;
            }
            set
            {
                this.vertice1.SetDado(value);
            }
        }

        public IDado getDadoVertice2
        {
            get
            {
                if (this.vertice2 != null) return this.vertice2.GetDado();
                else return null;
            }
            set
            {
                this.vertice2.SetDado(value);
            }
        }

        public Vertice GetVerticeDiferente(Vertice vertice)
        {
            if (this.vertice1.Equals(vertice))
            {
                return this.vertice2;
            }
            else if (this.vertice2.Equals(vertice))
            {
                return this.vertice1;
            }
            return null;
        }
    }
}
