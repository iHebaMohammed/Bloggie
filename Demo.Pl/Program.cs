using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Contexts;
using Demo.Pl.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Demo.Pl
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            
            builder.Services.AddDbContext<AppDbContext>(options => {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });
            builder.Services.AddDbContext<AuthAppDbContext>(options => {
                options.UseSqlServer(builder.Configuration.GetConnectionString("BloggieAuth"));
            });
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AuthAppDbContext>();
            builder.Services.Configure<IdentityOptions>(options => { 
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
            });

            builder.Services.AddScoped<ITagRepository , TagRepository>();
            builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
            builder.Services.AddScoped<IImageRepository, CloudinaryImageRepository>();
            builder.Services.AddScoped<IBlogPostLikeRepository, BlogPostLikeRepository>();
            builder.Services.AddScoped<IBlogPostCommentRepository, BlogPostCommentRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddAutoMapper(M => M.AddProfile(new TagProfile()));
            builder.Services.AddAutoMapper(M => M.AddProfile(new BlogPostProfile()));
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
