using Bibliotheek.Classes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
                    var name = model.CurrentUser();
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
    }
}
