using System;
using System.Threading;
using Serilog;

namespace console_app
{
    class Program
    {
        static void Main(string[] args)
        {
            var log = new LoggerConfiguration()
                .WriteTo.Console()    
                .WriteTo.Http("http://localhost:8080")
                .CreateLogger();

            
            while (true)
            {
                var customer = Customer.Generate();                
                
                log.Information("{@customer} registered", customer);
                
                Thread.Sleep(1000);
            }
        }
    }
}
