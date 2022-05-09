using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Data.Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.Services
{
    public class PostServices : Repository<Post>
    {
        private Repository<Post> _Post;

        public Repository<Post> Post
        {
            get
            {
                if (_Post == null)
                {
                    _Post = new Repository<Post>();
                }
                return _Post;
            }
        }
        public override IQueryable<Post> Include(Expression<Func<Post, bool>> Filter = null)
        {
            return base.Include(Filter);
        }
    }
}