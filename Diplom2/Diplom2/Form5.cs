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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=diplom;";
            if (String.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Введите название группы");
            }
            else
            {
                string query = "INSERT INTO groupa (group_name, user) VALUES ('" + textBox1.Text + "','" + Class1.logan + "')";
                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                MySqlDataReader reader;
                try
                {
                    databaseConnection.Open();
                    reader = commandDatabase.ExecuteReader();
                    MessageBox.Show("Группа создана");
                    databaseConnection.Close();
                    Form3 f = new Form3();
                    f.Combo();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
