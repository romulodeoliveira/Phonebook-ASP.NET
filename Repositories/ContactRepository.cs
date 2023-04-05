using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactRegister.Models;
using ContactRegister.Data;

namespace ContactRegister.Repositories
{
    public class ContactRepository : IContactRepository
    {

        // referencia em vídeo:
        // https://youtu.be/8pkGQKuW6Ss
        // a partir do minuto 30:00

        private readonly DataBaseContext _dataBaseContext;
        public ContactRepository(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }

        public List<ContactModel> FindAll()
        {
            return _dataBaseContext.Contacts.ToList();
        }

        public ContactModel ToAdd(ContactModel contact)
        {
            // aqui gravo no banco de dados
            _dataBaseContext.Contacts.Add(contact);
            _dataBaseContext.SaveChanges();
            return contact;
        }
    }
}
