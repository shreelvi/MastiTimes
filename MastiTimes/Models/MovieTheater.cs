using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MastiTimes.Models
{
    /// <summary>
    /// Class that hold information about movie and theater
    /// Used to display showtimes for a movie
    /// </summary>
    public class MovieTheater: Database.DatabaseRecord
    {
        #region Constructors
        public MovieTheater()
        {
        }
        internal MovieTheater (MySql.Data.MySqlClient.MySqlDataReader dr)
        {
            Fill(dr);
        }

        #endregion

        #region Private Variables
        private int _MovieID;
        private int _TheaterID;
        private DateTime _ShowTime;
        private bool _NowPlaying;
        private Movie _Movie;
        private Theater _Theater;
        private List<string> _ShowTimes;
        #endregion

        #region Public Properties
        [Required]
        public int MovieID
        {
            get
            {
                return _MovieID;
            }

            set
            {
                _MovieID = value;
            }
        }

        [Required]
        public int TheaterID
        {
            get
            {
                return _TheaterID;
            }

            set
            {
                _TheaterID = value;
            }
        }

        [Required]
        public DateTime ShowTime
        {
            get
            {
                return _ShowTime;
            }

            set
            {
                _ShowTime = value;
            }
        }

        [Required]
        public bool NowPlaying
        {
            get
            {
                return _NowPlaying;
            }

            set
            {
                _NowPlaying = value;
            }
        }


        /// <summary>
        /// Gets or sets the movie for this object.
        /// </summary>
        /// <remarks></remarks>
        [XmlIgnore]
        public Movie Movie
        {
            get
            {
                if (_Movie == null)
                {
                    _Movie = DAL.GetMovieByID(_MovieID);
                }
                return _Movie;
            }
            set
            {
                _Movie = value;
                if (value == null)
                {
                    _MovieID = -1;
                }
                else
                {
                    _MovieID = value.ID;
                }
            }
        }

        /// <summary>
        /// Gets or sets the theater for this object.
        /// </summary>
        /// <remarks></remarks>
        [XmlIgnore]
        public Theater Theater
        {
            get
            {
                if (_Theater == null)
                {
                    _Theater = DAL.GetTheaterByID(_TheaterID);
                }
                return _Theater;
            }
            set
            {
                _Theater = value;
                if (value == null)
                {
                    _TheaterID = -1;
                }
                else
                {
                    _TheaterID = value.ID;
                }
            }
        }

        /// <summary>
        /// Gets or sets the list of showtimes for this 
        /// movie theater object.
        /// </summary>
        /// <remarks></remarks>
        [XmlIgnore]
        public List<string> ShowTimes
        {
            get
            {
                if (_ShowTimes == null)
                {
                    _ShowTimes = DAL.GetShowTimesMovieTheater(_MovieID, _TheaterID);
                }
                return _ShowTimes;
            }
            set
            {
                _ShowTimes = value;
            }
        }
        #endregion

        #region Database String
        internal const string db_ID = "movie_theater_id";
        internal const string db_MovieID = "movie_id";
        internal const string db_TheaterID = "theater_id";
        internal const string db_ShowTime = "show_time";
        internal const string db_NowPlaying = "now_playing";
        #endregion

        #region Public Subs
        public override void Fill(MySqlDataReader dr)
        {
            
            _MovieID = dr.GetInt32(db_MovieID);
            _ID = dr.GetInt32(db_ID);
            _TheaterID = dr.GetInt32(db_TheaterID);
            _ShowTime = dr.GetDateTime(db_ShowTime);
            _NowPlaying = dr.GetBoolean(db_NowPlaying);
        }
        #endregion

        #region public methods
        public List<MovieTheater> GetMovieTimes()
        {
            var MovieTimes = DAL.GetMovieTimes();
            return MovieTimes;
        }
        #endregion

        public override string ToString()
        {
            return this.GetType().ToString();
        }
    }
}
