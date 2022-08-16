using PrismDemo.Exts.Enum;
using SQLite;
using System;

namespace PrismDemo.Models
{
    [Table("UserInfo")]
    public class UserInfo
    {
        [PrimaryKey, MaxLength(100), Indexed]
        public string UserName { get; set; }
        [MaxLength(100)]
        public string FirstName { get; set; }
        [MaxLength(100)]
        public string LastName { get; set; }
        public UserGenderOption Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Gets or sets the total KM (kilometer).
        /// </summary>
        /// <value>
        /// The total km.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 16/08/2022 created
        /// </Modified>
        public decimal TotalKM { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
