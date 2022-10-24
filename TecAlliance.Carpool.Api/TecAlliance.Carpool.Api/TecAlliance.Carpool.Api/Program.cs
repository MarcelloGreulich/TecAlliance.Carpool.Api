using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Filters;
using TecAlliance.Carpool.Business.Sample_Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.ExampleFilters();
});

// make this better
builder.Services.AddSwaggerExamplesFromAssemblyOf<CarpoolDtoSampleProvider>();

builder.Services.AddSingleton<CarpoolDtoSampleProvider>();

builder.Services.AddMvc(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(option =>
    {
        //option.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        //option.RoutePrefix = string.Empty;

    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
