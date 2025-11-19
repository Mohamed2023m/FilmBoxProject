namespace FilmBox.Api.DataAccess.Exceptions

{

public class DataAccessException : Exception
{
    public DataAccessException(string message, Exception inner)
        : base(message, inner)
    { }
}
}