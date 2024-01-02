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
using System.IO;
using System.Xml.Linq;
using System.Security.Cryptography.X509Certificates;

namespace KarieraPlus
{
    /// <summary>
    /// Interaction logic for Offer_Page.xaml
    /// </summary>
    public partial class Offer_Page : Window
    {
        private int offerId;
        int? userId = MainWindow.userId;

        public Offer_Page(int offerId)
        {
            InitializeComponent();

            if(userId != null)
            {
                offerPageButton.IsEnabled = true;
            }

            this.offerId = offerId;

            string dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "karieraplus.db");
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                string query = $"SELECT * FROM offer WHERE offer_id={offerId}";
                using (var command = new SqliteCommand(query, db))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            professionNameLabel.Content = "Nazwa zawodu: " + reader["profession_name"];
                            typeOfJobLabel.Content = "Typ pracy: " + reader["type_of_job"];

                            typeOfContractLabel.Content = "Typ contraktu: " + reader["type_of_contract"];

                            salaryLabel.Content = "Wynagrodzenie: " + reader["salary"];

                            int companyId = Convert.ToInt32(reader["company_id"]);
                            (string companyLogoSource, string companyName) = GetCompanyInfo(db, companyId);

                            companyLogoImage.Source = new BitmapImage(new Uri(companyLogoSource));

                            companyLabel.Content = companyName;

                            dutiesTextBlock.Text = "Obowiązki: " + reader["duties"].ToString();
                            
                            requirementsTextBlock.Text = "Wymagania: " + reader["requirements"].ToString();

                            int categoryId = Convert.ToInt32(reader["company_id"]);
                            string categoryName = GetCategoryName(db, categoryId);
                            categoryLabel.Content = "Kategoria: " + categoryName;

                        }
                    }
                }
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

                        string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Images");

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

        private void Offer_Click(object sender, RoutedEventArgs e)
        {
            string dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "karieraplus.db");

            if (File.Exists(dbpath))
            {
                using (var db = new SqliteConnection($"Filename={dbpath}"))
                {
                    db.Open();

                    var insertCommand = new SqliteCommand();
                    insertCommand.Connection = db;
                    insertCommand.CommandText = "INSERT INTO offers_claimed (offer_id, user_id)" +
                                                    "VALUES (@offerId, @userId);";
                    insertCommand.Parameters.AddWithValue("@offerId", offerId);
                    insertCommand.Parameters.AddWithValue("@userId", userId);
                    insertCommand.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Zgłosiłeś się pomyślnie", "Zgłoszenie", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }
    }
}
