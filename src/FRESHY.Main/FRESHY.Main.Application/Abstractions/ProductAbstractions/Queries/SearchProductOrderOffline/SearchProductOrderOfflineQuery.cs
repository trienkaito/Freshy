using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FRESHY.Common.Application.Interfaces.Abstractions;
using FRESHY.Common.Domain.Common.Interfaces;
using FRESHY.Main.Application.Abstractions.ProductAbstractions.Queries.GetAllProducts.Results;
using FRESHY.Main.Application.Abstractions.ProductAbstractions.Queries.Shared.Results;
using FRESHY.Main.Application.Abstractions.SupplierAbstractions.Queries.Shared.Results;
using FRESHY.Main.Application.Interfaces.Persistance;

namespace FRESHY.Main.Application.Abstractions.ProductAbstractions.Queries
{
    public record SearchProductOrderOfflineQuery
    (
        string? ProductName,

        int PageNumber,
        int PageSize
    ) : IQuery<QueryResult<IEnumerable<AllProductsResult>>>;

    public class SearchProductOrderOfflineQueryHandler : IQueryHandler<SearchProductOrderOfflineQuery, QueryResult<IEnumerable<AllProductsResult>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IProductUnitRepository _productUnitRepository;

        public SearchProductOrderOfflineQueryHandler(IProductRepository productRepository, IProductTypeRepository productTypeRepository, ISupplierRepository supplierRepository, IProductUnitRepository productUnitRepository)
        {
            _productRepository = productRepository;
            _productTypeRepository = productTypeRepository;
            _supplierRepository = supplierRepository;
            _productUnitRepository = productUnitRepository;
        }

        public async Task<QueryResult<IEnumerable<AllProductsResult>>> Handle(SearchProductOrderOfflineQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var allUnit = await _productUnitRepository.GetAllAsync(product => new
                {
                    product.Id,
                    product.ProductId
                });
                var allProduct1 = await _productRepository.GetAllAsync(product => new
                {
                    product.Id,
                    product.Name
                });
                var allproduct = allProduct1.Where(p => p.Name.Contains(request.ProductName));
                var data = allUnit.Join(allproduct,
                    unit => unit.ProductId,
                    product => product.Id,
                    (unit, product) => new
                    {
                        UnitId = unit.Id,
                        ProductId = product.Id
                    });
                
                var res = new QueryResult<IEnumerable<AllProductsResult>>();
                res.Succeeded = true;
                res.Message = "Search Unit Success";
                res.Data = (IEnumerable<AllProductsResult>)data;
                res.StatusCode = HttpStatusCode.OK;
                return res;
            }
            catch (Exception e)
            {
                return new PageQueryResult<IEnumerable<AllProductsResult>>(request.PageNumber, request.PageSize, HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
