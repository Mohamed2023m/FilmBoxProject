using FilmBox.Api.Models;
using System.Collections.Generic;   

namespace FilmBox.Api.BusinessLogic
{
    public interface IReviewRepository
    {
        Task<bool> InsertAsync(Review review);
        //Task<IEnumerable<Review>> GetByMediaIdAsync(int mediaId);
        //Task<bool> MediaExistsAsync(int mediaId);

    }
}
