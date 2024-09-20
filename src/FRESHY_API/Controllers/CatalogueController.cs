using System.Net;
using System.Text.Json.Serialization;
using System.Text.Json;
using Azure.Core;
using FRESHY.Common.Contract.Wrappers;
using FRESHY.Common.Domain.Common.Interfaces;
using FRESHY.Main.Application.Abstractions.ProductAbstractions.Commands.DeActiveProduct;
using FRESHY.Main.Application.Abstractions.ProductAbstractions.Commands.ImportProduct;
using FRESHY.Main.Application.Abstractions.ProductAbstractions.Commands.UpdateProduct;
using FRESHY.Main.Application.Abstractions.ProductAbstractions.Commands.UpdaterProductUnit;
using FRESHY.Main.Application.Abstractions.ProductAbstractions.Queries;
using FRESHY.Main.Application.Abstractions.ProductAbstractions.Queries.GetAllProducts;
using FRESHY.Main.Application.Abstractions.ProductAbstractions.Queries.GetAllProducts.Results;
using FRESHY.Main.Application.Abstractions.ProductAbstractions.Queries.GetProductDetails;
using FRESHY.Main.Application.Abstractions.ProductAbstractions.Queries.SearchProduct;
using FRESHY.Main.Application.Abstractions.ProductTypeAbstractions.Commands.CreateProductType;
using FRESHY.Main.Application.Abstractions.ProductTypeAbstractions.Queries.GetAllProductTypes;
using FRESHY.Main.Contract.Requests.ProductRequests;
using FRESHY.Main.Contract.Requests.ProducTypeRequests;
using FRESHY.Main.Contract.Responses.ProductResponses;
using FRESHY.Main.Contract.Responses.ProductTypeResponses;
using FRESHY.Main.Infrastructure.Persistance;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FRESHY.Main.Domain.Models.Aggregates.ProductUnitAggregate;
using FRESHY.Main.Domain.Models.Aggregates.ProductAggregate.ValueObjects;

namespace FRESHY_API.Controllers;

[ApiController]
[Route("catalogue")]
[Produces("application/json")]
public class CatalogueController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly FreshyDbContext _Context;

    public CatalogueController(IMapper mapper, IMediator mediator, FreshyDbContext context)
    {
        _mapper = mapper;
        _mediator = mediator;
        _Context = context;
    }


    [HttpPost("type")]
    public async Task<IActionResult> AddProductTypeAsync([FromBody] CreateProductTypeRequest request)
    {
        var command = _mapper.Map<CreateProductTypeCommand>(request);
        var result = await _mediator.Send(command);
        if (result.Succeeded)
        {
            return Ok();
        }
        return StatusCode((int)result.StatusCode, result.Message);
    }

    [HttpGet("types")]
    public async Task<IActionResult> GetAllProductTypesAsync()
    {
        var query = new GetAllProductTypesQuery();
        var result = await _mediator.Send(query);

        if (result.Succeeded)
        {
            return Ok(_mapper.Map<Response<IEnumerable<AllProductTypesResponse>>>(result));
        }
        return StatusCode((int)result.StatusCode, result.Message);
    }

    [HttpGet("products")]
    public async Task<IActionResult> GetAllProductsAsync([FromQuery] GetAllProductsQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.Succeeded)
        {
            return Ok(_mapper.Map<PageResponse<IEnumerable<AllProductsResponse>>>(result));
        }
        return StatusCode((int)result.StatusCode, result.Message);
    }



    [HttpPost("product")]
    public async Task<IActionResult> ImportProductAsync([FromBody] ImportProductRequest request)
    {
        var command = _mapper.Map<ImportProductCommand>(request);
        var result = await _mediator.Send(command);

        if (result.StatusCode == HttpStatusCode.OK) 
        {
            return Ok();
        }
        return StatusCode((int)result.StatusCode, result.Message);
    }

    [HttpPut("product/{id}")]
    public async Task<IActionResult> UpdateProductAsync(
        [FromBody] UpdateProductRequest request,
        [FromRoute] Guid id)
    {
        var updatedRequest = request with { Id = id };

        var command = _mapper.Map<UpdateProductCommand>(updatedRequest);
        var result = await _mediator.Send(command);

        if (result.Succeeded)
        {
            return Ok();
        }
        else if (result.StatusCode == HttpStatusCode.InternalServerError)
        {
            return StatusCode((int)result.StatusCode, result.Message);
        }
        return BadRequest();
    }

    [HttpGet("product/{id}")]
    public async Task<IActionResult> GetProductDetails(
    [FromRoute] Guid id)
    {
        var query = new GetProductDetailsQuery(id);
        var result = await _mediator.Send(query);

        if (result.Succeeded)
        {
            return Ok(_mapper.Map<Response<ProductDetailsResponse>>(result));
        }
        else if (result.StatusCode == HttpStatusCode.InternalServerError)
        {
            return StatusCode((int)result.StatusCode, result.Message);
        }
        return BadRequest();
    }

    [HttpDelete("product/de-active/{id}")]
    public async Task<IActionResult> DeActiveProductAsync([FromBody] DeActiveProductRequest request)
    {
        var command = _mapper.Map<DeActiveProductCommand>(request);
        var result = await _mediator.Send(command);

        if (result.Succeeded)
        {
            return Ok();
        }
        else if (result.StatusCode == HttpStatusCode.NotFound)
        {
            return NotFound(result.Message);
        }
        return StatusCode((int)result.StatusCode, result.Message);
    }

    [HttpGet("SearchProduct")]
    public async Task<IActionResult> SearchProducts([FromQuery] SearchProductQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(_mapper.Map<PageResponse<IEnumerable<AllProductsResponse>>>(result));
    }




    [HttpPut("product/{id}/unit/{unitId}")]
    public async Task<IActionResult> UpdateProductUnitAsync(
        [FromBody] UpdateProductUnitRequest request,
        [FromRoute] Guid id,
        [FromRoute] Guid unitId)
    {
        var filledRequest = request with { ProductId = id, ProductUnitId = unitId };

        var command = _mapper.Map<UpdateProductUnitCommand>(filledRequest);
        var result = await _mediator.Send(command);

        if (result.Succeeded)
        {
            return Ok();
        }

        if (result.StatusCode == HttpStatusCode.NotFound)
        {
            return NotFound();
        }

        return StatusCode((int)result.StatusCode, result.Message);
    }
    [HttpGet("SearchProductOrderOffline/{name}")]
    public async Task<IActionResult> SearchProductOrderOffline([FromRoute] string name)
    {

        var allUnit = await _Context.Units.ToListAsync();
        var allproduct = await _Context.Products.Where(p => p.Name.Contains(name)).ToListAsync();

        var data = allUnit.Join(allproduct,
            unit => unit.ProductId,
            product => product.Id,
            (unit, product) => new
            {
                UnitID = unit.Id,
                ProductId = unit.ProductId,
                ProductName = product.Name,
                UnitType = unit.UnitType,
                unitValue = unit.UnitValue,
                Price = unit.SellPrice,
                quantity = unit.Quantity
            }).ToList();


        return Ok(data);

    }
    [HttpGet("getallunits")]
    public async Task<IActionResult> getAllUnits()
    {

        var allUnit = await _Context.Units.ToListAsync();
        var allproduct = await _Context.Products.ToListAsync();

        var data = allUnit.Join(allproduct,
            unit => unit.ProductId,
            product => product.Id,
            (unit, product) => new
            {
                UnitID = unit.Id,
                ProductId = unit.ProductId,
                ProductName = product.Name,
                UnitType = unit.UnitType,
                unitValue = unit.UnitValue,
                Price = unit.SellPrice,
            }).ToList();


        return Ok(data);

    }
    [HttpGet("getunitbyid/{id}")]
    public async Task<IActionResult> getUnitById([FromRoute] Guid id)
    {
        var allUnit = await _Context.Units.ToListAsync();
        string idString = id.ToString(); // Chuyển đổi id thành chuỗi
        var unit = allUnit.FirstOrDefault(p => p.Id.Value.ToString() == idString);


        return Ok(unit);
    }

    [HttpPost("addunit/{productid}/{unittype}/{unitvalue}/{quantity}/{importprice}/{sellprice}")]
    public async Task<IActionResult> addUnit([FromRoute] Guid productid, [FromRoute] string unittype, [FromRoute] double unitvalue, [FromRoute] int quantity, [FromRoute] double importprice, [FromRoute] double sellprice, [FromBody] AddUnitRequest request
      )
    {
      
        var productId = new ProductId(productid);

        var unitadd = ProductUnit.Create(productId, unittype, unitvalue, quantity, importprice, sellprice, request.UnitFeatureImage);

        _Context.Units.Add(unitadd);
        _Context.SaveChanges();

        return Ok(unitadd);
    }
    [HttpDelete("deleteunit/{unitid}")]
    public async Task<IActionResult> removeUnit([FromRoute] Guid unitid)
    {
        var unit = await _Context.Units.FirstOrDefaultAsync(x => x.Id.Value == unitid);

        if (unit != null)
        {
            _Context.Units.Remove(unit);
            await _Context.SaveChangesAsync();
            return Ok();
        }
        else
        {
            return NotFound(); 
        }
    }







}

