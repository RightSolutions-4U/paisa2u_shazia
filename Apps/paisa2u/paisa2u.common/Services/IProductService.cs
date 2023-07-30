using paisa2u.common.Resources;

namespace paisa2u.common.Services
{
    public interface IProductService
    {
        Task<ProductResource> AddProduct(ProductResource resource, CancellationToken cancellationToken);

        Task<IEnumerable<ProductResource>> GetProducts(CancellationToken cancellationToken);

        Task<IEnumerable<ProductResource>> GetProduct(int productid, CancellationToken cancellationToken);

        Task<ProductResource> UpdateProduct(int productid, ProductResource resource, CancellationToken cancellationToken);

        Task<ProductResource> DeleteProduct(ProductResource resource, CancellationToken cancellationToken);
    }
}
