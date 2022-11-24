using Application;
using ConsoleUi.Interfaces;
using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace TestEfOwnedTypesInheritance
{
    class Program
    {

        #region " - - - - - - Fields - - - - - - "

        private static IServiceProvider s_ServiceProvider = null!;

        #endregion //Fields

        #region " - - - - - - Main - - - - - - "

        public static async Task<int> Main(string[] args)
        {
            await InitialiseAsync();

            int exitCode;

            try
            {
                var appHost = s_ServiceProvider.GetRequiredService<IAppHost>();
                exitCode = await appHost.RunAsync(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                exitCode = 1;
            }
            finally
            {
                CleanUp();
            }

            return exitCode;
        }

        #endregion //Main

        #region " - - - - - - Methods - - - - - - "

        private static void CleanUp()
        {
            // Dispose of services.
            if (s_ServiceProvider == null) return;
            if (s_ServiceProvider is IDisposable) ((IDisposable)s_ServiceProvider).Dispose();
        } //CleanUp

        private static async Task InitialiseAsync()
        {
            // Initialise Services.
            var services = new ServiceCollection();
            ConfigureServices(services);
            s_ServiceProvider = services.BuildServiceProvider();

            // Initialise and seed the database.
            using (var scope = s_ServiceProvider.CreateScope())
            {
                var initialiser = scope.ServiceProvider.GetRequiredService<TestDbContextInitialiser>();
                await initialiser.InitialiseAsync();
                await initialiser.SeedAsync();
            }
        } //Initialise

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication()
                .AddInfrastructure()
                .AddPresentation();
        }

        #endregion //Methods

    }

}
