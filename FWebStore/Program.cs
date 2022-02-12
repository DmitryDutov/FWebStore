////var builder = WebApplication.CreateBuilder(args);
////var app = builder.Build();

//var app =  WebApplication.CreateBuilder(args).Build();
//app.MapGet("/", () => "Hello World!");
//app.Run();

using System;

namespace FWebStore
{
    internal partial class Program
    {
        public static void Main(string[] args)
        {
            var app =  WebApplication.CreateBuilder(args).Build();
            app.MapGet("/", () => "Hello World!");
            app.Run();

        }
    }
}

