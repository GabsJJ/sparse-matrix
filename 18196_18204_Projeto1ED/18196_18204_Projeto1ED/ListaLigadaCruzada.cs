using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ListaLigadaCruzada
{
    Celula noCabeca;
    int quantosNos;

    bool primeiroAcessoDoPercurso;

    public ListaLigadaCruzada()
    {
        NoCabeca = null;
        quantosNos = 0;
        primeiroAcessoDoPercurso = false;
    }

    public Celula NoCabeca { get => noCabeca; set => noCabeca = value; }

    public void PercorrerLista()
    {
        for(var i = NoCabeca.Direita; i != null; i = i.Direita)
        {
            for(var ii = )
        }
    }
}
