using GeotimeNet.Client.Data.Interfaces;
using GeotimeNet.Client.Data.State;
using GeotimeNet.Client.Data;
using GeoTimeNet.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CurrieTechnologies.Razor.SweetAlert2;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) })
                .AddScoped<IGeoTimeConnect, GeoTimeConnect>()
                .AddScoped<LoginState>();

builder.Services.AddSweetAlert2();
builder.Services.AddBlazorBootstrap();
await builder.Build().RunAsync();
