using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

public class MatrizEsparsa
{
    int linhas, colunas;
    //Nó -1 e -1
    Celula noCabeca;
    bool primeiraLeitura;

    const int tamanhoNumero = 4;

    public MatrizEsparsa()
    {
        Linhas = Colunas = 0;
        NoCabeca = null;
        primeiraLeitura = true;
    }

    public int Linhas { get => linhas; set => linhas = value; }
    public int Colunas { get => colunas; set => colunas = value; }
    public bool EstaVazia { get => NoCabeca.Direita == null && NoCabeca.Abaixo == null; }
    public bool ColunasVazias { get => NoCabeca.Direita == null; }
    public bool LinhasVazias { get => NoCabeca.Abaixo == null; }
    public Celula NoCabeca { get => noCabeca; set => noCabeca = value; }

    public void CriarNosCabecas(int qtdLinhas, int qtdColunas)
    {
        if(qtdLinhas > 1 && qtdColunas > 1)
        {
            this.Linhas = qtdLinhas;
            this.Colunas = qtdColunas;
            if(NoCabeca == null)
            {
                Celula ultimoElementoAdicionado = null;
                NoCabeca = new Celula();
                int contAuxLinhas = 0, contAuxColunas = 0;
                //Adiciona os nosCabecas das linhas na matriz
                while (contAuxLinhas < qtdLinhas)
                {
                    var noCabecaLinhaNovo = new Celula(false);
                    if (LinhasVazias)
                        NoCabeca.Abaixo = noCabecaLinhaNovo;
                    else
                        ultimoElementoAdicionado.Abaixo = noCabecaLinhaNovo;
                    noCabecaLinhaNovo.Abaixo = NoCabeca;
                    noCabecaLinhaNovo.Direita = noCabecaLinhaNovo;
                    ultimoElementoAdicionado = noCabecaLinhaNovo;
                    contAuxLinhas++;
                }
                //Adiciona os nosCabecas das colunas na matriz
                while (contAuxColunas < qtdColunas)
                {
                    var noCabecaColunaNovo = new Celula(true);
                    if (ColunasVazias)
                        NoCabeca.Direita = noCabecaColunaNovo;
                    else
                        ultimoElementoAdicionado.Direita = noCabecaColunaNovo;
                    noCabecaColunaNovo.Direita = NoCabeca;
                    noCabecaColunaNovo.Abaixo = noCabecaColunaNovo;
                    ultimoElementoAdicionado = noCabecaColunaNovo;
                    contAuxColunas++;
                }
            } 
        }
    }

    public void PrintarMatriz()
    {
        if(!EstaVazia)
        {
            if(Linhas != 0 && Colunas != 0)
            {
                Celula linhaAtual = NoCabeca.Abaixo;
                Celula ultimoValor = linhaAtual.Direita;
                while (linhaAtual != NoCabeca)
                {
                    Celula colunaAtual = NoCabeca.Direita;
                    while (colunaAtual != NoCabeca)
                    {
                        Console.Write(ultimoValor.Valor + " ");
                        colunaAtual = colunaAtual.Direita;
                        ultimoValor = ultimoValor.Direita;
                    }
                    Console.WriteLine();
                    linhaAtual = linhaAtual.Abaixo;
                    ultimoValor = linhaAtual.Direita;
                }
            }
        }
    }

    public void CriarMatriz(StreamReader arquivo)
    {
        int contAuxLinhas = 0, contAuxColunas = 0;
        while(contAuxColunas < Colunas)
        {
            
        }
    }

    public void InserirCelulaMatriz(Celula dado)
    {
        if(!EstaVazia)
        {
            if(dado.Valor != 0)
            {
                if (dado.Coluna > 0 && dado.Linha > 0)
                {
                    Celula atual = NoCabeca.Abaixo, ultimoNoAdicionado = null;
                    int contAuxLinhas = 1, contAuxColunas = 1;
                    while (contAuxLinhas <= dado.Linha)
                    {
                        if (contAuxLinhas == dado.Linha)
                        {
                            //verifica se a lista circular da linha atual esta vazia
                            if (atual.Direita == atual)
                                atual.Direita = dado;
                            else
                                ultimoNoAdicionado.Direita = dado;
                            dado.Direita = atual;
                            ultimoNoAdicionado = dado;
                        }
                        else
                            atual = atual.Abaixo;
                        contAuxLinhas++;
                    }
                    atual = NoCabeca.Direita;
                    while (contAuxColunas <= dado.Coluna)
                    {
                        if (contAuxColunas == dado.Coluna)
                        {
                            //verifica se a lista circular da coluna atual esta vazia
                            if (atual.Abaixo == atual)
                                atual.Abaixo = dado;
                            else
                                ultimoNoAdicionado.Abaixo = dado;
                            dado.Abaixo = atual;
                            ultimoNoAdicionado = dado;
                        }
                        else
                            atual = atual.Direita;
                        contAuxColunas++;
                    }
                }
            }
            //else
                //remover
        }
    }
    
    public void LerRegistro(StreamReader arquivo)
    {
        if (!arquivo.EndOfStream)
        {
            string linha = arquivo.ReadLine();
            if (primeiraLeitura)
            {
                linhas = int.Parse(linha.Substring(0, 1));
            }
        }
    }

    /*public bool ExisteDado(Celula valor, ref ListaCircular linha, ref ListaCircular coluna)
    {
        bool achou = false;
        if (!EstaVazia)
        {
            ListaCircular linhaAnterior = null, colunaAnterior = null,
                   linhaAtual = NoCabecaLinhas.Abaixo, colunaAtual = NoCabecasColunas.Direita;
            while (linhaAtual != null && colunaAtual != null)
            {
                if (colunaAtual.Coluna < valor.Coluna && linhaAtual.Linha < valor.Linha)
                {
                    linhaAnterior = linhaAtual;
                    colunaAnterior = colunaAtual;
                    linhaAtual = linhaAtual.Abaixo;
                    colunaAtual = colunaAtual.Abaixo;
                }
                else
                {
                    linha = linhaAtual;
                    coluna = colunaAtual;
                    achou = true;
                }   
            }
        }
        return achou;
    }

    public void Inserir(Celula valor)
    {
        ListaCircular linhaAInserir = null, colunaAINserir = null;
        if(!ExisteDado(valor, ref linhaAInserir, ref colunaAINserir))
        {

        }
    }*/
}

