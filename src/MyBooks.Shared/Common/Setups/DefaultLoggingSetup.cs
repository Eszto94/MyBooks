using Microsoft.AspNetCore.Builder;
using Serilog;

namespace MyBooks.Shared.Common.Setups;

public static class DefaultLoggingSetup
{
    public static void AddDefaultLogging(this WebApplicationBuilder builder, string appName)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithEnvironmentUserName()
            .WriteTo.Console()
            .CreateLogger();

        builder.Host.UseSerilog();
    }

    public static void AddDefaultLoggingUsing(this WebApplication app)
    {
        app.UseSerilogRequestLogging(options =>
        {
            options.MessageTemplate = "Handled HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";

            options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
            {
                diagnosticContext.Set("http.request.method", httpContext.Request.Method);
                diagnosticContext.Set("http.request.referrer", httpContext.Request.Headers["Referer"].ToString());
                diagnosticContext.Set("http.request.user_agent", httpContext.Request.Headers["User-Agent"].ToString());
                diagnosticContext.Set("http.response.status_code", httpContext.Response.StatusCode);
                diagnosticContext.Set("url.path", httpContext.Request.Path);
                diagnosticContext.Set("url.scheme", httpContext.Request.Scheme);
                diagnosticContext.Set("host.name", httpContext.Request.Host.Value!);
            };
        });
    }
}
