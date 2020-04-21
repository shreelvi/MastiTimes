using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// This class is a root class to model JSON results for movies
/// </summary>

namespace MastiTimes.Models
{
    public class Movies
    {
        public List<Results> results { get; set; } //tmdb db produce output as 'results' array
        public List<Results> search { get; set; } //omdb results map
        public int page { get; set; }

        /// <summary>
        /// Get now playing movies from the TMDB API DB.
        /// </summary>
        /// <returns></returns>
        public List<Results> getUpcomingMovies()
        {
            string url = "https://api.themoviedb.org/3/movie/upcoming?api_key=312856db5a65474581b8885d46fc2c75&language=en-US&page=1";
            string poster = "https://image.tmdb.org/t/p/w500";
            //synchronous client.
            var client = new WebClient();
            var content = client.DownloadString(url);
            dynamic jsonContent = JsonConvert.DeserializeObject(content);

            List<Results> movies = new List<Results>();

            foreach (var obj in jsonContent.results)
            {
                Results movie = new Results();
                movie.title = obj.title;
                movie.poster_path = poster + obj.poster_path;
                movie.ID = obj.id;
                movies.Add(movie);
            }
            return movies;
        }


        public List<Results> getNowPlayingMovies()
        {
            string url = "https://api.themoviedb.org/3/movie/now_playing?api_key=312856db5a65474581b8885d46fc2c75&language=en-US&page=1";
            string poster = "https://image.tmdb.org/t/p/w500";
            //synchronous client.
            var client = new WebClient();
            var content = client.DownloadString(url);
            dynamic jsonContent = JsonConvert.DeserializeObject(content);

            List<Results> movies = new List<Results>();

            foreach (var obj in jsonContent.results)
            {
                Results movie = new Results();
                movie.title = obj.title;
                movie.poster_path = poster + obj.poster_path;
                movie.ID = obj.id;
                movies.Add(movie);
            }
            return movies;
        }

        public List<TrailerViewModel> getMovieTrailers()
        {
            List<Results> nowPlaying = getNowPlayingMovies();
            string url;
            string poster = "https://image.tmdb.org/t/p/w500";
            List<TrailerViewModel> trailers = new List<TrailerViewModel>();

            //Get youtube key for trailers.
            for (int i = 0; i < 4; i++)
            {
                TrailerViewModel trailer = new TrailerViewModel();
                url = "https://api.themoviedb.org/3/movie/" + nowPlaying[i].ID + "/videos?api_key=312856db5a65474581b8885d46fc2c75&language=en-US";
                //synchronous client.
                var client = new WebClient();
                var content = client.DownloadString(url);
                dynamic jsonContent = JsonConvert.DeserializeObject(content);

                trailer.key = jsonContent.results[0].key;
                trailers.Add(trailer);          
            }

            //Get backdrop posters
            for (int i = 0; i < trailers.Count; i++)
            {
                url = "https://api.themoviedb.org/3/movie/" + nowPlaying[i].ID + "?api_key=312856db5a65474581b8885d46fc2c75&language=en-US";
                //synchronous client.
                var client = new WebClient();
                var content = client.DownloadString(url);
                dynamic jsonContent = JsonConvert.DeserializeObject(content);
                trailers[i].backdrop_path = poster + jsonContent.backdrop_path;
                trailers[i].title = jsonContent.title;
            }

            return trailers;
        }

        public List<TrailerViewModel> GetNowPlayingTrailers()
        {
            List<TrailerViewModel> trailers = new List<TrailerViewModel>();
            trailers = getMovieTrailers();
            return trailers;
        }


        /// <summary>
        /// Get movies by search text from OMDB database
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public List<Results> getMoviesBySearchText(string text)
        {
            string url = "https://api.themoviedb.org/3/search/movie?api_key=312856db5a65474581b8885d46fc2c75&language=en-US&query=" + text + "&page=1";
            string poster = "https://image.tmdb.org/t/p/w500";
            var client = new WebClient();
            var content = client.DownloadString(url);
            dynamic jsonContent = JsonConvert.DeserializeObject(content);

            List<Results> movies = new List<Results>();

            if (((JObject)jsonContent).Count == 0)
            {
                return movies;//return empty list
            }
            else
            {
                foreach (var obj in jsonContent.results)
                {
                    Results movie = new Results();
                    movie.title = obj.title;
                    movie.poster_path = poster + obj.poster_path;
                    movie.ID = obj.id;
                    movie.Released = obj.release_date;
                    movies.Add(movie);

                }
            }
            return movies;
        }


        public Search getSelectedMovie(int id)
        {
            //Get Imdb id using movie id passed as param.
            string imdb = getImdbID(id);
            string url = "http://www.omdbapi.com/?apikey=de835211&i=" + imdb;
            var client = new WebClient();
            var content = client.DownloadString(url);
            dynamic jsonContent = JsonConvert.DeserializeObject(content);

            Search movie = new Search();

            if (((JObject)jsonContent).Count == 0)
            {
                return movie;//return empty 
            }
            else
            {
                movie.Title = jsonContent.Title; // Retrieve info from json obj
                movie.Poster = jsonContent.Poster;
                movie.Released = jsonContent.Released;
                movie.ImdbRating = jsonContent.imdbRating;
                movie.Rated = jsonContent.Rated;
                movie.Genre = jsonContent.Genre;
                movie.Plot = jsonContent.Plot;
                movie.ImdbVotes = jsonContent.imdbVotes;

            }
            return movie;
        }


        public string getImdbID(int id)
        {
            string url = "https://api.themoviedb.org/3/movie/" + id + "?api_key=312856db5a65474581b8885d46fc2c75";
            var client = new WebClient();
            var content = client.DownloadString(url);
            dynamic jsonContent = JsonConvert.DeserializeObject(content);

            return jsonContent.imdb_id;

        }
    }
}
