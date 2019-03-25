using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teste
{
    class Program
    {
        static void Main(string[] args)
        {
            var matriz = new MatrizEsparsa();
            matriz.CriarNosCabecas(2,2);

            Celula valor = new Celula(null,null,2,2,20);
            matriz.InserirCelulaMatriz(valor);
            matriz.PrintarMatriz();
            Console.ReadLine();
        }
    }
}
