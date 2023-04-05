using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactRegister.Models;

namespace ContactRegister.Repositories
{
    public interface IContactRepository
    {
        ContactModel ToAdd(ContactModel contact);
    }
}