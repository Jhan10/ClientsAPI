using ClientsAPI.Infraesctruture;
using ClientsAPI.Model;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ISuperuserRepository, SuperuserRepository>();
//builder.Services.AddSwaggerGen(c => { c.EnableAnnotations();
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
    //    options =>
    //{
    //    options.SwaggerEndpoint("/swagger/v1/swagger.json", "ClientAPI v1");
    //    options.RoutePrefix = string.Empty;
    //}
    );
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
