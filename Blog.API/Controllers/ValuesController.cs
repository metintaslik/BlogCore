using Blog.Core.Services;
using Blog.Models.DataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace Blog.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ICoreService service;
        public ValuesController(ICoreService service)
        {
            this.service = service;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("SignIn")]
        [EnableCors("Policy")]
        public async Task<IActionResult> SignInAsync(Users entity)
        {
            return Content(JsonConvert.SerializeObject(await service.SignInAsync(entity)), "application/json", Encoding.UTF8);
        }

        [HttpGet]
        [Route("Categories")]
        [EnableCors("Policy")]
        public async Task<IActionResult> GetCategoriesAsync()
        {
            return Content(JsonConvert.SerializeObject(await service.CategoriesAsync()), "application/json", Encoding.UTF8);
        }

        [HttpPost]
        [EnableCors("Policy")]
        [Route("CreateOrUpdateCategory")]
        public async Task<IActionResult> CreateOrUpdateCategoryAsync(Categories entity)
        {
            return Content(JsonConvert.SerializeObject(await service.CreateOrUpdateCatergoryAsync(entity)), "application/json", Encoding.UTF8);
        }

        [HttpPost]
        [EnableCors("Policy")]
        [Route("DeleteCategory")]
        public async Task<IActionResult> DeleteCategoryAsync(Categories entity)
        {
            return Content(JsonConvert.SerializeObject(await service.DeleteCategoryAsync(entity)), "application/json", Encoding.UTF8);
        }

        [HttpGet]
        [Route("Articles")]
        [EnableCors("Policy")]
        public async Task<IActionResult> GetArticlesAsync()
        {
            return Content(JsonConvert.SerializeObject(await service.ArticlesAsync()), "application/json", Encoding.UTF8);
        }

        [HttpPost]
        [EnableCors("Policy")]
        [Route("CreateOrUpdateArticle")]
        public async Task<IActionResult> CreateOrUpdateArticleAsync(Articles entity)
        {
            return Content(JsonConvert.SerializeObject(await service.CreateOrUpdateArticleAsync(entity)), "application/json", Encoding.UTF8);
        }

        [HttpPost]
        [EnableCors("Policy")]
        [Route("DeleteArticle")]
        public async Task<IActionResult> DeleteArticleAsync(Articles entity)
        {
            return Content(JsonConvert.SerializeObject(await service.DeleteArticleAsync(entity)), "application/json", Encoding.UTF8);
        }
    }
}