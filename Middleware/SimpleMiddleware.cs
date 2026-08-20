namespace FilmRentalNET25.Middleware
{
    public class SimpleMiddleware
    {
        private readonly RequestDelegate next;

        public SimpleMiddleware(RequestDelegate _next)
        {
            next = _next;
        }

        // Metod som hanterar Request & Response
        public async Task InvokeAsync(HttpContext context)
        {
            // Körs vid en Request (på väg in, innan den når en endpoint)
            Console.WriteLine("Request");


            // Kör vidare till nästa middleware
            await next(context);


            // Körs vid en response (på väg ut, efter en endpoint)
            Console.WriteLine("Response");
        }

    }
}
