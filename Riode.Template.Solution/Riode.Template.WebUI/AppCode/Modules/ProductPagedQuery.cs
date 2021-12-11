using MediatR;
using Microsoft.EntityFrameworkCore;
using Riode.Template.WebUI.Models.DataContext;
using Riode.Template.WebUI.Models.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Riode.Template.WebUI.AppCode.Modules
{
    public class ProductPagedQuery : IRequest<IEnumerable<ProductSizeColorCategoryCollection>>
    {
        public class ProductPagedQueryHandler : IRequestHandler<ProductPagedQuery, IEnumerable<ProductSizeColorCategoryCollection>>
        {
            readonly RiodeDbContext db;

            public ProductPagedQueryHandler(RiodeDbContext db)
            {
                this.db = db;
            }

            async public Task<IEnumerable<ProductSizeColorCategoryCollection>> Handle(ProductPagedQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<ProductSizeColorCategoryCollection> data = await db.ProductSizeColorCategoryCollections
                                                                               .Include(p => p.Product)
                                                                               .Include(p => p.Product.ProductImages)
                                                                               .Include(p => p.Product.Brand)
                                                                               .Include(p => p.Size)
                                                                               .Include(p => p.Category)
                                                                               .Include(p => p.Color)
                                                                               .Where(c => c.DeletedDate == null)
                                                                               .ToListAsync(cancellationToken);

                return data;
            }
        }
    }
}
