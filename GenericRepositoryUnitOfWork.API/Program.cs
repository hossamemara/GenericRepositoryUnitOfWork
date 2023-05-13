



using GenericRepositoryUnitOfWork.Core.AutoMapper;
using GenericRepositoryUnitOfWork.Core.DependencyInjection;
using GenericRepositoryUnitOfWork.Core.MicrosoftIdentity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var ConnectionStrings = builder.Configuration.GetConnectionString("CompanyConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseLazyLoadingProxies().UseSqlServer(ConnectionStrings));
builder.Services.AddAutoMapper(auto => auto.AddProfile(new DomainProfile()));
// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
;
builder.Services.AddDependencyInjection();
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
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
