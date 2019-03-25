using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ListaCircular
{
    private Celula noCabeca, ultima, primeira;
    private int qtosNos;

    public ListaCircular()
    {
        NoCabeca = null;
        Ultima = null;
        QtosNos = 0;
    }

    public ListaCircular(bool vaiSerColuna)
    {
        NoCabeca = new Celula(vaiSerColuna);
        NoCabeca.Abaixo = NoCabeca.Direita = NoCabeca;
        Ultima = null;
        QtosNos = 0;
    }

    public ListaCircular(Celula noCabecaNovo)
    {
        NoCabeca = noCabecaNovo;
        Ultima = null;
        QtosNos = 0;
    }

    public Celula NoCabeca { get => noCabeca; set => noCabeca = value; }
    public Celula Ultima { get => ultima; set => ultima = value; }
    public int QtosNos { get => qtosNos; set => qtosNos = value; }
    public bool EstaVazia { get => NoCabeca.Direita == null || NoCabeca.Abaixo == null; }

    public void PercorrerLista()
    {
        Celula aux = NoCabeca.Direita;
        while (aux.Direita != NoCabeca.Direita)
        {
            //Console.WriteLine(aux.ToString());
            aux = aux.Direita;
        }
    }

    public void InserirCelulaADireita(Celula novo)
    {
        if (EstaVazia)
            NoCabeca.Direita = novo;
        else
            Ultima.Direita = novo;
        novo.Direita = NoCabeca;
        Ultima = novo;
        qtosNos++;
    }

    public void InserirCelulaAbaixo(Celula novo)
    {
        if (EstaVazia)
            NoCabeca.Abaixo = novo;
        else
            Ultima.Abaixo = novo;
        novo.Abaixo = NoCabeca;
        Ultima = novo;
        qtosNos++;
    }
}

