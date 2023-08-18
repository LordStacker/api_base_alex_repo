using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler(e =>
{
    e.Run(async context =>
    {
        context.Response.StatusCode = 400;
        context.Response.ContentType = "text/plain";

        var exception = context.Features.Get<IExceptionHandlerFeature>();
        if (exception != null)
        {
            await context.Response.WriteAsync(exception.Error.Message);
        }
    });
});

app.Run();
