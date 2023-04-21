using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactRegister.Models;

namespace ContactRegister.Repositories
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