# SeriLog In Console App


## SeriLog in Console App (.Net Framework ) Complete Code and Configuration

> Package Required From Nuget Package
- Serilog
- Serilog.Sinks.File

> I am using Retriving Log File path Location From App.config

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <startup>  
  </startup>
  <runtime> 
  </runtime>
	<appSettings> 
		<!-- Serilog Config-->  
		<add key="LoggingPath" value="LogSerilog.txt" /> 
	</appSettings>
</configuration>
```

```cs
using Serilog;
using System;
using System.Configuration;
using System.Threading;
namespace ConsoleApp
{
    internal class Program
    {
        private static ILogger log;
        private static void SeriLogSetUp()
        {
            log = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.File(ConfigurationManager.AppSettings["LoggingPath"],rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
        static void Main(string[] args)
        {
            SeriLogSetUp();

            log.Information("Logged Using Serilog" + DateTime.Now);
        }

    }
}
```
