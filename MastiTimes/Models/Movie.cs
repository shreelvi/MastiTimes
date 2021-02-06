using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MastiTimes.Models
{
    /// <summary>
    /// Movie is a class that hold information about movies 
    /// that are now showing, coming soon in a particular place and time. 
    /// The records of this class
    /// is modified and stored directly in our db
    /// </summary>
    public class Movie : Database.DatabaseRecord
    {
        #region Constructors
        public Movie()
        {
        }
        public Movie(int theater)
        {
            TheaterId = theater;
        }
        internal Movie(MySqlDataReader dr)
        {
            Fill(dr);
        }
        internal Movie(MySqlDataReader dr, int theater)
        {
            TheaterId = theater;
            Fill(dr);
        }
        #endregion

    
        public string Title { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateReleased { get; set; }
        public string PosterUrl { get; set; }
        public string Actors { get; set; }
        public int Likes { get; set; }
        public string Rated { get; set; }
        public string Votes { get; set; }
        public string Rating { get; set; }
        public string Country { get; set; }
        public string Language { get; set; }
        public string Trailer { get; set; }
        public string Duration { get; set; }
        public string Genre { get; set; }
        public string Plot { get; set; }
        public int TheaterId { get; set; }

        private List<DateTime> _showtimes;
        public List<DateTime> showtimes
        {
            get
            {
                _showtimes = DAL.GetMovieShowtimesByTheater(_ID, TheaterId);
                return _showtimes;
            }
            set
            {
                _showtimes = value;

            }
        }
        
        [NotMapped]
        public List<Theater> theaters { get; set; }


        #region Database String
        internal const string db_ID = "movie_id";
        internal const string db_Title = "movie_title";
        internal const string db_Released = "movie_released";
        internal const string db_Poster = "poster_url";
        internal const string db_Actors = "actors";
        internal const string db_Likes = "likes";
        internal const string db_Rated = "rated";
        internal const string db_ImdbVotes = "imdb_votes";
        internal const string db_Rating = "rating";
        internal const string db_Country = "country";
        internal const string db_Language = "language";
        internal const string db_Trailer = "trailer";
        internal const string db_Duration = "duration";
        internal const string db_Genre = "genre";
        internal const string db_Plot = "plot";

        #endregion

        #region Public Subs
        public override void Fill(MySqlDataReader dr)
        {
            _ID = dr.GetInt32(db_ID);
            if (!dr.IsDBNull(1))
                DateReleased = dr.GetDateTime(db_Released);
            Title = dr.GetString(db_Title);
            if(!dr.IsDBNull(3))
                PosterUrl = dr.GetString(db_Poster);
            if (!dr.IsDBNull(4))
                Actors = dr.GetString(db_Actors);
            if (!dr.IsDBNull(5))
                Rated = dr.GetString(db_Rated);
            if (!dr.IsDBNull(6))
                Votes = dr.GetString(db_ImdbVotes);
            if (!dr.IsDBNull(7))
                Rating = dr.GetString(db_Rating);
            if (!dr.IsDBNull(8))
                Country = dr.GetString(db_Country);
            if (!dr.IsDBNull(9))
                Language = dr.GetString(db_Language);
            if (!dr.IsDBNull(10))
                Trailer = dr.GetString(db_Trailer);
            if (!dr.IsDBNull(11))
                Likes = dr.GetInt32(db_Likes);
            if (!dr.IsDBNull(12))
                Duration = dr.GetString(db_Duration);
            if (!dr.IsDBNull(13))
                Genre = dr.GetString(db_Genre);
            if (!dr.IsDBNull(14))
                Plot = dr.GetString(db_Plot);
        }
        #endregion

        public override string ToString()
        {
            return this.GetType().ToString();
        }

        public bool InsertMovie()
        {
            string url = "https://api.themoviedb.org/3/movie/now_playing?api_key=312856db5a65474581b8885d46fc2c75&language=en-US&page=1";
            string poster = "https://image.tmdb.org/t/p/w500";
            //synchronous client.
            var client = new WebClient();
            var content = client.DownloadString(url);
            dynamic jsonContent = JsonConvert.DeserializeObject(content);

            List<Movie> movies = new List<Movie>();

            foreach (var obj in jsonContent.results)
            {
                Movie movie = new Movie();
                movie.Title = obj.title;
                movie.PosterUrl = poster + obj.poster_path;
                movie.ID = obj.id;
                movie.Votes = obj.vote_average;
                GetMovieDetail(movie);
            }


            return true;
        }

        public void GetMovieDetail(Movie movie)
        {
            //Get Imdb id using movie id passed as param.
            string imdb = getImdbID(movie.ID);
            string url = "http://www.omdbapi.com/?apikey=de835211&i=" + imdb;
            var client = new WebClient();
            var content = client.DownloadString(url);
            dynamic jsonContent = JsonConvert.DeserializeObject(content);

            if (((JObject)jsonContent).Count == 0)
            {
                return;//return empty 
            }
            else
            {
                movie.Title = jsonContent.Title; // Retrieve info from json obj
                movie.DateReleased = jsonContent.Released;
                movie.Rated = jsonContent.Rated;
                movie.Genre = jsonContent.Genre;
                //movie.Votes = jsonContent.imdbVotes;
                movie.Rating = jsonContent.imdbRating;
                movie.Rated = jsonContent.Rated;
                movie.Actors = jsonContent.Actors;
                movie.Country = jsonContent.Country;
                movie.Language = jsonContent.Language;
                movie.Likes = 11;
                movie.Duration = "test";
                movie.Trailer = "test";
                movie.Plot = "plot";
            }
            int i = DAL.AddMovie(movie);
        }

        public string getImdbID(int id)
        {
            string url = "https://api.themoviedb.org/3/movie/" + id + "?api_key=312856db5a65474581b8885d46fc2c75";
            var client = new WebClient();
            var content = client.DownloadString(url);
            dynamic jsonContent = JsonConvert.DeserializeObject(content);

            return jsonContent.imdb_id;

        }

        public Search getNepalMovie(int id)
        {
            var mov = DAL.GetMovieByID(id);

            Search movie = new Search();

            if (mov == null)
            {
                return movie;//return empty 
            }
            else
            {
                movie.Title = mov.Title; // Retrieve info from json obj
                movie.Poster = mov.PosterUrl;
                movie.Released = mov.DateReleased.ToString();
                movie.Rated = mov.Rated;
                string Gen = mov.Genre;
                movie.Genre = Gen.Split(',').ToList<string>();
                movie.Plot = mov.Plot;
                movie.ImdbVotes = mov.Votes;
                movie.ImdbRating = mov.Rating;
                movie.Actors = mov.Actors;
                movie.Directors = "N/A";
                movie.Country = mov.Country;
                movie.Language = mov.Language;
                movie.Duration = mov.Duration;
            }
            return movie;
        }
    }
}
