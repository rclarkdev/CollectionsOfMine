using CollectionsOfMine.Authorization;
using CollectionsOfMine.Data.Context;
using CollectionsOfMine.Models;
using CollectionsOfMine.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CollectionsOfMineDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<CollectionnsOfMineDbContext>();

//builder.Services.AddIdentityServer()
//    .AddApiAuthorization<ApplicationUser, CollectionnsOfMineDbContext>();

//builder.Services.AddAuthentication()
//    .AddIdentityServerJwt();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//builder.Services.AddTransient(typeof(IItemService), typeof(ItemService));
//builder.Services.AddTransient(typeof(IFileService), typeof(FileService));
//builder.Services.AddTransient(typeof(IAttributeService), typeof(AttributeService));
//builder.Services.AddTransient(typeof(ICollectionService), typeof(CollectionService));
//builder.Services.AddTransient(typeof(IAreaService), typeof(AreaService));
//builder.Services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
//builder.Services.AddTransient<IUserService, UserService>();
//builder.Services.AddTransient<IContentTypeService, ContentTypeService>();

//builder.Services.AddScoped<IJwtUtils, JwtUtils>();

//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
//app.UseIdentityServer();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");
app.MapRazorPages();

app.MapFallbackToFile("index.html"); ;

app.Run();
