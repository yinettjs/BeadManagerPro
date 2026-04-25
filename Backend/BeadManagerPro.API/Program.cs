using Microsoft.EntityFrameworkCore;
using BeadManagerPro.API.Middlewares;
using BeadManagerPro.Application.MappingProfile;
using BeadManagerPro.Application.Services;
using BeadManagerPro.Infrastructure.Repositories;
using BeadManagerPro.Persistence.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BeadManagerProContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<DbContext>(sp => sp.GetRequiredService<BeadManagerProContext>());
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<PedidoService>();
builder.Services.AddScoped<GestionPersonasService>();
builder.Services.AddScoped<MaterialService>();
builder.Services.AddScoped<PiezaService>();

builder.Services.AddAutoMapper(cfg => {
    cfg.AddProfile<BeadManagerProMappingProfile>();
});


builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll"); // Aplicar política explícita
app.UseAuthorization();
app.MapControllers();

app.Run();