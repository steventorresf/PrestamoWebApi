namespace Persistence.Files;

public interface ILogErrorFile
{
    Task FileLogError(Exception exception);
}
