using CommandLine;
using CLI.Verbs;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CLI
{
    public class Program
    {
        /*
         * U S A G E
         * cd into project folder then run; 
         * dotnet run --project .\CLI.csproj scaffold
         * this will bring up help for the options
         */

        public static async Task Main(string[ ] args) =>
                await Parser.Default.ParseArguments(args, LoadVerbs( )).WithParsedAsync(Run);

        private static Task Run(object obj) => obj switch
        {
            ScaffoldOptions s => Task.Run(( ) =>
            {
                Console.WriteLine(ScaffoldOptions.Run(s));
                Console.ResetColor( );
            }),
            RunDayOptions r => Task.Run(( ) =>
            {
                Console.WriteLine(RunDayOptions.Run(r));
                Console.ResetColor( );
            }),
            GetInputOptions i => Task.Run(( ) =>
           {
               Console.WriteLine(GetInputOptions.Run(i));
               Console.ResetColor( );
           }),
        };

        private static Type[ ] LoadVerbs( ) => Assembly.GetExecutingAssembly( ).GetTypes( )
                .Where(t => t.GetCustomAttribute<VerbAttribute>( ) != null).ToArray( );
    }
}