using Samples.Net;

using System;
using System.Reflection;

namespace Samples
{
    class Program
    {
        static void Main(string[] args)
        { 
            // Create the console runner
            ConsoleRunner runner = new ConsoleRunner();

            // Setting up dependency injection
            runner.BuildServiceProvider += (services) => 
            { 
                services.AddSingleton("This is my DI string");
                services.AddSingleton(new DISample.ExampleType() { Name = "Thomas", Age = 92 });
            };

            runner.BuildNamedArguments += (arguments) => 
            {
                arguments.Add("apiToken", "abc123xyz");
                arguments.Add("callLimit", 10_000);
            };

            // Find all samples in this assembly.
            runner.FindSamples(typeof(Program).Assembly);

            // Run the console runner.
            runner.Run();
        }
    }
}
