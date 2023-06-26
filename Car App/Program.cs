using Car_App.Data.Context;
using Car_App.Helpers;
using Car_App.Service.Interface;
using Car_App.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using WebApi.Helpers;

namespace Car_App
{



    public class Program
    {
        public static void Main(String[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddDbContext<DatabaseContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseContext")));
            }
            else if (builder.Environment.IsProduction())
            {
                builder.Services.AddDbContext<DatabaseContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseContext")));
            }
            else if (builder.Environment.EnvironmentName == "Test")
            {
                builder.Services.AddDbContext<DatabaseContext>(options =>
                    options.UseInMemoryDatabase("InMemoryDbForTesting"));
            }

            builder.Services.AddScoped<ICarService, CarService>();
            builder.Services.AddScoped<IOwnerService, OwnerService>();

            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
            builder.Services.AddSingleton<JwtSettings>();

            if (builder.Environment.EnvironmentName != "Test")
            {
                builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.SecretKey)),
                            ValidateIssuer = true,
                            ValidIssuer = jwtSettings.Issuer,
                            ValidateAudience = true,
                            ValidAudience = jwtSettings.Audience,
                            ClockSkew = TimeSpan.Zero
                        };
                    });
            }

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                });

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "car",
                    Version = "version"
                });

                string filePath = Path.Combine(AppContext.BaseDirectory, Assembly.GetEntryAssembly()?.GetName().Name + ".xml");
                c.IncludeXmlComments(filePath);

                // Add JWT bearer authentication
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
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
            Array.Empty<string>()
        }
    });
            });

            var app = builder.Build();

            // Build the application.

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var error = context.Features.Get<IExceptionHandlerFeature>().Error;

                    if (error is AppException appException)
                    {
                        context.Response.StatusCode = appException.StatusCode;
                        await context.Response.WriteAsync(new ErrorResponse
                        {
                            Message = appException.Message,
                            Status = appException.StatusCode
                        }.ToString());
                    }
                    else
                    {
                        context.Response.StatusCode = 401;
                        await context.Response.WriteAsync(new ErrorResponse
                        {
                            Message = "Incorrect username or password"
                        }.ToString());
                    }
                });
            });
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.MapControllers();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();

        }

    }
}

