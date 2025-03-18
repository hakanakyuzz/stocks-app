using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApp.Database;
using WebApp.Interfaces;
using WebApp.Models;
using WebApp.Repository;
using WebApp.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

// Tell ASP.NET Core to use Identity for handling user authentication and authorization
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    // Sets rules for how strong user passwords need to be
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
})
    // Tells ASP.NET Identity to store user info in the database using Entity Framework Core
    .AddEntityFrameworkStores<AppDbContext>();

// AddAuthentication() : Registers authentication as a service in the Dependency Injection (DI) container
builder.Services.AddAuthentication(options =>
    {
        // An authentication schema is like a "strategy" that defines:
            // How to authenticate user
            // How to challenge user (when they are not logged in)
            // How to handle sign-in, sign-out, and forbidden actions 
            
        // int a, b, c;
        // a = b = c = 5;    
            
        // Which schema to use when authenticating users    
        options.DefaultAuthenticateScheme = 
            // Which schema to use when redirecting unauthenticated users
            options.DefaultChallengeScheme =
                // Which schema to use when returning a 403 Forbidden response
                options.DefaultForbidScheme = 
                    // Fallback if none of the above is set
                    options.DefaultScheme =
                        // Which schema to use when signing in users
                        options.DefaultSignInScheme =
                            //Which schema to use when singing out users
                            options.DefaultSignOutScheme = 
                                // JwtBearerDefaults.AuthenticationScheme : Use JWT Bearer Tokens for authentication
                                JwtBearerDefaults.AuthenticationScheme;
    })
    // AddJwtBearer() : Configures how ASP.NET Core should handle JWT Tokens
    .AddJwtBearer(options =>
    {
        // TokenValidationParameters : Define the rules for validating JWT token
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Requires that the issuer of the token matches the expected value
            ValidateIssuer = true,
            // Reads the expected issuer from appsettings.json
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            
            // Requires that the audience of the token matches the expected value
            ValidateAudience = true,
            // Reads the expected audience from appsettings.json
            ValidAudience = builder.Configuration["JWT:Audience"],
            
            // Requires that the token’s signature is valid
            ValidateIssuerSigningKey = true,
            
            // This sets the secret key that was used to sign the JWT
            IssuerSigningKey = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"] ?? string.Empty)
                )
        };
    });

// When the app needs IStockRepository, create a new instance of StockRepository, and reuse the same instance for the lifetime of the HTTP request.
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors(cors => cors
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    .SetIsOriginAllowed(origin => true));

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();