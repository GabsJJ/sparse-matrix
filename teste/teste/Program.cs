﻿using System;
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

            
            Celula val = new Celula(null, null, 1, 1, 20);
            Celula val2 = new Celula(null, null, 1, 2, 20);
            Celula valor1 = new Celula(null, null, 5, 1, 20);
            Celula valor2 = new Celula(null, null, 5, 2, 20);
            Celula valor3 = new Celula(null, null, 5, 3, 20);
            Celula valor4 = new Celula(null, null, 5, 4, 20);
            Celula valor5 = new Celula(null, null, 5, 5, 20);
            Celula valor6 = new Celula(null, null, 5, 6, 20);
            Celula valor7 = new Celula(null, null, 5, 7, -2);
            Celula valor8 = new Celula(null, null, 7, 3, 666);
            Celula valor9 = new Celula(null, null, 8, 8, 666);
            matriz.InserirCelulaMatriz(val2);
            matriz.InserirCelulaMatriz(val);
            
            matriz.InserirCelulaMatriz(valor1);
            matriz.InserirCelulaMatriz(valor2);
            matriz.InserirCelulaMatriz(valor3);
            matriz.InserirCelulaMatriz(valor4);
            matriz.InserirCelulaMatriz(valor5);
            matriz.InserirCelulaMatriz(valor6);
            matriz.InserirCelulaMatriz(valor7);
            matriz.InserirCelulaMatriz(valor8);
            matriz.InserirCelulaMatriz(valor9);
            matriz.PrintarMatriz();

            /*Celula valor = new Celula(null, null, 8, 8, 0);
            matriz.InserirCelulaMatriz(valor);
            Console.WriteLine();
            matriz.PrintarMatriz();*/

            Console.ReadLine();
        }
    }
}
