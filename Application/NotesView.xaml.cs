using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using MarkdownNotes.DataAccess;
using Microsoft.Win32;
using System.IO;
using Microsoft.VisualBasic;

namespace MarkdownNotes
{
    /// <summary>
    /// Interaction logic for NotesView.xaml
    /// </summary>
    public partial class NotesView : Window
    {
        public NotesView()
        {           
            InitializeComponent();
            InitNotes();

        }

        private static bool disableNavigation;

        private void InitNotes()
        {
            Resources["Notes"] = NotesDL.GetInstance.GetNotesList(Application.Current.Properties["UserName"].ToString());
            if (!NotesList.Items.IsEmpty)
            {
                NotesList.SelectedIndex = 0;
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var addedItems = e.AddedItems;
            if (addedItems.Count > 0)
            {
                NoteText.Text = NotesDL.GetInstance.GetNote(((Note)addedItems[0]).Id).Text;
            }
            RenderMarkDown(NoteText.Text);
        }

        private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
        {
            if (NotesList.SelectedItems.Count > 0)
            {
                NotesDL.GetInstance.UpdateNoteText(((Note) NotesList.SelectedItems[0]).Id, NoteText.Text);

            }
            else
            {
                Note note = new Note
                {
                    Name = NewNoteNameTextBox.Text,
                    Text = NoteText.Text
                };

                NotesDL.GetInstance.AddNote(Application.Current.Properties["UserName"].ToString(), note);
                InitNotes();
                
                NewNoteNameTextBox.Visibility = Visibility.Hidden;
                NewNoteNameTextBox.Text = string.Empty;
                
            }
        }

        private void CreateNewNote_OnClick(object sender, RoutedEventArgs e)
        {
            NoteText.Text = String.Empty;
            NotesList.UnselectAll();
            
            NewNoteNameTextBox.Visibility = Visibility.Visible;
        }

        private void FindAndChange_OnClick(object sender, RoutedEventArgs e)
        {
            ToFind.Visibility = Visibility.Visible;
            ToChange.Visibility = Visibility.Visible;
            FindAndChange.Visibility = Visibility.Visible;
            ToFindButton.Visibility = Visibility.Visible;
        }

        private void OpenFile_OnClick(object sender, System.EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.FileName = "Note";
            file.DefaultExt = ".md";
            file.Filter = "Markdown Files (.md)|*.md";
            file.RestoreDirectory = true;

            if (file.ShowDialog() == true)
            {
                string filename = file.FileName;
                NoteText.Text = File.ReadAllText(filename);
            }
        }

        private async void SaveFile_OnClick(object sender, RoutedEventArgs e)
        {
            System.IO.Stream myStream = null;
            SaveFileDialog file = new SaveFileDialog();
            file.FileName = "Note";
            file.DefaultExt = ".md";
            file.Filter = "Markdown Files (.md)|*.md";

            // Show save file dialog box
            //Nullable<bool> result = file.ShowDialog();

            // Process save file dialog box results
            if (file.ShowDialog() == true)
            {
                try
                {
                            string filename = file.FileName;
                            File.WriteAllText(filename, NoteText.Text);
                            
                            MessageBox.Show("Файл сохранен");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void ButtonItalic_OnClick(object sender, RoutedEventArgs e)
        {
            NoteText.Text += "**";
            NoteText.SelectionLength = 0;
            NoteText.SelectionStart = NoteText.Text.Length - 1;
        }

        private void ButtonBold_OnClick(object sender, RoutedEventArgs e)
        {
            NoteText.Text += "****";
            NoteText.SelectionLength = 0;
            NoteText.SelectionStart = NoteText.Text.Length - 2;
        }

        private void ButtonUnderline_OnClick(object sender, RoutedEventArgs e)
        {
            NoteText.Text += "~~~~";
            NoteText.SelectionLength = 0;
            NoteText.SelectionStart = NoteText.Text.Length - 2;
        }

        private void ButtonHeader1_OnClick(object sender, RoutedEventArgs e)
        {
            NoteText.Text += "# ";
            NoteText.SelectionLength = 0;
            NoteText.SelectionStart = NoteText.Text.Length;
        }

        private void ButtonHeader2_OnClick(object sender, RoutedEventArgs e)
        {
            NoteText.Text += "## ";
            NoteText.SelectionLength = 0;
            NoteText.SelectionStart = NoteText.Text.Length;
        }

        private void ButtonHeader3_OnClick(object sender, RoutedEventArgs e)
        {
            NoteText.Text += "### ";
            NoteText.SelectionLength = 0;
            NoteText.SelectionStart = NoteText.Text.Length;
        }

        private void ButtonListNumeric_OnClick(object sender, RoutedEventArgs e)
        {
            NoteText.Text += "0. ";
            NoteText.SelectionLength = 0;
            NoteText.SelectionStart = NoteText.Text.Length;
        }

        private void ButtonListBulleted_OnClick(object sender, RoutedEventArgs e)
        {
            NoteText.Text += "* ";
            NoteText.SelectionLength = 0;
            NoteText.SelectionStart = NoteText.Text.Length;
        }

        private void ButtonImage_OnClick(object sender, RoutedEventArgs e)
        {
            NoteText.Text += "![]()";
            NoteText.SelectionLength = 0;
            NoteText.SelectionStart = NoteText.Text.Length - 3;
        }

        private void ButtonLink_OnClick(object sender, RoutedEventArgs e)
        {
            NoteText.Text += "[]()";
            NoteText.SelectionLength = 0;
            NoteText.SelectionStart = NoteText.Text.Length - 3;
        }

        private void NewNoteNameTextBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (NewNoteNameTextBox.Text == "Note Name")
                NewNoteNameTextBox.Text = String.Empty;
        }

        private void NewNoteNameTextBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (NewNoteNameTextBox.Text == String.Empty)
                NewNoteNameTextBox.Text = "Note Name";
        }

        private void ToFind_OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (ToFind.Text == "To Find")
                ToFind.Text = String.Empty;
        }

        private void ToFind_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (ToFind.Text == String.Empty)
                ToFind.Text = "To Find";
        }

        private void ToChange_OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (ToChange.Text == "To Change")
                ToChange.Text = String.Empty;
        }

        private void ToChange_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (ToChange.Text == String.Empty)
                ToChange.Text = "To Change";
        }

        private void RenderMarkDown(string text)
        {
            if (!string.IsNullOrEmpty(NoteText.Text))
            {
                disableNavigation = false;
                RenderdMarkDownNote.NavigateToString(PrepareHtmlBody(CommonMark.CommonMarkConverter.Convert(text)));
            }
        }

        private void NoteText_OnTextChanged(object sender, TextChangedEventArgs e)
        {

                RenderMarkDown(NoteText.Text);
        }

        private void RenderdMarkDownNote_Navigating(object sender, NavigatingCancelEventArgs e)
        {

            // first page needs to be loaded in webBrowser control
            if (!disableNavigation)
            {
                disableNavigation = true;
                return;
            }
            e.Cancel = true;
           OpenWebsite(e.Uri.ToString());

        }

        public static void OpenWebsite(string url)
        {
            Process.Start(GetDefaultBrowserPath(), url);
        }

        private static string GetDefaultBrowserPath()
        {
            string key = @"http\shell\open\command";
            RegistryKey registryKey =
                Registry.ClassesRoot.OpenSubKey(key, false);
            return ((string)registryKey.GetValue(null, null)).Split('"')[1];
        }


        private static string PrepareHtmlBody(string HtmlNote)
        {
            return "<!DOCTYPE html>" +
                "<html lang=\"en\">" +
                "<head>" +
                "<meta charset=\"utf-8\">" +
                "<title>Parsed Markdown Result HTML</title>" +
                "<style>body { margin: 0; }</style>" +
                "</head>" +
                "<body>" + HtmlNote + "</body>" +
                "</html>";
        }

        private void MenuItemRename_Click(object sender, RoutedEventArgs e)
        {
            
            if (NotesList.SelectedIndex == -1)
            {
                return;
            }
            string newName = Interaction.InputBox("NewName");
            if (newName == "")
                return;
            (NotesList.SelectedItems[0] as Note).Name = newName;
            NotesList.Items.Refresh();


            //string NoteName = Interaction.InputBox("NewName");
            NotesDL.GetInstance.UpdateNoteName((NotesList.SelectedItems[0] as Note).Id, (NotesList.SelectedItems[0] as Note).Name);
            InitNotes();
            //(NotesList.Items.CurrentItem as Note).Name = Interaction.InputBox("NewName");
            //NotesList.Items.Refresh();
        }


        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            if (NotesList.SelectedIndex == -1)
            {
                return;
            }
            NotesDL.GetInstance.DelNote(((Note)NotesList.Items[NotesList.SelectedIndex]).Id);
            InitNotes();
        }

        private void MenuItemShare_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Properties["SelsectedNote"] =  ((Note)NotesList.Items[NotesList.SelectedIndex]).Id;
            UserList window1 = new UserList();
            window1.Show();
        }

       // private void MenuItemRename_Click(object sender, RoutedEventArgs e)
       // {
       //     throw new NotImplementedException();
       // }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window1 = new MainWindow();
            window1.Show();
            Close();
        }

        private void ButtonSync_OnClick(object sender, RoutedEventArgs e)
        {
            InitNotes();
        }

        //private void FindText_OnClick(object sender, EventArgs e)
        //{
        //    string textNotes = NoteText.Text;
        //    if (textNotes.Contains(ToFind.Text))
        //    {
        //        Console.ForegroundColor = ConsoleColor.Green;
        //    }
        //    else
        //    {
        //        MessageBox.Show("No matches found");
        //    }
        //}

        private void FilterString_TextChanged(object sender, TextChangedEventArgs e)
        {
            NotesList.Items.Filter = delegate (object obj)
            {
                if (obj is Note)
                {
                    Note note = obj as Note;
                    return note.Name.Contains(FilterString.Text);
                }
                return false;
            };
        }

        private void FindText_OnClick(object sender, RoutedEventArgs e)
        {
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

        }
    }
}
