using Ploomes.LiveCode.WebApi.Repositories;

namespace Ploomes.LiveCode.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddScoped<IDatabase, SqlDatabase>();

        builder.Services.AddScoped<IUserRepository, SqlUserRepository>();
        builder.Services.AddScoped<IClientRepository, SqlClientRepository>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
