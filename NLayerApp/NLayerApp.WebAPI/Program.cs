using Microsoft.EntityFrameworkCore;
using NLayerApp.BLL.Services;
using NLayerApp.DAL;
using NLayerApp.DAL.Interfaces;
using NLayerApp.DAL.Repository;
using NLayerApp.WebAPI.RabbitMQ;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IDepartmentServices, DepartmentServices>();
builder.Services.AddScoped<IDoctorServices, DoctorServices>();
builder.Services.AddScoped<IPatientServices, PatientServices>();
builder.Services.AddScoped<IRabbitMQProducer, RabbitMQProducer>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options => options.UseMySql($"server={Environment.GetEnvironmentVariable("DB_ADDRESS")};user=root;password=87dima87;database=hospital", ServerVersion.AutoDetect($"server={Environment.GetEnvironmentVariable("DB_ADDRESS")};user=root;password=87dima87;database=hospital")));
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = $"{Environment.GetEnvironmentVariable("REDIS_ADDRESS")}:6379";
});
var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
// }

//app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<DataContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}
app.Run();
