using Microsoft.AspNetCore.Mvc;
using paisa2u.common.Resources;
namespace paisa2u.common.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResource>> GetCategories(CancellationToken cancellationToken);
        Task<IEnumerable<ProductResource>> GetProductsForAllCategories(CancellationToken cancellationToken);
        Task<IEnumerable<ProductResource>> GetProductsForSingleCategory(int catid, CancellationToken cancellationToken);
        Task<CategoryResource> GetCategory(int Catid, CancellationToken cancellationToken);
        Task<CategoryResource> DeleteCategory(CategoryResource resource, CancellationToken cancellationToken);
        Task<CategoryResource> UpdateCategory(int catid, CategoryResource resource, CancellationToken cancellationToken);

    }
}