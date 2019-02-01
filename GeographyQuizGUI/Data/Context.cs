using GeographyQuizGUI.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeographyQuizGUI.Data
{
    /*
     * The Context Class holds relevant data from local disk in memory and
     * manages all connections to the database.
     */
    public class Context
    {
        public List<Country> countries { get; set; }
        public List<Highscore> highscores { get; set; }

        public Context() {
            countries = new List<Country>();
            highscores = new List<Highscore>();

            ReadFromDatabase();
        }

        /*
         * Persist highscore changes to the local database file.
         */
        public void SaveToDatabase() {
            string ifconsole = @"..\..\assets\mydb.db";
            // string ifgui = @"assets\mydb.db";
            string dbPath = Path.Combine(Environment.CurrentDirectory, ifconsole);
            string connString = string.Format("Data Source={0}", dbPath);

            try {
                using (SQLiteConnection conn = new SQLiteConnection(connString))
                {
                    string queryString = "DELETE FROM highscores";

                    using (SQLiteCommand cmd = new SQLiteCommand(queryString, conn))
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }

                    foreach (var highscore in highscores) {
                        string insertQuery = $"INSERT INTO highscores VALUES ({highscore.Id}, '{highscore.PlayerName}', {highscore.Score})";

                        using (SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, conn)) {
                        insertCommand.ExecuteNonQuery();
                        }
                    }
                }
            } catch (SQLiteException e) {
                throw new SQLiteException($"Error writing to database file.\n{e.StackTrace}");
            }        
        }

        /*
         * Load and populate the highscore and country lists with data from the
         * local database file.
         */
        private void ReadFromDatabase() {
            string ifconsole = @"..\..\assets\mydb.db";
            // string ifgui = @"assets\mydb.db";
            string dbPath = Path.Combine(Environment.CurrentDirectory, ifconsole);
            string connString = string.Format("Data Source={0}", dbPath);
            try {
                using (SQLiteConnection conn = new SQLiteConnection(connString))
                {
                    string queryString = "SELECT * FROM countries";

                    using (SQLiteCommand cmd = new SQLiteCommand(queryString, conn))
                    {
                        conn.Open();

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                            Int32 id = (Int32) dr["id"];
                            string name = dr.GetString(1);
                            string alpha_2 = dr.GetString(2);
                            string alpha_3 = dr.GetString(3);
                            string capital = dr.GetString(4);
                            countries.Add(new Country(id, name, alpha_2, alpha_3, capital));
                            }
                        }
                    }
                } 

                using (SQLiteConnection conn = new SQLiteConnection(connString))
                {
                    string queryString = "SELECT * FROM highscores";

                    using (SQLiteCommand cmd = new SQLiteCommand(queryString, conn))
                    {
                        conn.Open();

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                            Int32 id = (Int32) dr["id"];
                            string name = dr.GetString(1);
                            Int32 score = (Int32) dr["score"];
                            highscores.Add(new Highscore(id, name, score));
                            }
                        }
                    }
                }
            } catch (SQLiteException e) {
                throw new SQLiteException($"Error reading from database file.\n{e.StackTrace}");
            }
        }
    }
}
