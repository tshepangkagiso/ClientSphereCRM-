using CRM_API.Data;
using CRM_API.Data.Services;
using CRM_API.Data.Services.Interfaces;
using CRM_API.Services;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

Env.Load();
var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string not found in environment variables.");
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddTransient<IClientDbServices, ClientDbServices>();
builder.Services.AddTransient<ICommentDbServices, CommentDbServices>();
builder.Services.AddTransient<IEmployeeDbServices, EmployeeDbServices>();
builder.Services.AddTransient<ILoginDbServices, LoginDbServices>();

builder.Services.AddTransient<PasswordServices>();


builder.Services.AddControllers();

var app = builder.Build();



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
