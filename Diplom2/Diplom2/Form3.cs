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
using Microsoft.Office.Interop.Excel;

namespace Diplom2
{
    public partial class Form3 : Form
    {
        int k;
        int te = 0;
        int tables;
        int rc;
        int d;
        string n = "i";
        string g = "n";
        StringBuilder saveQuery = new StringBuilder();
        


        public Form3()
        {
            InitializeComponent();
            label1.Text = "Здравствуйте, "+ Class1.logan +"";
            Combo();
            

        }

        public void Combo()
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            string query = "SELECT table_name FROM information_schema.tables WHERE table_schema = '"+ Class1.logan +"';";
            string query2 = "SELECT group_name FROM groupa WHERE user = '" + Class1.logan + "';";
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=kurs1;";
            string connectionString2 = "datasource=127.0.0.1;port=3306;username=root;password=;database=diplom;";
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlConnection databaseConnection2 = new MySqlConnection(connectionString2);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlCommand commandDatabase2 = new MySqlCommand(query2, databaseConnection2);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;
            MySqlDataReader reader2;
            try
            {
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                while (reader.Read())
                {
                    string sName = reader.GetString(0);
                    comboBox1.Items.Add(sName);
                }
                databaseConnection.Close();
                databaseConnection2.Open();
                reader2 = commandDatabase2.ExecuteReader();
                while (reader2.Read())
                {
                    string mName = reader2.GetString(0);
                    comboBox2.Items.Add(mName);
                    comboBox3.Items.Add(mName);
                }
                databaseConnection2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            button6.Enabled = true;
            comboBox2.Enabled = true;
            comboBox3.Enabled = true;
            button4.Enabled = true;
            button3.Enabled = true;
            button7.Enabled = true;
            button10.Enabled = true;
            Spisok();
            Spisok2();


        }

        

        private void Spisok()
        {
            dataGridView1.Columns.Clear();
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=" + Class1.logan + ";";
            MySqlConnection con = new MySqlConnection(connectionString);
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();
            MySqlCommand commandDatabase;
            string query = "SELECT * FROM " + comboBox1.Text + ";";
            commandDatabase = new MySqlCommand(query, con);
            adapter.SelectCommand = commandDatabase;
            adapter.Fill(ds, 0, 10000, "" + comboBox1.Text + "");
            dataGridView1.DataSource = ds.Tables[0];
            tables = dataGridView1.ColumnCount;
            rc = dataGridView1.RowCount;
            label5.Text = Convert.ToString(tables);
            
            dataGridView1.Columns["ID"].ReadOnly = true;
            dataGridView1.Columns["VSEGO"].ReadOnly = true;
            dataGridView1.Columns["ITOGO"].ReadOnly = true;
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            Class1.logan = "";
            this.Hide();
            var form1 = new Form1();
            form1.Closed += (s, args) => this.Close();
            form1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var form4 = new Form4();
            form4.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var form5 = new Form5();
            form5.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Combo();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите удалить список?" + Environment.NewLine + "Все данные будут удалены.", "Удаление списка", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=" + Class1.logan + ";";
                string connectionString2 = "datasource=127.0.0.1;port=3306;username=root;password=;database=" + Class1.logan + "_uchet;";
                string query = "DROP TABLE " + comboBox1.Text + ";";
                string query2 = "DROP TABLE " + comboBox1.Text + "_uchett;";
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
                    dataGridView1.Columns.Clear();
                    comboBox1.Text = "";
                    comboBox2.Text = "";
                    comboBox3.Text = "";
                    databaseConnection.Close();
                    databaseConnection2.Open();
                    reader2 = commandDatabase2.ExecuteReader();
                    dataGridView2.Columns.Clear();
                    databaseConnection2.Close();
                    button6.Enabled = false;
                    Combo();
                    comboBox2.Enabled = false;
                    comboBox3.Enabled = false;
                    button4.Enabled = false;
                    button3.Enabled = false;
                    button7.Enabled = false;
                    button10.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            button5.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=" + Class1.logan + ";";
            string connectionString2 = "datasource=127.0.0.1;port=3306;username=root;password=;database=" + Class1.logan + "_uchet;";
            string query = "ALTER TABLE `" + comboBox1.Text + "` ADD `" + comboBox2.Text + "` INT(10) DEFAULT '0' not null AFTER `PREDMET`; ";
            string query2 = "ALTER TABLE `" + comboBox1.Text + "_uchett` ADD `" + comboBox2.Text + "` INT(10) DEFAULT '0' not null AFTER `PREDMET`; ";
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
                Spisok();
                databaseConnection.Close();
                databaseConnection2.Open();
                reader2 = commandDatabase2.ExecuteReader();
                Spisok2();
                databaseConnection2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("В колонке групп необходимо вводить только цифры");
        }

        private void dataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("В колонке групп необходимо вводить только цифры");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите удалить группу из списка?" + Environment.NewLine + "Все данные будут удалены.", "Удаление группы", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=" + Class1.logan + ";";
                string connectionString2 = "datasource=127.0.0.1;port=3306;username=root;password=;database=" + Class1.logan + "_uchet;";
                string query = "ALTER TABLE `" + comboBox1.Text + "` DROP `" + comboBox3.Text + "`; ";
                string query2 = "ALTER TABLE `" + comboBox1.Text + "_uchett` DROP `" + comboBox3.Text + "`; ";
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
                    comboBox3.Text = "";
                    databaseConnection.Close();
                    databaseConnection2.Open();
                    reader2 = commandDatabase2.ExecuteReader();
                    databaseConnection2.Close();
                    Spisok();
                    Spisok2();
                    Sum();
                    Sum2();
                    SaveTwice();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Sum();
            Sum2();
            SaveTwice();
            Spisok();
            Spisok2();
            button5.Enabled = false;
        }

        private void SaveTwice()
        {
            int d = dataGridView1.RowCount;
            for (int lie = 1; lie < tables; lie++)
            {
                for (int fake = 0; fake < dataGridView1.RowCount; fake++)
                {
                    
                        string connectionString2 = "datasource=127.0.0.1;port=3306;username=root;password=;database=" + Class1.logan + ";";
                        string connectionString3 = "datasource=127.0.0.1;port=3306;username=root;password=;database=" + Class1.logan + "_uchet;";
                        MySqlConnection databaseConnection2 = new MySqlConnection(connectionString2);
                        MySqlConnection databaseConnection3 = new MySqlConnection(connectionString3);
                        string query = "UPDATE `" + comboBox1.Text + "` SET `" + dataGridView1.Columns[lie].Name + "` = '" + dataGridView1.Rows[fake].Cells[lie].Value + "' WHERE `" + comboBox1.Text + "`.`ID` = " + dataGridView1.Rows[fake].Cells[0].Value + ";";
                        string query2 = "UPDATE `" + comboBox1.Text + "_uchett` SET `" + dataGridView2.Columns[lie].Name + "` = '" + dataGridView2.Rows[fake].Cells[lie].Value + "' WHERE `" + comboBox1.Text + "_uchett`.`ID` = " + dataGridView2.Rows[fake].Cells[0].Value + ";";
                    //Console.WriteLine("" + lie + " " + fake + " '" + dataGridView1.Rows[fake].Cells[lie].Value + "' " + fake + "");
                        MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection2);
                        MySqlCommand commandDatabase2 = new MySqlCommand(query2, databaseConnection3);
                        MySqlDataReader reader;
                        MySqlDataReader reader2;
                        try
                        {
                            databaseConnection2.Open();
                            reader = commandDatabase.ExecuteReader();
                            databaseConnection2.Close();
                            databaseConnection3.Open();
                            reader2 = commandDatabase2.ExecuteReader();
                            databaseConnection3.Close();

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                }
                
            }
            for (int lie = 1; lie <= 2; lie++)
            {
                for (int fake = 0; fake < dataGridView1.RowCount; fake++)
                {

                    string connectionString2 = "datasource=127.0.0.1;port=3306;username=root;password=;database=" + Class1.logan + "_uchet;";
                    MySqlConnection databaseConnection2 = new MySqlConnection(connectionString2);
                    string query = "UPDATE `" + comboBox1.Text + "_uchett` SET `" + dataGridView2.Columns[lie].Name + "` = '" + dataGridView1.Rows[fake].Cells[lie].Value + "' WHERE `" + comboBox1.Text + "_uchett`.`ID` = " + dataGridView2.Rows[fake].Cells[0].Value + ";";
                    MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection2);
                    MySqlDataReader reader;
                    try
                    {
                        databaseConnection2.Open();
                        reader = commandDatabase.ExecuteReader();
                        databaseConnection2.Close();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

            }
            MessageBox.Show("Сохранено");

        }

        /*private void Cleeear()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=" + Class1.logan + ";";
            string query = "TRUNCATE TABLE " + comboBox1.Text + ";";
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlDataReader reader;
            try
            {
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }*/

        /*private void Comm()
        {
            saveQuery.Clear();
            saveQuery.Append("INSERT INTO `" + comboBox1.Text + "` SET `grab`=`test`");
            /*if (tables >= 6)
            {
                int add = tables - 5;
                for (int l = 0; l < add; l++)
                {
                    saveQuery.Append(", `test`");
                }
                label6.Text = Convert.ToString(saveQuery);
            }
            label6.Text = Convert.ToString(saveQuery);
        }*/

        /*private void Test()
        {
            for (int fake = 0; fake < dataGridView1.RowCount; fake++)
            {
                for (int lie = 1; lie < tables; lie++)
                {
                    Console.WriteLine("" + lie + " " + fake + " '" + dataGridView1.Rows[fake].Cells[lie].Value + "'");
                }
            }
        }*/

        private void button10_Click(object sender, EventArgs e)
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=" + Class1.logan + ";";
            string connectionString2 = "datasource=127.0.0.1;port=3306;username=root;password=;database=" + Class1.logan + "_uchet;";
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlConnection databaseConnection2 = new MySqlConnection(connectionString2);
            string query = "INSERT INTO " + comboBox1.Text + " (ID) VALUES (NULL);";
            string query2 = "INSERT INTO " + comboBox1.Text + "_uchett (ID) VALUES (NULL);";
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
                SaveTwice();
                Spisok();
                Spisok2();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите удалить строку?" + Environment.NewLine + "Все данные будут удалены.", "Удаление строки", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                e.Cancel = true;
                string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=" + Class1.logan + ";";
                string connectionString2 = "datasource=127.0.0.1;port=3306;username=root;password=;database=" + Class1.logan + "_uchet;";
                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlConnection databaseConnection2 = new MySqlConnection(connectionString2);
                string query3 = "DELETE FROM " + comboBox1.Text + " WHERE ID = " + dataGridView1.SelectedRows[0].Cells[0].Value + ";";
                string query4 = "DELETE FROM " + comboBox1.Text + "_uchett WHERE ID = " + dataGridView1.SelectedRows[0].Cells[0].Value + ";";
                MySqlCommand commandDatabase2 = new MySqlCommand(query3, databaseConnection);
                MySqlCommand commandDatabase3 = new MySqlCommand(query4, databaseConnection2);
                MySqlDataReader reader3;
                MySqlDataReader reader4;
                try
                {
                    databaseConnection.Open();
                    reader3 = commandDatabase2.ExecuteReader();
                    databaseConnection.Close();
                    databaseConnection2.Open();
                    reader4 = commandDatabase3.ExecuteReader();
                    databaseConnection2.Close();
                    MessageBox.Show("Строка удалена");
                    Spisok();
                    Spisok2();
                    Sum();
                    Sum2();
                    SaveTwice();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void dataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {

        }

        private void Sum()
        {
            int cellls = tables - 3;
            int cell = tables - 1;
            
            
            Console.WriteLine("" + cellls + "");
            if (tables >=6)
            {
                for (int fake = 0; fake < dataGridView1.RowCount; fake++)
                {
                    dataGridView1.Rows[fake].Cells["VSEGO"].Value = 0;
                    dataGridView1.Rows[fake].Cells["ITOGO"].Value = 0;
                }
                int idiot = 0;
                for (int fake = 0; fake < dataGridView1.RowCount; fake++)
                {
                    int bastard = 0;
                    
                    for (int lie = 3; lie <= cellls; lie++)
                    {
                        int jerk;
                        if (dataGridView1.Rows[fake].Cells[lie].Value == null || dataGridView1.Rows[fake].Cells[lie].Value == DBNull.Value || String.IsNullOrWhiteSpace(dataGridView1.Rows[fake].Cells[lie].Value.ToString()))
                        {
                            dataGridView1.Rows[fake].Cells[lie].Value = 0;
                            jerk = Convert.ToInt32(dataGridView1.Rows[fake].Cells[lie].Value);
                        }
                        else
                        {
                            jerk = Convert.ToInt32(dataGridView1.Rows[fake].Cells[lie].Value);
                        }
                        bastard = bastard + jerk;
                        dataGridView1.Rows[fake].Cells["VSEGO"].Value = bastard;
                        //int moron = Convert.ToInt32(dataGridView1.Rows[0].Cells["ITOGO"].Value);
                        
                        //Console.WriteLine("" + lie + " " + fake + " '" + dataGridView1.Rows[fake].Cells[lie].Value + "' " + dataGridView1.Rows[fake].Cells["VSEGO"].Value + " " + idiot + " " + bastard + " ");
                    }
                    
                    int schmuck = Convert.ToInt32(dataGridView1.Rows[fake].Cells["VSEGO"].Value);
                    idiot = idiot + schmuck;
                    dataGridView1.Rows[0].Cells["ITOGO"].Value = idiot;
                    Console.WriteLine("" + dataGridView1.Rows[fake].Cells["VSEGO"].Value + "");
                }
            }
        }

        private void Sum2()
        {
            int cellls = tables - 3;
            int cell = tables - 1;


            Console.WriteLine("" + cellls + "");
            if (tables >= 6)
            {
                for (int fake = 0; fake < dataGridView1.RowCount; fake++)
                {
                    dataGridView2.Rows[fake].Cells["VSEGO"].Value = 0;
                    dataGridView2.Rows[fake].Cells["ITOGO"].Value = 0;
                }
                int idiot = 0;
                for (int fake = 0; fake < dataGridView1.RowCount; fake++)
                {
                    int bastard = 0;
                    int blur = 0;
                    int albarn = 0;
                    for (int lie = 3; lie <= cellls; lie++)
                    {
                        int jerk;
                        int damon;
                        if (dataGridView1.Rows[fake].Cells[lie].Value == null || dataGridView1.Rows[fake].Cells[lie].Value == DBNull.Value || String.IsNullOrWhiteSpace(dataGridView1.Rows[fake].Cells[lie].Value.ToString()))
                        {
                            dataGridView1.Rows[fake].Cells[lie].Value = 0;
                            jerk = Convert.ToInt32(dataGridView1.Rows[fake].Cells[lie].Value);
                        }
                        else
                        {
                            jerk = Convert.ToInt32(dataGridView1.Rows[fake].Cells[lie].Value);
                        }
                        if (dataGridView2.Rows[fake].Cells[lie].Value == null || dataGridView2.Rows[fake].Cells[lie].Value == DBNull.Value || String.IsNullOrWhiteSpace(dataGridView2.Rows[fake].Cells[lie].Value.ToString()))
                        {
                            dataGridView1.Rows[fake].Cells[lie].Value = 0;
                            damon = Convert.ToInt32(dataGridView2.Rows[fake].Cells[lie].Value);
                        }
                        else
                        {
                            damon = Convert.ToInt32(dataGridView2.Rows[fake].Cells[lie].Value);
                        }
                        blur = blur + damon;
                        bastard = bastard + jerk;
                        albarn = blur - bastard;

                        dataGridView2.Rows[fake].Cells["VSEGO"].Value = albarn;
                       
                    }

                    int schmuck = Convert.ToInt32(dataGridView2.Rows[fake].Cells["VSEGO"].Value);
                    idiot = idiot + schmuck;
                    dataGridView2.Rows[0].Cells["ITOGO"].Value = idiot;
                    Console.WriteLine("" + dataGridView1.Rows[fake].Cells["VSEGO"].Value + "");
                }
            }
        }

        /*private void Checking()
        {
            string connectionString2 = "datasource=127.0.0.1;port=3306;username=root;password=;database=diplom;";
            MySqlConnection databaseConnection2 = new MySqlConnection(connectionString2);
            string query = "SELECT " + Class1.logan + " FROM u_status WHERE u_owner = '" + Class1.logan+"'";
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection2);
            MySqlDataReader reader;
            try
            {
                databaseConnection2.Open();
                reader = commandDatabase.ExecuteReader();
                if (reader.HasRows)
                {
                    te = 1;
                    Spisok2();
                }
                else
                {
                    te = 0;
                }
                databaseConnection2.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }*/

        private void Spisok2()
        {
            dataGridView2.Columns.Clear();
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=" + Class1.logan + "_uchet;";
            MySqlConnection con = new MySqlConnection(connectionString);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataSet ds = new DataSet();
            MySqlCommand commandDatabase;
            string query = "SELECT * FROM " + comboBox1.Text + "_uchett;";
            commandDatabase = new MySqlCommand(query, con);
            adapter.SelectCommand = commandDatabase;
            adapter.Fill(ds, 0, 10000, "" + comboBox1.Text + "_uchett");
            dataGridView2.DataSource = ds.Tables[0];
            //tables = dataGridView1.ColumnCount;
            //rc = dataGridView1.RowCount;
            dataGridView2.Columns["ID"].ReadOnly = true;
            dataGridView2.Columns["VSEGO"].ReadOnly = true;
            dataGridView2.Columns["ITOGO"].ReadOnly = true;
            dataGridView2.Columns["FIO"].ReadOnly = true;
            dataGridView2.Columns["PREDMET"].ReadOnly = true;
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                te = 0;
                button10.Enabled = true;
            }
            if (tabControl1.SelectedTab == tabPage2)
            {
                te = 1;
                button10.Enabled = false;
            }
            label8.Text = Convert.ToString(te);
        }

        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView2_CellValueChanged_1(object sender, DataGridViewCellEventArgs e)
        {
            button5.Enabled = true;
        }

        

        private void button8_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook book = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            worksheet = book.Sheets["Лист1"];
            worksheet = book.ActiveSheet;
            worksheet.Name = "Spisok";

            if (te == 0)
            {
                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                {
                    worksheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                }
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 1; j < dataGridView1.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value;
                    }
                }
            }

            else if (te == 1)
            {
                for (int i = 1; i < dataGridView2.Columns.Count + 1; i++)
                {
                    worksheet.Cells[1, i] = dataGridView2.Columns[i - 1].HeaderText;
                }
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    for (int j = 1; j < dataGridView2.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dataGridView2.Rows[i].Cells[j].Value;
                    }
                }
            }

            var saveFileDialoge = new SaveFileDialog();
            saveFileDialoge.FileName = "PedNagruzka";
            saveFileDialoge.DefaultExt = ".xlsx";
            if (saveFileDialoge.ShowDialog()==DialogResult.OK)
            {
                book.SaveAs(saveFileDialoge.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            }
            app.Quit();

        }
    }
}
