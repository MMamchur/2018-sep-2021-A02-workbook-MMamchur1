using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ChinookSystem.DAL;
using ChinookSystem.BLL;
#endregion

namespace ChinookSystem
{
    public static class StartupExtentions
    {
        public static void AddBackendDependencies(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            services.AddDbContext<ChinookContext>(options);
            services.AddTransient<AboutServices>((serviceProvider) => 
            {
               //get the dbcontext class
                var context = serviceProvider.GetRequiredService<ChinookContext>();
                return new AboutServices(context);
            });
            services.AddTransient<AlbumServices>((serviceProvider) =>
             {
                //get the dbcontext class
                var context = serviceProvider.GetRequiredService<ChinookContext>();
                return new AlbumServices(context);
            });
            services.AddTransient<GenreServices>((serviceProvider) =>
            {
                //get the dbcontext class
                var context = serviceProvider.GetRequiredService<ChinookContext>();
                return new GenreServices(context);
            });
        }
    }
}
