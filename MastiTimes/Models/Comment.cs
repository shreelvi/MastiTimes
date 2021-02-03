using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MastiTimes.Models
{
    public class Comment: Database.DatabaseRecord
    {
        public Comment()
        {
        }

        internal Comment(MySql.Data.MySqlClient.MySqlDataReader dr)
        {
            Fill(dr);
        }

        public int _CommentID { get; set; }
        public int _MovieID { get; set; }
        public int _UserID { get; set; }
        public string _Comment { get; set; }
        public DateTime _DateCreated { get; set; }

        internal const string db_ID = "comment_id";
        internal const string db_MovieID = "movie_id";
        internal const string db_UserID = "UserID";
        internal const string db_Comment = "comment";
        internal const string db_DateCreated = "date_created";

        public override void Fill(MySqlDataReader dr)
        {
            if (!dr.IsDBNull(0))
                _ID = dr.GetInt32(db_ID);
            if (!dr.IsDBNull(1))
                _MovieID = dr.GetInt32(db_MovieID);
            if (!dr.IsDBNull(2))
                _UserID = dr.GetInt32(db_UserID);
            if (!dr.IsDBNull(3))
                _Comment = dr.GetString(db_Comment);
            if (!dr.IsDBNull(4))
                _DateCreated = dr.GetDateTime(db_DateCreated);
        }

        public override string ToString()
        {
            return this.GetType().ToString();
        }
    }
}
