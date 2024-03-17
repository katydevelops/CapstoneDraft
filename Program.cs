using CapstoneDraft.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Configure SQLite Database using Entity Framework and set connection string
builder.Services.AddDbContext<CapstoneContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("CapstoneDraftConnectionString")));

// Configure ASP.NET Identity in application to handle authentication and authorization and set require confirmed user account to false for easier testing to prevent having to verify real email addresses



// Services that will be used in Safety Net app


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

// ASP.NET Core Middleware that allows a user to be authenticated into the app and then will allow for authorization to persist across the app
// Per documentation, UseAuthentication() must always come before the UseAuthorizatiom() middleware to work properly
app.UseAuthentication();
app.UseAuthorization();


app.MapRazorPages(); // allows the Razor .cshtml pages to be used as endpoints in app
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
