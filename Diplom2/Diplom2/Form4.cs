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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            Regex rgx = new Regex(@"[А-Яа-я]");
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database="+ Class1.logan +";";
            string connectionString2 = "datasource=127.0.0.1;port=3306;username=root;password=;database="+ Class1.logan +"_uchet;";
            if (String.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Введите название списка");
            }
            else if (rgx.IsMatch(textBox1.Text))
            {
                MessageBox.Show("Название списка не должно содержать кириллицу");
            }
            else
            {
                string query = "CREATE TABLE " + textBox1.Text + "(ID int(10) auto_increment , FIO varchar(100) DEFAULT '' not null, PREDMET varchar(500) DEFAULT '' not null, VSEGO int(10) DEFAULT '0' not null, ITOGO int(10) DEFAULT '0' not null, primary key (id));";
                string query2 = "CREATE TABLE " + textBox1.Text + "_uchett (ID int(10) auto_increment , FIO varchar(100) DEFAULT '' not null, PREDMET varchar(500) DEFAULT '' not null, VSEGO int(10) DEFAULT '0' not null, ITOGO int(10) DEFAULT '0' not null, primary key (id));";
                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlConnection databaseConnection2 = new MySqlConnection(connectionString2);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                MySqlCommand commandDatabase2 = new MySqlCommand(query2, databaseConnection2);
                MySqlDataReader reader;
                MySqlDataReader reader2;
                try
                {
                    databaseConnection.Open();
                    reader = commandDatabase.ExecuteReader();
                    databaseConnection.Close();
                    databaseConnection2.Open();
                    reader2 = commandDatabase2.ExecuteReader();
                    databaseConnection2.Close();
                    MessageBox.Show("Список создан");
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
