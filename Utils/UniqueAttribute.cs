using System.ComponentModel.DataAnnotations;
using ContactRegister.Data;

namespace Project01.Utils
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public sealed class UniqueAttribute : ValidationAttribute
    {

        /**
        Aqui, a classe UniqueAttribute é definida. 
        Essa classe herda da classe ValidationAttribute, que é uma classe base para a criação de atributos de validação customizados. 
        A classe é decorada com o atributo AttributeUsage, que define onde o atributo pode ser aplicado (nesse caso, somente em propriedades).
        */

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            /**
            Aqui, o método IsValid é sobrescrito para implementar a validação customizada.
            Esse método recebe dois parâmetros: o valor a ser validado (value) e o contexto de validação (validationContext).
            O contexto de validação contém informações sobre o objeto a ser validado, como o tipo e os metadados.
            */

            var dbContext = (DataBaseContext)validationContext.GetService(typeof(DataBaseContext));

            if (dbContext.Users.Any(u => u.UserName == (string)value))
            {
                return new ValidationResult(ErrorMessage ?? "Este nome de usuário já está em uso.");
            }

            /**
            No corpo do método, é feito o cast do contexto de validação para o tipo do contexto do banco de dados (DataBaseContext).
            Em seguida, é verificado se já existe um usuário com o mesmo nome de usuário (UserName) no banco de dados. 
            Se existir, é retornado um objeto ValidationResult com a mensagem de erro definida no atributo ou uma mensagem padrão.
            */

            if (dbContext.Users.Any(u => u.Email == (string)value))
            {
                return new ValidationResult(ErrorMessage ?? "Este e-mail já está em uso.");
            }

            return ValidationResult.Success;

            // Se não houver erros, é retornado um objeto ValidationResult com sucesso.

        }
    }
}