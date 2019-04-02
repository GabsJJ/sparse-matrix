using System;
using System.IO;

public class Celula : IComparable<Celula>
{
    Celula direita;
    Celula abaixo;
    int linha, coluna;
    double valor;

    public Celula(Celula d, Celula ab, int li, int col, double val)
    {
        if(d != null && ab != null)
        {
            this.Direita = d;
            this.Abaixo = ab;
        }        
        this.Linha = li;
        this.Coluna = col;
        this.Valor = val;
    }

    //Usado pra instanciar o noCabeca
    public Celula()
    {
        this.Direita = null;
        this.Abaixo = null;  
        this.Linha = -1;
        this.Coluna = -1;
    }

    //Instancia o nóCabeça de uma lista circular que pode representar ou uma linha ou uma coluna
    public Celula(bool vaiSerColuna)
    {
        this.Direita = null;
        this.Abaixo = null;
        if (!vaiSerColuna)
            this.Linha = -1;
        else
            this.Coluna = -1;
    }

    public Celula Direita { get => direita; set => direita = value; }
    public Celula Abaixo { get => abaixo; set => abaixo = value; }
    public int Linha { get => linha; set => linha = value; }
    public int Coluna { get => coluna; set => coluna = value; }
    public double Valor { get => valor; set => valor = value; }

    public int CompareTo(Celula other)
    {
        return this.Valor.CompareTo(other.Valor);
    }

    public override string ToString()
    {
        return "Linha: " + linha + " Coluna: " + coluna + " Valor: " + valor;
    }

    public static Celula LerRegistro(StreamReader arq)
    {
        Celula novo = null;
        if (!arq.EndOfStream)
        {
            string linha = arq.ReadLine();
            string[] chars = linha.Split(';');
            int linhaElemento = int.Parse(chars[0]);
            int colunaElemento = int.Parse(chars[1]);
            int valorElemento;
            if (chars.Length == 3)
            {
                valorElemento = int.Parse(chars[2]);
                novo = new Celula(null, null, linhaElemento, colunaElemento, valorElemento);
            }
        }
        return novo;
    }
}