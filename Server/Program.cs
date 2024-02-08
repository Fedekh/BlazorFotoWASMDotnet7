using BlazorFotoWASMDotnet7.Shared.DB;
using BlazorFotoWASMDotnet7.Shared.Services;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


// Aggiungi il servizio HttpClient configurando il BaseAddress con la variabile d'ambiente "BaseUrl"
builder.Services.AddHttpClient("MyApiClient", client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ApiSettings:BaseUrl"));
});

builder.Services.AddDbContext<FotoContext>();
builder.Services.AddScoped<FotoContext,FotoContext>();

builder.Services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<FotoContext>();

builder.Services.AddScoped<IRepository,FotoRepository>();   
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();


/*
 * AddScoped(sp => new HttpClient { BaseAddress = new Uri("wwefwefw") }):
Questa riga di codice registra un'istanza specifica di HttpClient come un servizio scoped.
Ogni volta che viene richiesto un oggetto HttpClient all'interno dello stesso ambito (ad esempio, durante la stessa richiesta HTTP),
verrà restituita la stessa istanza di HttpClient. Questo è utile se hai bisogno di condividere la stessa istanza di HttpClient all'interno di uno stesso scopo, come una singola richiesta HTTP.

AddHttpClient("MyClient", client => client.BaseAddress = new Uri("sdsdsddsd")):
Questa riga di codice utilizza il metodo AddHttpClient, che è specificamente progettato per la configurazione di HttpClient.
In questo caso, viene configurato un client HttpClient denominato "MyClient" con una specifica BaseAddress. 
Quando questo servizio viene iniettato, verrà restituita un'istanza di HttpClient configurata con le impostazioni specificate.
Questo metodo è utile se hai bisogno di configurare diversi client HttpClient con opzioni diverse.
 */