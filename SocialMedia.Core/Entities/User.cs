﻿using System;
using System.Collections.Generic;

namespace SocialMedia.Core.Entities
{
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            Post = new HashSet<Post>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth{ get; set; }
        public string Telephone { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Post> Post { get; set; }
    }
}
