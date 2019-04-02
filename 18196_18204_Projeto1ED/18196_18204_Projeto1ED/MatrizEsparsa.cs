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
    Celula noCabeca, celulaLinhaAnterior, celulaColunaAnterior;
    bool primeiraLeitura;

    const int tamanhoNumero = 4;

    public MatrizEsparsa()
    {
        Linhas = Colunas = 0;
        NoCabeca = celulaColunaAnterior = celulaLinhaAnterior = null;
        primeiraLeitura = true;
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
                            dgvMatriz.Rows[contAuxLinha - 1].Cells[contAuxCol - 1].Value = elementoComValor.Valor;
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
                ExcluirTodaMatriz();
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
                Celula atual = NoCabeca.Abaixo, atualColuna = NoCabeca.Direita;
                int contAuxLinhas = 1, contAuxColunas = 1;
                celulaColunaAnterior = NoCabeca.Direita;
                celulaLinhaAnterior = NoCabeca.Abaixo;
                //Procura os nós cabeça correspondentes aos do elemento e guarda nas variáveis, linhaProcurada, colunaProcurada
                while (contAuxLinhas <= dado.Linha)
                {
                    if (contAuxLinhas == dado.Linha)
                        linhaProcurada = atual;
                    else
                        atual = atual.Abaixo;
                    contAuxLinhas++;
                }
                while (contAuxColunas <= dado.Coluna)
                {
                    if (contAuxColunas == dado.Coluna)
                        colunaProcurada = atualColuna;
                    else
                        atualColuna = atualColuna.Direita;
                    contAuxColunas++;
                }
                //Procura a célula do elemento procurado e também a celula anterior na mesma linha que o elemento em si
                atual = linhaProcurada;
                while (atual.Direita != linhaProcurada) //analogo: atual.direita != null (lista ligada simples)
                {
                    if (atual.Direita.Linha == dado.Linha && atual.Direita.Coluna == dado.Coluna)
                    {
                        linhaProcurada = atual.Direita;
                        achou = true;
                    }
                    celulaLinhaAnterior = atual = atual.Direita;
                }
                //Procura a célula do elemento procurado e também a celula anterior na mesma coluna que o elemento em si
                atualColuna = colunaProcurada;
                while (atualColuna.Abaixo != colunaProcurada) //analogo: atual.direita != null (lista ligada simples)
                {
                    if (atualColuna.Abaixo.Linha == dado.Linha && atualColuna.Abaixo.Coluna == dado.Coluna)
                    {
                        colunaProcurada = atualColuna.Abaixo;
                        achou = true;
                    }
                    celulaColunaAnterior = atualColuna = atualColuna.Abaixo;
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
                            celulaLinhaAnterior.Direita = dado;
                        dado.Direita = linhaAinserir;

                        //2º insere na coluna
                        //se a coluna esta vazia
                        if (colunaAinserir.Abaixo == colunaAinserir)
                            colunaAinserir.Abaixo = dado;
                        else
                            celulaColunaAnterior.Abaixo = dado;
                        dado.Abaixo = colunaAinserir;
                    }
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
            celulaColunaAnterior.Abaixo = colunaDoElemento.Abaixo;
            celulaLinhaAnterior.Direita = linhaDoElemento.Direita;
        }
    }

    public void ExcluirTodaMatriz()
    {
        //O ponteiro cabeça não apontando mais ao cabeçalho de linhas e colunas, o garbage collector
        //acaba por eliminando os ponteiros da estrutura da matriz da memória
        NoCabeca = null;
    }

    public void Pesquisar(int linha, int coluna, ref string resultado)
    {
        var elemento = new Celula(null,null,linha,coluna,0);
        Celula linhaDoElemento = null, colunaDoElemento = null;
        if (ExisteDado(elemento, ref linhaDoElemento, ref colunaDoElemento))
            resultado = "Valor: " + linhaDoElemento.Valor.ToString();
        else
            resultado = "Valor: Nada encontrado";
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

