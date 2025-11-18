using FilmBox.Api.Models;
using System.Collections.Generic;   

namespace FilmBox.Api.BusinessLogic
{
    // Defines all database operations related to Reviews
    public interface IReviewRepository
    {
        // Inserts a new review into the database
        Task<int> InsertAsync(Review review);
    }
}
