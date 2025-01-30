using WebApiSample.Abstractions;
using WebApiSample.DbAccess;
using WebApiSample.Entities;

namespace WebApiSample.Features.Products;

public static class CreateProduct
{
    public record Request(string Name, string Description, decimal Price);
    public record Response(int Id, string Name, decimal Price);

    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("products", Handler).WithTags("Products");
        }

        public static IResult Handler(Request request, AppDbContext context)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                CreatedOnUtc = DateTime.UtcNow
            };

            context.Products.Add(product);

            context.SaveChanges();

            return Results.Ok(
                new Response(product.Id, product.Name, product.Price));
        }
    }
}
