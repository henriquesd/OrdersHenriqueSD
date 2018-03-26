using OrdersHenriqueSD.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrdersHenriqueSD.Repositories
{
    public interface IUserPortalRepository
    {
        List<UserPortal> GetUsers(
            string name = null,
            string userName = null,
            string email = null,
            DateTime? creationDateInitial = null,
            DateTime? creationDateFinal = null,
            string sortBy = null);

        Task Create(UserPortal userPortal);
        UserPortal Login(string userName, string password);
    }
}
