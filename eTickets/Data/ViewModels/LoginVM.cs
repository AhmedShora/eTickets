﻿namespace eTickets.Data.ViewModels
{
    public class LoginVM
    {
        [Required]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
