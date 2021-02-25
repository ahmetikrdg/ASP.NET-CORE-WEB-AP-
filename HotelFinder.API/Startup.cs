using HotelFinder.Business.Abstract;
using HotelFinder.Business.Concrete;
using HotelFinder.DataAccess.Abstract;
using HotelFinder.DataAccess.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelFinder.API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<IHotelServices, HotelManager>();
            services.AddSwaggerDocument(Config=>//swareg entegrasyonumu yap�p i�indeki proje ismini ve s�r�m�n� de�i�tirdim ekranda g�r�zkmesi i�in
            {
                Config.PostProcess = (doc =>
                {
                    doc.Info.Title = "Hotels Api";
                    doc.Info.Version = "1.0.13";
                    doc.Info.Contact = new NSwag.OpenApiContact()
                    {
                        Name = "Ahmet Karada�",
                        Email = "ahmetikrdg@outlook.com",
                        
                    };
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseOpenApi();//daha sonra buradan a�t�m swagerlerimi
            app.UseSwaggerUi3();//bu kadar
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
