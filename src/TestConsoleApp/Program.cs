// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using RazorHtmlEmails.Common;
using RazorHtmlEmails.RazorClassLib.Services;
using TestConsoleApp;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

var listener = new DiagnosticListener("TimetasticListener");
            

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
        services
            .AddScoped<IRazorViewToStringRenderer, RazorViewToStringRenderer>()
            .AddSingleton<DiagnosticListener>(listener)
            .AddSingleton<DiagnosticSource>(listener)
            .AddScoped<IRegisterAccountService, RegisterAccountService>()
            .AddScoped<GetEmailContent, GetEmailContent>()
            .AddRazorPages(o =>
            {
                o.RootDirectory = "/Views/";
            })
            .AddRazorOptions(opts =>
                {
                    opts.ViewLocationFormats.Clear(); 
                    
                    opts.ViewLocationFormats.Add
                        ("/Views/{1}/{0}" + RazorViewEngine.ViewExtension);
                    opts.ViewLocationFormats.Add
                        ("../../../../RazorHtmlEmails.RazorClassLib/Views/Emails/{1}/{0}" + RazorViewEngine.ViewExtension); 

                }
            )
            
        ) .ConfigureServices((_, services) =>
        services.Configure<RazorViewEngineOptions>(o =>
        {
            
            o.ViewLocationFormats.Clear();
            o.ViewLocationFormats.Add
                ("/Views/{1}/{0}" + RazorViewEngine.ViewExtension);
            o.ViewLocationFormats.Add
                ("../../../../RazorHtmlEmails.RazorClassLib/Views/Emails/{1}/{0}" + RazorViewEngine.ViewExtension); //execution of console app folder
            
            
        })
)
    
    .Build();

Demo(host.Services, "scope1");
Demo(host.Services, "scope2");

host.Run();

static void Demo(IServiceProvider services, string scope)
{
    using IServiceScope serviceScope = services.CreateScope();
    IServiceProvider provider = serviceScope.ServiceProvider;

    var emailContent = provider.GetRequiredService<GetEmailContent>();
    var registerAccount = provider.GetRequiredService<IRegisterAccountService>();
    
    
    Console.WriteLine("Hello?");

    var content = emailContent.GetEmail().Result;
    Console.WriteLine(content);

    Console.WriteLine(".....");

    var content2 =  registerAccount.GetEmail().Result;

    
    Console.WriteLine(content2);
}

