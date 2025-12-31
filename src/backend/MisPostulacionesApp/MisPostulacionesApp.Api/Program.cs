using FluentValidation;
using FluentValidation.AspNetCore;
using MisPostulacionesApp.Api.Data.ConnectionSql;
using MisPostulacionesApp.Api.Data.Implements;
using MisPostulacionesApp.Api.Data.Repositories;
using MisPostulacionesApp.Api.Services;
using MisPostulacionesApp.Api.Services.Implements;
using MisPostulacionesApp.Api.Validations;

var builder = WebApplication.CreateBuilder(args);

//Servicios y Repositorios
builder.Services.AddScoped<IDbConnectionFactory, SqlConnectionFactory>();
builder.Services.AddScoped<IPostulacionRepository, PostulacionRepository>();
builder.Services.AddScoped<IPostulacionService, PostulacionService>();
builder.Services.AddSingleton<INotificacionService,WhatsAppService>();
builder.Services.AddHttpClient<IAService>();

builder.Services.AddControllers();

//FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<RegistrarPostulacionRequestValidation>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddValidatorsFromAssemblyContaining<RegistrarPostulacionRequestValidation>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.Run();
