using LearnWordApp.Models;
using LearnWordApp.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using LearnWordApp.Installer;
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
        [Route("/practice")]
        public IActionResult AnswerQuestion()
        {
            return View();
        }
        [HttpPost]
        [Route("/practice")]
        public IActionResult AnswerQuestion([FromBody] string[] ListIDs)
        {
            /*
            int excCount = Excercise.excerciseTypes.Count();
            if (excCount == 0) 
                return NotFound();
            int ranNum = new Random().Next(0, excCount);
            var obj = Activator.CreateInstance(Excercise.excerciseTypes[ranNum]);
            if (obj == null)
                return NotFound();
            return RedirectToAction();*/
            string a = "hello";
            foreach (var id in ListIDs)
                a = a + id;
            return Content(a);
        }

    }
}
