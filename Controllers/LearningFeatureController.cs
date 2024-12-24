using LearnWordApp.Models;
using LearnWordApp.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Text.Json;
namespace LearnWordApp.Controllers
{
    public class LearningFeatureController : Controller
    {
        private readonly WordDbContext dbcontext;
        
        public LearningFeatureController(WordDbContext dbcontext) {
            this.dbcontext = dbcontext;
        }
       
        public IActionResult Index()
        {
            
            return View();
        }
        
        [HttpGet]
        [Route("/practicebytopic")]
        public IActionResult ListTopic()
        {
            //return Content(dbcontext.GetAllWordSet().Count.ToString());
            
            return View(dbcontext.GetAllWordSet());
        }
        [HttpPost]
        [Route("/practicebytopic")]
        public IActionResult ListTopic(string listInput)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true
            };
            string[] ListIDs = listInput.Split('-');
            HttpContext.Response.Cookies.Append("lsid", string.Join(',', ListIDs), cookieOptions);
            HttpContext.Response.Cookies.Append("isid", "0");
            IEnumerable<Word> ws;
            if (ListIDs.Length == 0)
                return Content("something went wrong");
            else
            {
                ws = dbcontext.GetAllWordInSet<string>(ListIDs[0]);
                TempData["words"] = ws.ConvertIEnumarableToString();
                if (TempData.Peek("words") != null)
                    TempData["currentLen"] = ws.Count();
            }
            return RedirectToAction("QuizExcercise", "WordExcercise");
        }
    }
}
