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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
                nudColunas.Enabled = false;
                nudLinhas.Enabled = false;
            } 
        }

        private void btnCriar_Click(object sender, EventArgs e)
        {
            if(nudColunas.Enabled == true && nudLinhas.Enabled == true)
            {
                //Printar apenas
                matriz1.CriarNosCabecas(5, 5);
                matriz1.PrintarMatriz(dgvMatriz1);
            }
            else
            {
                //criar nós cabeças
                if (!matriz1.EstaVazia)
                {
                    matriz1.CriarNosCabecas(int.Parse(nudLinhas.Value.ToString()),
                        int.Parse(nudColunas.Value.ToString()));
                    //Se depois que eu criei uma matriz e outra já havia sido criada
                    // eu tenho que desabilitar o botão já que não podemos criar mais que duas
                    if (!matriz2.EstaVazia)
                        btnCriar.Enabled = false;
                } 
                if (!matriz2.EstaVazia)
                {
                    matriz2.CriarNosCabecas(int.Parse(nudLinhas.Value.ToString()),
                        int.Parse(nudColunas.Value.ToString()));
                    if (!matriz1.EstaVazia)
                        btnCriar.Enabled = false;
                }  
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dlgMatriz2.ShowDialog() == DialogResult.OK)
            {
                nudColunas.Enabled = false;
                nudLinhas.Enabled = false;
            } 
        }
    }
}
