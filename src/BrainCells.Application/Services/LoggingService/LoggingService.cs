using System;
using System.Text;

namespace BrainCells.Application.Services.LoggingService;

public class LoggingService : ILoggingService
{
    private string _rootPath;
   
    public LoggingService(string rootPath)
    {
        _rootPath = Path.Combine(rootPath, "log");
    }

    public enum LogMode
    {
        SignIn,
        SignUp,
        SignOut,
        ForgotPassword,
        OneTimePassword,
        ChangePassword,
        DeleteAccount,
    }

    public async Task<bool> LogAccountAsync(string email, LogMode mode)
    {
        try{
            string path = Path.Combine(_rootPath, "account");
            Directory.CreateDirectory(path);
            string filePath = Path.Combine(path, email.Replace('@','-') + "-log.txt");
            string log = "\n\n*Issue: " + mode.ToString() + "\n"
                    + "  Time: " + DateTime.Now.ToLongTimeString() + "\n"
                    + "  Date: " + DateTime.Now.ToLongDateString() + "\n"
                    + "_________________________________________________";
            FileStream file = new FileStream(filePath, FileMode.Append, FileAccess.Write);
            await file.WriteAsync(Encoding.UTF8.GetBytes(log), 0, log.Length);
            file.Close();
            return true;
        }
        catch{
            return false;
        }
    }
}