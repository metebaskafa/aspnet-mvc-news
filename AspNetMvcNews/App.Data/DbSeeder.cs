using App.Data.Entity;
using Bogus;
using Bogus.Extensions;

namespace App.Data
{
    public static class DbSeeder
    {
        /// <summary>
        /// Adds mock data
        /// </summary>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        public static async Task Seed(AppDbContext dbContext)
        {
            // Start: Add roles
            var roleAdmin = new Role
            {
                Name = "Admin"
            };
            dbContext.Roles.Add(roleAdmin);

            var roleModerator = new Role
            {
                Name = "Moderator"
            };
            dbContext.Roles.Add(roleModerator);

            var roleVisitor = new Role
            {
                Name = "Visitor"
            };
            dbContext.Roles.Add(roleVisitor);

            await dbContext.SaveChangesAsync();
            // Finish: add roles

            // Start: add users
            var adminUser = new User
            {
                Email = "admin@trt.com.tr",
                Name = "siteadmin",
                Password = "1234",
                CreatedAt = DateTime.Now,
                RoleId = roleAdmin.Id,
            };
            dbContext.Users.Add(adminUser);

            var modUser = new User
            {
                Email = "mnoderator@trt.com.tr",
                Name = "moderator",
                Password = "1234",
                CreatedAt = DateTime.Now,
                RoleId = roleModerator.Id,
            };
            dbContext.Users.Add(modUser);

            var visUser = new User
            {
                Email = "chilqinchocuk2010@gmail.com",
                Name = "murtaza",
                Password = "1234",
                CreatedAt = DateTime.Now,
                RoleId = roleVisitor.Id,
            };
            dbContext.Users.Add(visUser);

            await dbContext.SaveChangesAsync();
            // Finish: add users

            // Start : add categories
            List<Category> list = SeedCategory(10);
            foreach (var category in list)
            {
                dbContext.Categories.Add(category);
            }
            dbContext.SaveChanges();
            //Finish : add categories

            // Start : add news
            List<News> list2 = SeedNews(20);
            foreach (var news in list2)
            {
                dbContext.News.Add(news);
            }
            dbContext.SaveChanges();
            //Finish : add news
            
            // Start : add pages
            List<Page> list3 = SeedPage(20);
            foreach (var pages in list3)
            {
                dbContext.Pages.Add(pages);
            }
            dbContext.SaveChanges();
            //Finish : add pages

            // Start : add CategoryNews
            List<CategoryNews> list4 = SeedCategoryNews(300);
            foreach (var news in list4)
            {
                dbContext.CategoryNews.Add(news);
            }
            dbContext.SaveChanges();
            //Finish : add CategoryNews

            // Start : add NewsComment
            List<NewsComment> list5 = SeedNewsComment(100);
            foreach (var news in list5)
            {
                dbContext.Comments.Add(news);
            }
            dbContext.SaveChanges();
            //Finish : add NewsComment
        }
        public static List<Category> SeedCategory(int b)
        {
            int i = 0;
            List<Category> list = new();
            while (true)
            {
                
                var category1 = new Faker<Category>()
                .RuleFor(c => c.Name, f => f.Random.Word())
                .RuleFor(c => c.Description, f => f.Lorem.Text().ClampLength(50,200))
            ;
                list.Add(category1);
                i++;
                if (i == b)
                    return list;
            }
        }
        public static List<News> SeedNews(int b)
        {
            int i = 0;
            List<News> list = new();
            while (true)
            {
                
                News data = new Faker<News>()
                .RuleFor(c => c.Title, f => f.Random.Words(4))
                .RuleFor(c => c.Content, f => f.Lorem.Paragraphs(8).ClampLength(200,600))
                .RuleFor(c => c.CreatedAt, f => f.Date.Between(new DateTime(2022, 1, 1),new DateTime(2023, 7, 11)))
                .RuleFor(c => c.UserId, f=> f.Random.Int(1,2))
            ;
                
                list.Add(data);
                i++;
                if (i == b)
                    return list;
            }
        }
        public static List<Page> SeedPage(int b)
        {
            int i = 0;
            List<Page> list = new();
            while (true)
            {
                
                Page data = new Faker<Page>()
                .RuleFor(c => c.Title, f => f.Random.Words(4))
                .RuleFor(c => c.Content, f => f.Lorem.Paragraphs(8).ClampLength(200,600))
                .RuleFor(c => c.CreatedAt, f => f.Date.Between(new DateTime(2022, 1, 1), new DateTime(2023, 7, 11)))
            ;
                data.IsActive = true;
                list.Add(data);
                i++;
                if (i == b)
                    return list;
            }
        }
        public static List<CategoryNews> SeedCategoryNews(int b)
        {
            int i = 0;
            List<CategoryNews> list = new();
            while (true)
            {
                
                CategoryNews data = new Faker<CategoryNews>()
                .RuleFor(c => c.CategoryId, f => f.Random.Int(1,10))
                .RuleFor(c => c.NewsId, f => f.Random.Int(1,20))
                
            ;
                list.Add(data);
                i++;
                if (i == b)
                    return list;
            }
        }
        public static List<NewsComment> SeedNewsComment(int b)
        {
            int i = 0;
            List<NewsComment> list = new();
            while (true)
            {

                NewsComment data = new Faker<NewsComment>()
                .RuleFor(c => c.PostId, f => f.Random.Int(1, 20))
                .RuleFor(c => c.UserId, f => f.Random.Int(1, 3))
                .RuleFor(c=>c.Comment, f => f.Lorem.Paragraph(1))
                .RuleFor(c=>c.CreatedAt, f => f.Date.Between(new DateTime(2023, 7, 9), new DateTime(2023, 7, 12)))
            ;
                data.IsActive = true;
                list.Add(data);
                i++;
                if (i == b)
                    return list;
            }
        }
    }
}
