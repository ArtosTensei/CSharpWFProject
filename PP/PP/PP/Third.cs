using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace PP
{
    public partial class Third : UserControl
    {
        static string connectionString = "SERVER=192.168.1.45;PORT = 3306;DATABASE=app;UID=root;PASSWORD=root;";
        MySqlConnection con = new MySqlConnection(connectionString);
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        DataTable dt = new DataTable();
        public static string login;


        public Third()
        {
            InitializeComponent();
            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].Name = "id";
            dataGridView1.Columns[1].Name = "Name";
            dataGridView1.Columns[2].Name = "Lvl";
            dataGridView1.Columns[3].Name = "Class";
            dataGridView1.Columns[4].Name = "Player name";
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;

        }


        private void add(string name, string lvl, string classs, string playername)
        {
            string sql = "INSERT INTO app.character(name, lvl, class, playername) VALUES(@NAME, @LVL, @CLASS, @PLAYERNAME)";
            cmd = new MySqlCommand(sql, con);

            cmd.Parameters.AddWithValue("@NAME", name);
            cmd.Parameters.AddWithValue("@LVL", lvl);
            cmd.Parameters.AddWithValue("@CLASS", classs);
            cmd.Parameters.AddWithValue("@PLAYERNAME", playername);

            try
            {
                con.Open();

                if (cmd.ExecuteNonQuery() > 0)
                {
                    clearTxts();
                    MessageBox.Show("Successfully Inserted");
                }

                con.Close();

                show(login);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }

        private void populate(string id, string name, string lvl, string classs, string playername)
        {
            dataGridView1.Rows.Add(id, name, lvl, classs, playername);
        }

        private void show(string login)
        {
            dataGridView1.Rows.Clear();

            string sql = "SELECT * FROM app.character";
            cmd = new MySqlCommand(sql, con);

 

            try
            {
                con.Open();

                adapter = new MySqlDataAdapter(cmd);

                adapter.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    populate(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString());
                }

                con.Close();

                dt.Rows.Clear();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                con.Close();
            }
        }

        private void update(int id, string name, string lvl, string classs, string playername)
        {
            string sql = "UPDATE app.character SET name='" + name + "',lvl='" + lvl + "',class='" + classs + "',playername= '" + playername + "' WHERE idcharacter=" + id + "";
            cmd = new MySqlCommand(sql, con);

            try
            {
                con.Open();
                adapter = new MySqlDataAdapter(cmd);

                adapter.UpdateCommand = con.CreateCommand();
                adapter.UpdateCommand.CommandText = sql;

                if (adapter.UpdateCommand.ExecuteNonQuery() > 0)
                {
                    clearTxts();
                    MessageBox.Show("Successfully Updated");
                }

                con.Close();

                show(login);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }


        }

        private void delete(int id)
        {
            
            string sql = "DELETE FROM app.character WHERE idcharacter=" + id + "";
            cmd = new MySqlCommand(sql, con);

            try
            {
                con.Open();

                adapter = new MySqlDataAdapter(cmd);

                adapter.DeleteCommand = con.CreateCommand();

                adapter.DeleteCommand.CommandText = sql;

                if (MessageBox.Show("Are you sure?", "DELETE", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        clearTxts();
                        MessageBox.Show("Successfully deleted");
                    }
                }

                con.Close();

                show(login);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }

        }

        //clear txtx
        private void clearTxts()
        {

            nameTxt.Text = "";
            lvlTxt.Text = "";
            classsTxt.Text = "";
            playernameTxt.Text = "";
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            nameTxt.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            lvlTxt.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            classsTxt.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            playernameTxt.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            add(nameTxt.Text, lvlTxt.Text, classsTxt.Text, playernameTxt.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            show(login);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String selected = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            int id = Convert.ToInt32(selected);

            update(id, nameTxt.Text, lvlTxt.Text, classsTxt.Text, playernameTxt.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            String selected = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            int id = Convert.ToInt32(selected);

            delete(id);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }

        private void lvlTxt_KeyPress(object sender, KeyPressEventArgs e)
        {

            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }

        private void nameTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }

        private void classsTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }

        private void playernameTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            string Symbol = e.KeyChar.ToString();

            if (!Char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }
    }
}
