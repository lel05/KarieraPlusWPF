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
                                                    "`category_id` PRIMARY KEY AUTOINCREMENT, " +
                                                    "`name` varchar(50));";
                    categoryTableCommand.ExecuteReader();
                    db.Close();

                    db.Open();
                    var companyTableCommand = new SqliteCommand();
                    companyTableCommand.Connection = db;
                    companyTableCommand.CommandText = "CREATE TABLE `company` (" +
                                                    "`company_id` PRIMARY KEY AUTOINCREMENT, " +
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
                                                    "`offer_id` PRIMARY KEY AUTOINCREMENT," +
                                                    "`company_id` int(10)," +
                                                    "`profession_name` varchar(40)," +
                                                    "`profession_level` varchar(40)," +
                                                    "`type_of_contract` varchar(50)," +
                                                    "`employment` varchar(50)," +
                                                    "`type_of_job` varchar(30)," +
                                                    "`salary` text," +
                                                    "`days` text," +
                                                    "`hours` text," +
                                                    "`expired` date," +
                                                    "`category_id` int(10)," +
                                                    "`duties` text," +
                                                    "`requirements` text," +
                                                    "`benefits` int(11)," +
                                                    "`application_count` int(10));";
                    offerTableCommand.ExecuteReader();
                    db.Close();

                    db.Open();
                    var userTableCommand = new SqliteCommand();
                    userTableCommand.Connection = db;
                    userTableCommand.CommandText = "CREATE TABLE `user` (" +
                                                    "`user_id` PRIMARY KEY AUTOINCREMENT," +
                                                    "`firstname` varchar(30) NOT NULL," +
                                                    "`surname` varchar(30) NOT NULL," +
                                                    "`birth_date` date," +
                                                    "`email` varchar(50) NOT NULL," +
                                                    "`number` int(10)," +
                                                    "`avatar` text," +
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
                }
            }
        }
    }
}
