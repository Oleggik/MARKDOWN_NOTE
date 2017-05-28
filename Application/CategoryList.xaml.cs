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
using Microsoft.VisualBasic;

namespace MarkdownNotes
{
    /// <summary>
    /// Логика взаимодействия для CategoryList.xaml
    /// </summary>
    public partial class CategoryList : Window
    {
        public CategoryList()
        {
            InitializeComponent();
            InitCategory();
        }

        private void InitCategory()
        {
            Resources["Category"] = CategoryDL.GetInstance.GetCategoryList(Application.Current.Properties["UserName"].ToString());
            if (!CategoriesList.Items.IsEmpty)
            {
                CategoriesList.SelectedIndex = 0;
            }
        }

        private void SetCategory_OnClick(object sender, RoutedEventArgs e)
        {
            if (CategoriesList.SelectedIndex != -1)
                CategoryDL.GetInstance.SetCategory((int)Application.Current.Properties["SelsectedNote"], ((Category)CategoriesList.Items[CategoriesList.SelectedIndex]).Id);
            this.Close();
            MessageBox.Show("Note successfully moved to category");
        }

        private void CategoryRename_Click(object sender, RoutedEventArgs e)
        {
            if (CategoriesList.SelectedIndex == -1)
            {
                return;
            }

            string newName;

            Names name = new Names();
            name.ShowDialog();
            newName = name.NewName.Text;
            if (newName == "")
                return;

            //string newName = Interaction.InputBox("NewName");
            //    if (newName == "") return;

            //(CategoriesList.SelectedItems[0] as Category).Name = newName;
            //CategoriesList.Items.Refresh();

            CategoryDL.GetInstance.RenameCategory((CategoriesList.SelectedItems[0] as Category).Id, newName);
            InitCategory();
        }

        private void CategoryDelete_Click(object sender, RoutedEventArgs e)
        {
            if (CategoriesList.SelectedIndex == -1)
            {
                return;
            }
            CategoryDL.GetInstance.DelCategory(((Category)CategoriesList.Items[CategoriesList.SelectedIndex]).Id);
            InitCategory();
        }
    }
}
