using InspectionService.Data;
using InspectionService.Interfaces;
using InspectionService.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Console.WriteLine("--- Using InMemory DB");
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InpectDb"));


builder.Services.AddScoped<IInspectionRepo, InspectionRepo>();
builder.Services.AddScoped<IInspectionTypeRepo, InspectionTypeRepo>();
builder.Services.AddScoped<IUserRepo, UserRepo>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// builder.Services.AddAuthentication(options =>
// {
//     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//     options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
// })
// // Adding Jwt Bearer
// .AddJwtBearer(options =>
// {
//     options.SaveToken = true;
//     options.RequireHttpsMetadata = false;
//     options.TokenValidationParameters = new TokenValidationParameters()
//     {
//         ValidateIssuer = true,
//         ValidateAudience = true,
//         ValidateLifetime = true,
//         ValidateIssuerSigningKey = true,
//         ClockSkew = TimeSpan.Zero,
//         ValidAudience = builder.Configuration["JWT:Audience"],
//         ValidIssuer = builder.Configuration["JWT:Issuer"],
//         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
//     };
// });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Enable CORS to allow requests from this IP
// var inspectionAppURL = builder.Configuration["InspectionAppURL"]?.ToString();
builder.Services.AddCors();
// builder.Services.AddCors(options =>
// {
//     options.AddPolicy(name: myAllowSpecificOrigins,
//         builder =>
//         {
//             builder.WithOrigins(inspectionAppURL)
//             .AllowAnyMethod()
//             .AllowAnyHeader();
//         });
// });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.UseHttpsRedirection();
//app.UseCors(myAllowSpecificOrigins);
app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials());

//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

PrebDb.PrebPopulation(app);

app.Run();
