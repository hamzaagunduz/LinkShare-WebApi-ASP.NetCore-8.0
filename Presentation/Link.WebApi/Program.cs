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
using Link.Application.Interfaces.CommentRepository;
using Microsoft.OpenApi.Models;
using FluentValidation.AspNetCore;
using Link.Application.FluentValidations;
using System.Globalization;
using System.Reflection;
using Link.Application.AutoMapper.Link;
using Link.Application.Behavior;
using MediatR;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // JWT Authentication
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter JWT token in the format 'Bearer {token}'",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });
});

//builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddAutoMapper(typeof(LinkProfile).Assembly);
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

builder.Services.AddValidatorsFromAssembly(Link.Application.AssemblyReference.Assembly,
    includeInternalTypes: true);

builder.Services.AddControllersWithViews()
    .AddFluentValidation(opt =>
    {
        opt.RegisterValidatorsFromAssemblyContaining<LinkValidator>();
        opt.RegisterValidatorsFromAssemblyContaining<CommentValidator>();
        opt.RegisterValidatorsFromAssemblyContaining<AppUserValidator>();
        opt.RegisterValidatorsFromAssemblyContaining<FollowValidator>();

        opt.DisableDataAnnotationsValidation = true;
        opt.ValidatorOptions.LanguageManager.Culture = new CultureInfo("tr");
    });




builder.Services.AddScoped<LinkContext>();
//builder.Services.AddHttpContextAccessor();

//builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<BeldYazilimContext>();

builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    //options.User.RequireUniqueEmail = false;

    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
})
.AddEntityFrameworkStores<LinkContext>();



builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = JwtTokenDefaults.ValidIssuer,
        ValidAudience = JwtTokenDefaults.ValidAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key)),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();



builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(ILinkRepository), typeof(LinkRepository));
builder.Services.AddScoped(typeof(IFollowRepository), typeof(FollowRepository));
builder.Services.AddScoped(typeof(ICommentRepository), typeof(CommentRepository));

builder.Services.AddApplicationService(builder.Configuration);

//builder.Services.AddMediatR(typeof(MyHandler));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();  // Add this line to ensure authentication middleware is used
app.UseAuthorization();

app.MapControllers();

app.Run();
