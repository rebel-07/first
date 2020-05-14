using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Diplom2
{
    public partial class Form1 : Form
    {
        public string loggin = "";
        public Form1()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=diplom;";
            if (String.IsNullOrWhiteSpace(textBox1.Text) || String.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Введите логин и пароль");
            }
            else
            {
                string query2 = "SELECT login, password FROM account WHERE login = '" + textBox1.Text + "' && password = '" + textBox2.Text + "';";
                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase2 = new MySqlCommand(query2, databaseConnection);
                MySqlDataReader reader2;
                try
                {
                    databaseConnection.Open();
                    reader2 = commandDatabase2.ExecuteReader();
                    if (reader2.HasRows)
                    {
                        databaseConnection.Close();
                        databaseConnection.Open();
                        reader2 = commandDatabase2.ExecuteReader();
                        loggin = textBox1.Text;
                        Class1.logan = this.loggin;
                        databaseConnection.Close();
                        Passage();
                    }
                    else
                    {
                        MessageBox.Show("Логин или пароль были введены неправильно");
                        databaseConnection.Close();
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show("Введите корректные значения");
                }
            }
        }

        private void Passage()
        {
            this.Hide();
            var form3 = new Form3();
            form3.Closed += (s, args) => this.Close();
            form3.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form2 = new Form2();
            form2.Closed += (s, args) => this.Close();
            form2.Show();
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '*';
            }
            
        }

    }
}
