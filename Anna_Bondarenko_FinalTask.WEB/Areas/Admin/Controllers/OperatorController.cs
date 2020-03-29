using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Anna_Bondarenko_FinalTask.BLL.DTO;
using Anna_Bondarenko_FinalTask.BLL.Interfaces;
using Anna_Bondarenko_FinalTask.WEB.Models;
using AutoMapper;
using Microsoft.AspNet.Identity.Owin;

namespace Anna_Bondarenko_FinalTask.WEB.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OperatorController : Controller
    {
        private readonly IOperatorService _operatorService;
        private readonly IMapper _mapper;

        public OperatorController(IOperatorService operatorService,IMapper mapper)
        {
            _operatorService = operatorService;
            _mapper = mapper;
        }

        private IOperatorService UserService => HttpContext.GetOwinContext().GetUserManager<IOperatorService>();

        [HttpGet]
        public ActionResult OperatorRegister()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> OperatorRegister(OperatorDto operatorDto)
        {
            if (!ModelState.IsValid)
            {
                return View(operatorDto);
            }

            var sucsess = await UserService.Create(operatorDto);

            if (sucsess)
            {
                return RedirectToAction("GetFaculty", "Faculty", new { area = string.Empty });
            }

            return View(operatorDto);
        }

        [HttpGet]
        public ActionResult OperatorEdit(int id)
        {
            var operatorModel = _operatorService.Get(id);

            if (operatorModel == null)
            {
                return HttpNotFound();
            }

            return View(operatorModel);
        }

        [HttpPost]
        public ActionResult OperatorEdit(Anna_Bondarenko_FinalTask.Models.Models.Operator operatorModel)
        {
            if (!ModelState.IsValid)
            {
                return View(operatorModel);
            }

            _operatorService.Update(operatorModel);

            return RedirectToAction("GetFaculty", "Faculty", new { area = string.Empty });
        }

        public ActionResult GetOperators()
        {
            var operators = _operatorService.GetAll();

            return View(_mapper.Map<IEnumerable<Anna_Bondarenko_FinalTask.Models.Models.Operator>,IEnumerable<OperatorVm>>(operators));
        }

        [HttpGet]
        public ActionResult OperatorDelete(int id)
        {
            return View(_mapper.Map<Anna_Bondarenko_FinalTask.Models.Models.Operator,OperatorVm>(_operatorService.Get(id)));
        }

        [HttpPost]
        public ActionResult OperatorDelete(OperatorVm operatorModel)
        {
            _operatorService.Delete(operatorModel.Id);

            return RedirectToAction("GetOperators", "Operator", new { area = "Admin" });
        }

        public ActionResult OperatorDetails(int id)
        {
            var operatorModel = _operatorService.Get(id);

            return View(_mapper.Map<Anna_Bondarenko_FinalTask.Models.Models.Operator,OperatorVm>(operatorModel));
        }
    }
}
