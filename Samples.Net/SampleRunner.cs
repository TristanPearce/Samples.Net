using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Samples.Net
{

    /// <summary>
    /// Runs types correctly marked as samples.
    /// </summary>
    public class SampleRunner
    {

        /// <summary>
        /// Service provider for supporting dependancy injection.
        /// </summary>
        protected IServiceProvider Services { get; set; }

        /// <summary>
        /// Provides objects associated with a name.
        /// </summary>
        protected IDictionary<string, object> NamedArguments { get; set; }

        public SampleRunner(IServiceProvider serviceProvider = null, IDictionary<string, object> namedArguments = null)
        {
            Services = serviceProvider ?? new ServiceProvider();
            NamedArguments = namedArguments ?? new Dictionary<string, object>();
        }

        /// <summary>
        /// Run a sample.
        /// </summary>
        /// <param name="sample">SampleInfo to be run.</param>
        public virtual void Run(SampleInfo sample)
        {
            // Create object, a section should be added here for constructors with parameters.
            object obj = CreateObject(sample.Type);

            // Get all methods marked to be run.
            IEnumerable<MethodInfo> methods = FindRunMethods(sample);

            // Foreach method, create parameters and invoke it.
            // Trust that the methods were returned in the order specified by the user in [Run(order:...)].
            foreach (MethodInfo mi in methods)
            {
                object result = null; 
                var arguments = CreateArguments(mi.GetParameters());
                if (IsAsync(mi)) 
                {
                    var task = (mi.Invoke(obj, arguments));
                    if (task is Task<object> taskWithResult)
                    {
                        result = taskWithResult.GetAwaiter().GetResult();
                    }
                    else if(task is Task taskWithoutResult)
                    {
                        taskWithoutResult.Wait();
                        result = null;
                    }
                    else
                    {
                        throw new NotSupportedException("The return type of this async method is not supported");
                    }
                }
                else
                {
                    result = mi.Invoke(obj, arguments);
                }

                // do something with result maybe
            }
            // Check for dispose and run.
            if (FindDisposeMethod(sample) is MethodInfo dispose) 
            { 
                dispose.Invoke(obj, new object[] { }); 
            }
        }

        /// <summary>
        /// Discover methods marked to be run.
        /// </summary>
        /// <param name="info">SampleInfo to find the methods for.</param>
        /// <returns>MethodInfo's for the methods to be run in the order to be run.</returns>
        protected virtual IEnumerable<MethodInfo> FindRunMethods(SampleInfo info)
        {
            PriorityList<int, MethodInfo> sorted = new PriorityList<int, MethodInfo>();

            var methods = info.Type.GetMethods(); 
                      
            foreach (MethodInfo mi in methods)
            {
                if (mi.GetCustomAttribute<RunAttribute>() is RunAttribute a)
                {
                    sorted.Add(a.Order, mi);
                }
            }

            return sorted;
        }

        /// <summary>
        /// Find the dispose method ONLY IF the sample implements <see cref="IDisposable"/>.
        /// </summary>
        /// <param name="info">SampleInfo to find the dispose method for.</param>
        /// <returns>
        /// Null if the sample does not implement <see cref="IDisposable"/>, <see cref="MethodInfo"/> for Dispose otherwise.
        /// </returns>
        protected virtual MethodInfo FindDisposeMethod(SampleInfo info)
        {
            if (info.Type.GetInterfaces().Contains(typeof(IDisposable)))
            {
                return info.Type.GetMethod("Dispose", new Type[] { });
            }

            return null;
        }

        /// <summary>
        /// Determine whether a method is async.
        /// </summary>
        /// <param name="method"></param>
        /// <returns><see cref="true"/> if the method is async.</returns>
        protected virtual bool IsAsync(MethodInfo method)
        {
            var attribute = method.GetCustomAttribute<AsyncStateMachineAttribute>();

            return (attribute != null);
        }
        
        /// <summary>
        /// Create parameters for a given <see cref="MethodInfo"/>
        /// </summary>
        /// <param name="method">MethodInfo to create parameters for.</param>
        /// <returns>An object array containing the parameters.</returns>
        protected virtual object[] CreateArguments(IEnumerable<ParameterInfo> parameters)
        {
            if (parameters.Count()== 0)
            {
                return new object[] { };
            }
            else
            {
                List<object> arguments = new List<object>();
                foreach (var parameter in parameters)
                {
                    // NamedArguments
                    {
                        if (NamedArguments.TryGetValue(parameter.Name, out object argument))
                        {
                            if (argument.GetType() == parameter.ParameterType)
                            {
                                arguments.Add(argument);
                                continue;
                            }
                        }
                    }
                    // ServiceProvider
                    {
                        object argument = Services.GetService(parameter.ParameterType);
                        if (argument != null)
                        {
                            arguments.Add(argument);
                            continue;
                        }
                    }

                    throw new Exception($"No value found for parameter {parameter.Name} of type '{parameter.ParameterType}'.");
                }
                return arguments.ToArray();
            }
        }

        /// <summary>
        /// Create parameters for a given <see cref="MethodInfo"/>
        /// </summary>
        /// <param name="method">MethodInfo to create parameters for.</param>
        /// <returns>An object array containing the parameters.</returns>
        protected virtual object CreateObject(Type type)
        {
            var (constructor, arguments) = FindBestConstructor();
            object obj = constructor.Invoke(arguments);
            return obj;

            (ConstructorInfo constructor, object[] arguments) FindBestConstructor()
            {
                ConstructorInfo[] constructors = type.GetConstructors();

                // Check if only a parameterless constructor exists.
                if((constructors.Length == 1) &&
                   (constructors[0].GetParameters().Length == 0)) 
                {
                    return (constructors[0], new object[0]);
                }


                foreach (ConstructorInfo info in constructors)
                {
                    var parameters = info.GetParameters();
                    try
                    {
                        object[] args = CreateArguments(parameters);

                        // we will only reach this point if no exception occurs.
                        return (info, args);
                    }
                    catch (Exception e)
                    {
                        // handle exception if needed
                    }
                }

                throw new Exception($"No valid constructor could be found for type {type}");
            }
        }

    }
}
