using System;
using System.Net;
using System.Net.Mail;
using BrainCells.Application.Interfaces;
using BrainCells.Application.Services.AccountRepository;
using BrainCells.Application.Services.LoggingService;
using BrainCells.Application.Services.SupportEmailService;
using BrainCells.Infrastructure.Contexts;
using BrainCells.Infrastructure.Services;
using BrainCells.Presentation.Middlewares;
using BrainCells.Presentation.Models.Account.Validators;
using BrainCells.Presentation.Models.Account.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DatabaseContext>();
builder.Services.AddScoped<IDatabaseContext, DatabaseContext>();

builder.Services.AddAuthentication(options => {
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
    .AddCookie(options => {
        options.LoginPath = new PathString("/Account/SignIn");
        options.AccessDeniedPath = new PathString("/Error/403");
    });

builder.Services.AddAuthorization(options => {
    options.AddPolicy("requireLogin",policy => policy.RequireRole("ACCOUNT"));
});

builder.Services.AddScoped<ILoggingService,LoggingService>();

builder.Services.AddFluentEmailConfigure(builder.Configuration);
builder.Services.AddScoped<ISupportEmailService, SupportEmailService>();

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
    builder.Services.AddScoped<IValidator<SigninViewModel>, SigninValidator>();
    builder.Services.AddScoped<IValidator<SignupViewModel>, SignupValidator>();
    builder.Services.AddScoped<IValidator<ChangePasswordViewModel>, ChangePasswordValidator>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStatusCodeMiddleware();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
