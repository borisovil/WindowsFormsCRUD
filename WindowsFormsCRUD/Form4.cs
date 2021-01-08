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
    public partial class Form4 : Form
    {
        bool passwordchang;
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        public void frun(){
            comboBox1.Text = "Нет";
            passwordchang = false;
            textBox4.Text = "";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Program.f1.Refresh();
            Program.f1.Show();

            Program.f1.frun();
            Program.f4.Hide();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

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
            //            string text = "UPDATE users SET  name='" + name + "', login='" + login + "', password='" + password + "', isadmin=" + admin + " where id=" + this.id.ToString() + " ";
            string text = "INSERT INTO users (name, login, password, isadmin) VALUES ('"+name+"', '"+login+"', '"+password+"', "+admin+")";
            try
            {
                using (SqlConnection connection = new SqlConnection(""))
                {

                    connection.ConnectionString = @"Data Source=192.168.56.101,1433\SQLEXPRESS;Initial Catalog=mybase;Persist Security Info=True;User ID=User01;Password=User001";
                    connection.Open();
                    SqlCommand command = new SqlCommand(text, connection);
                    int number = command.ExecuteNonQuery();

                    connection.Close();
                    textBox4.ForeColor = System.Drawing.Color.Black;
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
    }
}
