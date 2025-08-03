using BookingClient.Contract;
using BookingClient.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDistributedMemoryCache(); // bisa juga Redis kalau production
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // waktu sesi
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<IRuanganService, RuanganService>();

builder.Services.AddHttpClient<BookingPijatService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7299/"); // Ganti sesuai alamat API-mu
});

var app = builder.Build();



app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
