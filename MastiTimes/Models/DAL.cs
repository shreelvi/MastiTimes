using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MastiTimes.Models
{
    public class DAL
    {
        //Database information for the hosting website db
        private static string ReadOnlyConnectionString = "Server=MYSQL5044.site4now.net;Database=db_a6d6cb_achapar;Uid=a6d6cb_achapar;Pwd=x129y190";
        private static string EditOnlyConnectionString = "Server=MYSQL5044.site4now.net;Database=db_a6d6cb_achapar;Uid=a6d6cb_achapar;Pwd=x129y190";
        public static string _Pepper = "gLj23Epo084ioAnRfgoaHyskjasf"; //HACK: set here for now, will move elsewhere later.
        public static int _Stretches = 10000;
        internal enum dbAction
        {
            Read,
            Edit
        }

        #region Database Connections
        internal static void ConnectToDatabase(MySqlCommand comm, dbAction action = dbAction.Read)
        {
            try
            {
                if (action == dbAction.Edit)
                    comm.Connection = new MySqlConnection(EditOnlyConnectionString);
                else
                    comm.Connection = new MySqlConnection(ReadOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
            }
            catch (Exception)
            {
            }
        }

        public static MySqlDataReader GetDataReader(MySqlCommand comm)
        {
            try
            {
                ConnectToDatabase(comm);
                comm.Connection.Open();
                return comm.ExecuteReader();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                return null;
            }
        }


        public static int GetIntReader(MySqlCommand comm)
        {
            try
            {
                ConnectToDatabase(comm);
                comm.Connection.Open();
                int count = Convert.ToInt32(comm.ExecuteScalar());
                return count;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                return 0;
            }
        }


        /// <summary>
        /// </summary>
        /// <param name="comm"></param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        internal static int AddObject(MySqlCommand comm, string parameterName)
        {
            int retInt = 0;
            try
            {
                comm.Connection = new MySqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();
                MySqlParameter retParameter;
                retParameter = comm.Parameters.Add(parameterName, MySqlDbType.Int32);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.ExecuteNonQuery();
                retInt = (int)retParameter.Value;
                comm.Connection.Close();
            }
            catch (Exception ex)
            {
                if (comm.Connection != null)
                    comm.Connection.Close();

                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            return retInt;
        }

        internal static int UpdateObject(MySqlCommand comm)
        {
            int retInt = 0;
            try
            {
                comm.Connection = new MySqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();
                retInt = comm.ExecuteNonQuery();
                comm.Connection.Close();
            }
            catch (Exception ex)
            
            {
                if (comm.Connection != null)
                    comm.Connection.Close();

                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            return retInt;
        }

        #endregion

        #region db methods


        internal static List<string> GetShowTimesMovieTheater(int movie, int theater)
        {
            MySqlCommand comm = new MySqlCommand("get_showtimes_movietheater");
            List<DateTime> retObj = new List<DateTime>();
            List<string> showtimes = new List<string>();
            try
            {
                comm.Parameters.AddWithValue("@" + MovieTheater.db_MovieID, movie);
                comm.Parameters.AddWithValue("@" + MovieTheater.db_TheaterID, theater);
                MySqlDataReader dr = GetDataReader(comm);

                while (dr.Read())
                {
                    retObj.Add(dr.GetDateTime("show_time"));
                }
                comm.Connection.Close();
            }
            catch (Exception ex)
            {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            foreach (var time in retObj)
            {
                showtimes.Add(time.ToString("H:mm"));
            }

            return showtimes;
        }
        internal static List<MovieTheater> GetMovieTimes()
        {
            MySqlCommand comm = new MySqlCommand("get_movie_timess");
            List<MovieTheater> retObj = new List<MovieTheater>();
            try
            {
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    retObj.Add(new MovieTheater(dr));
                }
                comm.Connection.Close();
            }
            catch (Exception ex)
            {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retObj;
        }
        #endregion

        #region movie theater
        internal static int AddMovieTheater(MovieTheater obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("insert_movie_theater");
            try
            {
                comm.Parameters.AddWithValue("@" + MovieTheater.db_MovieID, obj.MovieID);
                comm.Parameters.AddWithValue("@" + MovieTheater.db_TheaterID, obj.TheaterID);
                comm.Parameters.AddWithValue("@" + MovieTheater.db_ShowTime, obj.ShowTime);
                comm.Parameters.AddWithValue("@" + MovieTheater.db_NowPlaying, obj.NowPlaying);
                return AddObject(comm, "@" + MovieTheater.db_ID);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }

        internal static MovieTheater GetMovieTheaterByID(int? id)
        {
            MySqlCommand comm = new MySqlCommand("get_movie_theater");
            MovieTheater retObj = null;
            try
            {
                comm.Parameters.AddWithValue("@" + MovieTheater.db_ID, id);
                MySqlDataReader dr = GetDataReader(comm);

                while (dr.Read())
                {
                    retObj = new MovieTheater(dr);
                }
                comm.Connection.Close();
            }
            catch (Exception ex)
            {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retObj;
        }

        internal static List<MovieTheater> GetTimesByMovie(int? id)
        {
            MySqlCommand comm = new MySqlCommand("get_movie_showtimes");
            List<MovieTheater> retObj = new List<MovieTheater>();
            try
            {
                comm.Parameters.AddWithValue("@" + MovieTheater.db_MovieID, id);
                MySqlDataReader dr = GetDataReader(comm);

                while (dr.Read())
                {
                    retObj.Add(new MovieTheater(dr));
                }
                comm.Connection.Close();
            }
            catch (Exception ex)
            {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retObj;
        }

        internal static int DeleteMovieTheater(int movtheaterID)
        {
            if (movtheaterID == 0) return -1;
            MySqlCommand comm = new MySqlCommand();
            try
            {
                comm.CommandText = "remove_movie_theater";
                comm.Parameters.AddWithValue("@" + MovieTheater.db_ID, movtheaterID);
                return UpdateObject(comm);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }

        internal static int EditMovieTheater(MovieTheater obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("edit_movie_theater");
            try
            {
                comm.Parameters.AddWithValue("@" + MovieTheater.db_ID, obj.ID);
                comm.Parameters.AddWithValue("@" + MovieTheater.db_MovieID, obj.MovieID);
                comm.Parameters.AddWithValue("@" + MovieTheater.db_TheaterID, obj.TheaterID);
                comm.Parameters.AddWithValue("@" + MovieTheater.db_ShowTime, obj.ShowTime);
                comm.Parameters.AddWithValue("@" + MovieTheater.db_NowPlaying, obj.NowPlaying);
                UpdateObject(comm);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return -1;
            }
            return 1;
        }
        #endregion

        #region movie
        internal static List<Movie> GetMovies()
        {
            MySqlCommand comm = new MySqlCommand("get_movies");
            List<Movie> retObj = new List<Movie>();
            try
            {
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    retObj.Add(new Movie(dr));
                }
                comm.Connection.Close();
            }
            catch (Exception ex)
            {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retObj;
        }


        internal static int AddMovie(Movie obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("insert_movie");
            try
            {
                //comm.Parameters.AddWithValue("@" + Movie.db_ID, obj.ID);
                comm.Parameters.AddWithValue("@" + Movie.db_Title, obj.Title);
                comm.Parameters.AddWithValue("@" + Movie.db_Released, obj.DateReleased);
                comm.Parameters.AddWithValue("@" + Movie.db_Poster, obj.PosterUrl);
                comm.Parameters.AddWithValue("@" + Movie.db_Actors, obj.Actors);
                comm.Parameters.AddWithValue("@" + Movie.db_Likes, obj.Likes);
                comm.Parameters.AddWithValue("@" + Movie.db_Rated, obj.Rated);
                comm.Parameters.AddWithValue("@" + Movie.db_ImdbVotes, obj.Votes);
                comm.Parameters.AddWithValue("@" + Movie.db_Rating, obj.Rating);
                comm.Parameters.AddWithValue("@" + Movie.db_Country, obj.Country);
                comm.Parameters.AddWithValue("@" + Movie.db_Language, obj.Language);
                comm.Parameters.AddWithValue("@" + Movie.db_Trailer, obj.Trailer);
                comm.Parameters.AddWithValue("@" + Movie.db_Duration, obj.Duration);
                comm.Parameters.AddWithValue("@" + Movie.db_Genre, obj.Genre);
                comm.Parameters.AddWithValue("@" + Movie.db_Plot, obj.Plot);
                return AddObject(comm, "@" + Movie.db_ID);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }

        internal static int EditMovie(Movie obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("edit_movie");
            try
            {
                comm.Parameters.AddWithValue("@" + Movie.db_ID, obj.ID);
                comm.Parameters.AddWithValue("@" + Movie.db_Released, obj.DateReleased);
                comm.Parameters.AddWithValue("@" + Movie.db_Title, obj.Title);
                comm.Parameters.AddWithValue("@" + Movie.db_Poster, obj.PosterUrl);
                comm.Parameters.AddWithValue("@" + Movie.db_Actors, obj.Actors);
                comm.Parameters.AddWithValue("@" + Movie.db_Rated, obj.Rated);
                comm.Parameters.AddWithValue("@" + Movie.db_ImdbVotes, obj.Votes);
                comm.Parameters.AddWithValue("@" + Movie.db_Rating, obj.Rating);
                comm.Parameters.AddWithValue("@" + Movie.db_Country, obj.Country);
                comm.Parameters.AddWithValue("@" + Movie.db_Language, obj.Language);
                comm.Parameters.AddWithValue("@" + Movie.db_Trailer, obj.Trailer);
                comm.Parameters.AddWithValue("@" + Movie.db_Likes, obj.Likes);
                comm.Parameters.AddWithValue("@" + Movie.db_Duration, obj.Duration);
                comm.Parameters.AddWithValue("@" + Movie.db_Genre, obj.Genre);
                comm.Parameters.AddWithValue("@" + Movie.db_Plot, obj.Plot);
                UpdateObject(comm);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return -1;
            }
            return 1;
        }

        internal static int DeleteMovie(int movieID)
        {
            if (movieID == 0) return -1;
            MySqlCommand comm = new MySqlCommand();
            try
            {
                comm.CommandText = "remove_movie";
                comm.Parameters.AddWithValue("@" + Movie.db_ID, movieID);
                return UpdateObject(comm);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }
        
        internal static Movie GetMovieByID(int? id)
        {
            MySqlCommand comm = new MySqlCommand("get_movie");
            Movie retObj = null;
            try
            {
                comm.Parameters.AddWithValue("@" + Movie.db_ID, id);
                MySqlDataReader dr = GetDataReader(comm);

                while (dr.Read())
                {
                    retObj = new Movie(dr);
                }
                comm.Connection.Close();
            }
            catch (Exception ex)
            {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retObj;
        }

        #endregion

        #region theater
        internal static Theater GetTheaterByID(int? id)
        {
            MySqlCommand comm = new MySqlCommand("get_theater");
            Theater retObj = null;
            try
            {
                comm.Parameters.AddWithValue("@" + Theater.db_ID, id);
                MySqlDataReader dr = GetDataReader(comm);

                while (dr.Read())
                {
                    retObj = new Theater(dr);
                }
                comm.Connection.Close();
            }
            catch (Exception ex)
            {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retObj;
        }


        internal static List<Theater> GetTheaters()
        {
            MySqlCommand comm = new MySqlCommand("get_theaters");
            List<Theater> retObj = new List<Theater>();
            try
            {
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    retObj.Add(new Theater(dr));
                }
                comm.Connection.Close();
            }
            catch (Exception ex)
            {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retObj;
        }

        internal static async Task<List<MovieTheater>> GetNowShowingMovies()
        {
            MySqlCommand comm = new MySqlCommand("get_now_showing_movies");
            List<MovieTheater> retObj = new List<MovieTheater>();
            try
            {
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader dr =  GetDataReader(comm);
                while (dr.Read())
                {
                    retObj.Add(new MovieTheater(dr));
                }
                comm.Connection.Close();
            }
            catch (Exception ex)
            {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retObj;
        }

        internal static List<Theater> GetTheatersByCity(string city)
        {
            MySqlCommand comm = new MySqlCommand("get_theaters_by_city");
            List<Theater> retObj = new List<Theater>();
            try
            {
                comm.Parameters.AddWithValue("@" + Theater.db_City, city);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    retObj.Add(new Theater(dr));
                }
                comm.Connection.Close();
            }
            catch (Exception ex)
            {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retObj;
        }

        internal static List<MovieShowtimes> GetNowPlayingMoviesByTheater(int id)
        {
            MySqlCommand comm = new MySqlCommand("get_movies_by_theater");
            List<MovieShowtimes> retObj = new List<MovieShowtimes>();
            try
            {
                comm.Parameters.AddWithValue("@" + Theater.db_ID, id);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    retObj.Add(new MovieShowtimes(dr));
                }
                comm.Connection.Close();
            }
            catch (Exception ex)
            {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retObj;
        }

        internal static int AddTheater(Theater obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("insert_theater");
            try
            {
                comm.Parameters.AddWithValue("@" + Theater.db_Name, obj.Name);
                comm.Parameters.AddWithValue("@" + Theater.db_Address, obj.Address);
                comm.Parameters.AddWithValue("@" + Theater.db_City, obj.City);
                comm.Parameters.AddWithValue("@" + Theater.db_Likes, obj.Likes);
                comm.Parameters.AddWithValue("@" + Theater.db_Phone, obj.PhoneNumber);
                return AddObject(comm, "@" + Theater.db_ID);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }

        internal static int EditTheater(Theater obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("edit_theater");
            try
            {
                comm.Parameters.AddWithValue("@" + Theater.db_ID, obj.ID);
                comm.Parameters.AddWithValue("@" + Theater.db_Name, obj.Name);
                comm.Parameters.AddWithValue("@" + Theater.db_Address, obj.Address);
                comm.Parameters.AddWithValue("@" + Theater.db_City, obj.City);
                comm.Parameters.AddWithValue("@" + Theater.db_Likes, obj.Likes);
                comm.Parameters.AddWithValue("@" + Theater.db_Phone, obj.PhoneNumber);
                UpdateObject(comm);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return -1;
            }
            return 1;
        }

        internal static int DeleteTheater(int theaterID)
        {
            if (theaterID == 0) return -1;
            MySqlCommand comm = new MySqlCommand();
            try
            {
                comm.CommandText = "remove_theater";
                comm.Parameters.AddWithValue("@" + Theater.db_ID, theaterID);
                return UpdateObject(comm);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }
        #endregion

        public static User GetUser(string userName, string password)
        {

            MySqlCommand comm = new MySqlCommand("GetUserByUserName");
            User retObj = null;
            try
            {
                comm.Parameters.AddWithValue("@" + User.db_UserName, userName);
                MySqlDataReader dr = GetDataReader(comm);
                while (dr.Read())
                {
                    retObj = new User(dr);
                }
                comm.Connection.Close();
            }
            catch (Exception ex)
            {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            //Verify password matches.
            if (retObj != null)
            {
                if (!Tools.Hasher.IsValid(password, retObj.Salt, _Pepper, _Stretches, retObj.Password))
                {
                    retObj = null;
                }
            }

            return retObj;

        }

        internal static int AddUser(User obj)
        {
            if (obj == null) return -1;
            MySqlCommand comm = new MySqlCommand("sproc_UserAdd");
            try
            {
                // generate new password first.
                obj.Salt = Tools.Hasher.GenerateSalt(50);
                string newPass = Tools.Hasher.Get(obj.Password, obj.Salt, _Pepper, _Stretches, 64);
                obj.Password = newPass;
                // now set object to Database.
                comm.Parameters.AddWithValue("@" + User.db_EmailAddress, obj.EmailAddress);
                comm.Parameters.AddWithValue("@" + User.db_UserName, obj.UserName);
                comm.Parameters.AddWithValue("@" + User.db_Password, obj.Password);
                comm.Parameters.AddWithValue("@" + User.db_Salt, obj.Salt);
                return AddObject(comm, "@" + User.db_ID);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }


        ///<summary>
        /// Check if username exists in the database
        /// </summary>
        /// <remarks></remarks>
        internal static int CheckUserExists(string username)
        {
            if (username == null) return -1;
            MySqlCommand comm = new MySqlCommand("sproc_CheckUserName");
            try
            {
                comm.Parameters.AddWithValue("@" + User.db_UserName, username);
                int dr = GetIntReader(comm);

                return dr;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return -1;
        }

    }
}
