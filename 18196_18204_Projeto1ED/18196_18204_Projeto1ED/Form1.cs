using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    if (dlgMatriz1.ShowDialog() == DialogResult.OK)
                        LerArquivo(new StreamReader(dlgMatriz1.FileName), matriz1);
                    matriz1.PrintarMatriz(dgvMatriz1);
                }
            }
            else
            {
                if (dlgMatriz1.ShowDialog() == DialogResult.OK)
                    LerArquivo(new StreamReader(dlgMatriz1.FileName), matriz1);
                matriz1.PrintarMatriz(dgvMatriz1);
            }
            
        }

        private void btnLerArq2_Click(object sender, EventArgs e)
        {
            if (!matriz1.EstaVazia)
            {
                var result = MessageBox.Show("Deseja criar novamente a matriz ?", "Alerta",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (dlgMatriz1.ShowDialog() == DialogResult.OK)
                        LerArquivo(new StreamReader(dlgMatriz1.FileName), matriz2);
                    matriz2.PrintarMatriz(dgvMatriz2);
                }
            }
            else
            {
                if (dlgMatriz1.ShowDialog() == DialogResult.OK)
                    LerArquivo(new StreamReader(dlgMatriz1.FileName), matriz2);
                matriz2.PrintarMatriz(dgvMatriz2);
            }
        }

        private void LerArquivo(StreamReader arq, MatrizEsparsa mat)
        {
            bool primeiraLeitura = true;
            string linha = "";
            string[] chars;
            while (!arq.EndOfStream)
            {
                if(primeiraLeitura)
                {
                    linha = arq.ReadLine();
                    chars = linha.Split(';');
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
                if(result == DialogResult.Yes)
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
                matriz1.PrintarMatriz(dgvMatriz1);
        }

        private void btnPrintar2_Click(object sender, EventArgs e)
        {
            if (!matriz2.EstaVazia)
                matriz2.PrintarMatriz(dgvMatriz2);
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            var cbx = sender as ComboBox;
            if (!matriz1.EstaVazia && !cbx.Items.Contains(1))
                cbx.Items.Add(1);
            if(!matriz2.EstaVazia && !cbx.Items.Contains(2))
                cbx.Items.Add(2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cbxMatrizes.SelectedItem.ToString() == "1")
            {
                if(txtValor.Text.Trim() != "")
                {
                    var valor = new Celula(null, null, int.Parse(nudLinhas3.Value.ToString()), 
                        int.Parse(nudColunas3.Value.ToString()), int.Parse(txtValor.Text.Trim()));
                    matriz1.InserirCelulaMatriz(valor);
                    matriz1.PrintarMatriz(dgvMatriz1);
                }
            }
            else
            {
                if (txtValor.Text.Trim() != "")
                {
                    var valor = new Celula(null, null, int.Parse(nudLinhas3.Value.ToString()),
                        int.Parse(nudColunas3.Value.ToString()), int.Parse(txtValor.Text.Trim()));
                    matriz2.InserirCelulaMatriz(valor);
                    matriz2.PrintarMatriz(dgvMatriz2);
                }
            }
        }

        private void btnSomar_Click(object sender, EventArgs e)
        {
            if(txtValor2.Text.Trim() != "")
            {
                if(cbxMatriz3.SelectedItem.ToString() == "1")
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

        private void btnSomarDuasMatrizes_Click(object sender, EventArgs e)
        {
            if(!matriz1.EstaVazia && !matriz2.EstaVazia)
            {
                if(matriz1.Linhas == matriz2.Linhas && matriz1.Colunas == matriz2.Colunas)
                {
                    var mat3 = matriz1.SomarDuasMatrizes(matriz2);
                    mat3.PrintarMatriz(dgvMatriz3);
                }
                else
                    MessageBox.Show("As matrizes devem ter a mesma ordem!", "Alerta",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                MessageBox.Show("As matrizes devem não podem estar vazias!", "Alerta",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnMulti_Click(object sender, EventArgs e)
        {

        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (cbxMatrizes2.SelectedItem.ToString() == "1")
            {
                string val = "";
                matriz1.Pesquisar(int.Parse(nudLinhas4.Value.ToString()),
                    int.Parse(nudColunas4.Value.ToString()), ref val);
                lblRetorno.Text = val;
            }
            else
            {
                string val = "";
                matriz2.Pesquisar(int.Parse(nudLinhas4.Value.ToString()),
                    int.Parse(nudColunas4.Value.ToString()), ref val);
                lblRetorno.Text = val;
            }
        }
    }
}
