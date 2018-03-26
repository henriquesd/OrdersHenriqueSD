using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrdersHenriqueSD.Models;
using OrdersHenriqueSD.Repositories;

namespace OrdersHenriqueSD.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    public class UserPortalController : Controller
    {
        private readonly IUserPortalRepository _userPortalRepository;

        public UserPortalController(IUserPortalRepository userPortalRepository)
        {
            _userPortalRepository = userPortalRepository;
        }

        [HttpGet]
        [Route("users"), Authorize]
        public IActionResult GetUsers([FromQuery]
            string name = null,
            string userName = null,
            string email = null,
            DateTime? creationDateInitial = null,
            DateTime? creationDateFinal = null,
            string sortBy = null)
        {
            if (((creationDateInitial != null && creationDateFinal == null) ||
                     (creationDateInitial == null && creationDateFinal != null)) ||
                (creationDateInitial != DateTime.MinValue && creationDateFinal == DateTime.MinValue) ||
                 (creationDateInitial == DateTime.MinValue && creationDateFinal != DateTime.MinValue))
            {
                return StatusCode((int)HttpStatusCode.BadRequest, "The filter by Creation Date you need to inform the Initial and the Final date.");
            }

            var users = _userPortalRepository.GetUsers(name, userName, email, creationDateInitial, creationDateFinal, sortBy);

            if (users == null)
            {
                return NotFound();
            }
            return new ObjectResult(users);
        }

        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> Create([FromBody] UserPortal user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, "Please inform all fields of user.");
                }

                await _userPortalRepository.Create(user);

                return StatusCode((int)HttpStatusCode.Created, "The user was inserted!");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}