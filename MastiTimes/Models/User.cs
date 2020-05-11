using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MastiTimes.Models
{
    public class User: Database.DatabaseRecord
    {
        #region Constructors
        /// <summary>
        /// Code By Elvis
        /// Constructor to map results of sql query to the class
        /// Reference: GitHub PeerVal Project
        /// </summary>
        public User()
        {
        }
        internal User(MySql.Data.MySqlClient.MySqlDataReader dr)
        {
            Fill(dr);
        }

        #endregion

        #region private variable
        private string _FirstName;
        private string _LastName;
        private string _EmailAddress;
        private string _UserName;
        private string _Password;
        private string _Salt;
        //private int _RoleID;
        //private Role _Role;
        private DateTime _DateCreated;
        #endregion

        #region Database String
        internal const string db_ID = "ID";
        internal const string db_FirstName = "FirstName";
        internal const string db_LastName = "LastName";
        internal const string db_EmailAddress = "EmailAddress";
        internal const string db_UserName = "UserName";
        internal const string db_Salt = "Salt";
        //internal const string db_Role = "RoleID";
        internal const string db_Password = "Password";
        internal const string db_DateCreated = "DateCreated";
        #endregion

        #region public Properites
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        public string EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
        }
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        [Required]
        [DataType(DataType.Password)]
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }


        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [NotMapped]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Gets or sets the Salt for this PeerVal.User object
        /// </summary>
        public string Salt
        {
            get { return _Salt; }
            set { _Salt = value; }
        }

        /// <summary>
        /// Gets or sets the RoleID for this PeerVal.User object.
        /// </summary>
        /// <remarks></remarks>
        //public int RoleID
        //{
        //    get
        //    {
        //        return _RoleID;
        //    }
        //    set
        //    {
        //        _RoleID = value;
        //    }
        //}

        public DateTime DateCreated
        {
            get { return _DateCreated; }
            set { _DateCreated = value; }
        }
       

        #endregion


        #region Public Subs
        /// <summary>
        /// Fills object from a MySqlClient Data Reader
        /// </summary>
        /// <remarks></remarks>
        public override void Fill(MySql.Data.MySqlClient.MySqlDataReader dr)
        {
            _ID = dr.GetInt32(db_ID);
            _FirstName = dr.GetString(db_FirstName);
            _LastName = dr.GetString(db_LastName);
            _EmailAddress = dr.GetString(db_EmailAddress);
            _UserName = dr.GetString(db_UserName);
            _Password = dr.GetString(db_Password);
            // DateTime DateCreated = dr.GetDateTime(db_DateCreated);
            //_DateModified = dr.GetDateTime(db_DateModified);
            // _DateModified = DateTime.Parse(DateModified.ToString());
            //_DateDeleted = dr.GetDateTime(db_DateDeleted);
            _Salt = dr.GetString(db_Salt);
        }
        #endregion

        public override string ToString()
        {
            return this.GetType().ToString();
        }
    }
}

