using Logic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<HackerNewsService>();
builder.Services.AddScoped<ICacheService, CacheService>();

builder.Services.AddStackExchangeRedisCache(redisOptions =>
{
    string connection = builder.Configuration
        .GetConnectionString("Redis");
    redisOptions.Configuration = connection;
});


//builder.Services.AddScoped<IHttpClientService, HttpClientService>();
builder.Services.AddHttpClient<HttpClientService>((httpClient) =>
{
    httpClient.BaseAddress = new Uri("https://hacker-news.firebaseio.com");
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");
app.MapControllers();

app.MapFallbackToFile("index.html"); ;

app.Run();
