using System;
using System.Text;
using Microsoft.AspNetCore.Hosting;

namespace BrainCells.Application.Services.LoggingService;

public class LoggingService : ILoggingService
{
    private string logPath;
    private readonly IWebHostEnvironment _webHostEnvironment;
   
    public LoggingService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
        logPath = Path.Combine(_webHostEnvironment.WebRootPath, "log");
    }

    public enum LogMode
    {
        SignIn,
        SignUp,
        SignOut,
    }

    private void checkDirectory()
    {
        if(!Directory.Exists(logPath))
            Directory.CreateDirectory(logPath);
    }

    public async Task LogAccountAsync(string email, LogMode mode)
    {
        try{
            checkDirectory();
            string path = Path.Combine(logPath, "account");
            if(!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string filePath = Path.Combine(path, email.Replace('@','-') + ".txt");
            string log = "\n\n*" + mode.ToString() + "\n"
                    + "  Time: " + DateTime.Now.ToLongTimeString() + "\n"
                    + "  Date: " + DateTime.Now.ToLongDateString() + "\n"
                    + "_________________________________________________";
            FileStream file = new FileStream(filePath, FileMode.Append, FileAccess.Write);
            await file.WriteAsync(Encoding.UTF8.GetBytes(log), 0, log.Length);
            file.Close();
        }
        catch{}
    }
}