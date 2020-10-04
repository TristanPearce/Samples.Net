using Samples.Net;

using System;

namespace Samples
{
    [Sample]
    [Name("Platform Use")]
    [Description("This is a sample where a platform object is created and used.")]
    public class PlatformSample
    {

        public PlatformSample()
        {

            Platform pl = new Platform();

            Console.WriteLine("PlatformSample is running!");

            Console.WriteLine("Platform: {0}", pl.OS);

            Console.WriteLine("PlatformSample done!");
        }

    }
}
