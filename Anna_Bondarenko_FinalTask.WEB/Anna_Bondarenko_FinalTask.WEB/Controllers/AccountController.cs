using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Anna_Bondarenko_FinalTask.BLL.DTO;
using Anna_Bondarenko_FinalTask.BLL.Interfaces;
using Anna_Bondarenko_FinalTask.WEB.Models;
using AutoMapper;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Anna_Bondarenko_FinalTask.WEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;

        public AccountController(IMapper mapper, IAccountService accountService)
        {
            _mapper = mapper;
            _accountService = accountService;
        }

        private IEnrolleeService UserService => HttpContext.GetOwinContext().GetUserManager<IEnrolleeService>();

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(EnrolleeDto enrolleeDto, HttpPostedFileBase image)
        {
            string[] formats = new string[] { ".jpg", ".png", ".gif", ".jpeg" };

            if ((image == null || !image.ContentType.Contains("image") )|| !formats.Any(item =>
                    image.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase)))
            {
                ModelState.AddModelError(string.Empty, "Load your valid document");
            }

            if (!ModelState.IsValid)
            {
                return View(enrolleeDto);
            }

            if (image != null || image.ContentType.Contains("image") || !formats.Any(item =>
                    image.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase)))
            {
                enrolleeDto = _accountService.VerifyImage(enrolleeDto, image);
            }

            var sucsess = await UserService.Create(enrolleeDto);

            if (sucsess)
            {
                return RedirectToAction("Login", "Account");
            }

            ModelState.AddModelError("RegisterError", "The register was failed. Please, try again.");

            return View(enrolleeDto);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var claim = await UserService.Authenticate(_mapper.Map<LoginVm, LoginDto>(model));

            if (claim == null)
            {
                ModelState.AddModelError(string.Empty, "Wrong login or password.");

                return View(model);
            }

            AuthenticationManager.SignOut();

            if (claim.Label=="Locked")
            {
                return View("LockPage");
            }

            AuthenticationManager.SignIn(new AuthenticationProperties
            {
                IsPersistent = true,

            }, claim);

                return RedirectToAction("GetFaculty", "Faculty");
        }

        [Authorize]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();

            return RedirectToAction("GetFaculty", "Faculty");
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userForgotCode = await _accountService.GenerateForgotCode(model);

            if (userForgotCode == null)
            {
                return View("ForgotPasswordConfirmation");
            }

            var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = userForgotCode.Id, code = userForgotCode.Code }, Request.Url?.Scheme);

            _accountService.SendForgotLink(callbackUrl, model.Email);

            return RedirectToAction("ForgotPasswordConfirmation", "Account");
        }

        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        public ActionResult ResetPassword(string code)
        {
            if (code == null)
            {
                //return RedirectToAction("Error", "Error");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPassword model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var reset = await _accountService.ResetPassword(model);

            if (reset)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}