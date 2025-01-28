using System;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BrainCells.Infrastructure.Services;

public static class SupportEmailConfigure
{
    public static IServiceCollection AddFluentEmailConfigure(this IServiceCollection source,
        ConfigurationManager configuration)
    {
        string name = configuration["SupportEmail:Name"].ToString();
        string address = configuration["SupportEmail:Address"].ToString();
        string host = configuration["SupportEmail:Host"].ToString();
        int port = Convert.ToInt32(configuration["SupportEmail:Port"].ToString());
        string pwd = configuration["SupportEmail:AppPwd"].ToString();

        source.AddFluentEmail(address, name)
            .AddSmtpSender(new SmtpClient{
                Host = host,
                Port = port,
                Credentials = new NetworkCredential(address, pwd),
                EnableSsl = true,
            });
        return source;
    }
}