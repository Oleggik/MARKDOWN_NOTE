﻿using System;
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

namespace MarkdownNotes
{
    /// <summary>
    /// Interaction logic for NotesView.xaml
    /// </summary>
    public partial class NotesView : Window
    {
        Find_and_Change finder;
        public NotesView()
        {           
            InitializeComponent();
            InitNotes();
        }

        private static bool disableNavigation;

        private void InitNotes()
        {
            Resources["Categories"] = CategoryDL.GetInstance.GetCategoryList(Application.Current.Properties["UserName"].ToString());

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
            NoteText.Focus();
            NoteText.SelectionStart = NoteText.Text.Length;
        }

        private void ChoseCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ChoseCategory.SelectedIndex != -1)
            {
                Resources["Notes"] = NotesDL.GetInstance.GetNoteInCategorylist(Application.Current.Properties["UserName"].ToString(), ((Category)ChoseCategory.Items[ChoseCategory.SelectedIndex]).Id);
                if (!NotesList.Items.IsEmpty)
                {
                    NotesList.SelectedIndex = 0;
                }
            }
            RenderMarkDown(NoteText.Text);
            NoteText.Focus();
            NoteText.SelectionStart = NoteText.Text.Length;

        }

        private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
        {
            if (NotesList.SelectedItems.Count > 0)
            {
                NotesDL.GetInstance.UpdateNoteText(((Note) NotesList.SelectedItems[0]).Id, NoteText.Text);
            }
            MessageBox.Show("File successfully saved");
        }

        private void CreateNewNote_OnClick(object sender, RoutedEventArgs e)
        {
            string newName;

            NoteText.Text = String.Empty;
            NotesList.UnselectAll();

            Names name = new Names();
            name.ShowDialog();
            newName = name.NewName.Text;
            if (newName == "") return;

            Note note = new Note
            {
                Name = newName,
                Text = NoteText.Text
            };

            NotesDL.GetInstance.AddNote(Application.Current.Properties["UserName"].ToString(), note);
            InitNotes();

            MessageBox.Show("File successfully saved");
        }

        private void AddCategory_OnClick(object sender, RoutedEventArgs e)
        {
            string newName;

            Names name = new Names();
            name.ShowDialog();
            newName = name.NewName.Text;
            if (newName == "") return;

            Category category = new Category
            {
                Name = newName
            };

            CategoryDL.GetInstance.AddCategory(category, Application.Current.Properties["UserName"].ToString());
            InitNotes();
            MessageBox.Show("Category successfully created");
        }

        private void FindAndChange_OnClick(object sender, RoutedEventArgs e)
        {
            NoteText.Focus();
            NoteText.SelectionStart = 0;

            finder = new Find_and_Change(this);
            finder.Show();
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

        private void SaveFile_OnClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog file = new SaveFileDialog();
            file.FileName = "Note";
            file.DefaultExt = ".md";
            file.Filter = "Markdown Files (.md)|*.md";
            if (file.ShowDialog() == true)
            {
                            string filename = file.FileName;
                            File.WriteAllText(filename, NoteText.Text);
                            
                            MessageBox.Show("File successfully saved");    
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

        private void RenderMarkDown(string text)
        {
            if (!string.IsNullOrEmpty(NoteText.Text))
            {
                disableNavigation = false;
                RenderdMarkDownNote.NavigateToString(PrepareHtmlBody(CommonMark.CommonMarkConverter.Convert(text)));
            }
            //else
            //{
            //    RenderdMarkDownNote.Refresh();
            //}
        }

        private void NoteText_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            RenderMarkDown(NoteText.Text);
            string WordCount = (NoteText.Text.Count(x => x == ' ') + 2).ToString();
            string LinesCount = (NoteText.Text.Count(x => x == '\n') + 1).ToString();
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

            string newName;
            Names name = new Names();
            name.ShowDialog();
            newName = name.NewName.Text;
            if (newName == "")
                return;

            NotesDL.GetInstance.UpdateNoteName((NotesList.SelectedItems[0] as Note).Id, newName);
            InitNotes();
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

        private void MenuItemToCategory_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Properties["SelsectedNote"] = ((Note)NotesList.Items[NotesList.SelectedIndex]).Id;
            CategoryList window1 = new CategoryList();
            window1.Show();
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window1 = new MainWindow();
            window1.Show();
            Close();
        }

        private void ButtonSync_OnClick(object sender, RoutedEventArgs e)
        {
            InitNotes();
            //RenderdMarkDownNote.Refresh();
        }


        private void FilterString_TextChanged(object sender, TextChangedEventArgs e)
        {
            NotesList.Items.Filter = delegate (object obj)
            {
                if (obj is Note)
                {
                    Note note = obj as Note;

                    return note.Name.ToLower().Contains(FilterString.Text.ToLower());
                }
                return false;
            };
        }

    }
}
