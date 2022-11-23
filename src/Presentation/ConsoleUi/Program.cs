using Application;
using ConsoleUi.Interfaces;
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
            Initialise();

            int exitCode;

            try
            {
                //exitCode = await s_ServiceProvider.GetService<IAppHost>().RunAsync(args);
                //await new EntityFrameworkCoreThreePointOneDemo(s_ServiceProvider).DemoAsync();
                var appHost = s_ServiceProvider.GetRequiredService<IAppHost>();
                exitCode = await appHost.RunAsync(args);
                //await new EntityFrameworkCoreFivePointZeroDemo(serviceScopeFactory).DemoAsync();
                //CSharpNineDemo.Demo();
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

        private static void Initialise()
        {
            // Initialise Services.
            var services = new ServiceCollection();
            ConfigureServices(services);
            s_ServiceProvider = services.BuildServiceProvider();
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
