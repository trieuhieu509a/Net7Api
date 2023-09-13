﻿using System.ComponentModel.DataAnnotations;

namespace TodoList.Api.Entities
{
    public class User : Microsoft.AspNetCore.Identity.IdentityUser<Guid>
    {
        [MaxLength(250)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(250)]
        [Required]
        public string LastName { get; set; }


    }
}
