
using Bibliotheek.Classes;
using MySql.Data.MySqlClient;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Security;
namespace Bibliotheek.Models
{
    public class UserModel
    {
        //Values
        [Display(Name = "Email: ")]
        [Required(ErrorMessage = "Email is verplicht")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Wachtwoord is verplicht")]
        [Display(Name = "Wachtwoord:")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        public static bool CurrentUserLoggedIn
        {
            get
            {
                return HttpContext.Current.Request.IsAuthenticated;
            }
        }

        public static bool IsAdmin {
            get
            {
                if (!HttpContext.Current.Request.IsAuthenticated) return false;
                var user = HttpContext.Current.User.Identity as FormsIdentity;
                var ticket = user.Ticket;
                return ticket.UserData.Split('|')[1] == "1";
            }
        }

        public string CurrentUser() {
            var user = HttpContext.Current.User.Identity as FormsIdentity;
            var ticket = user.Ticket;
            var id = ticket.UserData.Split('|')[0];
            return id.ToString();
        }

        public bool AddAccount()
        {
            // Run model through sql prevention and save them to vars 
            var mail     = SqlInjection.SafeSqlLiteral(StringManipulation.ToLowerFast(Email));
            var salt     = Crypt.GetRandomSalt();

            // Validate email using regex since HTML5 validation doesn't handle some cases 
            if (!ValidateEmail.IsValidEmail(mail)) return false;

            // MySQL query 
            const string countStatement = "SELECT COUNT(*) " +
                                          "FROM gebruikers " +
                                          "WHERE Email = ?";

            using (var empConnection = DatabaseConnection.DatabaseConnect())
            {
                int count;
                using (var countCommand = new MySqlCommand(countStatement, empConnection))
                {
                    // Bind parameters 
                    countCommand.Parameters.Add("Email", MySqlDbType.VarChar).Value = mail;
                    try
                    {
                        DatabaseConnection.DatabaseOpen(empConnection);
                        // Execute command 
                        count = Convert.ToInt32(countCommand.ExecuteScalar());
                    }
                    catch (MySqlException)
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

                if (count > 0)
                {
                    // Email already in the database bail out 
                    return false;
                }

                // Insert user in the database 
                const string insertStatement = "INSERT INTO gebruikers " +
                                               "(Email, Password, Salt) " +
                                               "VALUES (?, ?, ?)";

                using (var insertCommand = new MySqlCommand(insertStatement, empConnection))
                {
                    // Bind parameters 
                    insertCommand.Parameters.Add("Email", MySqlDbType.VarChar).Value = mail;
                    insertCommand.Parameters.Add("Password", MySqlDbType.VarChar).Value = Crypt.HashPassword(Password, salt);
                    insertCommand.Parameters.Add("Salt", MySqlDbType.VarChar).Value = salt;

                    try
                    {
                        DatabaseConnection.DatabaseOpen(empConnection);
                        // Execute command 
                        insertCommand.ExecuteNonQuery();
                        return true;
                    }
                    catch (MySqlException)
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
