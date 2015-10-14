using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BlogTestWork.Models;
using BlogTestWork.Models.DbModels;
using BlogTestWork.Models.ViewModels;
using Newtonsoft.Json;

namespace BlogTestWork.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICommentService _commentService;

        public HomeController(ICommentService service)
        {
            _commentService = service;
        }

        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SearchComments(string search)
        {
            try
            {
                var returnData = _commentService.SearchComments(search).ToList();

                return Json(returnData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(400, ex.Message);
            }
        }

        [HttpGet]
        public ActionResult GetRecentComments(string date)
        {
            var lastDate = JsonConvert.DeserializeObject<DateObjectVM>(date);

            try
            {
                var returnData = _commentService.GetRecentComments(lastDate.LastDate);

                return new ContentResult
                {
                    ContentType = "text/plain",
                    Content = JsonConvert.SerializeObject(returnData),
                    ContentEncoding = Encoding.UTF8
                };
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(400, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult AddComment(string obj)
        {
            NewCommentVM comment = JsonConvert.DeserializeObject<NewCommentVM>(obj);
            try
            {
                _commentService.AddNewComment(comment);

                DateObjectVM dateVm = new DateObjectVM {LastDate = comment.LastDate};
                return GetRecentComments(JsonConvert.SerializeObject(dateVm));
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return new HttpStatusCodeResult(400, message);
            }
        }
    }
}