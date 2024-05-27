using Microsoft.EntityFrameworkCore;
using MiniProjectApp.BussinessLogics;
using MiniProjectApp.BussinessLogics.Interfaces;
using MiniProjectApp.BussinessLogics.Services;
using MiniProjectApp.Context;
using MiniProjectApp.Models;
using MiniProjectApp.Repositories;
using MiniProjectApp.Repositories.Interface;

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
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<LibraryManagementContext>(
             options => options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"))
             );


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
