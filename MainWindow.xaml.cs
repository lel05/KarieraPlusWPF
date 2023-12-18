using KarieraPlus.Classes;
using Microsoft.Data.Sqlite;
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
using System.IO;

namespace KarieraPlus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int? userId = null;
        public MainWindow()
        {
            InitializeComponent();

            Database.InitializeDatabase();
            GenerateOffers();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Login_Page page = new Login_Page();
            page.ShowDialog();

            string email = page.txtEmail.Text;
            string password = page.txtPassword.Password;

            if (email.Length > 0 && password.Length > 0)
            {
                string dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "karieraplus.db");

                if (File.Exists(dbpath))
                {
                    using (var db = new SqliteConnection($"Filename={dbpath}"))
                    {
                        db.Open();

                        var checkCommand = new SqliteCommand();
                        checkCommand.Connection = db;
                        checkCommand.CommandText = "SELECT COUNT(*) FROM user WHERE email=@email";
                        checkCommand.Parameters.AddWithValue("@email", email);
                        int rowCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                        if (rowCount > 0)
                        {
                            string userPass = null;

                            string query = "SELECT password FROM user WHERE email=@email";
                            using (var command = new SqliteCommand(query, db))
                            {
                                command.Parameters.AddWithValue("@email", email);
                                object result = command.ExecuteScalar();

                                if (result != null)
                                {
                                    userPass = result.ToString();
                                }
                            }

                            if (userPass == password)
                            {
                                loginBtn.Content = "Mój profil";
                                loginBtn.Click -= Login_Click;
                                loginBtn.Click += myProfile;
                                logOutBtn.Visibility = Visibility.Visible;

                                string queryId = "SELECT user_id FROM user WHERE email=@email";
                                using (var command = new SqliteCommand(queryId, db))
                                {
                                    command.Parameters.AddWithValue("@email", email);
                                    int? result = Convert.ToInt32(command.ExecuteScalar());

                                    if (result != null)
                                    {
                                        userId = result;

                                        if (userId == 1)
                                        {
                                            addOffer.Visibility = Visibility.Visible;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void myProfile(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("profil");
        }

        private void logOutBtn_Click(object sender, RoutedEventArgs e)
        {
            loginBtn.Content = "Zaloguj się";
            loginBtn.Click -= myProfile;
            loginBtn.Click += Login_Click;
            userId = null;
            logOutBtn.Visibility = Visibility.Hidden;
            addOffer.Visibility = Visibility.Hidden;
        }

        private void addOffer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void registerBtn_Click(object sender, RoutedEventArgs e)
        {
            Register_Page page = new Register_Page();
            page.ShowDialog();

            string name = page.txtName.Text;
            string surname = page.txtSurname.Text;
            string email = page.txtEmail.Text;
            string password = page.txtPassword.Password;

            if (name.Length > 0 && surname.Length > 0 && email.Length > 0 && password.Length > 0)
            {
                string dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "karieraplus.db");

                if (File.Exists(dbpath))
                {
                    using (var db = new SqliteConnection($"Filename={dbpath}"))
                    {
                        db.Open();

                        var insertCommand = new SqliteCommand();
                        insertCommand.Connection = db;
                        insertCommand.CommandText = "INSERT INTO user (firstname,surname,email,password)" +
                                                    "VALUES (@name,@surname,@email,@password);";
                        insertCommand.Parameters.AddWithValue("@name", name);
                        insertCommand.Parameters.AddWithValue("@surname", surname);
                        insertCommand.Parameters.AddWithValue("@email", email);
                        insertCommand.Parameters.AddWithValue("@password", password);
                        insertCommand.ExecuteNonQuery();
                    }
                }
            }
        }

        private void GenerateOffers()
        {
            string dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "karieraplus.db");
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                string query = "SELECT * FROM offer";
                using (var command = new SqliteCommand(query, db))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Grid dynamicGrid = new Grid();

                            dynamicGrid.Width = 400;
                            dynamicGrid.Height = 200;
                            dynamicGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
                            dynamicGrid.VerticalAlignment = VerticalAlignment.Stretch;
                            dynamicGrid.Margin = new Thickness(30);
                            dynamicGrid.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#F8B500");

                            dynamicGrid.ColumnDefinitions.Add(new ColumnDefinition());
                            dynamicGrid.ColumnDefinitions.Add(new ColumnDefinition());
                            dynamicGrid.ColumnDefinitions.Add(new ColumnDefinition());
                            dynamicGrid.RowDefinitions.Add(new RowDefinition());
                            dynamicGrid.RowDefinitions.Add(new RowDefinition());
                            dynamicGrid.RowDefinitions.Add(new RowDefinition());
                            dynamicGrid.RowDefinitions.Add(new RowDefinition());
                            dynamicGrid.RowDefinitions.Add(new RowDefinition());
                            dynamicGrid.RowDefinitions.Add(new RowDefinition());

                            Label profession_name = new Label() { Content = reader["profession_name"] };
                            Grid.SetRow(profession_name, 0);
                            Grid.SetColumnSpan(profession_name, 2);
                            dynamicGrid.Children.Add(profession_name);

                            Label type_of_job = new Label() { Content = reader["type_of_job"] };
                            Grid.SetRow(type_of_job, 0);
                            Grid.SetColumn(type_of_job, 2);
                            dynamicGrid.Children.Add(type_of_job);

                            Label type_of_contract = new Label() { Content = reader["type_of_contract"] };
                            Grid.SetRow(type_of_contract, 1);
                            Grid.SetColumn(type_of_contract, 0);
                            dynamicGrid.Children.Add(type_of_contract);

                            Label salary = new Label() { Content = reader["salary"] };
                            Grid.SetRow(salary, 1);
                            Grid.SetColumn(salary, 1);
                            Grid.SetColumnSpan(salary, 2);
                            dynamicGrid.Children.Add(salary);

                            int companyId = Convert.ToInt32(reader["company_id"]);
                            (string companyLogoSource, string companyName) = GetCompanyInfo(db, companyId);

                            Image companyLogo = new Image() { Source = new BitmapImage(new Uri(companyLogoSource)) };
                            companyLogo.Height = 40;
                            Grid.SetRow(companyLogo, 2);
                            Grid.SetColumn(companyLogo, 0);
                            dynamicGrid.Children.Add(companyLogo);

                            Label company_name = new Label() { Content = companyName };
                            Grid.SetRow(company_name, 2);
                            Grid.SetColumn(company_name, 1);
                            Grid.SetColumnSpan(company_name, 2);
                            dynamicGrid.Children.Add(company_name);

                            TextBlock duties = new TextBlock() { Text = reader["duties"].ToString() };
                            duties.TextWrapping = TextWrapping.Wrap;
                            Grid.SetRow(duties, 3);
                            Grid.SetColumn(duties, 0);
                            Grid.SetRowSpan(duties, 2);
                            dynamicGrid.Children.Add(duties);

                            TextBlock requirements = new TextBlock() { Text = reader["requirements"].ToString() };
                            Grid.SetRow(requirements, 3);
                            Grid.SetColumn(requirements, 2);
                            Grid.SetRowSpan(requirements, 2);
                            dynamicGrid.Children.Add(requirements);

                            int categoryId = Convert.ToInt32(reader["company_id"]);
                            string categoryName = GetCategoryName(db, categoryId);
                            Label category = new Label() { Content = categoryName };
                            Grid.SetRow(category, 3);
                            Grid.SetColumn(category, 1);
                            dynamicGrid.Children.Add(category);

                            offersStackPanel.Children.Add(dynamicGrid);
                        }
                    }
                }

                db.Close();
            }
        }

        private (string CompanyLogoSource, string CompanyName) GetCompanyInfo(SqliteConnection db, int companyId)
        {
            string query = $"SELECT logo, name FROM company WHERE company_id = {companyId}";
            using (var command = new SqliteCommand(query, db))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string? relativePath = reader["logo"].ToString();
                        string? companyName = reader["name"].ToString();

                        string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"..", "..", "..", "Images");

                        string fullPath = Path.Combine(basePath, relativePath);

                        return (fullPath, companyName);
                    }
                }
            }

            return (string.Empty, string.Empty);
        }

        private string GetCategoryName(SqliteConnection db, int categoryId)
        {
            string query = $"SELECT * FROM category WHERE category_id = {categoryId}";
            using (var command = new SqliteCommand(query, db))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string? category = reader["name"].ToString();
                        return (category);
                    }
                }
            }

            return string.Empty;
        }
    }
}
