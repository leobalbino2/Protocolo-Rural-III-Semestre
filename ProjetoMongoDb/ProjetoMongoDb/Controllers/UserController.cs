using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProtocoloRural.Models;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Threading.Tasks;

namespace ProtocoloRural.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Create(string role)
        {
            ViewBag.Role = role;
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterViewModel model, string role)
        {
            if (!ModelState.IsValid)
                return View(model);

            ApplicationUser appuser = new ApplicationUser();

            // gerar username a partir do nome (remover espaços, acentos e caracteres inválidos)
            string userName = (model.NomeCompleto ?? "").Replace(" ", "");
            var normalizedString = userName.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            foreach (char c in normalizedString)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }
            userName = sb.ToString().Normalize(NormalizationForm.FormC);
            userName = Regex.Replace(userName, @"[^a-zA-Z0-9\s]", "");
            appuser.UserName = userName;

            appuser.Email = model.Email;
            appuser.NomeCompleto = model.NomeCompleto;

            // atribuir celular (mantendo apenas dígitos) e PhoneNumber para compatibilidade com Identity
            if (!string.IsNullOrWhiteSpace(model.Celular))
            {
                var digits = new string(model.Celular.Where(char.IsDigit).ToArray());
                appuser.Celular = digits;
                appuser.PhoneNumber = digits;
            }

            IdentityResult result = await _userManager.CreateAsync(appuser, model.Password);
            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }

            // se foi criado, cuidar da role (garantir existência e depois atribuir)
            if (!string.IsNullOrEmpty(role))
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    var createRoleResult = await _roleManager.CreateAsync(new ApplicationRole { Name = role });
                    if (!createRoleResult.Succeeded)
                    {
                        foreach (var er in createRoleResult.Errors)
                            ModelState.AddModelError("", $"Erro criando perfil '{role}': {er.Description}");
                        ViewBag.Message = "Usuário criado, mas não foi possível criar o perfil.";
                        return View(model);
                    }
                }

                var addRoleResult = await _userManager.AddToRoleAsync(appuser, role);
                if (!addRoleResult.Succeeded)
                {
                    foreach (var er in addRoleResult.Errors)
                        ModelState.AddModelError("", $"Erro atribuindo perfil: {er.Description}");
                    ViewBag.Message = "Usuário criado, mas não foi possível atribuir o perfil.";
                    return View(model);
                }
            }

            ViewBag.Message = "Usuário cadastrado com sucesso";
            // limpar ModelState se quiser retornar view vazia:
            ModelState.Clear();
            return View(new RegisterViewModel());
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(UserRole useRole)
        {
            if (!ModelState.IsValid)
                return View();

            IdentityResult result = await _roleManager.CreateAsync(new ApplicationRole() { Name = useRole.RoleName });
            if (result.Succeeded)
            {
                ViewBag.Message = "Perfil cadastrado com sucesso";
            }
            else
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }
    }
}