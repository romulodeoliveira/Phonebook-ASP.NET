using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ContactRegister.Repositories;
using ContactRegister.Models;

namespace Project01.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            // Crio uma variável contacts e atribuo a ela o método que criei no meu repositório
            // Depois retorno a variável criada na minha view
            List<UserModel> users = _userRepository.FindAll();
            List<UserModel> sortedContacts = users.OrderBy(c => c.FirstName).ToList();
            return View(sortedContacts);
        }

        // Create

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        public IActionResult Create(UserModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _userRepository.ToAdd(user);
                    TempData["successMessage"] = "Usuário cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(user);
            }
            catch (System.Exception erro)
            {
                TempData["errorMessage"] = $"Ops... Não conseguimos cadastrar seu usuário. Tente novamente!\nDetalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        // Update

        [HttpGet("Update")]
        public IActionResult Update(Guid id)
        {
            UserModel user = _userRepository.ListById(id);
            return View(user);
        }

        [HttpPost("Update")]
        public IActionResult Update(UserModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _userRepository.ToUpdate(user);
                    TempData["successMessage"] = "Perfil atualizado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(user);

            }
            catch (System.Exception erro)
            {
                TempData["errorMessage"] = $"Ops... Não conseguimos atualizar seu perfil. Tente novamente!\nDetalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        // Delete

        [HttpGet("ConfirmDeletion")]
        public IActionResult ConfirmDeletion(Guid id)
        {
            UserModel user = _userRepository.ListById(id);
            return View(user);
        }

        [HttpGet("Delete")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                bool deleted = _userRepository.ToDelete(id);

                if (deleted)
                {
                    TempData["successMessage"] = "Usuário excluído com sucesso!";
                }
                else
                {
                    TempData["errorMessage"] = $"Ops... Não conseguimos excluir esse usuário!";
                }

                return RedirectToAction("Index");
            }
            catch (System.Exception erro)
            {
                TempData["errorMessage"] = $"Ops... Não conseguimos excluir esse usuário. Tente novamente!\nDetalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}