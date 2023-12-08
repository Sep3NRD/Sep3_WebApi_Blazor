using Blazor.Auth;
using Blazor.Data;
using Blazor.Services;
using Blazor.Services.Http;
using Blazor.Services.Interfaces;
using Blazored.LocalStorage;
using Blazored.Toast;
using Domain.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using IItemService = Blazor.Services.Interfaces.IItemService;
using IOrderService = Blazor.Services.Interfaces.IOrderService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<IAuthService, JwtAuthService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddScoped<IOrderService, OrderServiceImpl>();
builder.Services.AddScoped<IWishListService, WishListServiceImpl>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredToast();



builder.Services.AddScoped(
    sp => 
        new HttpClient { 
            BaseAddress = new Uri("http://localhost:5193") 
        }
);
AuthorizationPolicies.AddPolicies(builder.Services);

builder.Services.AddScoped<IItemService, ItemServiceImpl>();
builder.Services.AddScoped<ICustomerService, CustomerServiceImpl>();
builder.Services.AddScoped<IOrderService, OrderServiceImpl>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios,
    // see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");



app.Run();