# Samples.Net
An easy to use framework for creating usage samples.

## Easy Usage
1. Mark a class with the [Sample] attribute.
2. Without Dependancy Injection (DI), ensure that a parameterless constructor is present.
3. Mark at least one method with [Run], ensure that all parameters are supplied by DI.
4. Create a ConsoleRunner instance, supplying a ConsoleRunnerInfo if desired.
5. Subscribe to the BuildServiceProvider event of ConsoleRunner and add services used for dependency injection.
6. ConsoleRunner.Find() either an entire assembly, or specify the types to find samples from.
7. Finally, ConsoleRunner.Run().

All of the above is covered in the 'Samples' project, have a look at the Samples.Net samples (which is itself using Samples.Net... woah, meta).

## Features
- Attribute based approach to setting up samples.
- Support for dependency injection.
