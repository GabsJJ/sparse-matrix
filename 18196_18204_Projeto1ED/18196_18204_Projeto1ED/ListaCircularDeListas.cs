using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ListaCircularDeListas
{
    private ListaCircular listaCabeca, ultimaDireitaLista, ultimaAbaixoLista, direitaLista, abaixoLista;
    private int qtasListas;

    public ListaCircularDeListas()
    {
        ListaCabeca = UltimaDireitaLista = UltimaAbaixoLista = DireitaLista = AbaixoLista = null;
        qtasListas = 0;
    }

    public ListaCircular ListaCabeca { get => listaCabeca; set => listaCabeca = value; }
    public ListaCircular UltimaDireitaLista { get => ultimaDireitaLista; set => ultimaDireitaLista = value; }
    public ListaCircular UltimaAbaixoLista { get => ultimaAbaixoLista; set => ultimaAbaixoLista = value; }
    public ListaCircular DireitaLista { get => direitaLista; set => direitaLista = value; }
    public ListaCircular AbaixoLista { get => abaixoLista; set => abaixoLista = value; }
    public int QtasListas { get => qtasListas; set => qtasListas = value; }

    public bool ColunasListaEstaVazia { get => direitaLista == null; }
    public bool LinhasListaEstaVazia { get => abaixoLista == null; }

    public void InserirLista(bool coluna, ListaCircular listaAInserir)
    {
        if (coluna)
        {
            if (ColunasListaEstaVazia)
                DireitaLista = listaAInserir;
            else
                ultimaDireitaLista.direitaLista = listaAInserir;
            listaAInserir.direitaLista = ListaCabeca;
            ultimaDireitaLista = listaAInserir;
            qtosNos++;
        }
        else
        {
            if (ListaCabeca.LinhasListaEstaVazia)
                ListaCabeca.abaixoLista = listaAInserir;
            else
                ultimaAbaixoLista.abaixoLista = listaAInserir;
            listaAInserir.abaixoLista = ListaCabeca;
            ultimaAbaixoLista = listaAInserir;
            qtosNos++;
        }
    }
}

