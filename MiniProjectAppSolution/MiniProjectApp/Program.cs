using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MiniProjectApp.BussinessLogics;
using MiniProjectApp.BussinessLogics.Interfaces;
using MiniProjectApp.BussinessLogics.Services;
using MiniProjectApp.Context;
using MiniProjectApp.Models;
using MiniProjectApp.Repositories;
using MiniProjectApp.Repositories.Interface;
using MiniProjectApp.Services;
using MiniProjectApp.Services.Interfaces;
using System.Text;

namespace MiniProjectApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddLogging(l => l.AddLog4Net());
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<LibraryManagementContext>(
             options => options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"))
             );


            builder.Services.AddSwaggerGen(option =>
            {
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                    new string[] { }
                }
            });
            });



            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey:JWT"])),
                        
                    };

                });



            #region
            builder.Services.AddScoped<IRepository<int, User>, UserRepository>();
            builder.Services.AddScoped<ICompositeKeyRepository<int,Cart>, CartRepository>();
            builder.Services.AddScoped<IRepository<int, UserCredential>, UserCredentialRepository>();
            builder.Services.AddScoped<ITokenService, TokenBL>();
            builder.Services.AddScoped<IAuthService, AuthBL>();
            builder.Services.AddScoped<IUserServices, UserServices>();
            builder.Services.AddScoped< IRepository<int, SalesStock>, SaleStockRepository >();
            builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
            builder.Services.AddScoped<IRepository<int,Sale>, SaleRepository>();
            builder.Services.AddScoped<ICompositeKeyRepository<int,SaleDetail>, SaleDetailRepository>();
            builder.Services.AddScoped<IRepository<int, Feedback>, FeedbackRepository>();
            builder.Services.AddScoped<IRepository<int, Book>, BookRepository>();
            builder.Services.AddScoped<IRepository<int, Purchase>, PurchaseRepository>();
            builder.Services.AddScoped<ICompositeKeyRepository<int, PurchaseDetail>, PurchaseDetailRepository>();
            builder.Services.AddScoped<IAdminServices, AdminServices>();
            builder.Services.AddScoped<IRepository<int, RentStock>, RentStockRepository>();
            builder.Services.AddScoped<IRepository<int, Rent>, RentRepository>();
            builder.Services.AddScoped<ICompositeKeyRepository<int, RentDetail>, RentDetailRepository>();
            builder.Services.AddScoped<IRepository<int, Fine>, FineRepository>();
            builder.Services.AddScoped<ICompositeKeyRepository<int, FineDetail>, FineDetailRepository>();
            builder.Services.AddScoped<ICompositeKeyRepository<int, RentCart>, RentCartRepository>();
            builder.Services.AddScoped<ICompositeKeyRepository<int, SuperRentCart>, SuperRentCartRepository>();
            #endregion

            builder.Services.AddScoped<IBookServices, BookServices>();
            builder.Services.AddScoped<ICartServices, CartServices>();
            builder.Services.AddScoped <IPurchaseServices, PurchaseServices>();
            builder.Services.AddScoped <IFineServices, FineServices>();
            builder.Services.AddScoped <IRentServices, RentServices>();
            builder.Services.AddScoped <ISaleServices, SaleServices>();
            builder.Services.AddScoped <IUserValidationService, UserValidationService>();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
