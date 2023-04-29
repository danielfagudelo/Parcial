using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Parcial.API.Data;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(a =>
{
    a.SwaggerDoc("v1", new OpenApiInfo { Title = "Atanasio Girardot", Version = "v1" });
    a.AddSecurityDefinition("Client", new OpenApiSecurityScheme
    {
        Description = @"authorized customer <br /> <br />
                      Enter 'Client' enter token<br /> <br />
                      Example: 'Client 12345'<br /> <br />",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Client"
    });
    a.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Client"
              },
              Scheme = "oauth2",
              Name = "Client",
              In = ParameterLocation.Header,
          },
            new List<string>()
        }
    });
});
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("name=ConectionDB"));
builder.Services.AddTransient<SeedDb>();

var app = builder.Build();
SeedData(app);

void SeedData(WebApplication app)
{
    IServiceScopeFactory? scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (IServiceScope? scope = scopedFactory!.CreateScope())
    {
        SeedDb? service = scope.ServiceProvider.GetService<SeedDb>();
        service!.SeedAsync().Wait();
    }
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

app.Run();
