using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MastiTimes.Models
{
    /// <summary>
    /// This class is used to get output from tmdb database
    /// As the output is provided as 'Results' array, we create 
    /// the class with same properties as the output here.
    /// </summary>
    public class Results
    {
        public string ID { get; set; }
        public string title { get; set; }
        public string imdbID { get; set; }
        public string Released { get; set; }
        public string poster_path { get; set; }

    }
}
