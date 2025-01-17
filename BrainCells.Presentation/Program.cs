using System;
using BrainCells.Application.Interfaces;
using BrainCells.Application.Services.AccountRepository;
using BrainCells.Infrastructure.Contexts;
using BrainCells.Presentation.Middlewares;
using BrainCells.Presentation.Models.Account.Validators;
using BrainCells.Presentation.Models.Account.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IDatabaseContext,DatabaseContext>();
builder.Services.AddDbContext<DatabaseContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

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

builder.Services.AddScoped<IAccountRepository,AccountRepository>();

builder.Services.AddScoped<IValidator<SigninViewModel>,SigninValidator>();
builder.Services.AddScoped<IValidator<SignupViewModel>,SignupValidator>();

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
