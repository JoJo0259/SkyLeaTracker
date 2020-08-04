using DSharpPlus.CommandsNext;
using System;

namespace Discord_SkyLea
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var bot = new Bot();
            bot.RunAsync().GetAwaiter().GetResult();

            
        }
        
        
    }
}
