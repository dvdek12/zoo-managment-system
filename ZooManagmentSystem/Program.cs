using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuestPDF.Infrastructure;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using ZooManagmentSystem.Data;
using ZooManagmentSystem.Hubs;
using ZooManagmentSystem.Models.Employee;
using ZooManagmentSystem.Services;

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]);


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'ZooManagmentSystemContext' not found.")));

// External animals api 
builder.Services.AddHttpClient("ExternalAnimalsClient", client =>
{
    client.BaseAddress = new Uri("https://api.api-ninjas.com/v1/animals");
    client.DefaultRequestHeaders.Add("X-Api-Key", "Irz4oqPTeyM4tZ6lg6GfFOYeGjQjdn41mmz14S0N");
});

builder.Services.AddCors(options => {
    options.AddPolicy("AllowVue", builder =>
    {
        builder.WithOrigins("http://localhost:5173")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();    
    });
});

builder.Services.AddIdentityCore<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders()
    .AddSignInManager();

// KONFIGURACJA JWT BEARER
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            RoleClaimType = ClaimTypes.Role
        };
        options.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                context.HandleResponse();
                context.Response.StatusCode = 401;
                return Task.CompletedTask;
            },
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];
                var path = context.HttpContext.Request.Path;
                Console.WriteLine($"=== OnMessageReceived ===");
                Console.WriteLine($"Path: {path}");
                Console.WriteLine($"Token z query: '{accessToken}'");

                if (!string.IsNullOrEmpty(accessToken) &&
                    path.StartsWithSegments("/hubs/notifications"))
                {
                    context.Token = accessToken;
                    Console.WriteLine("Token ustawiony!");

                }
                else
                {
                    Console.WriteLine($"Token NIE ustawiony. Czy token pusty: {string.IsNullOrEmpty(accessToken)}, Czy œcie¿ka pasuje: {path.StartsWithSegments("/hubs/notifications")}");
                }
                return Task.CompletedTask;
            }
        };
    });


builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

// Configuration for JSON serialization to handle reference loops 
//Used because of the cycle in the relationship between TicketModel and TicketEntryTypeModel
//It may issue in the future, than it will be necessary to change the way of handling this relationship
builder.Services.AddControllers().AddJsonOptions(options => {
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();
builder.Services.AddScoped<NotificationService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    /*  Test Employees
    var testEmployees = new[]
    {
        new { Email = "anna.nowak@zoo.pl", First = "Anna", Last = "Nowak", Birth = new DateTime(1988, 3, 10) },
        new { Email = "piotr.wisniewski@zoo.pl", First = "Piotr", Last = "Wiœniewski", Birth = new DateTime(1992, 7, 22) },
    };
    */

    foreach (var role in new[] { "Employee", "Client", "Manager" })
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }

    // Set QuestPDF license
    QuestPDF.Settings.License = LicenseType.Community;



    /*  Test Employees
    foreach (var emp in testEmployees)
    {
        if (await userManager.FindByEmailAsync(emp.Email) == null)
        {
            var user = new ApplicationUser { UserName = emp.Email, Email = emp.Email };
            var result = await userManager.CreateAsync(user, "Test1234!");
            if (result.Succeeded)
            {
                context.Employees.Add(new EmployeeModel
                {
                    ApplicationUserId = user.Id,
                    Email = emp.Email,
                    FirstName = emp.First,
                    LastName = emp.Last,
                    BirthDay = emp.Birth,
                });
                await context.SaveChangesAsync();
                await userManager.AddToRoleAsync(user, "Employee");

                bool isManager = await context.Employees
                    .Where(e => e.ApplicationUserId == user.Id)
                    .Select(e => e.Role.IsManagerial)
                    .FirstOrDefaultAsync();

                Debug.WriteLine(isManager);

                if (isManager)
                {
                    Console.WriteLine(isManager);
                    await userManager.AddToRoleAsync(user, "Manager");
                }
            }
        }
    }
    */
}


app.UseSwaggerUI(options => {
    options.DocExpansion(DocExpansion.None); // wszystko zwiniête
});


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("AllowVue");

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapHub<GorillaHealthNot>("/gorillaHealthNot");

app.MapHub<NotificationHub>("/hubs/notifications");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
