using CapstoneDraft.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

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
