namespace tabuleiro
{
    class posicao
    {
        public int Linha { get; set; }
        public int Coluna { get; set; }

        public posicao(int linha, int coluna) 
        { 
            Linha = linha;
            Coluna = coluna;
        }

        public override string ToString()
        {
            return "Posição: ("
                + Linha
                + ", "
                + Coluna
                + ") ";

        }
    }
}
