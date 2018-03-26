using OrdersHenriqueSD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersHenriqueSD.Repositories
{
    public class UserPortalRepository : IUserPortalRepository
    {
        private readonly OrdersHenriqueSDContext _context;

        public UserPortalRepository(OrdersHenriqueSDContext context)
        {
            _context = context;
        }

        public List<UserPortal> GetUsers(
            string name = null,
            string userName = null,
            string email = null,
            DateTime? creationDateInitial = null,
            DateTime? creationDateFinal = null,
            string sortBy = null)
        {
            var users = from u in _context.UsersPortal
                        select u;

            if (name != null && name != "")
                users = users.Where(u => u.Name.Contains(name));
            if (userName != null && userName != "")
                users = users.Where(u => u.UserName.Contains(userName));
            if (email != null && email != "")
                users = users.Where(u => u.Email.Contains(email));
            if ((creationDateInitial != null && creationDateFinal != null) &&
                (creationDateInitial != DateTime.MinValue && creationDateFinal != DateTime.MinValue))
                users = users.Where(u => u.CreationDate.Date >= creationDateInitial && u.CreationDate.Date <= creationDateFinal);

            if (sortBy != null && sortBy != "")
            {
                bool desc = false;
                if (sortBy.Substring(0, 1) == "-")
                {
                    sortBy = sortBy.Substring(1, sortBy.Length - 1);
                    desc = true;
                }

                switch (sortBy)
                {
                    case "name":
                        if (desc)
                            users = users.OrderByDescending(u => u.Name);
                        else
                            users = users.OrderBy(u => u.Name);
                        break;
                    case "userName":
                        if (desc)
                            users = users.OrderByDescending(u => u.UserName);
                        else
                            users = users.OrderBy(u => u.UserName);
                        break;
                    case "email":
                        if (desc)
                            users = users.OrderByDescending(u => u.Email);
                        else
                            users = users.OrderBy(u => u.Email);
                        break;
                }
            }

            return users.ToList();
        }

        public async Task Create(UserPortal userPortal)
        {
            userPortal.CreationDate = DateTime.Now;
            await _context.UsersPortal.AddAsync(userPortal);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(UserPortal userPortal)
        {
            _context.Update(userPortal);
            await _context.SaveChangesAsync();
        }

        public UserPortal Login(string userName, string password)
        {
            var user = _context.UsersPortal
                .Where(u => u.UserName == userName &&
                u.Password == password).FirstOrDefault();

            return user;
        }
    }
}