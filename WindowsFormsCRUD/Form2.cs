using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsCRUD
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
//            frun();
        }
        public void frun()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(""))
                {
                    connection.ConnectionString = @"Data Source=192.168.56.101,1433\SQLEXPRESS;Initial Catalog=mybase;Persist Security Info=True;User ID=User01;Password=User001";

                    connection.Open();
                    string text = "SELECT users.name, users.login, users.password, users.isadmin, users.id FROM users where users.id=" + this.id.ToString();
                    using (SqlCommand command = new SqlCommand(text, connection))
                    using (System.Data.Common.DbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            textBox1.Text= reader.GetString(0);
                            textBox2.Text = reader.GetString(1);
                            textBox3.Text = reader.GetString(2);
                            int i = reader.GetInt32(3);
                            textBox4.Text = "Нет";
                            if (i == 1) textBox4.Text ="Да";
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Ошибка работы с базой данных " + ex);
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Program.f1.Refresh();
            Program.f1.Show();

            Program.f1.frun();
            Program.f2.Hide();
        }
 
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
