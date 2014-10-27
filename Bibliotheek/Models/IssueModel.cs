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

        public bool Issue() {

            return true;
        }
    }
}
