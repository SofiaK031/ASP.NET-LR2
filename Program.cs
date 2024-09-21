using LR2.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<ISelectCompany, SelectCompany>();

// �������� 1
// JSON
builder.Configuration.AddJsonFile("CustomConfigurations/Apple.json");
builder.Configuration["company_" + builder.Configuration["name"] + builder.Configuration["address"] + 
    builder.Configuration["specialization"]] = builder.Configuration["employeeCount"];

// XML
builder.Configuration.AddXmlFile("CustomConfigurations/Google.xml");
builder.Configuration["company_" + builder.Configuration["name"] + builder.Configuration["address"] +
    builder.Configuration["specialization"]] = builder.Configuration["employeeCount"];

// INI
builder.Configuration.AddIniFile("CustomConfigurations/Microsoft.ini");
builder.Configuration["company_" + builder.Configuration["name"] + builder.Configuration["address"] +
    builder.Configuration["specialization"]] = builder.Configuration["employeeCount"];


// �������� 2
builder.Configuration.AddJsonFile("CustomConfigurations/InfoAboutMe.json");

var app = builder.Build();

// ��������� ���������� ��� �������� �������
app.Use(async (context, next) =>
{
    var selectorCompanyService = app.Services.GetService<ISelectCompany>();
    await context.Response.WriteAsync("\tBiggest company\n");
    await context.Response.WriteAsync(selectorCompanyService.GetBiggestCompany().ToString() + "\n\n");
    await next();
});

// ��������� ���������� ��� �����������
app.Use(async (context, next) =>
{
    await context.Response.WriteAsync($"Student: {app.Configuration["student"]}\nGroup: {app.Configuration["group"]}\nHobbies: {app.Configuration["hobbies"]}");
    await next();
});

app.Run();
