using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VulnerableAPIProject;
using VulnerableAPIProject.Data;
using VulnerableAPIProject.JWT;
using VulnerableAPIProject.Repository.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

/* builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration.GetSection("JWTSettings:Token").Value,
        ValidAudience = builder.Configuration.GetSection("JWTSettings:Audience").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWTSettings:Key").Value))

    };
}); */

builder.Services.AddControllers();
builder.Services.AddTransient<Seed>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

 /* builder.Services.Configure<JWTConfig>(builder.Configuration.GetSection(nameof(JWTConfig)));
builder.Services.AddScoped<IJWTConfig>(sp => sp.GetRequiredService<IOptions<JWTConfig>>().Value);
builder.Services.AddScoped<JWTAuthManager>(); */

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IAccountRepo, AccountRepo>();
builder.Services.AddScoped<IProfileRepo, ProfileRepo>();




builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

SeedData(app);
void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<Seed>();
        service.SeedDataContext();
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
