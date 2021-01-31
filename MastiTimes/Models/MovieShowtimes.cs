using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MastiTimes.Models
{
    public class MovieShowtimes : Database.DatabaseRecord
    {
        #region Constructors
        public MovieShowtimes()
        {
        }
        internal MovieShowtimes(MySqlDataReader dr)
        {
            Fill(dr);
        }
        #endregion

        public string Title { get; set; }
        public string PosterUrl { get; set; }
        public string Actors { get; set; }
        public string Rated { get; set; }
        public string Rating { get; set; }
        public DateTime Showtime { get; set; }

        #region Database String
        internal const string db_ID = "movie_id";
        internal const string db_Title = "movie_title";
        internal const string db_Poster = "poster_url";
        internal const string db_Actors = "actors";
        internal const string db_Rated = "rated";
        internal const string db_Rating = "rating";
        internal const string db_Showtime = "show_time";
        #endregion

        public override void Fill(MySqlDataReader dr)
        {
            _ID = dr.GetInt32(db_ID);
            if (!dr.IsDBNull(1))
                Title = dr.GetString(db_Title);
            if (!dr.IsDBNull(2))
                PosterUrl = dr.GetString(db_Poster);
            if (!dr.IsDBNull(3))
                Actors = dr.GetString(db_Actors);
            if (!dr.IsDBNull(4))
                Rated = dr.GetString(db_Rated);
            if (!dr.IsDBNull(5))
                Rating = dr.GetString(db_Rating);
            if (!dr.IsDBNull(6))
                Showtime = dr.GetDateTime(db_Showtime);
        }

        public override string ToString()
        {
            return this.GetType().ToString();
        }
    }
}

