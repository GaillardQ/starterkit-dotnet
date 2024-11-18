using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.RegularExpressions;
using TemplateDotNet.Utils;

namespace TemplateDotNet.Middlewares;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;
    private readonly bool _isRequestBodyLogged;
    private string _requestBody;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
        _isRequestBodyLogged = false;
        _requestBody = "n/a";
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            if (_isRequestBodyLogged)
            {
                context.Request.EnableBuffering();
                string requestBody = await new StreamReader(context.Request.Body, Encoding.UTF8).ReadToEndAsync();
                string body = string.IsNullOrEmpty(requestBody) ? "n/a" : requestBody;
                _requestBody = Regex.Replace(body, @"\r\n?|\n|\t| ", "", RegexOptions.IgnoreCase, TimeSpan.FromSeconds(5));
                context.Request.Body.Position = 0;
            }

            await _next(context);
        }
        finally
        {
            LogRequest(context);
        }
    }

    private void LogRequest(HttpContext context)
    {
        HttpRequest request = context.Request;
        HttpResponse response = context.Response;

        string logDate = DateTime.Now.ToString();
        string method = request.Method;
        string path = request.Path;
        string host = request.Host.ToString();
        string statusCode = response.StatusCode.ToString();

        string log = "Requête : ";
        log += $"{logDate} | ";
        log += $"HTTP: {host} {method} {path} | ";
        log += $"Status code: {statusCode} | ";
        if (_isRequestBodyLogged)
        {
            log += $"Body: {_requestBody} | ";
        }

        _logger.LogInformation(InfoMessages.RequestLog, log);
    }
}
