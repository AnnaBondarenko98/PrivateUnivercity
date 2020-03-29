using System.Collections.Generic;
using System.Web.Mvc;
using Anna_Bondarenko_FinalTask.BLL.Interfaces;
using Anna_Bondarenko_FinalTask.Models.Models;
using Anna_Bondarenko_FinalTask.WEB.Models;
using AutoMapper;

namespace Anna_Bondarenko_FinalTask.WEB.Controllers
{
    public class AboutController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public AboutController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult About()
        {
            var comments = _commentService.GetAll();

            return View(_mapper.Map<IEnumerable<Comment>, IEnumerable<CommentVm>>(comments));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult About(Comment model)
        {
            if (!ModelState.IsValidField("Text"))
            {
                var comments = _commentService.GetAll();

                return View(_mapper.Map<IEnumerable<Comment>, IEnumerable<CommentVm>>(comments));
            }

            _commentService.Create(model,User.Identity.Name);

            return RedirectToAction("About");
        }
    }
}