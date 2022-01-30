using Microsoft.AspNetCore.Http;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ServiceApi.ErrorHandling
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }

        }
        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            AppResponse<string> response = new AppResponse<string>();
            response.Message = "Error Occurred while processing request";
            response.Success = false;

            // If it is ApplicationException, we have intentionally thrown this somewhere down the code
            // We return success response to the user with ApplicationException's message
            // Add anticipated excpetion types here
            if (ex is ApplicationException)
            {
                code = HttpStatusCode.OK;
                response.Message = ex.Message;
            }
            else
            {

                // log error
            }

            var result = JsonConvert.SerializeObject(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
