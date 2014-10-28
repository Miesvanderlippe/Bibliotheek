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
    public class IssueModel
    {
        [Display(Name = "Uniek nummer: ")]
        [Required(ErrorMessage = "Uniek nummer is verplicht")]
        [DataType(DataType.Text)]
        public string Identifier { get; set; }
        
        public static List<String> BooksInPosession() {
            var list = new List<String>();
            var model = new UserModel();

            const string readStatement = "SELECT boeken.ID, ISBN.Naam, DATEDIFF( CURRENT_TIMESTAMP( ) , boeken.IssuedAt ) AS Difference "+
                                        "FROM `boeken` "+
                                        "LEFT JOIN isbn ON boeken.ISBN = isbn.ISBN " +
                                        "WHERE IssuedTo = ? "+
                                        "ORDER BY boeken.IssuedAt " +
                                        "LIMIT 0 , 10";

            using (var empConnection = DatabaseConnection.DatabaseConnect())
            {
                using (var readCommand = new MySqlCommand(readStatement, empConnection))
                {
                    try
                    {
                        var userID = model.CurrentUserID();
                        // Bind parameters 
                        readCommand.Parameters.Add("IssuedTo", MySqlDbType.VarChar).Value = userID;

                        DatabaseConnection.DatabaseOpen(empConnection);
                        using (var myDataReader = readCommand.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (myDataReader.Read())
                            {
                                list.Add((myDataReader[0] == DBNull.Value ? "?" : myDataReader.GetString(0)));
                                list.Add((myDataReader[1] == DBNull.Value ? "?" : myDataReader.GetString(1)));
                                list.Add((myDataReader[2] == DBNull.Value ? "?" : myDataReader.GetString(2)));
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

        public bool Issue() {
            var model = new UserModel();
            const string insertStatement = "UPDATE boeken " +
                                            "SET " +
                                            "IssuedAt = ? ," +
                                            "IssuedTo = ? " +
                                            "WHERE ID = ? ";
            using (var empConnection = DatabaseConnection.DatabaseConnect())
            {
                using (var insertCommand = new MySqlCommand(insertStatement, empConnection))
                {
                    var name = model.CurrentUserID();
                    // Bind parameters 
                    insertCommand.Parameters.Add("IssuedAt", MySqlDbType.VarChar).Value = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"); ;
                    insertCommand.Parameters.Add("IssuedTo", MySqlDbType.VarChar).Value = name;
                    insertCommand.Parameters.Add("ID", MySqlDbType.VarChar).Value = Identifier;

                    try
                    {
                        DatabaseConnection.DatabaseOpen(empConnection);
                        // Execute command 
                        var test = insertCommand.ToString();
                        insertCommand.ExecuteNonQuery();
                        return true;
                    }
                    catch (MySqlException ex)
                    {
                        // MySqlException bail out
                        return false;
                    }
                    finally
                    {
                        // Make sure to close the connection 
                        DatabaseConnection.DatabaseClose(empConnection);
                    }
                }
            }
        }

        public bool Return()
        {
            const string insertStatement = "UPDATE boeken " +
                                            "SET " +
                                            "IssuedAt = ? ," +
                                            "IssuedTo = 0 " +
                                            "WHERE ID = ? ";
            using (var empConnection = DatabaseConnection.DatabaseConnect())
            {
                using (var insertCommand = new MySqlCommand(insertStatement, empConnection))
                {
                    // Bind parameters 
                    insertCommand.Parameters.Add("IssuedAt", MySqlDbType.VarChar).Value = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"); ;
                    insertCommand.Parameters.Add("ID", MySqlDbType.VarChar).Value = Identifier;

                    try
                    {
                        DatabaseConnection.DatabaseOpen(empConnection);
                        // Execute command 
                        var test = insertCommand.ToString();
                        insertCommand.ExecuteNonQuery();
                        return true;
                    }
                    catch (MySqlException ex)
                    {
                        // MySqlException bail out
                        return false;
                    }
                    finally
                    {
                        // Make sure to close the connection 
                        DatabaseConnection.DatabaseClose(empConnection);
                    }
                }
            }
        }
    }
}
