using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Globalization;
using System.Text;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Features.UploadFile;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystem.Agents;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystems.Installments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystems.Masters;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystems.Motors;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystems.Quotations;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CreateFiles;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Datas.Agents;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Datas.Motors;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Datas.Premiums;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Datas.Reports;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Datas.Rewards;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Notifications;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Payments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Utilities;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Verifies;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Datas.Installments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Services;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Services.Notifications;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Services.Payments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.CoreSystems.Agents;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.CoreSystems.Installments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.CoreSystems.Masters;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.CoreSystems.Motors;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.CoreSystems.Quotations;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.CreateFiles;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.Datas.Agents;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.Datas.Motors;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.Datas.Premiums;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.Datas.Reports;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.Datas.Rewards;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.Verifies;
using TB.WEBAPP.SUBMITMOTOR.INFRASTRUCTURE.Configurations;
using TB.WEBAPP.SUBMITMOTOR.INFRASTRUCTURE.ExternalServices;
using TB.WEBAPP.SUBMITMOTOR.INFRASTRUCTURE.Services;
using TB.WEBAPP.SUBMITMOTOR.SHARED.Helpers;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.Datas.Installments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Datas.Verifies;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.UseCases.Datas.Verifies;

var cultureInfo = new CultureInfo("en-GB");

CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

// Add limit request body
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 200 * 1024 * 1024; // 200MB
});

// configure strongly typed settings object
builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("AppSetting"));
builder.Services.Configure<CoreSystemSetting>(builder.Configuration.GetSection("CoreSystemSetting"));
builder.Services.Configure<CreateFileSetting>(builder.Configuration.GetSection("CreateFileSetting"));
builder.Services.Configure<DataSetting>(builder.Configuration.GetSection("DataWebSiteSetting"));
builder.Services.Configure<PaymentKBankSetting>(builder.Configuration.GetSection("PaymentKBankSetting"));
builder.Services.Configure<PaymentSetting>(builder.Configuration.GetSection("PaymentSetting"));
builder.Services.Configure<ServiceSetting>(builder.Configuration.GetSection("ServiceSetting"));

// Add services to the container.
HttpClientFactory.RegisterHttpClients(builder.Services);

// Register application services
builder.Services.AddScoped<IAgentTokenUseCase, AgentTokenUseCase>();
builder.Services.AddScoped<IAgentUseCase, AgentUseCase>();
builder.Services.AddScoped<IApiClientService, ApiClientService>();
builder.Services.AddScoped<ICreateFileUseCase, CreateFileUseCase>();
builder.Services.AddScoped<IInstallmentUseCase, InstallmentUseCase>();
builder.Services.AddScoped<IJwtReaderService, JwtReaderService>();
builder.Services.AddScoped<IMasterUseCase, MasterUseCase>();
builder.Services.AddScoped<IMotorUseCase, MotorUseCase>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IPaymentInstallmentUseCase, PaymentInstallmentUseCase>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IPremiumUseCase, PremiumUseCase>();
builder.Services.AddScoped<IQuotationUseCase, QuotationUseCase>();
builder.Services.AddScoped<IReportUseCase, ReportUseCase>();
builder.Services.AddScoped<IRewardUesCase, RewardUseCase>();
builder.Services.AddScoped<ISubmitMotorUseCase, SubmitMotorUseCase>();
builder.Services.AddScoped<IUtilityHelper, UtilityHelper>();
builder.Services.AddScoped<IServiceVerifyUseCase, ServiceVerifyUseCase>();
builder.Services.AddScoped<IVerifyUseCase, VerifyUseCase>();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(UploadFileCommand).Assembly);
});

builder.Services.AddControllersWithViews();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

await app.RunAsync();
