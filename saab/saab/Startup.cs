using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using saab.Data;
using saab.Middlewares;
using saab.Repository;
using saab.Repository.DBMysql;
using saab.Services.Alerts;
using saab.Services.AuthenticationSaab;
using saab.Services.Billing;
using saab.Services.Clients;
using saab.Services.Concentrate;
using saab.Services.ElectricRates;
using saab.Services.Hierarchy;
using saab.Services.Meter;
using saab.Services.Projects;
using saab.Services.Saving;
using AuthenticationMiddleware = saab.Middlewares.AuthenticationMiddleware;
using BearerAuthMiddleware = saab.Middlewares.BearerAuthMiddleware;

namespace saab
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("PolicitaSAAB", builder =>
            {
                builder.WithOrigins("http://localhost:4200","http://www.saab.autodidactamx.com:8020","http://34.68.45.91:8020")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
            services.AddControllers();
            services.AddDbContext<SaabContext>(
                options =>
                {
                    options.UseMySql(Configuration.GetConnectionString("Default"),
                        Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.23-mysql"));
                }, ServiceLifetime.Transient);
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Saab Service Web",
                        Version = "v1",
                        Description =
                            "This is a demo to see how documentation can easily be generated for ASP.NET Core Web APIs using Swagger and ReDoc.",
                    });
                options.EnableAnnotations();
            });

            services.AddScoped<IXmlCfeRepository, XmlCfeRepository>();
            services.AddScoped<ICuadroTarifarioRepository, CuadroTarifarioRepository>();
            services.AddScoped<ICentroDeCargaRepository, CentroDeCargaRepository>();
            services.AddScoped<IDesglosePagoHistoricoRepository, DesglosePagoHistoricoRepository>();
            services.AddScoped<IFacturacionRepository, FacturacionRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IParametrosHistoricoRepository, ParametrosHistoricoRepository>();
            services.AddScoped<IComparativaHistoricoRepository, ComparativaHistoricoRepository>();
            services.AddScoped<IMedidorRepository, MedidorRepository>();
            services.AddScoped<IPeriodoBipCfeRepository, PeriodoBipCfeRepository>();
            services.AddScoped<IMedidorBessRepository, MedidorBessRepository>();
            services.AddScoped<IMedidorEtbRepository, MedidorEtbRepository>();
            services.AddScoped<IMedidorOperatiRepository, MedidorOperatiRepository>();
            services.AddScoped<ICargaDescargaBateria, CargaDescargaBateria>();

            services.AddScoped<IElectricRates, ElectricRates>();
            services.AddScoped<IDetailAlertService, DetailAlertService>();
            services.AddScoped<IBillingService, BillingService>();
            services.AddScoped<ISavingService, SavingService>();
            services.AddScoped<IProjectsService, ProjectsService>();
            services.AddScoped<IClientsService, ClientsService>();
            services.AddScoped<IProjectsSummaryService, ProjectsSummaryService>();
            services.AddScoped<IHierarchyService, HierarchyService>();
            services.AddScoped<IConcentrateService, ConcentrateService>();
            services.AddScoped<IMeterService, MeterService>();
            services.AddScoped<IAuthenticationSaabService, AuthenticationSaabService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("PolicitaSAAB");

            if (env.IsDevelopment())
            {
            }

            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "saab v1"));

            app.UseWhen(context => context.Request.Path.StartsWithSegments("/auth"),
                applicationBuilder => { applicationBuilder.UseMiddleware<AuthenticationMiddleware>(); });

            app.UseWhen(context => context.Request.Path.StartsWithSegments("/api"),
                applicationBuilder => { applicationBuilder.UseMiddleware<BearerAuthMiddleware>(); });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}