using Microsoft.Data.Sqlite;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows;
using System.Xml.Linq;

namespace KarieraPlus.Classes
{
    public class Database
    {

        public static void InitializeDatabase()
        {
            string dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "karieraplus.db");

            if (!File.Exists(dbpath))
            {
                using (var db = new SqliteConnection($"Filename={dbpath}"))
                {
                    raw.SetProvider(new SQLite3Provider_e_sqlite3());

                    db.Open();

                    var categoryTableCommand = new SqliteCommand();
                    categoryTableCommand.Connection = db;
                    categoryTableCommand.CommandText = "CREATE TABLE `category` (" +
                                                        "`category_id` INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                                        "`name` varchar(50));";
                    categoryTableCommand.ExecuteReader();
                    db.Close();

                    db.Open();
                    var companyTableCommand = new SqliteCommand();
                    companyTableCommand.Connection = db;
                    companyTableCommand.CommandText = "CREATE TABLE `company` (" +
                                                        "`company_id` INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                                        "`name` varchar(50)," +
                                                        "`localization` text," +
                                                        "`logo` text," +
                                                        "`description` text);";
                    companyTableCommand.ExecuteReader();
                    db.Close();

                    db.Open();
                    var offerTableCommand = new SqliteCommand();
                    offerTableCommand.Connection = db;
                    offerTableCommand.CommandText = "CREATE TABLE `offer` (" +
                                                    "`offer_id` INTEGER PRIMARY KEY AUTOINCREMENT," +
                                                    "`company_id` int(10)," +
                                                    "`profession_name` varchar(40)," +
                                                    "`type_of_contract` varchar(50)," +
                                                    "`type_of_job` varchar(30)," +
                                                    "`salary` text," +
                                                    "`category_id` int(10)," +
                                                    "`duties` text," +
                                                    "`requirements` text);";
                    offerTableCommand.ExecuteReader();
                    db.Close();

                    db.Open();
                    var userTableCommand = new SqliteCommand();
                    userTableCommand.Connection = db;
                    userTableCommand.CommandText = "CREATE TABLE `user` (" +
                                                    "`user_id` INTEGER PRIMARY KEY AUTOINCREMENT," +
                                                    "`firstname` varchar(30) NOT NULL," +
                                                    "`surname` varchar(30) NOT NULL," +
                                                    "`birth_date` date," +
                                                    "`email` varchar(50) NOT NULL," +
                                                    "`phone_number` int(10)," +
                                                    "`address` varchar(40)," +
                                                    "`profession` varchar(40)," +
                                                    "`profession_description` text," +
                                                    "`experience` text," +
                                                    "`job_experience` text," +
                                                    "`education` text," +
                                                    "`languages` text," +
                                                    "`skills` text," +
                                                    "`courses` text," +
                                                    "`links` text," +
                                                    "`password` text NOT NULL);";
                    userTableCommand.ExecuteReader();
                    db.Close();

                    db.Open();
                    var offersCalimedTableCommand = new SqliteCommand();
                    offersCalimedTableCommand.Connection = db;
                    offersCalimedTableCommand.CommandText = "CREATE TABLE `offers_claimed` (" +
                                                            "`offer_id` INTEGER, " +
                                                            "`user_id` INTEGER);";
                    offersCalimedTableCommand.ExecuteReader();
                    db.Close();

                    db.Open();
                    var insertCommand = new SqliteCommand();
                    insertCommand.Connection = db;
                    insertCommand.CommandText = "INSERT INTO user (firstname,surname,email,password)" +
                                                "VALUES (@name,@surname,@email,@password);";
                    insertCommand.Parameters.AddWithValue("@name", "admin");
                    insertCommand.Parameters.AddWithValue("@surname", "admin");
                    insertCommand.Parameters.AddWithValue("@email", "admin@admin.admin");
                    insertCommand.Parameters.AddWithValue("@password", "admin");
                    insertCommand.ExecuteNonQuery();
                    db.Close();

                    db.Open();
                    var insertCompanyCommand = new SqliteCommand();
                    insertCompanyCommand.Connection = db;
                    insertCompanyCommand.CommandText = "INSERT INTO company (name, localization, logo, description)" +
                                                "VALUES (@name,@localization,@logo,@description);";
                    insertCompanyCommand.Parameters.AddWithValue("@name", "Idel");
                    insertCompanyCommand.Parameters.AddWithValue("@localization", "Limanowa");
                    insertCompanyCommand.Parameters.AddWithValue("@logo", "Idel_logo.png");
                    insertCompanyCommand.Parameters.AddWithValue("@description", "firma IT");
                    insertCompanyCommand.ExecuteNonQuery();
                    db.Close();

                    db.Open();
                    var insertCategoryCommand = new SqliteCommand();
                    insertCategoryCommand.Connection = db;
                    insertCategoryCommand.CommandText = "INSERT INTO category (name)" +
                                                "VALUES (@name);";
                    insertCategoryCommand.Parameters.AddWithValue("@name", "IT");
                    insertCategoryCommand.ExecuteNonQuery();
                    db.Close();

                }
            }
        }
    }
}
