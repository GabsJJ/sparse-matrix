using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

public class MatrizEsparsa
{
    int linhas, colunas;
    //Nó -1 e -1
    Celula noCabeca, ultimaLinhaAdicionada, ultimaColunaAdicionada, celulaLinhaAnterior, celulaColunaAnterior;
    bool primeiraLeitura;

    const int tamanhoNumero = 4;

    public MatrizEsparsa()
    {
        Linhas = Colunas = 0;
        NoCabeca = celulaColunaAnterior = celulaLinhaAnterior = null;
        primeiraLeitura = true;
        ultimaLinhaAdicionada = ultimaColunaAdicionada = null;
    }

    public int Linhas { get => linhas; set => linhas = value; }
    public int Colunas { get => colunas; set => colunas = value; }
    public bool EstaVazia { get => NoCabeca == null && NoCabeca == null; }
    public bool ColunasVazias { get => NoCabeca.Direita == null; }
    public bool LinhasVazias { get => NoCabeca.Abaixo == null; }
    public Celula NoCabeca { get => noCabeca; set => noCabeca = value; }

    public void PrintarMatriz(DataGridView dgvMatriz)
    {
        if (!EstaVazia)
        {
            if (Linhas != 0 && Colunas != 0)
            {
                Celula linhaAtual = NoCabeca.Abaixo;
                Celula elementoComValor = linhaAtual.Direita;
                dgvMatriz.RowCount = Linhas;
                dgvMatriz.ColumnCount = Colunas;
                int contAuxCol = 1;
                int contAuxLinha = 1;
                while (linhaAtual != NoCabeca)
                {
                    Celula colunaAtual = NoCabeca.Direita;
                    while (colunaAtual != NoCabeca)
                    {
                        if (contAuxCol == elementoComValor.Coluna && elementoComValor != linhaAtual)
                        {
                            dgvMatriz.Rows[contAuxCol - 1].Cells[contAuxLinha - 1].Value = elementoComValor.Valor;
                            elementoComValor = elementoComValor.Direita;
                        }
                        else
                            dgvMatriz.Rows[contAuxLinha - 1].Cells[contAuxCol - 1].Value = 0;
                        colunaAtual = colunaAtual.Direita;
                        contAuxCol++;
                    }
                    linhaAtual = linhaAtual.Abaixo;
                    elementoComValor = linhaAtual.Direita;
                    contAuxLinha++;
                    contAuxCol = 1;
                }
            }
        }
    }

    public void CriarNosCabecas(int qtdLinhas, int qtdColunas)
    {
        if (qtdLinhas > 1 && qtdColunas > 1)
        {
            this.Linhas = qtdLinhas;
            this.Colunas = qtdColunas;
            if (NoCabeca == null)
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
            else
            {
                NoCabeca = null;
                CriarNosCabecas(qtdLinhas, qtdColunas);
            }
                
        }
    }

    public void CriarMatriz(StreamReader arquivo)
    {
        int contAuxLinhas = 0, contAuxColunas = 0;
        while (contAuxColunas < Colunas)
        {
            //vai utilizar o método de criar nós cabeca
        }
    }

    public bool ExisteDado(Celula dado, ref Celula linhaProcurada, ref Celula colunaProcurada)
    {
        bool achou = false;
        if (!EstaVazia)
        {
            if (dado.Coluna > 0 && dado.Linha > 0 && dado.Linha <= Linhas && dado.Coluna <= Colunas)
            {
                Celula atual = NoCabeca.Abaixo;
                int contAuxLinhas = 1, contAuxColunas = 1;
                while (contAuxLinhas <= dado.Linha)
                {
                    if (contAuxLinhas == dado.Linha)
                        linhaProcurada = atual;
                    else
                        atual = atual.Abaixo;
                    contAuxLinhas++;
                }
                atual = NoCabeca.Direita;
                while (contAuxColunas <= dado.Coluna)
                {
                    if (contAuxColunas == dado.Coluna)
                        colunaProcurada = atual;
                    else
                        atual = atual.Direita;
                    contAuxColunas++;
                }
                atual = linhaProcurada;
                while (atual.Direita != linhaProcurada) //analogo: atual.direita != null (lista ligada simples)
                {
                    if (atual.Direita.Linha == dado.Linha && atual.Direita.Coluna == dado.Coluna)
                    {
                        linhaProcurada = atual.Direita;
                        colunaProcurada = atual.Direita;
                        achou = true;
                    }
                    else
                    {
                        celulaLinhaAnterior = atual;
                        atual = atual.Direita;
                    }
                }
            }
        }
        return achou;
    }

    public void InserirCelulaMatriz(Celula dado)
    {
        if (!EstaVazia)
        {
            if (dado.Valor != 0)
            {
                if (dado.Coluna > 0 && dado.Linha > 0 && dado.Linha <= Linhas && dado.Coluna <= Colunas)
                {
                    Celula linhaAinserir = null, colunaAinserir = null;
                    //Existe dado retorna o nó cabeca da linha e da coluna a inserir
                    if (!ExisteDado(dado, ref linhaAinserir, ref colunaAinserir))
                    {
                        //1º insere na linha
                        //se a linha esta vazia
                        if (linhaAinserir.Direita == linhaAinserir)
                            linhaAinserir.Direita = dado;
                        else
                            ultimaLinhaAdicionada.Direita = dado;
                        dado.Direita = linhaAinserir;
                        ultimaLinhaAdicionada = dado;

                        //2º insere na coluna
                        //se a coluna esta vazia
                        if (colunaAinserir.Abaixo == colunaAinserir)
                            colunaAinserir.Abaixo = dado;
                        else
                            ultimaColunaAdicionada.Abaixo = dado;
                        dado.Abaixo = colunaAinserir;
                        ultimaColunaAdicionada = dado;
                    }
                    else
                        linhaAinserir.Valor = dado.Valor;
                }
            }
            else
                Remover(dado);
        }
    }

    public void Remover(Celula dado)
    {
        Celula linhaDoElemento = null, colunaDoElemento = null;
        //Só deleta um elemento se ele existe
        if (ExisteDado(dado, ref linhaDoElemento, ref colunaDoElemento))
        {
            Console.WriteLine();
            Console.WriteLine(linhaDoElemento.Linha + " " + colunaDoElemento.Coluna + " " + linhaDoElemento.Valor);
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
}

