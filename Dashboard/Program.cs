using Microsoft.EntityFrameworkCore;
using Reservation_APIs.Data;
using Reservation_APIs.Interfaces;
using Reservation_APIs.Models;
using Reservation_APIs.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

builder.Services.AddDbContext<ReservationAppContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("local")));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

/*       Repositories       */
builder.Services.AddScoped<IGenericRepository<AppUser>, GenericRepository<AppUser>>();
builder.Services.AddScoped<IGenericRepository<Chat>, GenericRepository<Chat>>();
builder.Services.AddScoped<IGenericRepository<ChatsMessage>, GenericRepository<ChatsMessage>>();
builder.Services.AddScoped<IGenericRepository<FinancialAccount>, GenericRepository<FinancialAccount>>();
builder.Services.AddScoped<IGenericRepository<Reserve>, GenericRepository<Reserve>>();
builder.Services.AddScoped<IGenericRepository<Resort>, GenericRepository<Resort>>();
builder.Services.AddScoped<IGenericRepository<ResortService>, GenericRepository<ResortService>>();
builder.Services.AddScoped<IGenericRepository<ResortsPhoto>, GenericRepository<ResortsPhoto>>();
builder.Services.AddScoped<IGenericRepository<ResortType>, GenericRepository<ResortType>>();
builder.Services.AddScoped<IGenericRepository<TypesOfService>, GenericRepository<TypesOfService>>();
builder.Services.AddScoped<IGenericRepository<UserType>, GenericRepository<UserType>>();
builder.Services.AddScoped<IGenericRepository<ResortAndService>, GenericRepository<ResortAndService>>();


builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<IChatsMessageRepository, ChatsMessageRepository>();
builder.Services.AddScoped<IFinancialAccountRepository, FinancialAccountRepository>();
builder.Services.AddScoped<IReserveRepository, ReserveRepository>();
builder.Services.AddScoped<IResortRepository, ResortRepository>();
builder.Services.AddScoped<IResortServiceRepository, ResortServiceRepository>();
builder.Services.AddScoped<IResortsPhotoRepository, ResortsPhotoRepository>();
builder.Services.AddScoped<IResortTypeRepository, ResortTypeRepository>();
builder.Services.AddScoped<ITypesOfServiceRepository, TypesOfServiceRepository>();
builder.Services.AddScoped<IUserTypeRepository, UserTypeRepository>();
builder.Services.AddScoped<IResortAndServiceRepository, ResortAndServiceRepository>();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
