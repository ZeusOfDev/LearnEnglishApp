using LearnWordApp.Models;
using LearnWordApp.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.ComponentModel.Design.Serialization;
using System.Text.Json;

namespace LearnWordApp.Controllers
{
    public class WordExcerciseController : Controller
    {
        private readonly WordDbContext dbcontext;
        public WordExcerciseController(WordDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        
        
        [HttpGet]
        [Route("/practice/quiz/")]
        public IActionResult QuizExcercise()
        {
            if (TempData.Peek("words") == null || TempData.Peek("currentLen") == null)
                return RedirectToAction("ListTopic", "LearningFeature");

            string[]? words = null;
            int currentLen = -1;
            try
            {
                words = TempData["words"].ToString().Split(';');
                currentLen = Convert.ToInt32(TempData["currentLen"]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            if (currentLen == -1 || words == null) 
                return Content("error with words and len");
           
            Console.WriteLine("curlen:" +  currentLen);
            if (currentLen == 0 )
            {
                Console.WriteLine("currentLen = 0");
                if (!HttpContext.Request.Cookies.ContainsKey("lsid") || string.IsNullOrEmpty(HttpContext.Request.Cookies["isid"]))
                {
                    return Content("lsid or isid null");
                }
                var isidCookie = Request.Cookies["isid"];
                int isid = Convert.ToInt32(isidCookie); 
                ++isid;

                string[] lsid = HttpContext.Request.Cookies["lsid"].Split(',');
                //update cookie
                

                if (isid >= lsid.Length)
                {
                    return Content("you have learned all words");
                }
                Console.WriteLine("change list");
                words = dbcontext.GetAllWordInSet(lsid[isid]).ConvertIEnumarableToString().Split(';');
                currentLen = words.Length;
            }
            if (words.Length < 4)
            {
                return Content("list is not enough element");
            }
            Random rd = new Random();
            int[] opt = new int[3];
            int i0 = rd.Next(2, words.Length - 1);
            int i1 = rd.Next(1, i0);
            int i2 = rd.Next(i0 + 1, words.Length);
            var md = new QuizModel();

            md.words[0] = words[i0];
            md.words[1] = words[i1];
            md.words[2] = words[i2];
            md.words[3] = words[0];
           
            int answer = rd.Next(0, 3);
            md.ans = answer;
            
            md.words.SwapWord(3, answer);
            // chang word
            words.SwapWord(0, currentLen - 1);

            TempData["words"] = String.Join(';', words);
            TempData["currentLen"] = currentLen - 1;
            
            return View(md);
        }

        [HttpPost]
        [Route("/practice/quiz/")]
        public IActionResult QuizExcercise([FromForm]string accept)
        {
            if (accept != "1") {
                return Content("you dont have permission to change question");
            }
            return RedirectToAction("QuizExcercise", "WordExcercise");
        }
    }
}
