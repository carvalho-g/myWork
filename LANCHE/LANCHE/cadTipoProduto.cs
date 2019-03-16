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

    public partial class cadTipoProduto : Form
    {
        //true inserindo, false editando
        bool operacao = true;
        bool localiza = false;

        public cadTipoProduto()
        {
            InitializeComponent();
        }

        private void trava(bool trava)
        {
            txtCodigo.Enabled = trava;
            txtTipoProduto.Enabled = trava;
            btnNovo.Enabled = !trava;
            btnEditar.Enabled = !trava;
            btnExcluir.Enabled = !trava;
            btnSalvar.Enabled = trava;
            btnCancelar.Enabled = trava;
            btnLocaliza.Enabled = !trava;

        }

        private void cadTipoProduto_Load(object sender, EventArgs e)
        {

        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            trava(true);            
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {

            if (txtTipoProduto.Text != "")
            {
                string sql = (operacao) ? "INSERT INTO " : "UPDATE ";
                sql += "tb_tipo_produto SET nome = '" + txtTipoProduto.Text + "', data_cadastro = now()";
                if (DbConnection.inserir(sql))
                {
                    MessageBox.Show("Dados gravados!");
                    Program.clear(this);
                    trava(false);
                }
            }
            else
            {
                MessageBox.Show("Verifique se os campos foram preenchidos!!");
            }

        }

        private void btnLocaliza_Click(object sender, EventArgs e)
        {
            trava(true);
            txtCodigo.Focus();
            localiza = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            trava(false);
            localiza = false;
            operacao = true;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            operacao = false;
        }
    }
}
