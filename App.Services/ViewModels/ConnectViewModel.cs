using System;
using System.ComponentModel.DataAnnotations;

namespace App.Services
{
    public class ConnectViewModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
