﻿namespace listaPraticaGrafo.utils
{
    /// <summary>
    /// Classe usada para testes de leitura de arquivo
    /// O sumário de cada variável representa o valor do arquivo
    /// </summary>
    public class FileArray
    {
        /// <summary>
        /// <para>3</para> 
        /// <para>1;2;4</para> 
        /// <para>1;3;7</para>
        /// <para>2;3;10</para>
        /// </summary>
        public static string[] GRAFO1_NAO_DIRIGIDO = {
            "3",
            "1;2;4",
            "1;3;7",
            "2;3;10"
        };

        public static string[] GRAFO2_NAO_DIRIGIDO = {
            "7",
            "1;2;9",
            "1;4;6",
            "2;3;8",
            "2;4;15",
            "2;5;5",
            "3;5;6",
            "4;5;6",
            "4;6;11",
            "4;7;8",
            "5;7;7",
            "6;7;8"
        };

        public static string[] GRAFO3_NAO_DIRIGIDO = {
            "5",
            "1;4;6",
            "2;4;15",
            "2;5;1",
            "2;3;3",
            "3;5;2"
    };

        /// <summary>
        /// <para>3</para>
        /// <para>1;2;4;1</para>
        /// <para>1;2;11;-1</para>
        /// <para>1;3;7;1</para>
        /// <para>2;3;10;-1</para>
        /// </summary>
        public static string[] GRAFO01_DIRIGIDO = { "3",
            "1;2;4;1",
            "1;2;11;-1",
            "1;3;7;1",
            "2;3;10;-1" };
    }
}
