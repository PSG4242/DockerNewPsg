using DockerNewPsg.Models;
using DockerNewPsg.Services;
using MongoDB.Driver;


// Program.cs
var builder = WebApplication.CreateBuilder(args);

//var mongoUri = Environment.GetEnvironmentVariable("MONGODB_URI");
//var client = new MongoClient(mongoUri);


// Configure MongoDB settings
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

// Register the service
builder.Services.AddSingleton<UserService>();


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
