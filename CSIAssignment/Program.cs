using Microsoft.EntityFrameworkCore;
using CSIAssignment.Data;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<NotesDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.WithOrigins(
                            "https://urban-space-journey-4qr669pjw9rc4j4-3000.app.github.dev",
                            "https://urban-space-journey-4qr669pjw9rc4j4-5005.app.github.dev")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

var app = builder.Build();


app.UseCors("AllowReactApp");
app.MapControllers();
app.Run();
