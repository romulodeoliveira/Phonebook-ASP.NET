using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project01.Enums;
using System.ComponentModel.DataAnnotations;
using Project01.Utils;

namespace ContactRegister.Models
{
    public class UserModel
    {
        public ProfileEnum Profile { get; set; } = ProfileEnum.MEM;

        public Guid Id { get; set; } = Guid.NewGuid();

        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public string? ProfilePicturePath { get; set; }

        [Required(ErrorMessage = "Digite o seu nome de usuário!")]
        [Unique(ErrorMessage = "Este nome de usuário já está em uso.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Digite o e-mail!")]
        [EmailAddress(ErrorMessage = "O e-mail informado é inválido!")]
        [Unique(ErrorMessage = "Este e-mail já está em uso.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite a senha!")]
        public string Password { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public UserModel()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}