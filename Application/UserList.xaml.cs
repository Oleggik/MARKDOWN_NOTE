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
using System.Windows.Shapes;
using MarkdownNotes.DataAccess;

namespace MarkdownNotes
{
    /// <summary>
    /// Interaction logic for UserList.xaml
    /// </summary>
    public partial class UserList : Window
    {
        public UserList()
        {
            InitializeComponent();
            InitNotes();
        }

        private void InitNotes()
        {
            Resources["Users"] = UsersDL.GetInstance.GetUserlist();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if(UsersList.SelectedIndex !=-1)
                NotesDL.GetInstance.AddNoteSharing(((User)UsersList.Items[UsersList.SelectedIndex]).Id, (int)Application.Current.Properties["SelsectedNote"]);
            MessageBox.Show("You have successfully shared a note!");
        }
    }
}
