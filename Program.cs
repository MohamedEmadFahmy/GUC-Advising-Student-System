var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


builder.Services.AddDistributedMemoryCache();

// builder.Services.AddSession(options =>
// {
//     options.IdleTimeout = TimeSpan.FromMinutes(10);
//     // options.Cookie.HttpOnly = true;
//     // options.Cookie.IsEssential = true;
// });
builder.Services.AddSession();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSession();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapRazorPages();

app.Run();
