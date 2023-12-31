﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Tabloid.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [MaxLength(255)]
        public string ImageLocation { get; set; }
        [Required]
        public DateTime CreateDateTime { get; set; }
        public DateTime? PublishDateTime { get; set; }
        [Required]
        public Boolean IsApproved { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int UserProfileId { get; set; }
        // add Category Category
        public UserProfile UserProfile { get; set; }
        public Category Category { get; set; }
    }
}
