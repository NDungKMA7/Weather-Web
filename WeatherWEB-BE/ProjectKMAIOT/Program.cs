using ProjectKMAIOT.Hubs;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddSession();
builder.Services.AddCors();
builder.Services.AddSignalR();
var app = builder.Build();

app.UseSession();

app.UseStaticFiles();
app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials()
            );
app.MapHub<SignalRServer>("/signalServer");
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
