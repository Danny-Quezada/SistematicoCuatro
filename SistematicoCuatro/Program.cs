using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SistematicoCuatro.Domain.Interfaces;
using SistematicoCuatro.Domain.SistematicoCuatroEntities;
using System;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;
using SistematicoCuatro.Infraestructure.Repository;
using SistematicoCuatro.AppCore.IServices;
using SistematicoCuatro.AppCore.Services;

namespace SistematicoCuatro
{
    static class Program
    {
        public static IConfiguration Configuration;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        /// 
        [STAThread]
        static void Main()
        {
            Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddEnvironmentVariables().Build();
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();

            services.AddDbContext<PepitoSchoolContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Default"));
            });
            services.AddScoped<IStudentDBContext, PepitoSchoolContext>();
            services.AddScoped<IEstudianteRepository, EFEstudianteRepository>();
            services.AddScoped<IStudentServices, StudentServices>();
       
            services.AddScoped<Form1>();

            using (var serviceScope = services.BuildServiceProvider())
            {
                var main = serviceScope.GetRequiredService<Form1>();
                Application.Run(main);
            }
        }
    }
}
