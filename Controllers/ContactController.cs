using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

using ContactRegister.Models;
using ContactRegister.Repositories;

namespace ContactRegister.Controllers
{
    [Route("[controller]")]
    public class ContactController : Controller
    {
        private readonly IWebHostEnvironment _env;

        // Injeção de Dependencia
        private readonly IContactRepository _contactRepository;

        public ContactController(IContactRepository contactRepository, IWebHostEnvironment env)
        {
            _contactRepository = contactRepository;
            _env = env;
        }

        // Index

        [HttpGet("Contact")]
        public IActionResult Index()
        {
            // Crio uma variável contacts e atribuo a ela o método que criei no meu repositório
            // Depois retorno a variável criada na minha view
            List<ContactModel> contacts = _contactRepository.FindAll();
            List<ContactModel> sortedContacts = contacts.OrderBy(c => c.Name).ToList();
            return View(sortedContacts);
        }

        // Create

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        public IActionResult Create(ContactModel contact, IFormFile image, string name, string email, string phonenumber)
        {
            try
            {
                contact.Name = name;
                contact.Email = email;
                contact.PhoneNumber = phonenumber;

                if (image != null && image.Length > 0)
                {
                    string wwwrootPath = Path.Combine(_env.ContentRootPath, "wwwroot");
                    string contactsImagePath = Path.Combine(wwwrootPath, "images/contacts");

                    bool directoryExists = Directory.Exists(contactsImagePath);
                    if (!directoryExists)
                    {
                        Directory.CreateDirectory(contactsImagePath);
                    }

                    string fileExtension = Path.GetExtension(image.FileName);
                    string fileName = Guid.NewGuid().ToString() + fileExtension;
                    string filePath = Path.Combine(contactsImagePath, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        image.CopyTo(fileStream);
                    }

                    contact.ImagePath = filePath;
                }
                _contactRepository.ToAdd(contact);

                TempData["successMessage"] = "Contato cadastrado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (System.Exception erro)
            {
                TempData["errorMessage"] = $"Ops... Não conseguimos cadastrar seu contato. Tente novamente!\nDetalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        // Update

        [HttpGet("Update")]
        public IActionResult Update(Guid id)
        {
            ContactModel contact = _contactRepository.ListById(id);
            return View(contact);
        }

        [HttpPost("Update")]
        public IActionResult Update(ContactModel contact)
        {
            // Tratamento de erro com ASP.NET:

            try
            {
                if (ModelState.IsValid)
                {
                    _contactRepository.ToUpdate(contact);
                    TempData["successMessage"] = "Contato atualizado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(contact);

                // caso queiramos especificar o nome do arquivo da view, podemos fazer da seguinte forma:
                // return View("nome-da-view", nome-da-variavel-de-retorno)
                // a variavel nesse caso seria contact.
            }
            catch (System.Exception erro)
            {
                TempData["errorMessage"] = $"Ops... Não conseguimos atualizar seu contato. Tente novamente!\nDetalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        // Delete

        [HttpGet("ConfirmDeletion")]
        public IActionResult ConfirmDeletion(Guid id)
        {
            ContactModel contact = _contactRepository.ListById(id);
            return View(contact);
        }

        [HttpGet("Delete")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                bool deleted = _contactRepository.ToDelete(id);

                if (deleted)
                {
                    TempData["successMessage"] = "Contato excluído com sucesso!";
                }
                else
                {
                    TempData["errorMessage"] = $"Ops... Não conseguimos excluir seu contato!";
                }

                return RedirectToAction("Index");
            }
            catch (System.Exception erro)
            {
                TempData["errorMessage"] = $"Ops... Não conseguimos excluir seu contato. Tente novamente!\nDetalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}