using Microsoft.Extensions.Primitives;
using System.Collections.Concurrent;
using System.Globalization;
using System.IO;


      
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

//app.MapGet("/", () => "TESTE 123");

//app.Run();

//app.run(async (httpcontext context) =>
//{
//    streamreader reader = new streamreader(context.request.body);
//    string body = await reader.readtoendasync();

//    convert to string from dictionary 
//   dictionary<string, stringvalues> querydict=  microsoft.aspnetcore.webutilities.queryhelpers.parsequery(body);

//    if (querydict.containskey("age"))
//    {
//        foreach (var item in querydict["age"])
//        {

//            await context.response.writeasync(item);
//            await context.response.writeasync(", "); 

//        }
//    }



//});

app.Run(async (HttpContext context) =>
{
    if (context.Request.Method == "GET" && context.Request.Path == "/")
    {
        int firstNumber = 0;
        int secondNumber = 0;
        string operation = null;
        long? total = null;

        if (context.Request.Query.ContainsKey("firstNumber"))
        {
            string firstNumberString = context.Request.Query["firstNumber"][0];
            if (!string.IsNullOrEmpty(firstNumberString))
            {
                firstNumber = Convert.ToInt32(firstNumberString);

            }
            else
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid input for 'first number'\n");

            }
        }

        if (context.Request.Query.ContainsKey("secondNumber"))
        {
            string secondNumberString = context.Request.Query["secondNumber"][0];
            if (!string.IsNullOrEmpty(secondNumberString))
            {
                secondNumber = Convert.ToInt32(context.Request.Query["secondNumber"][0]);


            }
            else
            {
                if (context.Response.StatusCode == 200)
                    context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Incorret input'\n");


            }
        }

        if (context.Request.Query.ContainsKey("operation"))
        {

            operation = Convert.ToString(context.Request.Query["operation"][0]);

            switch (operation)
            {
                case "add": total = firstNumber + secondNumber; break;
                case "subtract": total = firstNumber - secondNumber; break;
                case "multiply": total = firstNumber * secondNumber; break;
                case "divide": total = (secondNumber != 0) ? firstNumber / secondNumber : 0; break; 
                case "mod": total = (secondNumber != 0) ? firstNumber % secondNumber : 0; break; 
            }

            if (total.HasValue)
            {
                await context.Response.WriteAsync(total.Value.ToString());
            }

            else
            {
                if (context.Response.StatusCode == 200)
                    context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Incorret input'\n");
            }
        }

        else
        {
            if (context.Response.StatusCode == 200)
                context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Incorret input'\n");
        }
    }

});

            app.Run();  
     