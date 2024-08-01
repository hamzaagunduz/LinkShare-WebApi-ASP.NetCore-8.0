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
using System.Globalization;
using System.Reflection;
using Link.Application.AutoMapper.Link;
using MediatR;
using FluentValidation;
using Link.Application.Features.Mediator.Validations.CommentValidation;
using Link.Application.Behavior;
using Link.Application.Exceptions;
using Link.Application.ExceptionsHandlers;
using Link.Application.Middleware;
using Link.Persistence.Repository.CommentRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();

// Add services to the container.



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer()
    .AddProblemDetails(); 


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

builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
builder.Services.AddExceptionHandler<AuthorizationExceptionHandler>();
builder.Services.AddExceptionHandler<BadRequestExceptionHandler>();
builder.Services.AddExceptionHandler<NullReferenceExceptionHandler>();
builder.Services.AddExceptionHandler<TransactionExceptionHandler>();


builder.Services.AddAutoMapper(typeof(LinkProfile).Assembly);


//////////////
builder.Services.AddControllers(
    options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

builder.Services.AddRateLimiter(configure =>
{
    configure.AddFixedWindowLimiter("fixed", optinos =>
    {
        optinos.PermitLimit = 20;
        optinos.Window = TimeSpan.FromSeconds(1);
        optinos.QueueLimit=1;
        optinos.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
    });
});




builder.Services.AddHttpClient<RecaptchaService>();



builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy =>
        {
            policy.WithOrigins("https://localhost:7132") // Frontend'in URL'si
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});



///////////
///




builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestResponseLoggingBehavior<,>));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
builder.Services.AddValidatorsFromAssembly(Link.Application.AssemblyReference.Assembly,
    includeInternalTypes: true);












builder.Services.AddScoped<LinkContext>();
//builder.Services.AddHttpContextAccessor();

//builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<BeldYazilimContext>();

builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.SignIn.RequireConfirmedEmail = true;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
})
.AddEntityFrameworkStores<LinkContext>()
.AddDefaultTokenProviders();




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
        ClockSkew = TimeSpan.Zero,
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

app.UseRateLimiter();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}


// Genel hata iþleme yapýlandýrmasý
app.UseExceptionHandler("/Home/Error");
app.UseHsts();

app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();
//app.Use(async (context, next) =>
//{
//    // Test amaçlý NullReferenceException fýrlatma
//    throw new NotFoundException("Test null reference exception");
//    await next.Invoke();
//});


app.UseAuthentication();  // Add this line to ensure authentication middleware is used
app.UseAuthorization();
//app.UseMiddleware<AccessTokenMiddleware>();


app.MapControllers()/*.RequireRateLimiting("fixed")*/;


app.Run();
