using B1TestTask2.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(x =>
{
    x.UseSqlite(connectionString);
});

builder.Services.AddMvc();

builder.Services.AddRazorPages();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();
app.UseDeveloperExceptionPage();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(name: "default", pattern: "{controller=Main}/{action=Index}/{id?}");
});

app.Run();