using Domain.DTO;
using Microsoft.Extensions.Options;

namespace Application.Implementations
{
    public class FileTxtService : IFileTxtService
    {
        private readonly IOptions<AppSettings> _options;

        public FileTxtService(IOptions<AppSettings> options)
        {
            _options = options;
        }

        public async Task FileLogError(Exception exception)
        {
            Directory.CreateDirectory(_options.Value.TextConfiguration.LoggerExceptionTxtDirectory);
            string filePath = _options.Value.TextConfiguration.LoggerExceptionTxtDirectory + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            
            if(!File.Exists(filePath))
                File.Create(filePath).Close();

            using (StreamWriter sw = File.AppendText(filePath))
            {
                await sw.WriteLineAsync(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "|" + exception.Message);
                sw.Close();
            }
        }
    }
}
