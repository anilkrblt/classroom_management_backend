
using ClassroomManagement.Extensions;
using Contracts;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Load NLog configuration
// LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Configure services using extensions
builder.Services.ConfigureCors();
builder.Services.ConfigureISSIntegration();
// builder.Services.ConfigureLoggerService();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureTokenService();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));

// Configure custom responses
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter() =>
    new ServiceCollection()
        .AddLogging()
        .AddMvc()
        .AddNewtonsoftJson()
        .Services.BuildServiceProvider()
        .GetRequiredService<IOptions<MvcOptions>>().Value.InputFormatters
        .OfType<NewtonsoftJsonPatchInputFormatter>().First();

builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = false;
    config.ReturnHttpNotAcceptable = true;
    config.InputFormatters.RemoveType<TextInputFormatter>(); // text/plain'i devre dışı bırak
    config.OutputFormatters.RemoveType<TextOutputFormatter>();
    config.OutputFormatters.RemoveType<XmlDataContractSerializerOutputFormatter>();
}).AddApplicationPart(typeof(ClassroomManagementPresentation.AssemblyReference).Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<DefaultResponseContentTypeFilter>();

    // ...
    // c.OperationFilter<FormFileOperationFilter>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
        c.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Model);
    });
}

if (app.Environment.IsProduction())
    app.UseHsts();

// var logger = app.Services.GetRequiredService<ILoggerManager>();
// app.ConfigureExceptionHandler(logger);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();




























/*using ClassroomManagement.Extensions;
using Contracts;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Load NLog configuration


LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Configure services using extensions
builder.Services.ConfigureCors();
builder.Services.ConfigureISSIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureTokenService();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureJWT(builder.Configuration); 
builder.Services.AddAutoMapper(typeof(Program));

// Configure custom responses
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter() =>
    new ServiceCollection()
        .AddLogging()
        .AddMvc()
        .AddNewtonsoftJson()
        .Services.BuildServiceProvider()
        .GetRequiredService<IOptions<MvcOptions>>().Value.InputFormatters
        .OfType<NewtonsoftJsonPatchInputFormatter>().First();

builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
    config.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
}).AddXmlDataContractSerializerFormatters()
  .AddApplicationPart(typeof(ClassroomManagementPresentation.AssemblyReference).Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction())
    app.UseHsts();

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
    
app.Run();
*/