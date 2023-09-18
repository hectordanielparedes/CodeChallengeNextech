using Logic;
using StackExchange.Redis;

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

builder.Services.AddScoped(cfg =>
{
    string connection = builder.Configuration
        .GetConnectionString("RedisAzure");
    IConnectionMultiplexer multiplexer = ConnectionMultiplexer.Connect(connection);
    return multiplexer.GetDatabase();
});

builder.Services.AddScoped<IHackerNewsService, HackerNewsService>();
builder.Services.AddScoped<IHttpClientService, HttpClientService>();

builder.Services.AddHttpClient("hackernews",(httpClient) =>
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
