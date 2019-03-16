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
            localiza = false;
            operacao = true;
            Program.clear(this);
        }

        private void btnLocaliza_Click(object sender, EventArgs e)
        {
            trava(true);
            txtCodigo.Focus();
            localiza = true;
            txtCodigo.ReadOnly = false;
        }

        private void carrega()
        {
            string sql = "SELECT * FROM tb_produto WHERE 1=1";
            if (txtCodigo.Text != "")
            {
                sql += " AND cod_produto = " + txtCodigo.Text;
            }
            else if (txtProduto.Text != "")
            {
                sql += " AND nome LIKE '%" + txtProduto.Text + "%'";
            }
            if (DbConnection.gridDados(sql,dtgResultados))
            {
                tabControl1.SelectedTab = tabPage2;
                tabPage2.Show();
            }
            else
            {
                if (MessageBox.Show("Deseja realizar outra busca?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    btnLocaliza_Click(null, null);
                }
            }
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
                string mrgLucro;
                string confirma = "SELECT cod_tipo_produto FROM tb_tipo_produto WHERE nome LIKE '%" + cmbTipoProd.SelectedText + "%'";
                cod_tipo = DbConnection.Select_Text(confirma);
                mrgLucro = (cbMargemLucro.Checked == true) ? txtMargem.Text : "NULL";
                sql += "tb_produto SET nome = '" + txtProduto.Text.Trim()
                    + "', vl_compra = " + txtVlC.Text.Trim().Replace(",", ".")
                    + ", vl_venda = " + txtVlV.Text.Trim().Replace(",", ".")
                    + ", ativo = " + cmbStatus.SelectedIndex
                    + ", cod_tipo_produto = " + cod_tipo
                    + ", m_lucro = " + mrgLucro + "";
                if (DbConnection.inserir(sql))
                {
                    MessageBox.Show("Registros salvo com sucesso" , "Êxito!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Program.clear(this);
                    trava(false);
                }
                else
                {
                    MessageBox.Show("Erro ao salvar!!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Verifique se os campos foram preenchidos!!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cadProduto_Load(object sender, EventArgs e)
        {
            cmbStatus.SelectedIndex = 1;
            DbConnection.Select_ComboBox("SELECT DISTINCT nome FROM tb_tipo_produto", cmbTipoProd);
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            //chamada do delete aqui
        }

        private void txtVlC_KeyPress(object sender, KeyPressEventArgs e)
        {
            Program.soNumeros(e);
        }

        private void txtVlV_KeyPress(object sender, KeyPressEventArgs e)
        {
            Program.soNumeros(e);
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Program.soNumeros(e);
        }

        private void txtMargem_KeyPress(object sender, KeyPressEventArgs e)
        {
            Program.soNumeros(e);

        }

        private void cadProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //SendKeys.Send("{TAB}");
                if (localiza)
                {
                    txtCodigo.ReadOnly = true;
                    localiza = false;
                    carrega();
                }
            }
        }
    }
}
