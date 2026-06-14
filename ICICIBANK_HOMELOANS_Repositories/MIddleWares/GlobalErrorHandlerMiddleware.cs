using ICICIBANK_HOMELOANS_BusinessEntities.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ICICIBANK_HOMELOANS_Repositories.MIddleWares
{
    public class GlobalErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;        //RequestDelegate Process The HttpRequest.

        private readonly ILoggingFactory _loggingFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GlobalErrorHandlerMiddleware(RequestDelegate next, ILoggingFactory loggingFactory, IHttpContextAccessor httpContextAccessor)
        {        // Middleware constructor takes the next RequestDelegate in the pipeline

            _next = next;
            _loggingFactory = loggingFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task InvokeAsync(HttpContext context)
        {//HttpContext is predfeined class,it  represents all the information about an individual HTTP request and response.

            // Invoke method handles each request and passes control to the next middleware

            //here need to CREATE ONE INVOKEASYNC METHOD, this method is responsible for handling each incoming HTTP request.
            //It accepts an HttpContext object, which contains all the information about the current request and response.
            //Inside this method, you can perform any necessary processing on the request,
            //such as logging, authentication, or modifying the request before passing it to the next middleware in the pipeline. After processing the request, you call _next(context) to pass control to the next middleware. Once the next middleware has completed its processing, you can also perform actions on the response if needed.

            try
            {
                //If you are getting any error ,it will call the next middleware in the pipeline,
                //and if any exception occurs during the processing of the request, it will be caught in the catch block.
                await _next(context);// Call the next middleware in the pipeline
            }
            catch (Exception error)
            {
                var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";
                await _loggingFactory.AddLoggingMessages(userName, "Information", "GlobalErrorHandlerMiddleware: Excution Starts");//logg the message in database using custom logging factory
                var response = context.Response;//here we are getting the response object from the http context to set the status code and content type for the error response.
                response.ContentType = "application/json";
                switch (error)
                {
                    case AppException:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException:
                        // not found error 
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                //here we are creating a JSON object that contains the status code, error message, stack trace, and inner
                var result = JsonConvert.SerializeObject(new
                {
                    StatusCode = response.StatusCode.ToString(),
                    ErrorMessage = error?.Message,
                    StackTraceError = error?.StackTrace?.ToString(),
                    InnerExceptionError = error?.InnerException?.ToString()
                });
                //here log the messages in the text file by using serilog.
                Log.Error("Custom Failure: {@StatusCode}, {@ErrorMessage}, {@StackTraceError},{@InnerExceptionError},LoggedinCurrnetUsername is:{@Username}",
                response.StatusCode.ToString(), Convert.ToString(error?.Message), Convert.ToString(error?.StackTrace), Convert.ToString(error?.InnerException));
                await _loggingFactory.AddLoggingMessages(userName, "Error", $"Custom Failure: StatusCode:{response.StatusCode}, ErrorMessage:{Convert.ToString(error?.Message)}, StackTraceError:{Convert.ToString(error?.StackTrace)}, InnerExceptionError:{Convert.ToString(error?.InnerException)}");
                //here log the message in our project text file by using serilog.
                //in sqlserver database also we are logging the exceptions.
                //in Azure application insights  we are logging the exceptions
                //in Aws we are logging the exceptions in cloud watch
                //in  network log also some of the companies log the error messages.
                //here log the messages in sql server database.

                await _loggingFactory.AddProjectLevelErrorLogAsync(response.StatusCode.ToString(), Convert.ToString(error?.Message), Convert.ToString(error?.StackTrace), Convert.ToString(error?.InnerException), userName);

                //.......Write The logic In Future Based on Your Cloud Usage requirment.
                //If you use Azure cloud,Add the Azure Application Insights Logic Here.To Log The Exceptions in Azure cloud.
                //If You use Aws cloud Add the Aws CloudWatchLogic Here.To Log The exceptions In Aws cloud.
                await _loggingFactory.AddLoggingMessages(userName, "Information", "GlobalErrorHandlerMiddleware: Excution Ends");//logg the message in database using custom logging factory
                var errorFriendlyMessage = new ProblemDetails
                {
                    Type = "API Exception",
                    Status = (short)HttpStatusCode.InternalServerError,
                    Title = "Internal server error occured in the api"
                };
                //while returning the message to api show user friendly error message
                var ErrorResult = JsonConvert.SerializeObject(errorFriendlyMessage);
                await response.WriteAsync(ErrorResult);
            }
        }
    }
}
