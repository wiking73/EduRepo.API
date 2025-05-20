using Microsoft.EntityFrameworkCore;
using EduRepo.Infrastructure;
using EduRepo.Application.Zadania;
using EduRepo.API.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using EduRepo.Domain;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IPasswordHasher<AppUser>, PasswordHasher<AppUser>>();

builder.Services.AddControllers(opt =>
{ 
var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
opt.Filters.Add(new AuthorizeFilter(policy));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    {
    c.CustomSchemaIds(type => type.FullName);
});
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddMediatR(cfg =>
cfg.RegisterServicesFromAssembly(typeof(List.Handler).Assembly));


builder.Services.AddScoped<PasswordHasher<AppUser>>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000", "https://localhost:7157")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); 
    });
});
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("CorsPolicy");
app.MapControllers();
app.UseStaticFiles(); // pozwala na serwowanie plików z wwwroot
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<DataContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    await context.Database.MigrateAsync();

    await Seed.SeedData(context, userManager);
    context.Database.Migrate();
 

}
catch (Exception ex)
{
    var message = ex.ToString();
}

app.Run();
