﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using ZendeskApi_v2.Requests.HelpCenter;

namespace DreamTeam_Blog.Models
{
    public class BlogPostModel
    {
        //proprietati jenerale  
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<string> Tags { get; set; }
          
        public DateTime CreateTime { get; set; }

        
    }

    // clasa care ruleaza contentul  
    public class PostManager
    {
        // Define the members  
        private static string PostsFile = HttpContext.Current.Server.MapPath("~/App_Data/Posts.json");
        private static List<BlogPostModel> posts = new List<BlogPostModel>();

        // The CRUD functions  
        public static void Create(string postJson)
        {
            var obj = JsonConvert.DeserializeObject<BlogPostModel>(postJson);

            if (posts.Count > 0)
            {
                posts = (from post in posts
                         orderby post.CreateTime
                         select post).ToList();
                obj.ID = posts.Last().ID + 1;
            }
            else
            {
                obj.ID = 1;
            }
        }

        posts.Add(obj);  
          save();
    }

    public static List<BlogPostModel> Read()
    {
        // Check if the file exists.  
        if (!File.Exists(PostsFile))
        {
            File.Create(PostsFile).Close();
            File.WriteAllText(PostsFile, "[]"); // Create the file if it doesn't exist.  
}
        posts = JsonConvert.DeserializeObject<List<BlogPostModel>>(File.ReadAllText(PostsFile));
        return posts;
    }

    public static void Update(int id, string postJson)
    {
        Delete(id);
        Create(postJson);
        save();
    }

    public static void Delete(int id)
{
posts.Remove(posts.Find(x => x.ID == id));
save();
    }

    // Output function  
    private static void save()
{
        File.WriteAllText(PostsFile, JsonConvert.SerializeObject(posts));
    }
}  
}