using Common;
using System;
using System.Threading.Tasks;

public class Program
{
    public static async Task<int> Main(string[ ] args)
    {

        

        var template = new SolutionTemplate(2015, 1);

        Console.ReadLine( );
        return await Task.FromResult(0);
    }    
}