using System.Text;
using Application.Abstarcts.Repositories;
using Application.Abstarcts.Services;
using Application.Common;
using Application.Validations.Company;
using Application.Validations.Post;
using Application.Validations.Review;
using Application.Validations.Service;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence.Context;
using Persistence.Repositories;
using Persistence.Services;

namespace API.Extensions;

public static class ServiceRegistration
{
    public static void AddMyServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // ================= CONTROLLERS =================
        services.AddControllers();

        // ================= SWAGGER =================
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                    Array.Empty<string>()
                }
            });
        });

        // ================= DATABASE =================
        services.AddDbContext<CompUserDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

        // ================= IDENTITY =================
        services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<CompUserDbContext>()
            .AddDefaultTokenProviders();

        // ================= JWT OPTIONS =================
        services.Configure<JwtOptions>(
            configuration.GetSection(JwtOptions.SectionName));

        var jwtOptions = configuration
            .GetSection(JwtOptions.SectionName)
            .Get<JwtOptions>();

        // ================= AUTHENTICATION =================
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme =
                JwtBearerDefaults.AuthenticationScheme;

            options.DefaultChallengeScheme =
                JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters =
                new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,

                    IssuerSigningKey =
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtOptions.Secret)),

                    ClockSkew = TimeSpan.Zero
                };
        });

        services.AddAuthorization();

        // ================= REPOSITORIES =================
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IFollowRepository, FollowRepository>();
        services.AddScoped<IFeedRepository, FeedRepository>();

        // ================= SERVICES =================
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IServiceService, ServiceService>();
        services.AddScoped<IPostService, PostService>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped<IFollowService, FollowService>();
        services.AddScoped<IFeedService, FeedService>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();

        // ================= VALIDATION =================
        services.AddValidatorsFromAssemblyContaining<CreateCompanyValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateServiceValidator>();
        services.AddValidatorsFromAssemblyContaining<CreatePostValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateReviewValidator>();
    }
}