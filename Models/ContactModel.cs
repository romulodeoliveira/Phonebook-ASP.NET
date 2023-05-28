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

        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Digite o nome!")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Digite o e-mail!")]
        [EmailAddress(ErrorMessage = "O e-mail informado é inválido!")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Digite o número de telefone!")]
        [Phone(ErrorMessage = "O número de telefone informado não é válido!")]
        public string? PhoneNumber { get; set; }

        public string? ImagePath { get; set; }

    }
}