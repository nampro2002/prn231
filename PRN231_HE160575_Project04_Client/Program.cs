
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddSession();
builder.Services.AddMemoryCache();
builder.Services.AddSignalR();
builder.Services.AddMvc();

var app = builder.Build();
app.UseSession();
app.MapRazorPages();
app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();


app.Run();
