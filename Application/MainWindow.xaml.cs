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

            if (passwordBoxPassword.Password.ToString() == UsersDL.GetInstance.GetUserPassword(textBoxLogin.Text))
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
            if (!string.IsNullOrEmpty(UsersDL.GetInstance.AddNewUser(textBoxLogin.Text, passwordBoxPassword.Password.ToString())))
                MessageBox.Show("This name is already use. Try a different name");
            else
            {
                MessageBox.Show("Congratulations, you have successfully registered!");
            }
        }

        private void passwordBoxPassword_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void ShowPassword_Checked(object sender, RoutedEventArgs e)
        {
       //     MessageBox.Show($"{passwordBoxPassword.Password}");
            passwordBoxPassword.Password = passwordBoxPassword.Password;
        }

        private void ShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        //private void TextBoxLogin_OnGotFocus(object sender, RoutedEventArgs e)
        //{
        //    if (textBoxLogin.Text == "Login")
        //        textBoxLogin.Text = String.Empty;
        //}

        //private void TextBoxLogin_OnLostFocus(object sender, RoutedEventArgs e)
        //{
        //    if (textBoxLogin.Text == String.Empty)
        //        textBoxLogin.Text = "Login";
        //}

        //private void TextBoxPassword_OnGotFocus(object sender, RoutedEventArgs e)
        //{
        //    if (textBoxPassword.Text == "Password")
        //        textBoxPassword.Text = String.Empty;
        //}

        //private void TextBoxPassword_OnLostFocus(object sender, RoutedEventArgs e)
        //{
        //    if (textBoxPassword.Text == String.Empty)
        //        textBoxPassword.Text = "Password";
        //}
    }
}
