using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KontinentService.Models;

namespace KontinentService.Models
{
    public class DefaultContext : DbContext
    {
        public DbSet<CategoriesModel> Categories { get; set; }
        public DbSet<SubcategoriesModel> Subcategories { get; set; }
        public DbSet<UsersModel> Users { get; set; }
        public DbSet<ToursModel> Tours { get; set; }
        public DbSet<FiltersModel> Filters { get; set; }
        public DbSet<FilterAllowablesModel> FilterAllowables { get; set; }
        public DbSet<FilterValuesModel> FilterValues { get; set; }
        public DbSet<CabinsModel> Cabins { get; set; }
        public DbSet<CabinValuesModel> CabinValues { get; set; }
        public DbSet<SchedulesModel> Schedules { get; set; }
        public DbSet<ScheduleValuesModel> ScheduleValues { get; set; }
        public DbSet<ImagesModel> Images { get; set; }
        public DbSet<PropertiesModel> Properties { get; set; }
        public DbSet<PropertyValuesModel> PropertyValues { get; set; }
        public DbSet<FeedbackMessagesModel> FeedbackMessages { get; set; }
        public DbSet<ReviewsModel> Reviews { get; set; }
        public DbSet<OrdersModel> Orders { get; set; }

        public DefaultContext(DbContextOptions<DefaultContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
