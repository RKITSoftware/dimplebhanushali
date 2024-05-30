using Filters.Filters.Asynchronous_Filter;
using Filters.Filters.Authorisation_FIlter;
using Filters.Filters.Exception_Filter;
using Filters.Filters.Resource_FIlter;
using Filters.Filters.Result_FIlter;
using Filters.Filters.Synchronous_Filter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    // Register the synchronous filter globally for all controllers
    options.Filters.Add(typeof(LogActionFilter));
});

// Register the asynchronous filter globally for all controllers
builder.Services.AddScoped<LogActionFilter>();
builder.Services.AddScoped<LogAsyncActionFilter>();
builder.Services.AddScoped<MyAuthorizationFilter>();
builder.Services.AddScoped<MyResourceFilter>();
builder.Services.AddScoped<MyResultFilter>();
builder.Services.AddScoped<MyExceptionFilter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
