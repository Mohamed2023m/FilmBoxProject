using System.Data;

namespace FilmBox.Api.BusinessLogic
{
    public abstract class BaseRepository
    {
        protected readonly IDbConnection _db;

        protected BaseRepository(IDbConnection db)
        {
            _db = db;
        }

        protected void EnsureOpen()
        {
            if (_db.State != ConnectionState.Open)
                _db.Open();
        }
    }
}
