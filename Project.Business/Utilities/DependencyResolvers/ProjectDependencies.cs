using Microsoft.Extensions.DependencyInjection;
using Project.Core.DataAccess.Abstract;
using Project.Core.DataAccess.Concrete.EntityFramework.Repositories;
using Project.DataAccess.Repositories.Abstract.CardOperations;
using Project.DataAccess.Repositories.Abstract.Common;
using Project.DataAccess.Repositories.Abstract.System;
using Project.DataAccess.Repositories.Concrete.CardOperations;
using Project.DataAccess.Repositories.Concrete.Common;
using Project.DataAccess.Repositories.Concrete.System;

namespace Project.Business.Utilities.DependencyResolvers
{
    public static class ProjectDependencies
    {

        public static void AddProjectDependencies(this IServiceCollection services)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(assembly => assembly.GetName().ToString().Contains("Project"));

            foreach (var assembly in assemblies)
            {
                var concreteClassTypes = assembly.GetExportedTypes().Where(x => (x.Name.EndsWith("Repository") || x.Name.EndsWith("Service")) && x.IsClass);

                foreach (var concreteClassType in concreteClassTypes)
                {
                    var interfaceType = concreteClassType.GetInterfaces().Where(x => !x.Name.StartsWith("IEntityRepositoryBase")).FirstOrDefault();  // exclude IEntityRepositoryBase

                    if (interfaceType != null)
                        services.AddScoped(interfaceType, concreteClassType);
                }
            }

            //#region System
            //services.AddScoped<IAuthRepository, AuthRepository>();
            //services.AddScoped<IGlobalRepository, GlobalRepository>();

            //services.AddScoped<IMenuRepository, MenuRepository>();
            //services.AddScoped<IModuleRepository, ModuleRepository>();
            //services.AddScoped<IUserTokenRepository, UserTokenRepository>();
            //services.AddScoped<IRoleMenuRepository, RoleMenuRepository>();
            //services.AddScoped<IRoleRepository, RoleRepository>();
            //services.AddScoped<IGlobalRepository, GlobalRepository>();
            //services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            //services.AddScoped<IUserRepository, UserRepository>();
            //services.AddScoped<ILanguageRepository, LanguageRepository>();
            //services.AddScoped<IUserLoginHistoryRepository, UserLoginHistoryRepository>();
            //#endregion

            //#region CardOperations
            //services.AddScoped<ICardLangRepository, CardLangRepository>();
            //services.AddScoped<ITranslateRepository, TranslateRepository>();
            //services.AddScoped<IModuleLangRepository, ModuleLangRepository>();
            //services.AddScoped<IObjectContentsRepository, ObjectContentRepository>();
            //#endregion

            //#region Common
            //services.AddScoped<IAutoCompleteRepository, AutoCompleteRepository>();
            //services.AddScoped<IComboBoxRepository, ComboBoxRepository>();
            //services.AddScoped<IFileUploadRepository, FileUploadRepository>();
            //#endregion

        }
    }
}
