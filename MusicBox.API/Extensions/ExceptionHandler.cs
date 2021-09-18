using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MusicBox.API.Extensions
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                string errorMessage = error.Message;
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

                if (error.GetType() == typeof(ValidationException))
                {
                    context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                }

                await context.Response.WriteAsync(new ErrorDetail
                {
                    Message = errorMessage,
                    StatusCode = context.Response.StatusCode
                }.ToString());
            }
        }
    }
}