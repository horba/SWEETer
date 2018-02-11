﻿using Microsoft.AspNetCore.Mvc;
using Sweeter.DataProviders;
using Sweeter.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace Sweeter.Controllers
{

    [Route("/Posts")]
    public class PostsController : Controller
    {
        private IPostDataProvider postDataProvider;
        private IAccountDataProvider accountDataProvider;
        public PostsController(IPostDataProvider postData, IAccountDataProvider accountData)
        {
            this.postDataProvider = postData;
            this.accountDataProvider = accountData;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
       

        
        [HttpGet]
        public ActionResult Index(int id = 80003)
        {
            //int id = Convert.ToInt32(Request.Cookies["0"]);
            IEnumerable<PostsModel> feeds = postDataProvider.GetPosts();
            AccountModel account = accountDataProvider.GetAccount(id);
            byte[] ImageData = account.Avatar;
            string path = "wwwroot/ForPics/av" + id.ToString() + ".jpeg";
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                fs.Write(ImageData, 0, ImageData.Length);
            }
            ViewData["Pic"] = path.Substring(7);
            ViewData["Username"] = account.Username;
            return View();
        }
             

          
        // GET api/values
        /*[HttpGet]
         * 
        private IPostDataProvider postDataProvider;
        public PostsController(IPostDataProvider postData)
        {
            this.postDataProvider = postData;
        }
        public async Task<IEnumerable<PostsModel>> Get()
        {
            return await this.postDataProvider.GetPosts();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<PostsModel> Get(int id)
        {
            return await this.postDataProvider.GetPost(id); ;
        }

        // POST api/values
        [HttpPost]
        public async void Post([FromBody]PostsModel postsModel)
        {
            await this.postDataProvider.AddPost(postsModel);

        }
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody]PostsModel post)
        {
            await this.postDataProvider.UpdatePost(post);
        }*/

    }
}
