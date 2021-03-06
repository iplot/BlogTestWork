﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
        public ActionResult SearchComments(string search = "")
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
        public ActionResult GetRecentComments(int? lastId)
        {
            try
            {
                var returnData = _commentService.GetRecentComments(lastId);

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
        public ActionResult AddComment(NewCommentVM comment)
        {
            try
            {
                ModelState.Clear();
                ValidateModel(comment);

                _commentService.AddNewComment(comment);
                saveFiles(comment.UserName);

                return GetRecentComments(comment.LastId);
            }
            catch (Exception ex)
            {
                string message = "";

                if (ModelState.IsValid)
                {
                    message = ex.Message;
                }
                else
                {
                    message += string.Join(".",
                    ModelState.Values.SelectMany(x => x.Errors, (x, err) => err.ErrorMessage));
                }

                return new HttpStatusCodeResult(400, message);
            }
        }

        public ActionResult LoadAvatar(string userName)
        {
            var dir = Server.MapPath("~/App_Data/Images");
            var path = Path.Combine(dir, userName);

            if (!System.IO.File.Exists(path))
            {
                path = Path.Combine(dir, "google.jpeg");
            }

            return File(path, "image/jpeg");
        }

        private void saveFiles(string userName)
        {
            try
            {
                foreach (string fileName in Request.Files)
                {
                    var content = Request.Files[fileName];

                    if (content != null && content.ContentLength > 0)
                    {
                        var stream = content.InputStream;
                        var path = Path.Combine(Server.MapPath("~/App_Data/Images"), userName);

                        if (System.IO.File.Exists(path))
                        {
                            using (var fileStream = System.IO.File.OpenWrite(path))
                            {
                                stream.CopyTo(fileStream);
                            }
                        }
                        else
                        {
                            using (var fileStream = System.IO.File.Create(path))
                            {
                                stream.CopyTo(fileStream);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}