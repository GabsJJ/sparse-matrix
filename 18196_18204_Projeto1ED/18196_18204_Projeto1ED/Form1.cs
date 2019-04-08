using System;
using System.Windows.Forms;
using System.IO;

namespace _18196_18204_Projeto1ED
{
    public partial class frmMatriz : Form
    {
        MatrizEsparsa matriz1, matriz2;
        public frmMatriz()
        {
            InitializeComponent();
        }

        private void frmMatriz_Load(object sender, EventArgs e)
        {
            matriz1 = new MatrizEsparsa();
            matriz2 = new MatrizEsparsa();
        }

        private void btnLerArq1_Click(object sender, EventArgs e)
        {
            if (!matriz1.EstaVazia)
            {
                var result = MessageBox.Show("Deseja criar novamente a matriz ?", "Alerta",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        if (dlgMatriz1.ShowDialog() == DialogResult.OK)
                            LerArquivo(new StreamReader(dlgMatriz1.FileName), matriz1);
                        matriz1.PrintarMatriz(dgvMatriz1);
                    }
                    catch
                    {
                        MessageBox.Show("Erro ao ler arquivo", "Alerta",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            else
            {
                try
                {
                    if (dlgMatriz1.ShowDialog() == DialogResult.OK)
                        LerArquivo(new StreamReader(dlgMatriz1.FileName), matriz1);
                    matriz1.PrintarMatriz(dgvMatriz1);
                }
                catch
                {
                    MessageBox.Show("Erro ao ler arquivo", "Alerta",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btnLerArq2_Click(object sender, EventArgs e)
        {
            if (!matriz2.EstaVazia)
            {
                var result = MessageBox.Show("Deseja criar novamente a matriz ?", "Alerta",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        if (dlgMatriz1.ShowDialog() == DialogResult.OK)
                            LerArquivo(new StreamReader(dlgMatriz1.FileName), matriz2);
                        matriz2.PrintarMatriz(dgvMatriz2);
                    }
                    catch
                    {
                        MessageBox.Show("Erro ao ler arquivo", "Alerta",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            else
            {
                try
                {
                    if (dlgMatriz1.ShowDialog() == DialogResult.OK)
                        LerArquivo(new StreamReader(dlgMatriz1.FileName), matriz2);
                    matriz2.PrintarMatriz(dgvMatriz2);
                }
                catch
                {
                    MessageBox.Show("Erro ao ler arquivo", "Alerta",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void LerArquivo(StreamReader arq, MatrizEsparsa mat)
        {
            bool primeiraLeitura = true;
            string linha = "";
            string[] chars;
            while (!arq.EndOfStream)
            {
                if (primeiraLeitura)
                {
                    linha = arq.ReadLine();
                    chars = linha.Split(';');
                    if((int.Parse(chars[0]) <= 600 && int.Parse(chars[1]) <= 600))
                        mat.CriarNosCabecas(int.Parse(chars[0]), int.Parse(chars[1]));
                    primeiraLeitura = false;
                }
                else
                {
                    var celulaNova = Celula.LerRegistro(arq);
                    mat.InserirCelulaMatriz(celulaNova);
                }
            }
        }

        private void btnCriar_Click(object sender, EventArgs e)
        {
            if (!matriz1.EstaVazia)
            {
                var result = MessageBox.Show("Deseja criar novamente a matriz ?", "Alerta",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    matriz1.CriarNosCabecas(int.Parse(nudLinhas1.Value.ToString()),
                                    int.Parse(nudColunas1.Value.ToString()));
                    matriz1.PrintarMatriz(dgvMatriz1);
                }
            }
            else
            {
                matriz1.CriarNosCabecas(int.Parse(nudLinhas1.Value.ToString()),
                                    int.Parse(nudColunas1.Value.ToString()));
                matriz1.PrintarMatriz(dgvMatriz1);
            }
        }

        private void btnCriar2_Click(object sender, EventArgs e)
        {
            if (!matriz2.EstaVazia)
            {
                var result = MessageBox.Show("Deseja criar novamente a matriz ?", "Alerta",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    matriz2.CriarNosCabecas(int.Parse(nudLinhas2.Value.ToString()),
                                    int.Parse(nudColunas2.Value.ToString()));
                    matriz2.PrintarMatriz(dgvMatriz2);
                }
            }
            else
            {
                matriz2.CriarNosCabecas(int.Parse(nudLinhas2.Value.ToString()),
                                    int.Parse(nudColunas2.Value.ToString()));
                matriz2.PrintarMatriz(dgvMatriz2);
            }
        }

        private void btnPrintar1_Click(object sender, EventArgs e)
        {
            if (!matriz1.EstaVazia)
                matriz1.ExcluirTodaMatriz();
            matriz1.PrintarMatriz(dgvMatriz1);
        }

        private void btnPrintar2_Click(object sender, EventArgs e)
        {
            if (!matriz2.EstaVazia)
                matriz2.ExcluirTodaMatriz();
            matriz2.PrintarMatriz(dgvMatriz2);
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            var cbx = sender as ComboBox;
            if (!matriz1.EstaVazia && !cbx.Items.Contains(1))
                cbx.Items.Add(1);
            else
                cbx.Items.Remove(1);
            if (!matriz2.EstaVazia && !cbx.Items.Contains(2))
                cbx.Items.Add(2);
            else
                cbx.Items.Remove(2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cbxMatrizes.SelectedItem != null)
            {
                if (txtValor.Text.Length < 11)
                {
                    if (cbxMatrizes.SelectedItem.ToString() == "1")
                    {
                        if (txtValor.Text.Trim() != "")
                        {
                            //o usuário deve indicar um número de colunas e linhas valido
                            if (int.Parse(nudLinhas3.Value.ToString()) <= matriz1.Linhas
                                && int.Parse(nudColunas3.Value.ToString()) <= matriz1.Colunas)
                            {
                                var btnClicado = sender as Button;
                                Celula valor = null;
                                if (btnClicado.Text == "Remover")
                                    valor = new Celula(null, null, int.Parse(nudLinhas3.Value.ToString()),
                                        int.Parse(nudColunas3.Value.ToString()), 0);
                                else
                                    valor = new Celula(null, null, int.Parse(nudLinhas3.Value.ToString()),
                                        int.Parse(nudColunas3.Value.ToString()), int.Parse(txtValor.Text.Trim()));
                                matriz1.InserirCelulaMatriz(valor);
                                matriz1.PrintarMatriz(dgvMatriz1);
                            }
                            else
                                MessageBox.Show("Indique uma coluna/linha válida!", "Alerta",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        if (txtValor.Text.Trim() != "")
                        {
                            if (int.Parse(nudLinhas3.Value.ToString()) <= matriz2.Linhas && int.Parse(nudLinhas3.Value.ToString()) <= matriz2.Colunas)
                            {
                                var btnClicado = sender as Button;
                                Celula valor = null;
                                if (btnClicado.Text == "Remover")
                                     valor = new Celula(null, null, int.Parse(nudLinhas3.Value.ToString()),
                                        int.Parse(nudColunas3.Value.ToString()), 0);
                                else
                                    valor = new Celula(null, null, int.Parse(nudLinhas3.Value.ToString()),
                                        int.Parse(nudColunas3.Value.ToString()), int.Parse(txtValor.Text.Trim()));
                                matriz2.InserirCelulaMatriz(valor);
                                matriz2.PrintarMatriz(dgvMatriz2);
                            }
                            else
                                MessageBox.Show("Indique uma coluna/linha válida!", "Alerta",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
                else
                    MessageBox.Show("Valor muito grande!", "Alerta",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                MessageBox.Show("Escolha uma matriz!", "Alerta",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnSomar_Click(object sender, EventArgs e)
        {
            if (cbxMatriz3.SelectedItem != null)
            {
                if (txtValor2.Text.Length < 11)
                {
                    if (txtValor2.Text.Trim() != "")
                    {
                        if (cbxMatriz3.SelectedItem.ToString() == "1")
                        {
                            matriz1.SomarConstanteColuna(int.Parse(nudColuna5.Value.ToString()),
                                int.Parse(txtValor2.Text));
                            matriz1.PrintarMatriz(dgvMatriz1);
                        }
                        else
                        {
                            matriz2.SomarConstanteColuna(int.Parse(nudColuna5.Value.ToString()),
                                                    int.Parse(txtValor2.Text));
                            matriz2.PrintarMatriz(dgvMatriz2);
                        }
                    }
                }
                else
                    MessageBox.Show("Valor muito grande!", "Alerta",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                MessageBox.Show("Escolha uma matriz!", "Alerta",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnSomarDuasMatrizes_Click(object sender, EventArgs e)
        {
            if (!matriz1.EstaVazia && !matriz2.EstaVazia)
            {
                if (matriz1.Linhas == matriz2.Linhas && matriz1.Colunas == matriz2.Colunas)
                {
                    var mat3 = matriz1.SomarDuasMatrizes(matriz2);
                    mat3.PrintarMatriz(dgvMatriz3);
                }
                else
                    MessageBox.Show("As matrizes devem ter a mesma ordem!", "Alerta",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                MessageBox.Show("As matrizes devem ser criadas!", "Alerta",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnMulti_Click(object sender, EventArgs e)
        {
            if (cbxDuasMatrizesAmultiplicar.SelectedItem != null)
            {
                if (cbxDuasMatrizesAmultiplicar.Items.Count != 0)
                {
                    if (cbxDuasMatrizesAmultiplicar.SelectedItem.ToString() == "A x B")
                    {
                        //Verifica se o numero de colunas da matriz A é igual ao numero de linhas da matriz B
                        if (matriz1.Colunas == matriz2.Linhas)
                        {
                            var mat3 = matriz1.Multiplicar(matriz2);
                            mat3.PrintarMatriz(dgvMatriz3);
                        }
                        else
                            MessageBox.Show("Matrizes inválidas!", "Alerta",
                                  MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        if (matriz2.Colunas == matriz1.Linhas)
                        {
                            var mat3 = matriz2.Multiplicar(matriz1);
                            mat3.PrintarMatriz(dgvMatriz3);
                        }
                        else
                            MessageBox.Show("Matrizes inválidas!", "Alerta",
                                  MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                    MessageBox.Show("As matrizes devem ser criadas!", "Alerta",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                MessageBox.Show("Escolha uma matriz!", "Alerta",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void cbxDuasMatrizesAmultiplicar_Click(object sender, EventArgs e)
        {
            if (!matriz1.EstaVazia && !matriz2.EstaVazia)
            {
                if (cbxDuasMatrizesAmultiplicar.Items.Count == 0)
                {
                    cbxDuasMatrizesAmultiplicar.Items.Add("A x B");
                    cbxDuasMatrizesAmultiplicar.Items.Add("B x A");
                }
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (cbxMatrizes2.SelectedItem != null)
            {
                if (cbxMatrizes2.SelectedItem.ToString() == "1")
                {
                    if (int.Parse(nudLinhas4.Value.ToString()) <= matriz1.Linhas
                                && int.Parse(nudColunas4.Value.ToString()) <= matriz1.Colunas)
                    {
                        string val = "";
                        matriz1.Pesquisar(int.Parse(nudLinhas4.Value.ToString()),
                            int.Parse(nudColunas4.Value.ToString()), ref val);
                        lblRetorno.Text = val;
                    }
                    else
                        MessageBox.Show("Indique uma coluna/linha válida!", "Alerta",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    if (int.Parse(nudLinhas4.Value.ToString()) <= matriz2.Linhas
                                && int.Parse(nudColunas4.Value.ToString()) <= matriz2.Colunas)
                    {
                        string val = "";
                        matriz2.Pesquisar(int.Parse(nudLinhas4.Value.ToString()),
                            int.Parse(nudColunas4.Value.ToString()), ref val);
                        lblRetorno.Text = val;
                    }
                    else
                        MessageBox.Show("Indique uma coluna/linha válida!", "Alerta",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
                MessageBox.Show("Escolha uma matriz!", "Alerta",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
