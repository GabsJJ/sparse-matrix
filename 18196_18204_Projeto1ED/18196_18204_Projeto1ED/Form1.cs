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
            if(dlgMatriz1.ShowDialog() == DialogResult.OK)
            {
                var arq = new StreamReader(dlgMatriz1.FileName);
                matriz1.CriarMatriz(arq);
                nudColunas1.Enabled = false;
                nudLinhas1.Enabled = false;
            } 
        }

        private void btnCriar_Click(object sender, EventArgs e)
        {
            matriz1.CriarNosCabecas(int.Parse(nudLinhas1.Value.ToString()),
                int.Parse(nudColunas1.Value.ToString()));
            matriz1.PrintarMatriz(dgvMatriz1);
        }

        private void btnCriar2_Click(object sender, EventArgs e)
        {
            matriz2.CriarNosCabecas(int.Parse(nudLinhas2.Value.ToString()),
                   int.Parse(nudColunas2.Value.ToString()));
            matriz2.PrintarMatriz(dgvMatriz2);
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
            if (cbxMatrizes.SelectedIndex == 0)
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

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (cbxMatrizes2.SelectedIndex == 0)
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (dlgMatriz2.ShowDialog() == DialogResult.OK)
            {
                nudColunas1.Enabled = false;
                nudLinhas1.Enabled = false;
            } 
        }
    }
}
