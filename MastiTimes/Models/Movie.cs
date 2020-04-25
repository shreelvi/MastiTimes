using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MastiTimes.Models
{
    public class Movie : Database.DatabaseRecord
    {
        #region Constructors
        public Movie()
        {
        }
        internal Movie(MySqlDataReader dr)
        {
            Fill(dr);
        }
        #endregion

        public string Title { get; set; }
        public DateTime DateReleased { get; set; }
        public string PosterUrl { get; set; }
        public string Actors { get; set; }
        public int Likes { get; set; }
        public string Reviews { get; set; }
        public string Rated { get; set; }
        public string Votes { get; set; }
        public string Rating { get; set; }
        public string Country { get; set; }
        public string Language { get; set; }
        public string Trailer { get; set; }


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
        #endregion

        #region Public Subs
        public override void Fill(MySqlDataReader dr)
        {
            _ID = dr.GetInt32(db_ID);
            Title = dr.GetString(db_Title);
            DateReleased = dr.GetDateTime(db_Released);
            PosterUrl = dr.GetString(db_Poster);
            Actors = dr.GetString(db_Actors);
            Likes = dr.GetInt32(db_Likes);
            Rated = dr.GetString(db_Rated);
            Votes = dr.GetString(db_ImdbVotes);
            Rating = dr.GetString(db_Rating);
            Country = dr.GetString(db_Country);
            Language = dr.GetString(db_Language);
            Trailer = dr.GetString(db_Trailer);
        }
        #endregion

        public override string ToString()
        {
            return this.GetType().ToString();
        }

    }
}
