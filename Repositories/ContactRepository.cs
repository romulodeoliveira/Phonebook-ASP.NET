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

        public ContactModel ListById(Guid id)
        {
            return _dataBaseContext.Contacts.FirstOrDefault(x => x.Id == id);
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

        public ContactModel ToUpdate(ContactModel contact)
        {
            ContactModel contactDB = ListById(contact.Id);

            if(contactDB == null) throw new System.Exception("Houve um erro na atualização do contato!");

            contactDB.Name = contact.Name;
            contactDB.Email = contact.Email;
            contactDB.PhoneNumber = contact.PhoneNumber;

            _dataBaseContext.Contacts.Update(contactDB);
            _dataBaseContext.SaveChanges();

            return contactDB;
        }

        public bool ToDelete(Guid id)
        {
            ContactModel contactDB = ListById(id);

            if(contactDB == null) throw new System.Exception("Houve um erro na exclusão do contato!");

            _dataBaseContext.Contacts.Remove(contactDB);
            _dataBaseContext.SaveChanges();

            return true;
        }
    }
}
