using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LANCHE
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void cadastroDeProdutosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cadastroDeProdutosToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            cadProduto cd = new cadProduto();
            cd.MdiParent = this;
            cd.Show();
        }

        private void cadastroDeTipoDeProdutosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cadTipoProduto cd = new cadTipoProduto();
            cd.MdiParent = this;
            cd.Show();
        }
    }
}
