
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Smart_Library.SmartLibraryManagement;
using Smart_Library.SmartLibraryManagement.Interface;
using Smart_Library.SmartLibraryManagement.Repositories;
using Smart_Library.SmartLibraryManagement.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DatabaseLibrary>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("SmartLibrary")));

builder.Services.AddMemoryCache();

builder.Services.AddCors(options =>
{
    options.AddPolicy( "AllowAll",
        policy => policy.AllowAnyOrigin()
                                 .AllowAnyHeader()
                                 .AllowAnyMethod());
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ILoanRepository, LoanRepository>();
builder.Services.AddScoped<IFineRepository, FineRepository>();
builder.Services.AddScoped<IFacultyRepository, FacultyRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
