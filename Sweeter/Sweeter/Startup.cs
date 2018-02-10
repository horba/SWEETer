﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sweeter.DataProviders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Sweeter
{
    public class Startup
    {
		public Startup(IConfiguration configuration)
		{
		Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           
            services.AddTransient<IAccountDataProvider, AccountDataProvider>();
           services.AddTransient<IPostDataProvider, PostDataProvider>();
        services.AddTransient<ICommentDataProvider, CommentDataProvider>();
		services.AddTransient<ILikesToCommentsProvider, LikesToCommentsProvider>();
          services.AddTransient<ILikesToPostsProvider, LikesToPostsProvider>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
 .AddCookie();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseStaticFiles();
       
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
