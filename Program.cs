using projectTemplate.Extensions;
using Microsoft.EntityFrameworkCore;
using ProjectTemplate.DAL;
using Microsoft.OpenApi.Models;
using ProjectTemplate.Services.Implementations;
using ProjectTemplate.Services.Interfaces;
using Microsoft.AspNetCore.HttpOverrides;
using projectTemplate.utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<YouBankingDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("YouBankingDbConnection")));
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("AppSetting"));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.ConfigureCors();
//builder.Services.ConfigureIISIntegration();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// builder.Services.AddSwaggerGen(x =>
// {
//     x.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
//     {
//         Title = "my banking api doc",
//         Version = "v2",
//         Description = "this is it",
//         Contact = new Microsoft.OpenApi.Models.OpenApiContact
//         {
//             Name = "Igwe Sunday",
//             Email = "sundayigwe03@gmail.com",
//             Url = new Uri("https://github.com/sunnycool038")
//         }
//     });
//     x.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
// });

// builder.Services.AddControllers();

// var app = builder.Build();

// if (app.Environment.IsDevelopment())
//     app.UseDeveloperExceptionPage();
// else
//     app.UseHsts();



// app.UseForwardedHeaders(new ForwardedHeadersOptions
// {
//     ForwardedHeaders = ForwardedHeaders.All
// });
//app.UseHttpsRedirection();
// app.UseRouting();
// app.UseDeveloperExceptionPage();
//app.UseCors("CorsPolicy");

app.UseAuthorization();
// app.UseSwagger();
// app.UseSwaggerUI(x =>
// {
//     var prefix = string.IsNullOrEmpty(x.RoutePrefix) ? "." : "..";
//     x.SwaggerEndpoint($"{prefix}/v1/swagger.json", "my banking api doc");
// });

// app.UseEndpoints(endpoints =>
// {
// app.MapControllers();
//}

//);
app.MapControllers();

app.Run();
