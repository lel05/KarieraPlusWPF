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

namespace KarieraPlus
{
    /// <summary>
    /// Interaction logic for Register_Page.xaml
    /// </summary>
    public partial class Register_Page : Window
    {
        public Register_Page()
        {
            InitializeComponent();
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            string emailPattern = @"^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$";
            string firstLetterPattern = @"^[A-ZŚŻŹĆĄĘŁÓŃ][a-zśżźćąęłóń]*$";

            Regex emailRegex = new Regex(emailPattern);
            Regex firstLetterRegex = new Regex(firstLetterPattern);

            if (emailRegex.IsMatch(txtEmail.Text) && firstLetterRegex.IsMatch(txtName.Text) && firstLetterRegex.IsMatch(txtSurname.Text))
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
