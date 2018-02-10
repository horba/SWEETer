﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sweeter.DataProviders;
using Sweeter.Services.ConnectionFactory;
using Sweeter.Services.HashService;

namespace Sweeter
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));
            services.AddTransient<IAccountDataProvider, AccountDataProvider>();
            services.AddTransient<IPostDataProvider, PostDataProvider>();
            services.AddTransient<ICommentDataProvider, CommentDataProvider>();
            services.AddTransient<ILikesToCommentsProvider, LikesToCommentsProvider>();
            services.AddTransient<ILikesToPostsProvider, LikesToPostsProvider>();
            services.AddTransient<IHashService, HashService>();
            services.AddTransient<IConnectionFactory, ConnectionFactory>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
