using KarieraPlus.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace KarieraPlus
{
    /// <summary>
    /// Interaction logic for Login_Page.xaml
    /// </summary>
    public partial class Login_Page : Window
    {
        public Login_Page()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string emailPattern = @"^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$";

            Regex emailRegex = new Regex(emailPattern);

            if (emailRegex.IsMatch(txtEmail.Text))
            {
                Close();
            }
            else
            {
                MessageBox.Show("Nieprawidłowe dane.", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
