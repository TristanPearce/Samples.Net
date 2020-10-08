using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Samples.Net
{

    /// <summary>
    /// An easy to use, console based interface for running samples.
    /// </summary>
    public sealed class ConsoleRunner
    {

        private readonly SampleFinder finder;

        private SampleRunner runner;

        private readonly ConsoleRunnerInfo info;

        /// <summary>
        /// Called before the run loop, use to add services for DependecyInjection.
        /// </summary>
        /// <remarks>
        /// For easy use, add 'using Microsoft.Extensions.DependencyInjection' for extention methods. 
        /// </remarks>
        public event Action<ServiceProvider> BuildServiceProvider;

        public ConsoleRunner(ConsoleRunnerInfo info = null)
        {
            this.finder = new SampleFinder();
            this.info = info ?? new ConsoleRunnerInfo();
        }

        /// <summary>
        /// Find and register all samples found in the given assembly.
        /// </summary>
        /// <param name="assembly">Assembly to search for samples.</param>
        public void FindSamples(Assembly assembly)
        {
            finder.Find(assembly);
        }

        /// <summary>
        /// Find and register all samples found in types given.
        /// </summary>
        /// <param name="types">Types to find samples in.</param>
        public void FindSamples(params Type[] types)
        {
            finder.Find(types);
        }

        /// <summary>
        /// Run the Console Sample Runner.
        /// </summary>
        public void Run()
        {
            // Setup dependency injection
            var provider = new ServiceProvider();
            this.BuildServiceProvider?.Invoke(provider);

            // Create runner.
            this.runner = new SampleRunner(provider);

            // Run loop
            SampleInfo[] samples;
            string response = "-1";
            while(!info.QuitPhrases.Contains(response))
            {
                samples = finder.GetSamples().ToArray();
                if(int.TryParse(response, out int id))
                {
                    if ((id >= 0) && (id < samples.Length))
                    {
                        // Header
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Sample #{id}, {samples[id].Name}.");
                        if(info.ShowDescriptionAtSampleStart && samples[id].Description != null)
                            Console.WriteLine($"{samples[id].Description}");
                        Console.WriteLine();
                        Console.WriteLine("Starting...");
                        Console.ForegroundColor = ConsoleColor.White;

                        // Run the sample
                        Console.WriteLine("");
                        runner.Run(samples[id]);
                        Console.WriteLine("");

                        // End text
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Sample #{id}, {samples[id].Name} has finished.");
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey();
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                else
                {
                    // Error text
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Value: {0} is not a valid ID.", response);
                    Console.ForegroundColor = ConsoleColor.White;
                }

                // List all samples.
                Console.WriteLine();
                ListSamples(ref samples);
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Enter the sample ID you would like to run: ");
                Console.ForegroundColor = ConsoleColor.White;
                response = Console.ReadLine();
                Console.Clear();
            }
        }

        /// <summary>
        /// Write the samples out in list form to the console.
        /// </summary>
        /// <param name="samples"></param>
        private void ListSamples(ref SampleInfo[] samples)
        {
            Console.WriteLine($"| ID\t| Name");
            for (int i = 0; i < samples.Length; i++)
            {
                Console.WriteLine($"| {i}\t| {samples[i].Name}");
            }
        }
    }
}
