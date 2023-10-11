using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using BackendNET.Models; // Ganti dengan namespace yang sesuai dengan model Karyawan Anda
using DotNetEnv;
using System.Linq.Expressions;

var builder = WebApplication.CreateBuilder(args);
DotNetEnv.Env.Load();
string connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");

// Konfigurasi koneksi MongoDB
Console.WriteLine("Connecting to MongoDB...");
IMongoClient mongoClient;

try
{
    mongoClient = new MongoClient(connectionString); // Ganti dengan URL dan port MongoDB Anda
    Console.WriteLine("MongoDB connection successful.");
}
catch (Exception ex)
{
    Console.WriteLine("Failed to connect to MongoDB: " + ex.Message);
    throw; // Teruskan pengecualian
}

builder.Services.AddSingleton<IMongoClient>(mongoClient);


// Tambahkan kontroler dan Swagger
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Konfigurasi middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
