using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using PP;

namespace Reg
{
    public partial class RegistrationForm : Form
    {
        /* Переменные, которые будут хранить на протяжение работы программы логин и пароль. */
        public string login = string.Empty;
        public string password = string.Empty;
        private Users user = new Users(); // Экземпляр класса пользователей.

        public RegistrationForm()
        {
            InitializeComponent();

            LoadUsers(); // Метод десериализует класс.
        }

        private void LoadUsers()
        {
            try
            {
                FileStream fs = new FileStream("Users.dat", FileMode.Open);

                BinaryFormatter formatter = new BinaryFormatter();

                user = (Users)formatter.Deserialize(fs);

                fs.Close();
            }
            catch { return; }
        }

        private void EnterToForm()
        {
            if (loginTextBox.Text == "" || passwordTextBox.Text == "") { MessageBox.Show("No login or password entered!"); return; }
            for (int i = 0; i < user.Logins.Count; i++) // Ищем пользователя и проверяем правильность пароля.
            {
                if (user.Logins[i] == loginTextBox.Text && user.Passwords[i] == passwordTextBox.Text)
                {
                    login = user.Logins[i];
                    password = user.Passwords[i];

                    MessageBox.Show("Welcome!");

                    Hide();
                    Form1 form1 = new Form1();
                    form1.ShowDialog();
                }
                else if (user.Logins[i] == loginTextBox.Text && passwordTextBox.Text != user.Passwords[i])
                {
                    login = user.Logins[i];

                    MessageBox.Show("Wrong password!");
                }
            }

            if (login == "") { MessageBox.Show("The user " + loginTextBox.Text + " was not found!"); }
        }

        private void AddUser() // Регистрируем нового пользователя.
        {
            if (loginTextBox.Text == "" || passwordTextBox.Text == "") { MessageBox.Show("No login or password entered!"); return; }
            for (int i = 0; i < user.Logins.Count; i++) // Ищем пользователя и проверяем правильность пароля.
            {
                if (user.Logins[i] == loginTextBox.Text)
                {
                    MessageBox.Show("You are already registered!");
                    return;
                }
            }

                user.Logins.Add(loginTextBox.Text);
            user.Passwords.Add(passwordTextBox.Text);

            FileStream fs = new FileStream("Users.dat", FileMode.OpenOrCreate);

            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, user); // Сериализуем класс.

            fs.Close();

            login = loginTextBox.Text;

            MessageBox.Show("You are registered!");

            loginTextBox.Text = "";
            passwordTextBox.Text = "";

        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < user.Logins.Count; i++) // Ищем пользователя и проверяем правильность пароля.
            {
                user.Logins[i] = "";
                user.Passwords[i] = "";
            }
                Application.Exit(); // Закрываем программу.
        }

        private void regButton_Click(object sender, EventArgs e)
        {
            AddUser();
        }

        private void enterButton_Click(object sender, EventArgs e)
        {
            EnterToForm();
        }

        private void RegistrationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (login == "" | password == "") { Application.Exit(); }
        }
    }
}
