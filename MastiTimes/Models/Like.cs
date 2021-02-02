using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MastiTimes.Models
{
    public class Like: Database.DatabaseRecord
    {
        public Like()
        {
        }
        internal Like(MySql.Data.MySqlClient.MySqlDataReader dr)
        {
            Fill(dr);
        }

        public int _LikeID { get; set; }
        public int _MovieID { get; set; }
        public int _UserID { get; set; }

        internal const string db_ID = "like_id";
        internal const string db_MovieID = "movie_id";
        internal const string db_UserID = "UserID";

        public override void Fill(MySqlDataReader dr)
        {
            if (!dr.IsDBNull(0))
                _ID = dr.GetInt32(db_ID);
            if (!dr.IsDBNull(1))
                _MovieID = dr.GetInt32(db_MovieID);
            if (!dr.IsDBNull(2))
                _UserID = dr.GetInt32(db_UserID);
        }

        public override string ToString()
        {
            return this.GetType().ToString();
        }
    }
}
