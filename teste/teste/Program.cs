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
            matriz.CriarNosCabecas(10,10);

            Celula valor1 = new Celula(null, null, 5, 5, 20);
            matriz.InserirCelulaMatriz(valor1);

            Celula valor = new Celula(null,null,5,5,0);
            matriz.InserirCelulaMatriz(valor);
            matriz.PrintarMatriz();
            Console.ReadLine();
        }
    }
}
