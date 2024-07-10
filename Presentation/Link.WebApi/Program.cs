using Link.Application.Interfaces;
using Link.Persistence.Context;
using Link.Persistence.Repository;
using Link.Application.Services;
using Link.Domain.Entities;
using Link.Application.Interfaces.LinkInterfaces;
using Link.Persistence.Repository.LinkRepositories;
using Link.Application.Interfaces.FollowInterfaces;
using Link.Persistence.Repository.FollowRepositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Link.Application.Tools;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<LinkContext>();

//builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<BeldYazilimContext>();

builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    //options.User.RequireUniqueEmail = false;

    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
})
.AddEntityFrameworkStores<LinkContext>();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        // if URL path starts with "/api" then use Bearer authentication instead
        options.ForwardDefaultSelector = httpContext => httpContext.Request.Path.StartsWithSegments("/api") ? JwtBearerDefaults.AuthenticationScheme : null;
    })
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidAudience = JwtTokenDefaults.ValidAudience,
            ValidIssuer = JwtTokenDefaults.ValidIssuer,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key)),
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };
    });


builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(ILinkRepository), typeof(LinkRepository));
builder.Services.AddScoped(typeof(IFollowRepository), typeof(FollowRepository));

builder.Services.AddApplicationService(builder.Configuration);

//builder.Services.AddMediatR(typeof(MyHandler));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
