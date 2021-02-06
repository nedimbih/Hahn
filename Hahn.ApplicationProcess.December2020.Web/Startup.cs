using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hahn.ApplicationProcess.December2020.Domain;
using Hahn.ApplicationProcess.December2020.Models.Interfaces;
using Hahn.ApplicationProcess.December2020.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Hahn.ApplicationProcess.December2020.Data;
using Microsoft.Extensions.Hosting;
using FluentValidation.AspNetCore;
using Hahn.ApplicationProcess.December2020.Domain.Validations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using System.IO;

namespace Hahn.ApplicationProcess.December2020.Web {
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
			services.AddMvc(options => {
				options.EnableEndpointRouting = false; // mandatory for app.UseMvc in Configure method
				
				// Rejects requests that are not in acceptable format. Acceptable format is to application/json with Produces and Consumes attributes
				options.ReturnHttpNotAcceptable = true;

				// Adds global response type for proper swagger documentation. Alternatively, attributes could be applied on a controller and/or an action level
				options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
				options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status404NotFound));
				options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
				options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
			})
						.AddFluentValidation(fvOptions => fvOptions.RegisterValidatorsFromAssemblyContaining<ApplicantValidator>());
			// if above does not work, remove fvOptions lambda and enable line below (fluent validation nuget might need to be added to the csproj)
			//services.AddTransient<IValidator<Applicant>, ApplicantValidator>();

			services.AddScoped<IApplicantManager, ApplicantManager>();
			services.AddScoped<IApplicantRepo, ApplicantRepo>();

			services.AddSwaggerGen(setup => {
				// setup.SwaggerDoc("upisat naziv", new OpenApiInfo(){
				// 	// todo dodat postavke
				// });
				setup.EnableAnnotations();
				
				// Set the XML comments path for the Swagger JSON and UI.
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				setup.IncludeXmlComments(xmlPath);
				var xmlFile2 = $"{Assembly.GetAssembly(typeof (Applicant)).GetName().Name}.xml";
				var xmlPath2 = Path.Combine(AppContext.BaseDirectory, xmlFile2);
				setup.IncludeXmlComments(xmlPath2);
			}); 
			
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				//app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions {
				//	HotModuleReplacement = true
				//});
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();
			app.UseSwagger();
			app.UseSwaggerUI(setup =>
			{
				setup.SwaggerEndpoint("/swagger/v1/swagger.json", "Applicant API V1");
			});
			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");

				routes.MapSpaFallbackRoute(
					name: "spa-fallback",
					defaults: new { controller = "Home", action = "Index" });
			});
		}
	}
}
