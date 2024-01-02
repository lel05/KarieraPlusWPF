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
using System.Globalization;

namespace KarieraPlus
{
    /// <summary>
    /// Interaction logic for User_Page.xaml
    /// </summary>
    public partial class User_Page : Window
    {
        public User_Page()
        {
            InitializeComponent();

            int? userId = MainWindow.userId;

            string dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "karieraplus.db");
            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                string query = $"SELECT * FROM user WHERE user_id={userId}";
                using (var command = new SqliteCommand(query, db))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            firstNameTxt.Text = reader["firstname"].ToString();
                            surnameTxt.Text = reader["surname"].ToString();
                            if (!reader.IsDBNull(reader.GetOrdinal("birth_date")))
                            {
                                DateTime birthdate = reader.GetDateTime(reader.GetOrdinal("birth_date"));
                                string formattedDate = birthdate.ToString("yyyy-MM-dd");
                                birthDateDatePicker.SelectedDate = DateTime.ParseExact(formattedDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                            }
                            emailTxt.Text = reader["email"].ToString();
                            phoneNumberlTxt.Text = reader["phone_number"].ToString();
                            addressTxt.Text = reader["address"].ToString();
                            professionTxt.Text = reader["profession"].ToString();
                            professionDescriptionTxt.Text = reader["profession_description"].ToString();
                            experienceTxt.Text = reader["experience"].ToString();
                            jobExperienceTxt.Text = reader["job_experience"].ToString();
                            educationTxt.Text = reader["education"].ToString();
                            languagesTxt.Text = reader["languages"].ToString();
                            skillsTxt.Text = reader["skills"].ToString();
                            coursesTxt.Text = reader["courses"].ToString();
                            linksTxt.Text = reader["links"].ToString();
                            passwordPasswordBox.Password = reader["password"].ToString();
                        }
                    }
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            int? userId = MainWindow.userId;

            string firstname = firstNameTxt.Text;
            string surname = surnameTxt.Text;
            DateTime? birthDate = birthDateDatePicker.SelectedDate;
            string email = emailTxt.Text;
            string phonenumber = phoneNumberlTxt.Text;
            string address = addressTxt.Text;
            string profession = professionTxt.Text;
            string professionDescription = professionDescriptionTxt.Text;
            string experience = experienceTxt.Text;
            string jobExperience = experienceTxt.Text;
            string education = educationTxt.Text;
            string languages = languagesTxt.Text;
            string skills = skillsTxt.Text;
            string courses = coursesTxt.Text;
            string links = linksTxt.Text;
            string password = passwordPasswordBox.Password;

            if (firstname.Length > 0 && surname.Length > 0 && email.Length > 0 && phonenumber.Length > 0 && address.Length > 0 && profession.Length > 0 && professionDescription.Length > 0
                 && experience.Length > 0 && jobExperience.Length > 0 && education.Length > 0 && languages.Length > 0 && skills.Length > 0 && courses.Length > 0
                  && links.Length > 0 && password.Length > 0)
            {
                string dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "karieraplus.db");

                if (File.Exists(dbpath))
                {
                    using (var db = new SqliteConnection($"Filename={dbpath}"))
                    {
                        db.Open();

                        var updateCommand = new SqliteCommand();
                        updateCommand.Connection = db;
                        updateCommand.CommandText = "UPDATE user SET firstname = @firstname, surname = @surname, birth_date = @birthdate, email = @email, phone_number = @phone_number, address = @address, " +
                            "profession = @profession, profession_description = @profession_description, experience = @experience, job_experience = @job_experience, " +
                            "education = @education, languages = @languages, skills = @skills, courses = @courses, links = @links, password = @password " +
                            "WHERE user_id = @user_id;";
                        updateCommand.Parameters.AddWithValue("@firstname", firstname);
                        updateCommand.Parameters.AddWithValue("@surname", surname);
                        updateCommand.Parameters.AddWithValue("@birthdate", birthDate);
                        updateCommand.Parameters.AddWithValue("@email", email);
                        updateCommand.Parameters.AddWithValue("@phone_number", phonenumber);
                        updateCommand.Parameters.AddWithValue("@address", address);
                        updateCommand.Parameters.AddWithValue("@profession", profession);
                        updateCommand.Parameters.AddWithValue("@profession_description", professionDescription);
                        updateCommand.Parameters.AddWithValue("@experience", experience);
                        updateCommand.Parameters.AddWithValue("@job_experience", jobExperience);
                        updateCommand.Parameters.AddWithValue("@education", education);
                        updateCommand.Parameters.AddWithValue("@languages", languages);
                        updateCommand.Parameters.AddWithValue("@skills", skills);
                        updateCommand.Parameters.AddWithValue("@courses", courses);
                        updateCommand.Parameters.AddWithValue("@links", links);
                        updateCommand.Parameters.AddWithValue("@password", password);
                        updateCommand.Parameters.AddWithValue("@user_id", userId);

                        updateCommand.ExecuteNonQuery();
                    }
                }
            }
            Close();
        }
    }
}
