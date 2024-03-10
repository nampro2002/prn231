using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PRN231_HE160575_Project04;
using PRN231_HE160575_Project04.ModelsV2;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
#region 1.Adding Controllers Service to Use Controller
builder.Services.AddControllers();
//MVC service
builder.Services.AddControllersWithViews();
#endregion
#region 2.Setting DBcontext
builder.Services.AddDbContext<PRN231_ProjectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("default")));
#endregion
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<MyAuthorizationFilter>();
builder.Services.AddSwaggerGen(c =>
{
    // Thêm đoạn mã sau
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("BACHSONGDUCHE160575")) // Thay thế bằng một khóa bí mật thực tế
        };
    });

#region ALLOW CROS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin();
                          policy.AllowAnyMethod();
                          policy.AllowAnyHeader();
                          policy.WithExposedHeaders("Content-Range");
                      });
});
#endregion
var app = builder.Build();
#region 1.Setting Up Controlerr Rule
//default controller : "Product".
//default action :"Index".
//variable"id" is an option
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );
#endregion
#region 2.Allow Use Statics File URL
app.UseStaticFiles(); //Use Default folder wwwroot/js/myScript.js
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "wwwroot")),// custom Path folder ~/lib/...
    RequestPath = "/wwwroot" // URL to Acess StaticFiles
});
#endregion
#region 3.Config Development Enviroment
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#endregion

app.UseAuthentication();
app.UseAuthorization();
//app.UseCors(MyAllowSpecificOrigins);
app.UseCors(x => x
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true));
app.Run();
