using App;
using CommandLine;
using Common;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

public class Program
{
    public static async Task Main(string[ ] args) =>
            await Parser.Default.ParseArguments(args, LoadVerbs( )).WithParsedAsync(Run);

    private static Task Run(object obj) => obj switch
    {
        ScaffoldOptions s => Task.Run(( ) => Console.WriteLine(ScaffoldOptions.Run(s)))
    };

    private static Type[ ] LoadVerbs( ) => Assembly.GetExecutingAssembly( ).GetTypes( )
            .Where(t => t.GetCustomAttribute<VerbAttribute>( ) != null).ToArray( );
}