using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactRegister.Models
{
    public class ContactModel
    {
        
        public Guid Id
        {
            get;
            set;
        } = Guid.NewGuid();

        [Required(ErrorMessage = "Digite o nome!")]
        public string? Name
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Digite o e-mail!")]
        public string? Email
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Digite o número de telefone!")]
        public string? PhoneNumber
        {
            get;
            set;
        }

    }
}