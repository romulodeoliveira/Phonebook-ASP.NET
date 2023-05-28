using Phonebook.Models;

namespace Phonebook.Repositories
{
    public interface IUserRepository
    {
        UserModel ListById(Guid id);
        List<UserModel> FindAll();
        UserModel ToAdd(UserModel user);
        UserModel ToUpdate(UserModel user);
        bool ToDelete(Guid id);
    }
}