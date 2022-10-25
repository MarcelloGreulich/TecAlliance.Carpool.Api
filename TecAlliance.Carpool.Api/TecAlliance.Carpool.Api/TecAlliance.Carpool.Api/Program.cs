using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Filters;
using TecAlliance.Carpool.Business.Sample_Data;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    //sample Provider
    options.ExampleFilters();
    //Change UserInterface Information
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Carpool API",
        Description = "An ASP.NET Core Web API for managing Carpools",
        //TermsOfService = new Uri("https://example.com/terms"),
        //Contact = new OpenApiContact
        //{
        //    Name = "Example Contact",
        //    Url = new Uri("https://example.com/contact")
        //},
        //License = new OpenApiLicense
        //{
        //    Name = "Example License",
        //    Url = new Uri("https://example.com/license")
        //}
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// make this better
builder.Services.AddSwaggerExamplesFromAssemblyOf<CarpoolDtoSampleProvider>();

builder.Services.AddSingleton<CarpoolDtoSampleProvider>();

//builder.Services.AddSwaggerExamplesFromAssemblyOf<CarpoolDtoWithUserInformationSampleProvider>();

builder.Services.AddSingleton<CarpoolDtoWithUserInformationSampleProvider>();

//builder.Services.AddSwaggerExamplesFromAssemblyOf<UserDtoSampleProvider>();

builder.Services.AddSingleton<UserDtoSampleProvider>();

builder.Services.AddMvc(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //Abwärtskomatiblität 
    app.UseSwagger(options =>
    {
        options.SerializeAsV2 = true;
    });

    //Link verändern launchsettings Url nicht mehr nötig
    app.UseSwaggerUI(option =>
    {
        option.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        option.RoutePrefix = string.Empty;

    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
