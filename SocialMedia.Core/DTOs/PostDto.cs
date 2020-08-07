using System;

namespace SocialMedia.Core.DTOs
{
    /// <summary>
    /// 
    /// </summary>
    public class PostDto
    {
        /// <summary>
        /// Identity columns
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Describe el id UserId
        /// </summary>
        public int UserId { get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
