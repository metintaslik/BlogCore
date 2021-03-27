using Blog.Models;
using Blog.Models.DataModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Core.Services
{
    public interface ICoreService
    {
        Task<ResponseModel<Users>> SignInAsync(Users model);
        Task<ResponseModel<List<Categories>>> CategoriesAsync();
        Task<ResponseModel<Categories>> CreateOrUpdateCatergoryAsync(Categories model);
        Task<ResponseModel<Categories>> DeleteCategoryAsync(Categories model);
        Task<ResponseModel<List<Articles>>> ArticlesAsync();
        Task<ResponseModel<Articles>> CreateOrUpdateArticleAsync(Articles model);
        Task<ResponseModel<Articles>> DeleteArticleAsync(Articles model);
    }
}