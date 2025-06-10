using LawSearchLuceneDemo.Models;
using LawSearchLuceneDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace LawSearchLuceneDemo.Controllers
{
  
        public class SearchController : Controller
        {
            private static List<LawClause> FakeLawData => new()
            {
                new LawClause { Id = 1, Title = "刑法第10條", Content = "犯罪是指違反法律規定的行為..." },
                new LawClause { Id = 2, Title = "刑法第11條", Content = "依法成立的法人，亦負刑事責任..." },
                new LawClause { Id = 3, Title = "民法第1條", Content = "民事，法律所未規定者，依習慣..." }
            };

            public IActionResult Index() => View();

            [HttpPost]
            public IActionResult Index(string keyword)
            {
                var service = new LuceneIndexer();
                var results = service.Search(keyword);
                return View("Result", results);
            }

            public IActionResult Build()
            {
                var service = new LuceneIndexer();
                service.CreateIndex(FakeLawData);
                return Content("索引建立完成");
            }
        }
}
