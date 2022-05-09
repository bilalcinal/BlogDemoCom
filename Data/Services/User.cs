using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Data.Domain;

namespace Data.Services
{
    public class UserServices : Repository<User>
    {
        private Repository<User> _User;

        public Repository<User> User
        {
            get
            {
                if (_User == null)
                {
                    _User = new Repository<User>();
                }
                return _User;
            }
        }
        public override IQueryable<User> Include(Expression<Func<User, bool>> Filter = null)
        {
            return base.Include(Filter);
        }
        public User login(string eposta, string password)
        {
            Expression<Func<User, bool>> filter = f => f.Status & f.Email == eposta && f.Password == password;
            var data = User.Where(filter).FirstOrDefault();
            if (data != null)
                return data;
            return null;
        }
    }
}