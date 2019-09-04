﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phone.Data.DTOs.User
{
    public class SellerShopDto
    {
        /// <summary>
        /// Email user.
        /// </summary>
        [Required]
        [EmailAddress]
        public virtual string Email { get; set; }

        /// <summary>
        /// Name for user.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// LastName for user.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        /// <summary>
        /// SurName for user.
        /// </summary>
        [MaxLength(100)]
        public string SurName { get; set; }

        /// <summary>
        /// Sex user (true - man, false - woomen).
        /// </summary>
        public bool? Sex { get; set; }

        /// <summary>
        /// Birthday user.
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// All about user (characteristik).
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Experience user (before works, experience and others).
        /// </summary>
        public string Experience { get; set; }

        /// <summary>
        /// Mobile phones user.
        /// </summary>
        [MaxLength(30)]
        [RegularExpression(@"^\d{10}$")]
        public string Phone { get; set; }

        /// <summary>
        /// Position user.
        /// </summary>
        [MaxLength(50)]
        public string Position { get; set; }

        /// <summary>
        /// Salary user.
        /// </summary>
        public int? Salary { get; set; }
    }
}
