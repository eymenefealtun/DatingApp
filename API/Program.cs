using API.Data;
using API.Extensions;
using API.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200").AllowCredentials());
// app.UseCors(x => x.AllowAnyMethod()
//                   .AllowAnyHeader()
//                   .SetIsOriginAllowed(origin => true) // allow any origin
//                   .AllowCredentials());

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.


app.UseAuthentication(); //Ask user if she/he has a valid token
app.UseAuthorization(); // After Authetntication it tels us what are we allowed to do

app.MapControllers();

using var scope = app.Services.CreateScope();

var service = scope.ServiceProvider;

try
{
    var context = service.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedUsers(context);
}
catch (Exception ex)
{
    var logger = service.GetService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
}

app.Run();
