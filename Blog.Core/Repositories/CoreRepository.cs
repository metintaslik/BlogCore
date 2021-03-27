using Blog.Core.Helper;
using Blog.Core.Services;
using Blog.Models;
using Blog.Models.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Repositories
{
    public class CoreRepository : ICoreService
    {
        private readonly BlogDBContext context;

        public CoreRepository(BlogDBContext context)
        {
            this.context = context;
        }

        public async Task<ResponseModel<Users>> SignInAsync(Users model)
        {
            try
            {
                ResponseModel<Users> response = new() { Error = false };
                Users signInUser = await context.Users.FirstOrDefaultAsync(x => x.Mail == model.Mail && x.Password == Hasher.TextHashing(model.Password));
                return new ResponseModel<Users>
                {
                    Error = signInUser == null,
                    Code = signInUser == null ? 1 : null,
                    Message = signInUser == null ? "Yanlış mail ve şifre bilgisi, lütfen tekrardan deneyiniz." : null,
                    Result = signInUser ?? null
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseModel<List<Categories>>> CategoriesAsync()
        {
            try
            {
                ResponseModel<List<Categories>> response = new() { Error = false };
                response.Result = await context.Categories.ToListAsync();
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ResponseModel<Categories>> CreateOrUpdateCatergoryAsync(Categories model)
        {
            try
            {
                ResponseModel<Categories> response = new() { Error = false };
                if (model.Id == 0)
                {
                    EntityEntry<Categories> category = await context.Categories.AddAsync(model);
                    response.Result = category.Entity;
                }
                else
                {
                    Categories category = await context.Categories.FirstOrDefaultAsync(x => x.Id == model.Id);
                    category.UpdateDateTime = DateTime.Now;
                    category.CategoryName = model.CategoryName;
                    context.Categories.Update(category);
                    response.Result = category;
                }

                await context.SaveChangesAsync();
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ResponseModel<Categories>> DeleteCategoryAsync(Categories model)
        {
            try
            {
                ResponseModel<Categories> response = new() { Error = false };
                Categories category = await context.Categories.FirstOrDefaultAsync(x => x.Id == model.Id);
                context.Articles.RemoveRange(await context.Articles.Where(x => x.CategoryId == model.Id).ToListAsync());
                context.Categories.Remove(category);
                await context.SaveChangesAsync();
                response.Error = false;
                response.Result = category;
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ResponseModel<List<Articles>>> ArticlesAsync()
        {
            try
            {
                ResponseModel<List<Articles>> response = new() { Error = false };
                var articles = await context.Articles.ToListAsync();
                foreach (var item in articles)
                {
                    item.Creative = await context.Users.FirstOrDefaultAsync(x => x.Id == item.CreativeId);
                    item.Category = await context.Categories.FirstOrDefaultAsync(x => x.Id == item.CategoryId); 
                }
                response.Result = articles;
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ResponseModel<Articles>> CreateOrUpdateArticleAsync(Articles model)
        {
            try
            {
                ResponseModel<Articles> response = new() { Error = false };
                if (model.Id == 0)
                {
                    EntityEntry<Articles> article = await context.Articles.AddAsync(model);
                    response.Result = article.Entity;
                }
                else
                {
                    Articles article = await context.Articles.FirstOrDefaultAsync(x => x.Id == model.Id);
                    article.UpdateDateTime = DateTime.Now;
                    article.CategoryId = model.CategoryId;
                    article.Theme = model.Theme;
                    context.Articles.Update(article);
                    response.Result = article;
                }

                await context.SaveChangesAsync();
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseModel<Articles>> DeleteArticleAsync(Articles model)
        {
            try
            {
                ResponseModel<Articles> response = new() { Error = false };
                Articles article = await context.Articles.FirstOrDefaultAsync(x => x.Id == model.Id);
                context.Articles.Remove(article);
                await context.SaveChangesAsync();
                response.Error = false;
                response.Result = article;
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}