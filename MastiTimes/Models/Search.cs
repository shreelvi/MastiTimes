using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MastiTimes.Models
{
    /// <summary>
    /// This class is used to get output from omdb database
    /// As the output is provided as 'Search' array, we create 
    /// the class with same properties as the output here.
    /// </summary>
    public class Search
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Poster { get; set; }
        public string Released { get; set; }
        public string ImdbID { get; set; }
        public string ImdbRating { get; set; }
        public string ImdbVotes { get; set; }
        public string Rated { get; set; }
        public string Plot { get; set; }
        public List<string> Genre { get; set; }
        public string Actors { get; set; }
        public string Directors { get; set; }
        public string Country { get; set; }
        public string Language { get; set; }
        public string Duration { get; set; }
    }
}
