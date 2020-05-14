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
using System.Text.RegularExpressions;

namespace Diplom2
{
    public partial class Form2 : Form
    {

        public string loggin = "";
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form1 = new Form1();
            form1.Closed += (s, args) => this.Close();
            form1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Regex rgx = new Regex(@"[А-Яа-я]");
            string login = textBox1.Text;
            string password = textBox2.Text;
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=diplom;";
            if (String.IsNullOrWhiteSpace(textBox1.Text) || String.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Введите логин и пароль");
            }
            else if (rgx.IsMatch(login) || rgx.IsMatch(password))
            {
                MessageBox.Show("Логин или пароль не должны содержать кириллицу");
            }
            else if (login.Length <= 5 && login.Length >= 1 || password.Length <= 5 && login.Length >= 1)
            {
                MessageBox.Show("Пароль или логин должен состоять не меньше, чем из 6 символов");
            }
            else
            {
                string query = "INSERT INTO account (login, password) VALUES ('" + textBox1.Text + "', '" + textBox2.Text + "')";
                string query2 = "SELECT login FROM account WHERE login = '" + textBox1.Text + "';";
                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                MySqlCommand commandDatabase2 = new MySqlCommand(query2, databaseConnection);
                MySqlDataReader reader;
                MySqlDataReader reader2;
                try
                {
                    databaseConnection.Open();
                    reader2 = commandDatabase2.ExecuteReader();
                    if (reader2.HasRows)
                    {
                        MessageBox.Show("Данный логин уже занят");
                        databaseConnection.Close();
                    }
                    else
                    {
                        databaseConnection.Close();
                        databaseConnection.Open();
                        reader = commandDatabase.ExecuteReader();
                        MessageBox.Show("Регистрация прошла успешно");
                        loggin = textBox1.Text;
                        Class1.logan = this.loggin;
                        databaseConnection.Close();
                        Passage();
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
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;";
            string query = "CREATE DATABASE " + loggin + "";
            string query2 = "CREATE DATABASE " + loggin + "_uchet";
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlCommand commandDatabase2 = new MySqlCommand(query2, databaseConnection);
            MySqlDataReader reader;
            MySqlDataReader reader2;
            try
            {
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                databaseConnection.Close();
                databaseConnection.Open();
                reader2 = commandDatabase2.ExecuteReader();
                databaseConnection.Close();
                this.Hide();
                var form3 = new Form3();
                form3.Closed += (s, args) => this.Close();
                form3.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
