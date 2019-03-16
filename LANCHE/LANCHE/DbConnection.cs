using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;



namespace LANCHE
{
    class DbConnection
    {
        private static MySqlConnection conn = new MySqlConnection();
        private static DataTable dt = new DataTable();
        public static MySqlDataReader dr;

        static MySqlConnection Conn
        {
            get { return conn; }
        }

        private static Boolean startConnection(Boolean action)
        {
            if (action)
            {
                conn.ConnectionString = "server=localhost;uid=root;pwd=admin@ti!;database=tim_lanches";
                try
                {
                    conn.Open();
                    //MessageBox.Show("deu boa");
                    return true;
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message.ToString());
                }
                return false;
            }
            else
            {
                conn.Close();
                return false;
            }
        }

        public static bool inserir(string sql)
        {
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            try
            {
                DbConnection.startConnection(true);
                cmd.ExecuteNonQuery();
                DbConnection.startConnection(false);
                return true;

            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message.ToString());
                DbConnection.startConnection(false);
                return false;
            }
        }

        public static bool gridDados(string query, DataGridView dtgresultados)
        {
            MySqlDataAdapter adp = new MySqlDataAdapter(query, conn);
            dt = new DataTable();
            try
            {
                DbConnection.startConnection(true);
                adp.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Nenhum item encontrado!");
                    DbConnection.startConnection(false);
                    return false;
                }
                else
                {
                    dtgresultados.AutoGenerateColumns = false;
                    dtgresultados.DataSource = dt;
                    dtgresultados.Refresh();
                    DbConnection.startConnection(false);
                    return true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro " + e.ToString());
                DbConnection.startConnection(false);
                return false;

            }

        }


        public static ComboBox Select_ComboBox(String query, ComboBox combo)
        {
            try
            {
                if (DbConnection.startConnection(true))
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        combo.Items.Add(dr[0].ToString());
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message.ToString());
            }

            DbConnection.startConnection(false);
            return combo;
        }

        public static String Select_Text(String query)
        {
            String result = "";

            try
            {
                if (DbConnection.startConnection(true))
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);


                    result = cmd.ExecuteScalar().ToString();
                    DbConnection.startConnection(false);
                    return result;
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message.ToString());
            }

            DbConnection.startConnection(false);
            return "";
        }
    }
}
