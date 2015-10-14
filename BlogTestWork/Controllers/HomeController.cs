using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogTestWork.Models;
using BlogTestWork.Models.DbModels;
using BlogTestWork.Models.ViewModels;

namespace BlogTestWork.Controllers
{
    public class HomeController : Controller
    {
        private readonly CommentService _commentService;

        public HomeController()
        {
            _commentService = new CommentService(new BlogContext());
        }

        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetComments(string search = "")
        {
            try
            {
                var returnData = _commentService.GetComments(search).ToList();

                return Json(returnData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(400, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult AddComment(NewCommentVM comment)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception();
                }

                _commentService.AddNewComment(comment);

                return GetComments();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                message += string.Join(". ", ModelState);

                return new HttpStatusCodeResult(400, message);
            }
        }
    }
}