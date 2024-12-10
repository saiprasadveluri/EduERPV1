using EduERPApi.Data;
using EduERPApi.RepoImpl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using EduERPApi.Infra;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(mvcOpts => { mvcOpts.Filters.Add<OrganizationValueReaderFilter>(); }).AddJsonOptions(options =>
{
    options.JsonSerializerOptions
    .PropertyNamingPolicy = null;
}); ;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var services = builder.Services;
var Config = builder.Configuration;
services.AddDbContext<EduERPDbContext>(opts =>
{
    opts.UseSqlServer(Config.GetConnectionString("EduERPDbConnectionString"));
});
services.AddScoped<UnitOfWork>();
services.AddAuthentication(cfg =>
{
    cfg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt => opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
{
    ValidateIssuer = false,
    ValidateAudience = false,
    ValidateIssuerSigningKey = true,
    ValidIssuer = Config.GetSection("JwtConfig:Issuer").Value,
    ValidAudience= Config.GetSection("JwtConfig:Audience").Value,
    IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config.GetSection("JwtConfig:SigningKey").Value))
}) ;
services.AddAuthorization();
services.AddCors(cfg =>
{
    cfg.AddPolicy("AllowAll", pol =>
    {
        pol.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    });
});
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(30);
    
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
/*app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");*/

app.Run();
