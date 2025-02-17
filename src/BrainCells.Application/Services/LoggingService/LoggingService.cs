using System;
using System.Text;
using static BrainCells.Application.Services.LoggingService.ILoggingService;

namespace BrainCells.Application.Services.LoggingService;

public class LoggingService : ILoggingService
{

    private string _rootPath;
   
    public LoggingService(string rootPath)
    {
        _rootPath = Path.Combine(rootPath, "log");
        Directory.CreateDirectory(_rootPath);
    }

    private async Task setLog(string filePath, string log)
    {
        using(FileStream file = new FileStream(filePath, FileMode.Append, FileAccess.Write))
        {
            await file.WriteAsync(Encoding.UTF8.GetBytes(log), 0, log.Length);
        }
    }

    public async Task<string> LogAccountAsync(string email, LogTitle title)
    {
        try{
            string path = Path.Combine(_rootPath, "account");
            Directory.CreateDirectory(path);
            string filePath = Path.Combine(path, email.Replace('@','-') + "-log.txt");
            string log = "\n\n*Issue: " + title.ToString() + "\n"
                    + "  Time: " + DateTime.Now.ToLongTimeString() + "\n"
                    + "  Date: " + DateTime.Now.ToLongDateString() + "\n"
                    + "_________________________________________________";
            await setLog(filePath, log);
            return filePath;
        }
        catch{
            return "error";
        }
    }
}