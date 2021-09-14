
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using DocFx;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API
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
            services.Configure<KestrelServerOptions>(Configuration.GetSection("Kestrel"));
            services.AddControllers();
            //Service du versionning
            services.AddApiVersioning(config =>
            {
                //Renvoie dans le header les versions d’api supportées
                config.ReportApiVersions = true;
            });
            //Versioning
            services.AddApiVersioning(config =>
            {
                config.ReportApiVersions = true;
            });
            //End Versioning

            //Changer le mode d'exploration d'api avec la version dans l'url!
            services.AddVersionedApiExplorer(options => {
                options.SubstituteApiVersionInUrl = true;
            });
            // End

            // Documentation Client API
            services.AddOpenApiDocument(config =>
            {
                config.DocumentName = "v1.0";
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1.0";
                    document.Info.Title = "Public API Connector";
                    document.Info.Description = "A simple ASP.NET Core web API";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Mohamed",
                        Email = string.Empty,
                        Url = ""
                    };
                    document.Info.License = new NSwag.OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = ""
                    };
                };
                config.ApiGroupNames = new[] { "1.0" };
            });
            // End OpenAPI Document
            
            // JWT Authentification
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => 
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidAudience = Configuration["JwtIssuer"],
                        ValidIssuer = Configuration["JwtIssuer"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
                        
                        // REtourne la différence de temps maximale autorisée entre le client et les paramètres de l'horloge du serveur
                        ClockSkew =TimeSpan.Zero// remove delay of token  when expire
                    };
                });
            services.AddBLL();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //Middleware de génération du OpenAPIjson
            app.UseOpenApi(config =>
            {
                config.Path = "/api/doc/{documentName}/api.json";
            });
            //END

            // Interface utilisateur en fonction du OpenApi.json
            app.UseSwaggerUi3(config =>
            {
                config.DocumentPath = "/api/doc/{documentName}/api.json";
                config.Path = "/api/doc";
            });
            //END
            // Authentification => code errur 401
            app.UseAuthentication();

            // Autorisation code 403
            app.UseAuthorization();

            app.UseDocFx(config =>
            {
                config.rootPath = "/doc";
            });
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
           
        }
    }
}
