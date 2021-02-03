using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MastiTimes.Models
{
    public class MovieComment
    {
        public MovieComment()
        {
        }

        internal MovieComment(MySql.Data.MySqlClient.MySqlDataReader dr)
        {
            Fill(dr);
        }
        public string comment { get; set; }
        public DateTime dateCreated { get; set; }
        public string userName { get; set; }

        internal const string db_comment = "comment";
        internal const string db_dateCreated = "date_created";
        internal const string db_userName = "UserName";

        public void Fill(MySqlDataReader dr)
        {
            if (!dr.IsDBNull(0))
                comment = dr.GetString(db_comment);
            if (!dr.IsDBNull(1))
                dateCreated = dr.GetDateTime(db_dateCreated);
            if (!dr.IsDBNull(2))
                userName = dr.GetString(db_userName);
        }
    }
}
