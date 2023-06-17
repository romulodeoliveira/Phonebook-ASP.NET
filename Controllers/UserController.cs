using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

using Phonebook.Repositories;
using Phonebook.Models;

namespace Phonebook.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("user/")]
        public IActionResult Index()
        {
            // Crio uma variável contacts e atribuo a ela o método que criei no meu repositório
            // Depois retorno a variável criada na minha view
            List<UserModel> users = _userRepository.FindAll();
            List<UserModel> sortedContacts = users.OrderBy(c => c.FirstName).ToList();
            return View(sortedContacts);
        }

        // Create

        [HttpGet("user/create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("user/create")]
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

        [HttpGet("user/update/{id}")]
        public IActionResult Update(Guid id)
        {
            UserModel user = _userRepository.ListById(id);
            return View(user);
        }

        [HttpPost("user/update/{id}")]
        public IActionResult Update(UserModel user, IFormFile photograph, string firstName, string middleName, string lastName, string email, string userName, string password)
        {
            try
            {
                var user1 = _userRepository.ListById(user.Id);
                
                if (userName == null)
                {
                    user.UserName = user1.UserName;
                }

                if (email == null)
                {
                    user.Email = user1.Email;
                }
                                
                if (password == null)
                {
                    user.Password = user1.Password;
                }

                user1.UserName = user.UserName;
                user1.Email = user.Email;
                user1.Password = user.Password;
                
                if (userName != null)
                {
                    user1.UserName = userName;
                }

                if (email != null)
                {
                    user1.Email = email;
                }
                                
                if (password != null)
                {
                    user1.Password = password;
                }
                
                if (photograph != null)
                {
                    if (Path.GetExtension(photograph.FileName) == ".jpg" || Path.GetExtension(photograph.FileName) == ".jpeg" || Path.GetExtension(photograph.FileName) == ".png")
                    {
                        if (photograph.Length > 0 && photograph.Length < (1 * 1024 * 1024))
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                photograph.CopyTo(memoryStream);
                                user1.ProfilePicture = memoryStream.ToArray();
                            }
                        }
                        else
                        {
                            // Retorna uma mensagem de erro
                            TempData["errorMessage"] = "O tamanho máximo da imagem permitido é de 1MB.";
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        // Retorna uma mensagem de erro
                        TempData["errorMessage"] = $"A extensão '{Path.GetExtension(photograph.FileName)}' não é permitida. Utilize apenas '.png', '.jpg' ou '.jpeg'.";
                        return RedirectToAction("Index");
                    }
                }

                if (firstName != null)
                {
                    user1.FirstName = firstName;
                }
                
                if (middleName != null)
                {
                    user1.MiddleName = middleName;
                }
                
                if (lastName != null)
                {
                    user1.LastName = lastName;
                }
                
                // Utiliza o repository para salvar o model no banco
                _userRepository.ToUpdate(user1);
                
                // Retorna uma mensagem de sucesso
                TempData["successMessage"] = "Usuário cadastrado com sucesso!";
                return RedirectToAction("Index");
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