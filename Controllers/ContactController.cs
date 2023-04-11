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
            return View(contacts);
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
            if (ModelState.IsValid)
            {
                _contactRepository.ToAdd(contact);
                return RedirectToAction("Index");
            }

            return View(contact);
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
            if (ModelState.IsValid)
            {
                _contactRepository.ToUpdate(contact);
                return RedirectToAction("Index");
            }

            return View(contact);

            // caso queiramos especificar o nome do arquivo da view, podemos fazer da seguinte forma:
            // return View("nome-da-view", nome-da-variavel-de-retorno)
            // a variavel nesse caso seria contact.
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
            _contactRepository.ToDelete(id);
            return RedirectToAction("Index");
        }
    }
}