using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add EF Core SQL Server using ConnectionStrings:Default
//builder.Services.AddDbContext<Jurr.Data.JurrDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
//);

// Admin password service
//builder.Services.AddSingleton<Jurr.Services.IAdminPasswordService, Jurr.Services.AdminPasswordService>();

var app = builder.Build();

// Seed initial super admin if missing
//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<Jurr.Data.JurrDbContext>();
//    var pw = scope.ServiceProvider.GetRequiredService<Jurr.Services.IAdminPasswordService>();
//    var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();

//    var adminUserName = config["InitialAdmin:UserName"] ?? "admin";   // fallback for dev
//    var adminEmail = config["InitialAdmin:Email"] ?? "admin@example.com";
//    var adminPassword = config["InitialAdmin:Password"];

//    if (string.IsNullOrWhiteSpace(adminPassword))
//    {
//        // For production you *want* to fail loud if password is not provided
//        throw new InvalidOperationException(
//            "Initial admin password is not configured. Set InitialAdmin:Password or environment variable.");
//    }

//    var existing = db.Admins.FirstOrDefault(a => a.UserName == "pnrmnpn");
//    if (existing == null)
//    {
//        var admin = new Jurr.Data.Admin
//        {
//            UserName = adminUserName,
//            NormalizedUserName = adminEmail.ToUpperInvariant(),
//            Email = adminEmail,
//            NormalizedEmail = adminEmail.ToUpperInvariant(),
//            IsActive = true,
//            IsSuperAdmin = true,
//            CreatedAt = DateTime.UtcNow,
//            LastLoginAt = DateTime.UtcNow
//        };
//        admin.PasswordHash = pw.HashPassword(admin, adminPassword);
//        db.Admins.Add(admin);
//        db.SaveChanges();
//    }
//}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
