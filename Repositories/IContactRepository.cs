using Phonebook.Models;

namespace Phonebook.Repositories
{
    public interface IContactRepository
    {
        ContactModel ListById(Guid id);
        List<ContactModel> FindAll();
        ContactModel ToAdd(ContactModel contact);
        ContactModel ToUpdate(ContactModel contact);
        bool ToDelete(Guid id);
    }
}