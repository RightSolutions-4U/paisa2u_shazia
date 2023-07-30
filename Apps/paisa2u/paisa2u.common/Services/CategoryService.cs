using Microsoft.EntityFrameworkCore;
using paisa2u.common.Models;
using paisa2u.common.Resources;
namespace paisa2u.common.Services
{
    public class CategoryService : ICategoryService
    {
        public readonly PaisaDbContext _context;
        public CategoryService(PaisaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryResource>> GetCategories(CancellationToken cancellationToken)
        {
            List<CategoryResource> categoryList = new List<CategoryResource>();
            var a = await _context.Categories.ToListAsync(cancellationToken);
            foreach(Category category in a) { 
                categoryList.Add
                    (
                    new CategoryResource(
                        category.Catid,
                        category.Catdesc
                  
                        )
                    );
        }
            return categoryList;
        }
        public async Task<CategoryResource> GetCategory(int catid, CancellationToken cancellationToken)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(x => x.Catid == catid, cancellationToken);


            if (category == null)

                throw new Exception("Product does not exist!");

            return new CategoryResource(
                category.Catid,
                category.Catdesc

                );


        }
        public async Task<IEnumerable<ProductResource>> GetProductsForAllCategories(CancellationToken cancellationToken)
        {
            List<ProductResource> productlist = new List<ProductResource>();
            var product1 = await _context.Products
                .ToListAsync(cancellationToken);
            foreach (Product product in product1)
            {
                productlist.Add
                (
                new ProductResource(
                    product.Productid,
                    product.Catid,
                    product.Productname,
                    product.Productcode,
                    product.Picture,
                    (float)product.Discountpercentage,
                    (float)product.Discountamount,
                    product.Discountcode,
                    product.Dicountcondition
                    )
                );
            }
            return productlist;
        }
        //get products of one category
        public async Task<IEnumerable<ProductResource>> GetProductsForSingleCategory(int catid, CancellationToken cancellationToken)
        {
            List<ProductResource> productlist = new List<ProductResource>();
            var product1 = await _context.Products.Where(x => x.Catid == catid)
                .ToListAsync(cancellationToken);
            foreach (Product product in product1)
            {
                productlist.Add
                (
                new ProductResource(
                    product.Productid,
                    product.Catid,
                    product.Productname,
                    product.Productcode,
                    product.Picture,
                    (float)product.Discountpercentage,
                    (float)product.Discountamount,
                    product.Discountcode,
                    product.Dicountcondition
                    )
                );
            }
            return productlist;
        }
        public async Task<CategoryResource> DeleteCategory(CategoryResource resource, CancellationToken cancellationToken)
        {
            var category = await _context.Categories
               .FirstOrDefaultAsync(x => x.Catid == resource.catid, cancellationToken);
            //check for cascade
            if (category.Catid != resource.catid)
            {
                throw new Exception("Category to remove does not exist");
            }
            var cat = new CategoryResource(
                category.Catid,
                category.Catdesc
    
                );

            _context.Entry(category).State = EntityState.Deleted;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("User can not be deleted!");
            }

            return cat;
        }

        public async Task<CategoryResource> UpdateCategory(int catid, CategoryResource resource, CancellationToken cancellationToken)
        {
            var edCategory = await _context.Categories
                .FirstOrDefaultAsync(x => x.Catid == catid, cancellationToken);
            if (edCategory.Catid != catid)
            {
                throw new Exception("Category to update does not exist");
            }

            edCategory.Catid = resource.catid;
            edCategory.Catdesc = resource.catdesc;
            
            _context.Entry(edCategory).State = EntityState.Modified;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;

            }
            return new CategoryResource(
                edCategory.Catid,
                edCategory.Catdesc
                );
        }

    }
}