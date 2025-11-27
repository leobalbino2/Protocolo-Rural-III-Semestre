using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProtocoloRural.Models;
using ProtocoloRural.Services;
using ProtocoloRural.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Linq;
using System.Threading.Tasks;

namespace ProtocoloRural.Controllers
{
    public class AccountController : Controller
    {
        private EmailService _emailService;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            EmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([Required][EmailAddress] string email,
            [Required] string password)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appuser = await _userManager.FindByEmailAsync(email);
                if (appuser != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(appuser, password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                // Mensagem de erro para credencial inválida
                TempData["Error"] = "E-mail ou senha inválidos!";
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("", "Informe o e-mail");
                return View();
            }
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return RedirectToAction("ForgotPasswordConfirmation");
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = HttpUtility.UrlEncode(token);
            var callbackUrl = Url.Action("ResetPassword", "Account",
                new { userId = user.Id, token = encodedToken }, Request.Scheme);
            string assunto = "Redefinição de Senha";
            string corpo = $"Clique no link para redefinir sua senha:" +
                $"<a href='{callbackUrl}'>Redefinir Senha</a>";
            await _emailService.SendEmailAsync(email, assunto, corpo);
            return RedirectToAction("ForgotPasswordConfirmation");
        }

        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        public IActionResult ResetPassword(string token, string userId)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(userId))
            {
                ModelState.AddModelError("", "Token Inválido");
            }
            var model = new ResetPasswordViewModel
            {
                Token = token,
                UserId = userId
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return RedirectToAction("ResetPasswordConfirmation");
            }
            var decodedToken = HttpUtility.UrlDecode(model.Token);
            var result = await _userManager.ResetPasswordAsync(user, decodedToken, model.NewPassword);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(model);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        // ==========================================
        // MINHA CONTA / PERFIL =====================
        // ==========================================

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> MinhaConta()
        {
            var user = await _userManager.GetUserAsync(User);

            var model = new MinhaContaViewModel
            {
                NomeCompleto = user.NomeCompleto,
                Email = user.Email,
                Celular = user.Celular
            };
            if (TempData["Message"] != null)
                model.Mensagem = TempData["Message"].ToString();

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> MinhaConta(MinhaContaViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.GetUserAsync(User);
            user.NomeCompleto = model.NomeCompleto;
            user.Celular = model.Celular;
            await _userManager.UpdateAsync(user);

            TempData["Message"] = "Dados atualizados!";
            return RedirectToAction("MinhaConta");
        }

        // ================= ALTERAR EMAIL =======================
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AlterarEmail(AlterarEmailViewModel model)
        {
            if (model.NovoEmail != model.ConfirmarEmail)
            {
                TempData["Message"] = "Os emails não conferem.";
                return RedirectToAction("MinhaConta");
            }
            var user = await _userManager.GetUserAsync(User);
            var token = await _userManager.GenerateChangeEmailTokenAsync(user, model.NovoEmail);
            var result = await _userManager.ChangeEmailAsync(user, model.NovoEmail, token);

            if (result.Succeeded)
            {
                TempData["Message"] = "E-mail alterado com sucesso!";
            }
            else
            {
                TempData["Message"] = "Erro ao alterar email: " +
                    string.Join("; ", result.Errors.Select(e => e.Description));
            }
            return RedirectToAction("MinhaConta");
        }

        // ================ ALTERAR SENHA =======================
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AlterarSenha(AlterarSenhaViewModel model)
        {
            if (model.NovaSenha != model.ConfirmarSenha)
            {
                TempData["Message"] = "As senhas não conferem.";
                return RedirectToAction("MinhaConta");
            }
            var user = await _userManager.GetUserAsync(User);
            var result = await _userManager.ChangePasswordAsync(user, model.SenhaAtual, model.NovaSenha);

            if (result.Succeeded)
            {
                TempData["Message"] = "Senha alterada com sucesso!";
            }
            else
            {
                TempData["Message"] = "Erro ao alterar senha: " +
                    string.Join("; ", result.Errors.Select(e => e.Description));
            }
            return RedirectToAction("MinhaConta");
        }

        // =============== EXCLUIR CONTA ========================
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ExcluirConta()
        {
            var user = await _userManager.GetUserAsync(User);
            var result = await _userManager.DeleteAsync(user);
            await _signInManager.SignOutAsync();

            if (result.Succeeded)
            {
                TempData["Message"] = "Conta excluída!";
            }
            else
            {
                TempData["Message"] = "Erro ao excluir: " +
                    string.Join("; ", result.Errors.Select(e => e.Description));
            }
            return RedirectToAction("Index", "Home");
        }

        // ===================== FIM ============================
    }
}