using Microsoft.EntityFrameworkCore;
using paisa2u.common.Resources;
using paisa2u.common.Models;

namespace paisa2u.common.Services
{
    public class ProductService : IProductService
    {
        private readonly PaisaDbContext _context;

        public ProductService(PaisaDbContext context)
        {
            _context = context;
        }
        public async Task<ProductResource> AddProduct(ProductResource resource, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Productid = resource.productid,
                Catid = resource.catid,
                Productname = resource.productname,
                Productcode = resource.productcode,
                Picture = resource.picture,
                Discountpercentage = resource.discountpercentage,
                Discountamount = resource.discountamount,
                Discountcode = resource.discountcode,
                Dicountcondition = resource.dicountcondition
            };
            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);


            return new ProductResource(
                product.Productid,
                product.Catid,
                product.Productname,
                product.Productcode,
                product.Picture,
                (float)product.Discountpercentage,
                (float)product.Discountamount,
                product.Discountcode,
                product.Dicountcondition

                );
        }

        public async Task<IEnumerable<ProductResource>> GetProduct(int productid, CancellationToken cancellationToken)
        {
            List<ProductResource> productlist = new List<ProductResource>();
            var product1 = await _context.Products.Where(x => x.Productid == productid)
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

        public async Task<IEnumerable<ProductResource>> GetProducts(CancellationToken cancellationToken)
        {
            List<ProductResource> productlist = new List<ProductResource>();
            var product1 = await _context.Products.ToListAsync(cancellationToken);
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
        public async Task<ProductResource> UpdateProduct(int productid, ProductResource resource, CancellationToken cancellationToken)
        {
            var edproduct = await _context.Products
                .FirstOrDefaultAsync(x => x.Productid == productid, cancellationToken);
            if (edproduct.Productid != productid)
            {
                throw new Exception("Product to update does not exist");
            }

            edproduct.Productcode = resource.productcode;
            edproduct.Productname = resource.productname;
            edproduct.Catid = resource.catid;
            edproduct.Picture = resource.picture;
            edproduct.Discountpercentage = resource.discountpercentage;
            edproduct.Discountamount = resource.discountamount;
            edproduct.Discountcode = resource.discountcode;
            edproduct.Dicountcondition = resource.dicountcondition;

            _context.Entry(edproduct).State = EntityState.Modified;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;

            }
            return new ProductResource(
                edproduct.Productid,
                edproduct.Catid,
                edproduct.Productname,
                edproduct.Productcode,
                edproduct.Picture,
                edproduct.Discountpercentage,
                edproduct.Discountamount,
                edproduct.Discountcode,
                edproduct.Dicountcondition
                );
        }
        public async Task<ProductResource> DeleteProduct(ProductResource resource, CancellationToken cancellationToken)
        {
            var product = await _context.Products
               .FirstOrDefaultAsync(x => x.Productid == resource.productid, cancellationToken);
            //check for cascade
            if (product.Productid != resource.productid)
            {
                throw new Exception("Product to remove does not exist");
            }
            var prod = new ProductResource(
                product.Productid,
                product.Catid,
                product.Productname,
                product.Productcode,
                product.Picture,
                product.Discountpercentage,
                product.Discountamount,
                product.Discountcode,
                product.Dicountcondition
                );

            _context.Entry(product).State = EntityState.Deleted;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("User can not be deleted!");
            }

            return prod;
        }
    }
}
