using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MastiTimes.Models
{
    public class Theater : Database.DatabaseRecord
    {
        #region Constructors
        public Theater()
        {
        }
        internal Theater(MySqlDataReader dr)
        {
            Fill(dr);
        }
        #endregion

        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public int Likes { get; set; }
        public List<Movie> _nowPlayingMoviesByMovie{get;set;}
        [NotMapped]
        public List<Movie> _NowPlayingMovies;
        public int MovieId;

        public List<Movie> NowPlayingMovies
        {
            get
            {
                _NowPlayingMovies = DAL.GetNowPlayingMoviesByTheater(_ID);
                return _NowPlayingMovies;
            }
            set
            {
                _NowPlayingMovies = value;

            }
        }

        public List<Movie> NowPlayingMoviesByMovie
        {
            get
            {
                _NowPlayingMovies = DAL.GetNowPlayingMoviesByMovieTheater(_ID, MovieId);
                return _NowPlayingMovies;
            }
            set
            {
                _NowPlayingMovies = value;

            }
        }

        #region Database String
        internal const string db_ID = "theater_id";
        internal const string db_Name = "theater_name";
        internal const string db_Address = "address";
        internal const string db_City = "city";
        internal const string db_Phone = "phone_number";
        internal const string db_Likes = "likes";
        #endregion

        

        #region Public Subs
        public override void Fill(MySqlDataReader dr)
        {
            _ID = dr.GetInt32(db_ID);
            Name = dr.GetString(db_Name);
            if(!dr.IsDBNull(2))
                Address = dr.GetString(db_Address);
            if (!dr.IsDBNull(3))
                City = dr.GetString(db_City);
            if (!dr.IsDBNull(4))
                PhoneNumber = dr.GetString(db_Phone);
            if (!dr.IsDBNull(5))
                Likes = dr.GetInt32(db_Likes);

        }
        #endregion

        public override string ToString()
        {
            return this.GetType().ToString();
        }

        internal List<Theater> GetTheatersByCity(string city)
        {
            return DAL.GetTheatersByCity(city);
        }

       
    }
}
