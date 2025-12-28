using FoodOrderingSystem.Models;
using FoodOrderingSystem.Repositories.Implementations;
using FoodOrderingSystem.Repositories.Interfaces;
using FoodOrderingSystem.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//add session
builder.Services.AddSession();


//register ko lagi repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();

//register for servies

builder.Services.AddScoped<AuthServices>();


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var userRepo = scope.ServiceProvider.GetRequiredService<IUserRepository>();

    // Check if admin exists
    if (userRepo.GetUserByUsername("admin") == null)
    {
        var adminUser = new User
        {
            Username = "admin",
            Email = "admin@food.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
            Role = "Admin"
        };

        userRepo.AddUser(adminUser);
        Console.WriteLine("Admin user created!");
    }
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Register}/{id?}");

app.Run();
