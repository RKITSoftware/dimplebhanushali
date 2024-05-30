var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

//Configuring Middleware Component using Use and Run Extension Method

////First Middleware Component Registered using Use Extension Method
//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("Middleware1: Incoming Request\n");
//    //Calling the Next Middleware Component
//    await next();
//    await context.Response.WriteAsync("Middleware1: Outgoing Response\n");
//});

////Second Middleware Component Registered using Use Extension Method
//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("Middleware2: Incoming Request\n");
//    //Calling the Next Middleware Component
//    await next();
//    await context.Response.WriteAsync("Middleware2: Outgoing Response\n");
//});


////Third Middleware Component Registered using Run Extension Method
//app.Run(async (context) =>
//{
//    await context.Response.WriteAsync("Middleware3: Incoming Request handled and response generated\n");
//    //Terminal Middleware Component i.e. cannot call the Next Component
//});

//app.MapGet("/", () => $"EnvironmentName: {app.Environment.EnvironmentName} \n" +
//            $"ApplicationName: {app.Environment.ApplicationName} \n" +
//            $"WebRootPath: {app.Environment.WebRootPath} \n" +
//            $"ContentRootPath: {app.Environment.ContentRootPath}");


//// Use UseFileServer instead of UseDefaultFiles and UseStaticFiles
//FileServerOptions fileServerOptions = new FileServerOptions();
//fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
//fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("home.html");
//app.UseFileServer(fileServerOptions);

//app.UseDefaultFiles(); // To set index.html as default web page.

//app.UseDirectoryBrowser(); // Enables Directory Browser for Current web root folder

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.Run();
