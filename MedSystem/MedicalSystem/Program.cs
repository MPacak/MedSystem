using BL.AutoMapper;
using BL.DTO;
using BL.IServices;
using BL.Services;
using DAL.IRepositories;
using DAL.Models;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MedicalSystemContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("MSDatabaseConnStr"));
});
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<PatientDtoValidator>();

builder.Services.AddScoped<IUnitofWork, UnitofWork>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IRepositoryFactory, RepositoryFactory>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<ICheckupService, CheckupService>();
builder.Services.AddScoped<IMedicalHistoryService, MedicalHistoryService>();
builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();
builder.Services.AddScoped<IGenderService, GenderService>();
builder.Services.AddScoped<IDiseaseService, DiseaseService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Patient}/{action=Index}/{id?}");

app.Run();
