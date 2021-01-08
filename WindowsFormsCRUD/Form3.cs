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
using System.Security.Cryptography;

namespace WindowsFormsCRUD
{
    public partial class Form3 : Form
    {
        public int id;
        private bool passwordchang;
        public Form3()
        {
            InitializeComponent();
            frun();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        public void frun()
        {
            textBox4.Text = "";
            this.passwordchang = false;
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
                            textBox1.Text = reader.GetString(0);
                            textBox2.Text = reader.GetString(1);
                            textBox3.Text = reader.GetString(2);
                            int i = reader.GetInt32(3);
                            comboBox1.Text = "Нет";
                            if (i == 1) comboBox1.Text = "Да";
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
            Program.f3.Hide();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string login = textBox2.Text;
            string password = textBox3.Text;
            if (this.passwordchang)
            {
//                password = SHA1(password);
            };
            string admin = comboBox1.Text;
            if (admin == "Нет") { admin = "0"; }
            else { admin = "1"; }
            string text = "UPDATE users SET  name='"+name+ "', login='" + login+ "', password='" + password+"', isadmin="+admin +" where id=" + this.id.ToString()+" ";
            try
            {
                using (SqlConnection connection = new SqlConnection(""))
                {
                    
                    connection.ConnectionString = @"Data Source=192.168.56.101,1433\SQLEXPRESS;Initial Catalog=mybase;Persist Security Info=True;User ID=User01;Password=User001";
                    connection.Open();
                    SqlCommand command = new SqlCommand(text, connection);
                    int number = command.ExecuteNonQuery();

                    connection.Close();
                    textBox4.ForeColor = System.Drawing.Color.Black ;
                    textBox4.Text = "Успешная запись";
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    textBox4.ForeColor = System.Drawing.Color.Red; ;
                    textBox4.Text = "Дубликат логина!";
                }
                else
                {
                    MessageBox.Show("Ошибка работы с базой данных " + ex);
                };
            }

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            this.passwordchang = true;
        }
    }
}
