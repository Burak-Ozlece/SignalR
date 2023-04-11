using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SignalRServerExample.Business;
using SignalRServerExample.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRServerExample
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors(options => options.AddDefaultPolicy(policy => 
				policy.AllowAnyMethod()
				.AllowAnyHeader()
				.AllowCredentials()
				.SetIsOriginAllowed(origin => true)
			
			));
			services.AddSignalR();

			services.AddTransient<MyBusiness>();
			services.AddControllers();
		}
		
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseCors();
			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapHub<MyHub>("/myHub");
				endpoints.MapControllers();
			});
		}
	}
}
