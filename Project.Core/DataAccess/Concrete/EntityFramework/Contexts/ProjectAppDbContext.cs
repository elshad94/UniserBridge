using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Project.Core.Settings;

namespace Project.Core.DataAccess.Concrete.EntityFramework.Contexts
{
    public class ProjectAppDbContext : ProjectAppDbContextBase
    {
        public ProjectAppDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(AppSettings.Settings.ProjectAppDbConnectionModel.ToString());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var spTypes = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.Name.StartsWith("SP_") 
                            || t.Name.StartsWith("FN_"))
                .ToHashSet();

            var orderedTypes = new List<Type>();

            while (spTypes.Count > 0)
            {
                foreach (var spType in spTypes)
                {
                    var isParentClass = spTypes.Any(spTypeInner => spTypeInner.IsSubclassOf(spType));

                    if (isParentClass)
                    {
                        continue;
                    }

                    orderedTypes.Add(spType);
                    spTypes.Remove(spType);
                }
            }


            foreach (var spType in orderedTypes)
            {
                modelBuilder.Entity(spType).HasNoKey();
            }
        }
    }
}