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
    const int tamanhoNumero = 4;

    public MatrizEsparsa()
    {
        Linhas = Colunas = 0;
        NoCabeca = celulaColunaAnterior = celulaLinhaAnterior = null;
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
                while (atual.Direita.Linha != linhaProcurada.Linha) //analogo: atual.direita != null (lista ligada simples)
                {
                    if (atual.Direita.Linha == dado.Linha && atual.Direita.Coluna == dado.Coluna)
                    {
                        linhaProcurada = atual.Direita;
                        achou = true;
                        break;
                    }
                    if (dado.Coluna < atual.Direita.Coluna)
                    {
                        celulaLinhaAnterior = atual;
                        break;
                    }
                    else
                    {
                        celulaLinhaAnterior = atual.Direita;
                        atual = atual.Direita;
                    }
                }
                //Procura a célula do elemento procurado e também a celula anterior na mesma coluna que o elemento em si
                atualColuna = colunaProcurada;
                while (atualColuna.Abaixo.Coluna != colunaProcurada.Coluna) //analogo: atual.direita != null (lista ligada simples)
                {
                    if (atualColuna.Abaixo.Linha == dado.Linha && atualColuna.Abaixo.Coluna == dado.Coluna)
                    {
                        colunaProcurada = atualColuna.Abaixo;
                        achou = true;
                        break;
                    }
                    if (dado.Linha < atualColuna.Abaixo.Linha)
                    {
                        celulaColunaAnterior = atualColuna;
                        break;
                    }
                    else
                    {
                        celulaColunaAnterior = atualColuna.Abaixo;
                        atualColuna = atualColuna.Abaixo;
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
                    Celula aux1 = dado;
                    //Existe dado retorna o nó cabeca da linha e da coluna a inserir
                    if (!ExisteDado(dado, ref linhaAinserir, ref colunaAinserir))
                    {
                        //1º insere na linha
                        //se a linha esta vazia
                        if (linhaAinserir.Direita == linhaAinserir)
                            linhaAinserir.Direita = dado;
                        else if (celulaLinhaAnterior.Direita.Coluna > dado.Coluna)
                        {
                            var aux2 = celulaLinhaAnterior.Direita;
                            celulaLinhaAnterior.Direita = dado;
                            dado.Direita = aux2;
                            while(aux2.Direita != linhaAinserir)
                                if(aux2.Direita != linhaAinserir)
                                    aux2 = aux2.Direita;
                            dado = aux2;
                        }
                        else
                            celulaLinhaAnterior.Direita = dado;
                        dado.Direita = linhaAinserir;
                        dado = aux1;

                        //2º insere na coluna
                        //se a coluna esta vazia
                        if (colunaAinserir.Abaixo == colunaAinserir)
                            colunaAinserir.Abaixo = dado;
                        else if (celulaColunaAnterior.Abaixo.Linha > dado.Linha)
                        {
                            var aux2 = celulaColunaAnterior.Abaixo;
                            celulaColunaAnterior.Abaixo = dado;
                            dado.Abaixo = aux2;
                            while (aux2.Abaixo != colunaAinserir)
                                if (aux2.Abaixo != colunaAinserir)
                                    aux2 = aux2.Abaixo;
                            dado = aux2;
                        }
                        else
                            celulaColunaAnterior.Abaixo = dado;
                        dado.Abaixo = colunaAinserir;
                        dado = aux1;
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
            celulaColunaAnterior.Abaixo = colunaDoElemento.Abaixo;
            celulaLinhaAnterior.Direita = linhaDoElemento.Direita;
        }
    }

    public void ExcluirTodaMatriz()
    {
        /*
         * O ponteiro cabeça não apontando mais ao cabeçalho de linhas e colunas, o garbage collector
         * acaba por eliminando os ponteiros da estrutura da matriz da memória
        */
        NoCabeca.Direita = NoCabeca;
        NoCabeca.Abaixo = NoCabeca;
        NoCabeca = null;
    }

    public void Pesquisar(int linha, int coluna, ref string resultado)
    {
        var elemento = new Celula(null,null,linha,coluna,0);
        Celula linhaDoElemento = null, colunaDoElemento = null;
        /*
         * ExisteDado retorna por referência, celulas correspondentes, ou aos nós cabeça
         * do elemento, ou a celula da coluna e da linha do elemento em si caso ele exista
         */
        if (ExisteDado(elemento, ref linhaDoElemento, ref colunaDoElemento))
            resultado = "Valor: " + linhaDoElemento.Valor.ToString();
        else
            resultado = "Valor: Nada encontrado";
    }

    public void SomarConstanteColuna(int colunaAsomar, double constante)
    {
        int contAuxCol = 1, contAuxLinha = 1;
        var colunaAtual = NoCabeca.Direita;
        //Vai percorrer a matriz até achar a coluna correspondente a que o usuário deseja
        while(contAuxCol <= colunaAsomar)
        {
            //Caso o valor do contador auxiliar seja igual ao da coluna procurada, adicionamos na coluna
            //Senão, vamos para a coluna seguinte e implementamos o conAuxCol (contAuxCol = contador que indica a coluna atual)
            if (contAuxCol == colunaAsomar)
            {
                //Caso achou a coluna correspondente, vai entrar em outra repetição abaixo
                var celulaColunaAtual = colunaAtual.Abaixo;
                double soma = 0;
                while(contAuxLinha <= Linhas)
                {
                    //Se falta algum item na coluna, esse item será inserido (contAuxLinha = linha que o elemento deveria estar)
                    if(celulaColunaAtual.Linha != contAuxLinha)
                        InserirCelulaMatriz(new Celula(null, null, contAuxLinha, colunaAsomar, constante)); 
                    else
                    {
                        soma = celulaColunaAtual.Valor + constante;
                        if (soma == 0)
                            Remover(celulaColunaAtual);
                        else
                            celulaColunaAtual.Valor = soma;
                        celulaColunaAtual = celulaColunaAtual.Abaixo;
                    }
                    contAuxLinha++;
                }
            }
            else
                colunaAtual = colunaAtual.Direita;
            contAuxCol++;
        }
    }

    public MatrizEsparsa SomarDuasMatrizes(MatrizEsparsa outra)
    {
        var matNova = new MatrizEsparsa();
        matNova.CriarNosCabecas(Linhas, Colunas);

        int contAuxLinha = 1, contAuxColuna = 1;
        var linhaAtual      = NoCabeca.Abaixo;
        var linhaAtualOutra = outra.NoCabeca.Abaixo;

        var celulaLinhaMatrizAtual = linhaAtual.Direita;
        var celulaLinhaOutraMatriz = linhaAtualOutra.Direita;
        while (contAuxLinha <= Linhas)
        {
            while(contAuxColuna <= Colunas)
            {
                if (celulaLinhaMatrizAtual.Coluna == contAuxColuna && celulaLinhaMatrizAtual.Linha == contAuxLinha)
                {
                    if (celulaLinhaOutraMatriz.Coluna == contAuxColuna && celulaLinhaOutraMatriz.Linha == contAuxLinha)
                    {
                        var cel = new Celula(null, null, contAuxLinha, contAuxColuna,
                        (celulaLinhaMatrizAtual.Valor + celulaLinhaOutraMatriz.Valor));
                        matNova.InserirCelulaMatriz(cel);
                        celulaLinhaMatrizAtual = celulaLinhaMatrizAtual.Direita;
                        celulaLinhaOutraMatriz = celulaLinhaOutraMatriz.Direita;
                    }
                    else
                    {
                        var cel = new Celula(null, null, contAuxLinha, contAuxColuna, celulaLinhaMatrizAtual.Valor);
                        matNova.InserirCelulaMatriz(cel);
                        celulaLinhaMatrizAtual = celulaLinhaMatrizAtual.Direita;
                    }
                }
                else
                {
                    if (celulaLinhaOutraMatriz.Coluna == contAuxColuna && celulaLinhaOutraMatriz.Linha == contAuxLinha)
                    {
                        var cel = new Celula(null, null, contAuxLinha, contAuxColuna, celulaLinhaOutraMatriz.Valor);
                        matNova.InserirCelulaMatriz(cel);
                        celulaLinhaOutraMatriz = celulaLinhaOutraMatriz.Direita;
                    }
                }
                contAuxColuna++;
            }
            linhaAtual = linhaAtual.Abaixo;
            linhaAtualOutra = linhaAtualOutra.Abaixo;

            celulaLinhaMatrizAtual = linhaAtual.Direita;
            celulaLinhaOutraMatriz = linhaAtualOutra.Direita;

            contAuxLinha++;
            contAuxColuna = 1;
        }
        return matNova;
    }
}