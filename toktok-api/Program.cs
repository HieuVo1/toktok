using toktok_api.Extensions;
using toktok_api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureCors();

builder.Services.AddControllers();

builder.Services.ConfigureDbContext(builder.Configuration);

builder.Services.ConfigureJWT(builder.Configuration);

builder.Services.ConfigureServices();

builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureSwaggerGen();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
