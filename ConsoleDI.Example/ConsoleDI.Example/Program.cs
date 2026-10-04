using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConsoleDI.Example
{
    internal class Program
    {

        /*static async void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            await d();
        }*/

        public static  void Main(string[] args)
        {
           // Console.WriteLine("Hello, World!");



              d(args);




        }


        public static   void  d(string[] args)
        {


            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddTransient<IExampleTransientService, ExampleTransientService>();
            builder.Services.AddScoped<IExampleScopedService, ExampleScopedService>();
            builder.Services.AddSingleton<IExampleSingletonService, ExampleSingletonService>();
            builder.Services.AddTransient<ServiceLifetimeReporter>();

            using IHost host = builder.Build();

            ExemplifyServiceLifetime(host.Services, "Lifetime 1");
            ExemplifyServiceLifetime(host.Services, "Lifetime 2");


            host.Run();


           // await host.RunAsync();

            static void ExemplifyServiceLifetime(IServiceProvider hostProvider, string lifetime)
            {
                using IServiceScope serviceScope = hostProvider.CreateScope();
                IServiceProvider provider = serviceScope.ServiceProvider;
                ServiceLifetimeReporter logger = provider.GetRequiredService<ServiceLifetimeReporter>();
                logger.ReportServiceLifetimeDetails(
                    $"{lifetime}: Call 1 to provider.GetRequiredService<ServiceLifetimeReporter>()");

                Console.WriteLine("...");

                logger = provider.GetRequiredService<ServiceLifetimeReporter>();
                logger.ReportServiceLifetimeDetails(
                    $"{lifetime}: Call 2 to provider.GetRequiredService<ServiceLifetimeReporter>()");

                Console.WriteLine();
            }





        }


    }
}
