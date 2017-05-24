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
using System.IO;

namespace MarkdownNotes
{
    /// <summary>
    /// Логика взаимодействия для Find_and_Change.xaml
    /// </summary>
    public partial class Find_and_Change : Window
    {
        NotesView parent;
        //TextBox NoteText;
        public Find_and_Change(NotesView parent)
        {
            InitializeComponent();
            this.parent = parent;
            ToFind.Focus();
        }
        
        private void FindText_OnClick(object sender, RoutedEventArgs e)
        {
            TextBox NoteText = parent.NoteText;
            string textNotes = NoteText.Text;
            if (textNotes == "")
                return;
            int pos = NoteText.SelectionStart + NoteText.SelectionLength;
            string nextText = NoteText.Text.Substring(pos, NoteText.Text.Length - pos);
            int posOffset = nextText.IndexOf(ToFind.Text);
            if (posOffset < 0)
            {
                MessageBox.Show("No matches found");
                NoteText.SelectionStart = pos = 0;
                NoteText.SelectionLength = 0;
                return;
            }
            NoteText.Focus();
            NoteText.SelectionStart = pos + posOffset;
            NoteText.SelectionLength = ToFind.Text.Length;
        }

        private void CatchText_OnClick(object sender, RoutedEventArgs e)
        {
            TextBox NoteText = parent.NoteText;
            string textNotes = NoteText.Text;
            if (textNotes == "")
                return;
            int pos = NoteText.SelectionStart + NoteText.SelectionLength;
            string nextText = NoteText.Text.Substring(pos, NoteText.Text.Length - pos);
            int posOffset = nextText.IndexOf(ToFind.Text);
            if (posOffset < 0)
            {
                MessageBox.Show("No matches found");
                NoteText.SelectionStart = pos = 0;
                NoteText.SelectionLength = 0;
                return;
            }
            string newText = NoteText.Text.Remove(pos + posOffset, ToFind.Text.Length);
            NoteText.Text = newText.Insert(pos + posOffset, ToChange.Text);
            NoteText.Focus();
            NoteText.SelectionStart = pos + posOffset;
            NoteText.SelectionLength = ToChange.Text.Length;
        }

        private void ChangeAll_onClick(object sender, RoutedEventArgs e)
        {
            parent.NoteText.Text = parent.NoteText.Text.Replace(ToFind.Text, ToChange.Text);
        }
    }
}
