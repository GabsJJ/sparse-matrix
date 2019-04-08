using System.Windows.Forms;

public class MatrizEsparsa
{
    //Atributos
    int linhas, colunas;
    Celula noCabeca, celulaLinhaAnterior, celulaColunaAnterior;

    public MatrizEsparsa()
    {
        Linhas = Colunas = 0;
        NoCabeca = celulaColunaAnterior = celulaLinhaAnterior = null;
    }

    //Propriedades
    public int Linhas { get => linhas; set => linhas = value; }
    public int Colunas { get => colunas; set => colunas = value; }
    public bool EstaVazia { get => NoCabeca == null && NoCabeca == null; }
    public bool ColunasVazias { get => NoCabeca.Direita == null; }
    public bool LinhasVazias { get => NoCabeca.Abaixo == null; }
    public Celula NoCabeca { get => noCabeca; set => noCabeca = value; }
    public Celula CelulaLinhaAnterior { get => celulaLinhaAnterior; set => celulaLinhaAnterior = value; }
    public Celula CelulaColunaAnterior { get => celulaColunaAnterior; set => celulaColunaAnterior = value; }

    public void PrintarMatriz(DataGridView dgvMatriz)
    {
        if (!EstaVazia)
        {
            if (Linhas != 0 && Colunas != 0)
            {
                Celula linhaAtual = NoCabeca.Abaixo;
                //elementoComValor vai guardar um elemento que tenha algo dentro
                Celula elementoComValor = linhaAtual.Direita;
                dgvMatriz.RowCount = Linhas;
                dgvMatriz.ColumnCount = Colunas;
                int contAuxCol = 1;
                int contAuxLinha = 1;
                //Vai percorrer todas as cabeças das linhas
                while (linhaAtual != NoCabeca)
                {
                    //Vai percorrer todas as cabeças das colunas
                    Celula colunaAtual = NoCabeca.Direita;
                    while (colunaAtual != NoCabeca)
                    {
                        /*
                         * se na coluna atual e na linha atual era suposto estar um elemento com um valor
                         * este é adicionado no dgv, se a coluna atual e a linha não correspondem as do elemento
                         * adicionamos 0 no dgv e vamos para outro elemento da lista
                         */
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
        else
        {
            dgvMatriz.Rows.Clear();
            dgvMatriz.Columns.Clear();
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
                /*
                 * Adiciona os nosCabecas das linhas na matriz;
                 * Os contAux vão servir para saber quantos nós cabeça programa deve criar, de acordo
                 * com a quantidade de linhas/colunas que devem existir.
                 */
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
            //Se chegou aqui, é porque o usuário quer criar a matriz novamente
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
                /*
                 * Procura os nós correspondentes aos do elemento e guarda nas variáveis, linhaProcurada, colunaProcurada.
                 * Se o elemento não existe, as váriaveis vão ser configuradas nos nós cabeça de onde o elemento
                 * deveria estar
                 */
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
                //Procura a célula do elemento procurado e também as celulas anteriores
                atual = linhaProcurada;
                celulaLinhaAnterior = atual;
                while (atual.Direita.Linha != linhaProcurada.Linha) //analogo: atual.direita != null (lista ligada simples)
                {
                    if (atual.Direita.Linha == dado.Linha && atual.Direita.Coluna == dado.Coluna)
                    {
                        linhaProcurada = atual.Direita;
                        achou = true;
                        break;
                    }
                    /*
                     * se o elemento da direita tem uma coluna atual maior que a do elemento
                     * procurado, o anteriror fica igual ao atual e paramos a execução.
                     * Se não for, percorremos a linha. 
                     */
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
                atualColuna = colunaProcurada;
                celulaColunaAnterior = atualColuna;
                while (atualColuna.Abaixo.Coluna != colunaProcurada.Coluna)
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
            //inserir o valor 0 é equivalente a remover o elemento
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
                        /*se a coluna do atual é maior que a do elemento a inserir, temos que inserir esse elemento antes do atual
                        senão, inserimos ele depois do atual*/
                        else if (celulaLinhaAnterior.Direita.Coluna > dado.Coluna)
                        {
                            var aux2 = celulaLinhaAnterior.Direita;
                            celulaLinhaAnterior.Direita = dado;
                            dado.Direita = aux2;
                            while (aux2.Direita != linhaAinserir)
                                if (aux2.Direita != linhaAinserir)
                                    aux2 = aux2.Direita;
                            dado = aux2;
                        }
                        else
                            celulaLinhaAnterior.Direita = dado;
                        dado.Direita = linhaAinserir;
                        dado = aux1;

                        //Fazemos o mesmo processo que fizemos acima, porém agora tratando as colunas
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
            //a celula anterior recebe como próximo o elemento que esta a frente do atual
            //e isso remove o atual da lista circular
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
        NoCabeca = null;
        Linhas = 0;
        Colunas = 0;
    }

    public void Pesquisar(int linha, int coluna, ref string resultado)
    {
        var elemento = new Celula(null, null, linha, coluna, 0);
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
        while (contAuxCol <= colunaAsomar)
        {
            /*Caso o valor do contador auxiliar seja igual ao da coluna procurada, adicionamos na coluna
            Senão, vamos para a coluna seguinte e implementamos o conAuxCol (contAuxCol = contador que indica a coluna atual)*/
            if (contAuxCol == colunaAsomar)
            {
                //Caso achou a coluna correspondente, vai entrar em outra repetição abaixo
                var celulaColunaAtual = colunaAtual.Abaixo;
                double soma = 0;
                while (contAuxLinha <= Linhas)
                {
                    //Se falta algum item na coluna, esse item será inserido (contAuxLinha = linha que o elemento deveria estar)
                    if (celulaColunaAtual.Linha != contAuxLinha)
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
        //mesmo que não existam elementos nas duas, a matriz será criada (vazia)
        matNova.CriarNosCabecas(Linhas, Colunas);

        int contAuxLinha = 1, contAuxColuna = 1;
        var linhaAtual = NoCabeca.Abaixo;
        var linhaAtualOutra = outra.NoCabeca.Abaixo;

        var celulaLinhaMatrizAtual = linhaAtual.Direita;
        var celulaLinhaOutraMatriz = linhaAtualOutra.Direita;
        //Temos que percorrer as duas matrizes simultaneamente
        while (contAuxLinha <= Linhas)
        {
            while (contAuxColuna <= Colunas)
            {
                //se existe um elemento na matriz A na posição atual
                if (celulaLinhaMatrizAtual.Coluna == contAuxColuna && celulaLinhaMatrizAtual.Linha == contAuxLinha)
                {
                    //se nessa mesma posição também existe um elemento na matriz B, somamos os dois e adicionamos na matriz resultante
                    if (celulaLinhaOutraMatriz.Coluna == contAuxColuna && celulaLinhaOutraMatriz.Linha == contAuxLinha)
                    {
                        //detalhe, se a soma der 0, o item vai ser removido, já que isso é tratado no método de inserção
                        var cel = new Celula(null, null, contAuxLinha, contAuxColuna,
                        (celulaLinhaMatrizAtual.Valor + celulaLinhaOutraMatriz.Valor));
                        matNova.InserirCelulaMatriz(cel);
                        //se existe um elemento nas duas, percorremos as celulas nas duas matrizes
                        celulaLinhaMatrizAtual = celulaLinhaMatrizAtual.Direita;
                        celulaLinhaOutraMatriz = celulaLinhaOutraMatriz.Direita;
                    }
                    //Caso não exista um elemento na matriz B, adicionamos apenas o elemento que existe na A
                    else
                    {
                        var cel = new Celula(null, null, contAuxLinha, contAuxColuna, celulaLinhaMatrizAtual.Valor);
                        matNova.InserirCelulaMatriz(cel);
                        //percorremos apenas na matriz A, já que ainda temos que tratar o elemento da matriz B
                        celulaLinhaMatrizAtual = celulaLinhaMatrizAtual.Direita;
                    }
                }
                //Caso não exista um elemento na posição atual na matriz A
                else
                {
                    //vamos verificar se existe um elemento na matriz B, caso exista, adicionamos na matriz resultante
                    if (celulaLinhaOutraMatriz.Coluna == contAuxColuna && celulaLinhaOutraMatriz.Linha == contAuxLinha)
                    {
                        var cel = new Celula(null, null, contAuxLinha, contAuxColuna, celulaLinhaOutraMatriz.Valor);
                        matNova.InserirCelulaMatriz(cel);
                        celulaLinhaOutraMatriz = celulaLinhaOutraMatriz.Direita;
                    }
                    //Se não existir elementos nas posições atuais nas duas matrizes, nada é adicionado
                }
                //vamos para outra coluna na mesma linha
                contAuxColuna++;
            }
            //vamos para outra linha nas duas matrizes
            linhaAtual = linhaAtual.Abaixo;
            linhaAtualOutra = linhaAtualOutra.Abaixo;
            //temos só um contAux porque vamos percorrer as duas simultaneamente
            contAuxLinha++;

            celulaLinhaMatrizAtual = linhaAtual.Direita;
            celulaLinhaOutraMatriz = linhaAtualOutra.Direita;

            //voltamos para a primeira coluna
            contAuxColuna = 1;
        }
        return matNova;
    }

    public MatrizEsparsa Multiplicar(MatrizEsparsa outra)
    {
        var matNova = new MatrizEsparsa();
        matNova.CriarNosCabecas(Linhas, outra.Colunas);

        int contAuxLinha = 1, contAuxColuna = 1;

        var linhaMatrizA = NoCabeca.Abaixo;
        var celulaLinhaMatrizA = linhaMatrizA.Direita;

        var linhaMatrizB = outra.NoCabeca.Abaixo;
        var colunaMatrizB = outra.NoCabeca.Direita;
        var celulaColunaMatrizB = colunaMatrizB.Abaixo;

        var celulaAtualMatNova = matNova.NoCabeca.Abaixo;
        double somaMultiplicacoes = 0;
        //vai multiplicar até que a matriz resultante esteja toda "cheia"
        while (contAuxLinha <= matNova.Linhas && contAuxColuna <= matNova.Colunas)
        {
            //enquanto não percorreu toda a linha da matriz A e toda a coluna da matriz B...
            while(celulaLinhaMatrizA != linhaMatrizA && celulaColunaMatrizB != colunaMatrizB)
            {
                somaMultiplicacoes += celulaLinhaMatrizA.Valor * celulaColunaMatrizB.Valor;
                celulaLinhaMatrizA = celulaLinhaMatrizA.Direita;
                celulaColunaMatrizB = celulaColunaMatrizB.Abaixo;
            }
            /*depois que percorreu tudo e guardou na váriavel somaMultiplicacoes, a celula da linha da 
            matriz A volta pro inicio e trocamos a coluna da matriz B*/
            celulaLinhaMatrizA = linhaMatrizA.Direita;
            colunaMatrizB = colunaMatrizB.Direita;
            celulaColunaMatrizB = colunaMatrizB.Abaixo;            
            if (celulaColunaMatrizB == linhaMatrizB)
            {
                linhaMatrizA = linhaMatrizA.Abaixo;
                celulaLinhaMatrizA = linhaMatrizA.Direita;
                colunaMatrizB = outra.NoCabeca.Direita;
                celulaColunaMatrizB = colunaMatrizB.Abaixo;
            }

            if (somaMultiplicacoes != 0)
                matNova.InserirCelulaMatriz(new Celula(null, null, contAuxLinha, contAuxColuna, somaMultiplicacoes));
            somaMultiplicacoes = 0;

            //se já chegamos na ultima coluna trocamos de linha e voltamos pra coluna inicial
            if (contAuxColuna == matNova.Colunas)
            {
                contAuxLinha++;
                contAuxColuna = 0;
            } 
            // se já chegamos na ultima linha e na ultima coluna, incrementamos um nos auxiliares para para o while
            if(contAuxColuna == matNova.Colunas && contAuxLinha == matNova.Linhas)
            {
                contAuxColuna = matNova.Colunas + 1;
                contAuxLinha = matNova.Linhas + 1;
            }
            contAuxColuna++;
        }

        return matNova;
    }
}