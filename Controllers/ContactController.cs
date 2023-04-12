using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ContactRegister.Models;
using ContactRegister.Repositories;

namespace ContactRegister.Controllers
{
    [Route("[controller]")]
    public class ContactController : Controller
    {

        private readonly IContactRepository _contactRepository;

        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
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
        public IActionResult Create(ContactModel contact)
        {
            // Tratamento de erro com ASP.NET:

            try
            {
                if (ModelState.IsValid)
                {
                    _contactRepository.ToAdd(contact);
                    TempData["successMessage"] = "Contato cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(contact);
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
                    TempData["successMessage"] = "Contato deletado com sucesso!";
                }
                else
                {
                    TempData["errorMessage"] = $"Ops... Não conseguimos atualizar seu contato!";
                }

                return RedirectToAction("Index");
            }
            catch (System.Exception erro)
            {
                TempData["errorMessage"] = $"Ops... Não conseguimos atualizar seu contato. Tente novamente!\nDetalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}