using DeveloperEvaluation.Application;
using DeveloperEvaluation.Common.HealthChecks;
using DeveloperEvaluation.Common.Logging;
using DeveloperEvaluation.Common.MessageBroker.Services;
using DeveloperEvaluation.Common.Security;
using DeveloperEvaluation.Common.Validation;
using DeveloperEvaluation.IoC;
using DeveloperEvaluation.ORM;
using DeveloperEvaluation.WebApi.MessageBroker.Services;
using DeveloperEvaluation.WebApi.Middleware;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DeveloperEvaluation.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Log.Information("Starting web application");

            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            builder.AddDefaultLogging();

            builder.Services.AddControllers();
            builder.Services.AddHostedService<CreateCDBConsumerService>();
            builder.Services.AddEndpointsApiExplorer();

            builder.AddBasicHealthChecks();
            builder.Services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(type => type.FullName);
            });

            builder.Services.AddDbContext<DefaultContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("DeveloperEvaluation.ORM")
                )
            );

            builder.Services.AddJwtAuthentication(builder.Configuration);

            builder.RegisterDependencies();

            builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(ApplicationLayer).Assembly);

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    typeof(ApplicationLayer).Assembly,
                    typeof(Program).Assembly
                );
            });

            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            builder.Services.Configure<RabbitMQSetting>(builder.Configuration.GetSection("RabbitMQ"));
            builder.Services.AddScoped(typeof(IMessageBrokerPublisher), typeof(RabbitMQPublisher));

            var app = builder.Build();
            app.UseMiddleware<ValidationExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
               //.WithOrigins("http://localhost:4200")
               //.AllowCredentials()
               .WithExposedHeaders("Content-Disposition"));

            app.UseAuthorization();

            app.UseBasicHealthChecks();

            app.MapControllers();

            ApplyMigrations(app);

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    static void ApplyMigrations(WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<DefaultContext>();

            var pendingMigrations = dbContext.Database.GetPendingMigrations();
            if (pendingMigrations.Any())
            {
                Console.WriteLine("Applying pending migrations...");
                dbContext.Database.Migrate();
                Console.WriteLine("Migrations applied successfully.");
            }
            else
            {
                Console.WriteLine("No pending migrations found.");
            }
        }
    }
}
