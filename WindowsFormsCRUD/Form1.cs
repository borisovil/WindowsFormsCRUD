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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            frun();
        }
        public void frun(){
            dataGridView1.Rows.Clear();
            try
            {
                using (SqlConnection connection = new SqlConnection(""))
                {
                    connection.ConnectionString = @"Data Source=192.168.56.101,1433\SQLEXPRESS;Initial Catalog=mybase;Persist Security Info=True;User ID=User01;Password=User001";

                    connection.Open();
                    string text = "SELECT users.name, users.login, users.password, users.isadmin, users.id FROM users ";
                    using (SqlCommand command = new SqlCommand(text, connection))
                    using (System.Data.Common.DbDataReader reader = command.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                dataGridView1.Rows.Add();
                                dataGridView1["name", dataGridView1.Rows.Count - 2].Value = reader.GetString(0);
                                dataGridView1["login", dataGridView1.Rows.Count - 2].Value = reader.GetString(1);
                                dataGridView1["password", dataGridView1.Rows.Count - 2].Value = reader.GetString(2);
                                dataGridView1["isadmin", dataGridView1.Rows.Count - 2].Value = reader.GetInt32(3);
                                dataGridView1["id", dataGridView1.Rows.Count - 2].Value = reader.GetInt32(4);
                                dataGridView1["read", dataGridView1.Rows.Count - 2].Value = "Читать";
                                dataGridView1["update", dataGridView1.Rows.Count - 2].Value = "Редактировать";
                                dataGridView1["delete", dataGridView1.Rows.Count - 2].Value = "Удалить";
                            }
                        }
                    };

                    connection.Close();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Ошибка работы с базой данных " + ex);
            }


        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            
            int id = Convert.ToInt32(dataGridView1[7,e.RowIndex].Value.ToString());
            if (id > 0)
            {

                switch (e.ColumnIndex)
                {
                    case 4:
                        // чтение
                        fread4(id);
                        break;
                    case 5:
                        // редактирование
                        fread5(id);
                        break;
                    case 6:
                        //удаление
                        fread(id,dataGridView1[0, e.RowIndex].Value.ToString());
                        break;

                }
            }
        }
        private void fread4(int id)
        {
            if (Program.f2 == null) Program.f2 = new Form2();
            
            Program.f2.id = id;
            Program.f2.frun();
            Program.f2.Show();

            Program.f1.Hide();
        }
        private void fread5(int id)
        {
            if (Program.f3 == null)  Program.f3 = new Form3();
            Program.f3.id = id;
            Program.f3.frun();
            Program.f3.Show();

            Program.f1.Hide();
        }
        private void fread(int id,string s)
        {
            DialogResult e =MessageBox.Show("Вы уверены что хотите удалить запись о "+s+"?", "", MessageBoxButtons.OKCancel);
            if (e== DialogResult.OK)
            {
                string text = "DELETE FROM users WHERE id ="+id;
                try
                {
                    using (SqlConnection connection = new SqlConnection(""))
                    {

                        connection.ConnectionString = @"Data Source=192.168.56.101,1433\SQLEXPRESS;Initial Catalog=mybase;Persist Security Info=True;User ID=User01;Password=User001";
                        connection.Open();
                        SqlCommand command = new SqlCommand(text, connection);
                        int number = command.ExecuteNonQuery();
                        connection.Close();
                        frun();
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Ошибка работы с базой данных " + ex);
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Program.f4 == null)  Program.f4 = new Form4();
            Program.f4.frun();
            Program.f4.Show();

            Program.f1.Hide();

        }
    }
}
