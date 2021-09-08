using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


namespace PP
{
    public partial class Form1 : Form
    {
        string server;
        string database;
        string uid;
        string password;
        MySqlConnection mycon;

        public Form1()
        {
            InitializeComponent();

            firstCustomControl1.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel4.Height = button3.Height;
            panel4.Top = button3.Top;
            firstCustomControl1.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel4.Height = button2.Height;
            panel4.Top = button2.Top;
            third1.BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://vk.com/mrhotdogger");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.instagram.com/mrhotdogger/");
        }

        private void button5_Click(object sender, EventArgs e)
        {

            server = "192.168.1.45";
            database = "app";
            uid = "root";
            password = "root";
            string connectionString;
            connectionString = "SERVER=" + server + "; PORT = 3306 ;" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            mycon = new MySqlConnection(connectionString);

                try
                {
                    mycon.Open();
                    MessageBox.Show("Success!");
                    mycon.Close(); // Закрываем соединение
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error!");
                }
            
        }


    }
}
