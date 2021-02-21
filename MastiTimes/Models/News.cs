﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MastiTimes.Models
{
    /// <summary>
    /// Class to get news from api
    /// </summary>
    public class News
    {
        public List<Articles> articles { get; set; }

        public async Task<List<Articles>> getBollyWoodNews()
        {
            string url = "http://newsapi.org/v2/top-headlines?country=in&category=entertainment&apiKey=f2fb14b93057475b84c589edf3909959";
            using (HttpClient client = new HttpClient())
            {
                var content = await client.GetStringAsync(url);
                dynamic jsonContent = JsonConvert.DeserializeObject(content);

                List<Articles> articles = new List<Articles>();

                foreach (var obj in jsonContent.articles)
                {
                    Articles article = new Articles();
                    article.title = obj.title;
                    article.url = obj.url;
                    article.urlToImage = obj.urlToImage;
                    article.description = obj.description;
                    article.publishedAt = obj.publishedAt;
                    articles.Add(article);

                }
                return articles;
            }
        }

        public async Task<List<Articles>> getHollywoodNews()
        {
            string url = "http://newsapi.org/v2/top-headlines?country=us&category=entertainment&apiKey=f2fb14b93057475b84c589edf3909959";
            //synchronous client.
            var client = new HttpClient();
            var content = await client.GetStringAsync(url);
            dynamic jsonContent = JsonConvert.DeserializeObject(content);

            List<Articles> articles = new List<Articles>();

            foreach (var obj in jsonContent.articles)
            {
                Articles article = new Articles();
                article.title = obj.title;
                article.url = obj.url;
                article.urlToImage = obj.urlToImage;
                article.description = obj.description;
                article.publishedAt = obj.publishedAt;
                articles.Add(article);

            }
            return articles;
        }
    }
}
