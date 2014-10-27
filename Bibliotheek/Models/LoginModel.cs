using Bibliotheek.Classes;
using MySql.Data.MySqlClient;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Bibliotheek.Models
{
    public class LoginModel
    {
        [Display(Name = "Email: ")]
        [Required(ErrorMessage = "Email is verplicht")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Wachtwoord is verplicht")]
        [Display(Name = "Wachtwoord:")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        private int Admin { get; set; }

        // login functie
        public bool Login() {

            var email = StringManipulation.ToLowerFast(Email);
            var password = Password;
            var savedPassword = String.Empty;
            var savedSalt = String.Empty;
            var savedId = String.Empty;
            var Admin = 0;

            const string comando = "SELECT * FROM gebruikers WHERE Email=?";
            
            using (var empConnection = DatabaseConnection.DatabaseConnect())
            {
                using (var result = new MySqlCommand(comando, empConnection))
                {
                    result.Parameters.Add("Email", MySqlDbType.VarChar).Value = email;
                    try
                    {
                        DatabaseConnection.DatabaseOpen(empConnection);
                        using (var myDataReader = result.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (myDataReader.Read())
                            {
                                savedId          = myDataReader.GetValue(0).ToString();
                                savedPassword    = myDataReader.GetString(2);
                                savedSalt        = myDataReader.GetString(3);
                                Admin            = Convert.ToInt16(myDataReader.GetValue(4));
                            }
                        }

                        if (Crypt.ValidatePassword(password, savedPassword, savedSalt))
                        {
                            Cookies.MakeCookie(email, savedId, Admin.ToString(CultureInfo.InvariantCulture));
                            return true;
                        }
                    }
                    catch
                    {
                        //Geen gebruiker gevonden
                        return false;
                    }
                    finally {
                        empConnection.Close();
                    }
                }
            }

            return false;
        }

    }
}
