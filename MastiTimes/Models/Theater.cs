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

        [NotMapped]
        public List<Movie> Movies { get; set; }

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
                Likes = dr.GetInt32(db_Likes);
            if(!dr.IsDBNull(5))
                PhoneNumber = dr.GetString(db_Phone);

        }
        #endregion

        public override string ToString()
        {
            return this.GetType().ToString();
        }

       
    }
}
