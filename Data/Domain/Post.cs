using System;
using System.Collections.Generic;
using System.Text;
using Data.Base;
using System.Web.Mvc;
namespace Data.Domain
{
    public class Post : IBaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string Content { get; set; }
        public string Image { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime UpdateTime { get; set; } = DateTime.Now;
        public bool Status { get; set; }
        public bool IsOnline { get; set; }
        public string AuthorName { get; set; }
        public virtual User User { get; set; }
    }
}