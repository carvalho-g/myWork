using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LANCHE
{
    class FormControl
    {

        public static void soNumeros(KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar != 45 && e.KeyChar != 43 && e.KeyChar != 44 && e.KeyChar != 8 && (e.KeyChar < 48 || e.KeyChar > 57))
                    e.Handled = true;
            }
            catch { }
        }
        // fazer um if verificando se o componente é groupbox e fazer outro foreach
        // para entrar nos controles
        public static void ativaComponente(bool padrao, Control frm)
        {
            /*foreach (Control item in frm.Controls)
            {
                if (item is ToolStrip)
                {
                    foreach (Control ts in item.Controls)
                    {
                        if (ts is Button)
                        {
                            MessageBox.Show("teste");
                        }

                    }
                }
                if (!(item is ToolStrip))
                {
                    if (item is TabControl)
                    {
                        foreach (Control comp in item.Controls)
                        {
                            if (item is GroupBox)
                            {
                                foreach (Control com in comp.Controls)
                                {
                                    if (!(com is Label))
                                    {
                                        com.Enabled = padrao;
                                    }                                    
                                }
                            }
                        }
                    }

                    if (item is GroupBox)
                    {
                        foreach (Control com in item.Controls)
                        {
                            if (!(com is Label))
                            {
                                com.Enabled = padrao;
                            }
                        }
                    }

                    if (item.Tag.ToString() == "1")
                    {
                        item.Enabled = !padrao;
                    }
                    else
                    {
                        item.Enabled = padrao;
                    }
                     
                }
                else
                {
                    item.Enabled = true;
                }
            
            }*/
        }

        public static void clear(Control formulario)
        {
            foreach (Control c in formulario.Controls)
            {
                if (c is TextBox)
                {
                    (c as TextBox).Text = "";
                }

                if (c is MaskedTextBox)
                {
                    (c as MaskedTextBox).Text = "";
                }

                if (c is ComboBox)
                {
                    (c as ComboBox).SelectedText = "";
                    (c as ComboBox).SelectedIndex = -1;
                }

                if (c is PictureBox)
                {
                    (c as PictureBox).Image.Dispose();
                    (c as PictureBox).Image = null;
                }

                if (c is RadioButton)
                {
                    (c as RadioButton).Checked = false;
                }

                if (c is DataGridView)
                {
                    (c as DataGridView).DataSource = null;
                    (c as DataGridView).Refresh();
                }

                if (c is CheckedListBox)
                {
                    (c as CheckedListBox).ClearSelected();
                    for (int i = 0; i < (c as CheckedListBox).Items.Count; i++)
                        (c as CheckedListBox).SetItemCheckState(i, CheckState.Unchecked);
                }

                clear(c);
            }
        }




    }
}
