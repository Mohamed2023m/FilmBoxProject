using DataAccess;
using FilmBox.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmBox.Api.DataAccess

{
    public interface IUserAccess
    {
   
        Task<User> GetEmailAsync(string email);
    }
}
