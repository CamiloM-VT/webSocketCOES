var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddHttpClient(); 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy =>
        {
            policy.WithOrigins("http://localhost:5200") 
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials(); 
        });
});

var app = builder.Build();

app.UseRouting();

app.UseCors("AllowSpecificOrigin");


app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<FrecuenciaHub>("/frecuenciaHub");
});

app.Run();
