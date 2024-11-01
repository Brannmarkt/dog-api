using Application.Interfaces;
using Infrastructure.Context;
using Infrastructure.Helpers.Mapper;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Db connection
builder.Services.AddDbContext<ApiDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<IDogRepository, DogRepository>();

// Services
builder.Services.AddScoped<IDogService, DogService>();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Rate limiting
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter(policyName: "fixed", limiterOptions =>
    {
        limiterOptions.PermitLimit = 2;
        limiterOptions.Window = TimeSpan.FromSeconds(1);
        limiterOptions.QueueLimit = 0;
    });
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRateLimiter();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers().RequireRateLimiting("fixed");
app.MapControllers();

app.Run();
