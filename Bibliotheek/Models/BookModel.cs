using Bibliotheek.Classes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotheek.Models
{
    public class BookModel
    {
        [Display(Name = "ISBN: ")]
        [Required(ErrorMessage = "ISBN is verplicht")]
        [DataType(DataType.Text)]
        public string ISBN { get; set; }

        [Display(Name = "Auteur: ")]
        [Required(ErrorMessage = "Auteur is verplicht")]
        [DataType(DataType.Text)]
        public string Author { get; set; }

        [Display(Name = "Titel: ")]
        [Required(ErrorMessage = "Titel is verplicht")]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Display(Name = "Uitgever: ")]
        [Required(ErrorMessage = "Uitgever is verplicht")]
        [DataType(DataType.Text)]
        public string Publisher { get; set; }

        public static List<String> GetRecentBooks()
        {
            var list = new List<String>();

            const string readStatement = "SELECT DISTINCT isbn.Naam, isbn.ISBN, auteurs.ID AS autorID, CONCAT(auteurs.Voornaam, ' ' ,auteurs.Achternaam) as AutorName FROM `boeken`LEFT JOIN isbn ON boeken.ISBN = isbn.ISBN LEFT JOIN auteurs ON isbn.Auteur = auteurs.ID ORDER BY boeken.DateAdded LIMIT 0 , 5";

            using (var empConnection = DatabaseConnection.DatabaseConnect())
            {
                using (var readCommand = new MySqlCommand(readStatement, empConnection))
                {
                    try
                    {
                        DatabaseConnection.DatabaseOpen(empConnection);
                        using (var myDataReader = readCommand.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (myDataReader.Read())
                            {
                                list.Add((myDataReader[0] == DBNull.Value ? "?" : myDataReader.GetString(0)));
                                list.Add((myDataReader[1] == DBNull.Value ? "?" : myDataReader.GetString(1)));
                                list.Add((myDataReader[2] == DBNull.Value ? "?" : myDataReader.GetString(2)));
                                list.Add((myDataReader[3] == DBNull.Value ? "?" : myDataReader.GetString(3)));
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        return list;
                    }
                    finally
                    {
                        DatabaseConnection.DatabaseClose(empConnection);
                    }
                }
            }
            return list;
        }
    }
}
