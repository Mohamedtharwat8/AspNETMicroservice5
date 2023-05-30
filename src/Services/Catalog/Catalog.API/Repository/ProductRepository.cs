using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Catalog.API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;
        public ProductRepository(ICatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Product> GetProduct(string id)
        {
            return await _context.Prodcuts.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryname)
        {
            FilterDefinition<Product> filters = Builders<Product>.Filter.Eq(p => p.Category, categoryname);
            return await _context.Prodcuts.Find(filters).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filters = Builders<Product>.Filter.ElemMatch(p=>p.Name , name);
            return await _context.Prodcuts.Find(filters).ToListAsync();
        }

        public async  Task<IEnumerable<Product>> GetProducts()
        {
           return await _context.Prodcuts.Find(p=> true).ToListAsync();
        }
        public async Task CreateProduct(Product product)
        {
            await _context.Prodcuts.InsertOneAsync(product);

        }

        public async Task<bool> UpdateProduct(Product product)
        {
             var updateResult = await _context
                .Prodcuts
                .ReplaceOneAsync(filter: g=>g.Id == product.Id,replacement: product);

            return updateResult.IsAcknowledged
                && updateResult.ModifiedCount == 0;

        }
        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id , id);

            DeleteResult deleteResult = await _context.Prodcuts.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount == 0;
        }


    }
}
