using Phonebook.Models;
using Phonebook.Data;

namespace Phonebook.Repositories
{
    public class UserRepository : IUserRepository
    {

        // referencia em vídeo:
        // https://youtu.be/8pkGQKuW6Ss
        // a partir do minuto 30:00

        private readonly DataBaseContext _dataBaseContext;
        public UserRepository(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }

        public UserModel ListById(Guid id)
        {
            return _dataBaseContext.Users.FirstOrDefault(x => x.Id == id);
        }

        public List<UserModel> FindAll()
        {
            return _dataBaseContext.Users.ToList();
        }

        public UserModel ToAdd(UserModel user)
        {
            // aqui gravo no banco de dados
            _dataBaseContext.Users.Add(user);
            _dataBaseContext.SaveChanges();
            return user;
        }

        public UserModel ToUpdate(UserModel user)
        {
            UserModel userDB = ListById(user.Id);

            if (userDB == null)
            {
                throw new System.Exception("Houve um erro na atualização do usuário!");
            }

            userDB.FirstName = user.FirstName;
            userDB.MiddleName = user.MiddleName;
            userDB.LastName = user.LastName;
            userDB.UserName = user.UserName;
            userDB.Email = user.Email;
            userDB.Password = user.Password;
            userDB.ProfilePicture = user.ProfilePicture;
            userDB.UpdatedAt = DateTime.Now;

            _dataBaseContext.Users.Update(userDB);
            _dataBaseContext.SaveChanges();

            return userDB;
        }

        public bool ToDelete(Guid id)
        {
            UserModel userDB = ListById(id);

            if (userDB == null) throw new System.Exception("Houve um erro na exclusão do contato!");

            _dataBaseContext.Users.Remove(userDB);
            _dataBaseContext.SaveChanges();

            return true;
        }
    }
}
