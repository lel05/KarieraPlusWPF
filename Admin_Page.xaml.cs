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

namespace KarieraPlus
{
    /// <summary>
    /// Interaction logic for Admin_Page.xaml
    /// </summary>
    public partial class Admin_Page : Window
    {
        public Admin_Page()
        {
            InitializeComponent();

            string dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "karieraplus.db");
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                string queryCompanies = "SELECT * FROM company";
                using (var command = new SqliteCommand(queryCompanies, db))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        int row = 0;

                        while (reader.Read())
                        {

                            companiesGrid.RowDefinitions.Add(new RowDefinition());

                            Label companyId = new Label() { Content = "Id: " + reader["company_id"] };
                            Grid.SetRow(companyId, row);
                            Grid.SetColumn(companyId, 0);
                            companiesGrid.Children.Add(companyId);

                            Label companyName = new Label() { Content = "Name: " + reader["name"] };
                            Grid.SetRow(companyName, row);
                            Grid.SetColumn(companyName, 1);
                            companiesGrid.Children.Add(companyName);

                            row++;
                        }
                    }
                }

                string queryCategories = "SELECT * FROM category";
                using (var command = new SqliteCommand(queryCategories, db))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        int row = 0;

                        while (reader.Read())
                        {

                            categoriesGrid.RowDefinitions.Add(new RowDefinition());

                            Label companyId = new Label() { Content = "Id: " + reader["category_id"] };
                            Grid.SetRow(companyId, row);
                            Grid.SetColumn(companyId, 0);
                            categoriesGrid.Children.Add(companyId);

                            Label companyName = new Label() { Content = "Name: " + reader["name"] };
                            Grid.SetRow(companyName, row);
                            Grid.SetColumn(companyName, 1);
                            categoriesGrid.Children.Add(companyName);

                            row++;
                        }
                    }
                }

                db.Close();
            }
        }

        private void AddOffer_Click(object sender, RoutedEventArgs e)
        {
            string companyId = companyTxt.Text;
            string profession_name = professionNameTxt.Text;
            string typeOfContract = typeOfContractTxt.Text;
            string typeOfJob = typeOfJobTxt.Text;
            string salary = salaryTxt.Text;
            string categoryId = categoryTxt.Text;
            string duties = dutiesTxt.Text;
            string requirements = requirementsTxt.Text;

            if (companyId.Length > 0 && profession_name.Length > 0 && typeOfContract.Length > 0 && typeOfJob.Length > 0 && salary.Length > 0 && categoryId.Length > 0 && duties.Length > 0 && requirements.Length > 0)
            {
                string dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "karieraplus.db");

                if (File.Exists(dbpath))
                {
                    using (var db = new SqliteConnection($"Filename={dbpath}"))
                    {
                        db.Open();

                        var insertCommand = new SqliteCommand();
                        insertCommand.Connection = db;
                        insertCommand.CommandText = "INSERT INTO offer (company_id, profession_name, type_of_contract, type_of_job, salary, category_id, duties, requirements)" +
                                                    "VALUES (@company_id, @profession_name, @type_of_contract, @type_of_job, @salary, @cateogry_id, @duties, @requirements);";
                        insertCommand.Parameters.AddWithValue("@company_id", companyId);
                        insertCommand.Parameters.AddWithValue("@profession_name", profession_name);
                        insertCommand.Parameters.AddWithValue("@type_of_contract", typeOfContract);
                        insertCommand.Parameters.AddWithValue("@type_of_job", typeOfJob);
                        insertCommand.Parameters.AddWithValue("@salary", salary);
                        insertCommand.Parameters.AddWithValue("@cateogry_id", categoryId);
                        insertCommand.Parameters.AddWithValue("@duties", duties);
                        insertCommand.Parameters.AddWithValue("@requirements", requirements);
                        insertCommand.ExecuteNonQuery();
                    }
                }
            }
            Close();
        }
    }
}
