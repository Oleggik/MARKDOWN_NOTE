using MarkdownNotes.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Cryptography;
using System.Threading;

namespace MarkdownNotes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            textBoxLogin.Focus();
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            MD5 md5Hasher = MD5.Create();

            // Преобразуем входную строку в массив байт и вычисляем хэш
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(pas));

            // Создаем новый Stringbuilder (Изменяемую строку) для набора байт
            StringBuilder sBuilder = new StringBuilder();

            // Преобразуем каждый байт хэша в шестнадцатеричную строку
            for (int i = 0; i < data.Length; i++)
            {
                //указывает, что нужно преобразовать элемент в шестнадцатиричную строку длиной в два символа
                sBuilder.Append(data[i].ToString("x2"));
            }

            string s;
            s = sBuilder.ToString();

            if (s == UsersDL.GetInstance.GetUserPassword(textBoxLogin.Text))
            {

                Application.Current.Properties["UserName"] =  textBoxLogin.Text;
                NotesView window1 = new NotesView();
                window1.Show();
                Close();
            }
                
            else
            {
                MessageBox.Show("Wrong Login or Password");
            }
        }

        private void buttonRegistr_Click(object sender, RoutedEventArgs e)
        {

            MD5 md5Hasher = MD5.Create();

            // Преобразуем входную строку в массив байт и вычисляем хэш
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(pas));

            // Создаем новый Stringbuilder (Изменяемую строку) для набора байт
            StringBuilder sBuilder = new StringBuilder();

            // Преобразуем каждый байт хэша в шестнадцатеричную строку
            for (int i = 0; i < data.Length; i++)
            {
                //указывает, что нужно преобразовать элемент в шестнадцатиричную строку длиной в два символа
                sBuilder.Append(data[i].ToString("x2"));
            }

            string s;
            s = sBuilder.ToString();

            if (!string.IsNullOrEmpty(UsersDL.GetInstance.AddNewUser(textBoxLogin.Text, s)))
                MessageBox.Show("This name is already use. Try a different name");
            else
            {
                MessageBox.Show("Congratulations, you have successfully registered!");
            }
        }

        string pas = "";
        int i = 0;

        private void ChangeSymbolsInPassword(object sender, TextCompositionEventArgs e)

        {
            pas = pas + e.Text;
            i++;
            passwordBox.Text = new string('*', i);
            passwordBox.SelectionStart = passwordBox.Text.Length;
        }

        private void Backspace_OnClick(object sender, KeyEventArgs e)
        {
            
            if (e.Key == Key.Back)
            
            {
                i--;
                pas = pas.Remove(pas.Length - 1, 1);
                passwordBox.SelectionStart = passwordBox.Text.Length - 1;
            }
        }

        private void ShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            passwordBox.Text = pas;
        }

        private void ShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            string s = new string('*', passwordBox.Text.Length);
            s.Replace(passwordBox.Text, s);
            passwordBox.Text = s;
        }
    }
}
