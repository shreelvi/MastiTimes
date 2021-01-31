using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MastiTimes.Models.Database
{
    public abstract class DatabaseRecord
    {
        public int _ID;
        /// <summary>
        /// The User given Name for the Object.
        /// </summary>
        [Display(Name = "ID")]
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
      
        public abstract void Fill(MySql.Data.MySqlClient.MySqlDataReader dr);

        public abstract override string ToString();
    }

    public abstract class DatabaseNamedRecord : DatabaseRecord
    {
        protected string _Name;
        private string _EmailAddress;
        private DateTime _DateCreated;
        private DateTime _DateModified;
        private DateTime _DateDeleted;
        /// <summary>
        /// The User given Name for the Object.
        /// </summary>
        //[Display(Name = "Name")]
        [DataType(DataType.Text)]
        //[Required]
        [Display(Name = "Name")]
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public string EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
        }
        [DataType(DataType.DateTime)]
        [Display(Name = "DateCreated")]
        public DateTime DateCreated
        {
            get { return _DateCreated; }
            set { _DateCreated = value; }
        }

        [DataType(DataType.DateTime)]
        [Display(Name = "DateModified")]
        public DateTime DateModified
        {
            get { return _DateModified; }
            set { _DateModified = value; }
        }

        [DataType(DataType.DateTime)]
        [Display(Name = "DateDeleted")]
        public DateTime DateDeleted
        {
            get { return _DateDeleted; }
            set { _DateDeleted = value; }
        }
    }
}
