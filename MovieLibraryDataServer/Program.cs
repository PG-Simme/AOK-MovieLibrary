using Microsoft.EntityFrameworkCore;
using MovieLibraryDataServer.Components;
using MovieLibraryDataServer.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSignalR();

builder.Services.AddDbContextFactory<MovieContext>(options =>
{
    options.UseSqlServer(
        "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=\"AOK SignalR\";Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False",
        o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
});

// Add services to the container from the library project
builder.Services.AddLibraryServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<MovieHub>("/moviehub");
    endpoints.MapHub<PersonHub>("/personhub");
});

app.Run();
