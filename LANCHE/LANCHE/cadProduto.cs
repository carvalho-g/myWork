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
    public partial class cadProduto : Form
    {
        //true inserindo false editando;
        bool operacao = true;
        bool localiza = false;
        
        public cadProduto()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void trava(bool trava)
        {
            btnNovo.Enabled = !trava;
            btnEditar.Enabled = !trava;
            btnExcluir.Enabled = !trava;
            btnSalvar.Enabled = trava;
            btnCancelar.Enabled = trava;
            btnLocaliza.Enabled = !trava;
            txtVlC.Enabled = trava;
            txtVlV.Enabled = trava;
            cbMargemLucro.Enabled = trava;
            //txtMargem.Enabled = trava;
            txtCodigo.Enabled = trava;
            txtProduto.Enabled = trava;
            cmbStatus.Enabled = trava;
            cmbTipoProd.Enabled = trava;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //FormControl.ativaComponente(true, this);
            trava(true);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (cbMargemLucro.Checked == true)
            {
                txtMargem.Enabled = true;
            }
            else
            {
                txtMargem.Enabled = false;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            trava(false);
        }

        private void btnLocaliza_Click(object sender, EventArgs e)
        {
            trava(true);
            txtCodigo.Focus();
            localiza = true;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            operacao = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {

            string sql = (operacao) ? "INSERT INTO " : "UPDATE ";
            if (txtProduto.Text != "" || txtVlC.Text != "" || txtVlV.Text != "" || cmbTipoProd.SelectedIndex != -1)
            {
                string cod_tipo;
                string confirma = "SELECT cod_tipo_produto FROM tb_tipo_produto WHERE nome LIKE '%" + cmbTipoProd.SelectedText + "%'";
                cod_tipo = DbConnection.Select_Text(confirma);


                sql += "tb_produto SET nome = '" + txtProduto.Text.Trim()
                    + "', vl_compra = " + Convert.ToDouble(txtVlC.Text.Trim().Replace(",", "."))
                    + ", vl_venda = " + Convert.ToDouble(txtVlV.Text.Trim().Replace(",", "."))
                    + ", ativo = " + cmbStatus.SelectedIndex
                    + ", cod_tipo_produto = " + cod_tipo;

            }
        }

        private void cadProduto_Load(object sender, EventArgs e)
        {
            cmbStatus.SelectedIndex = 1;
            DbConnection.Select_ComboBox("SELECT DISTINCT nome FROM tb_tipo_produto", cmbTipoProd);
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {

        }
    }
}
