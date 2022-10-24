﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using Service.DTO;
using ApplicationCore.Context;
using System;
using Service.Services.IService;
using Service.Services.Service;
using Newtonsoft.Json;
using Service.View;
using FoodyAPI.Helper.Azure.IBlob;
using FoodyAPI.Helper.Azure;
using FoodyAPI.Helper.Azure.IBlob;
using Service.Helper;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodyAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // GET: api/<ProductController>
        IProductService _productService;
        IProductBlob _productBlob;
        /*
        public ProductController(FoodyContext context, IMapper mapper)
        {
            _productService = new ProductService(mapper,context);
        }
        */
        public ProductController(IProductService productService, IProductBlob productBlob)
        {
            _productService = productService;
            _productBlob = productBlob;
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] PagingRequest paging)
        {
            var list = await _productService.GetAsync(paging: paging, includeProperties: "Category,Store");
            if(list == null)
            {
                return NotFound();
            }
            foreach(var product in list)
            {
                //product.Picture = _productBlob.GetURL(product);
            }
            var products = new ProductView(list);
            var json = JsonConvert.SerializeObject(products);
            return Ok(json);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var dto = (await _productService.GetAsync(filter: p => p.Id == id)).FirstOrDefault();
            if(dto == null)
            {
                return NotFound();
            }
            dto.Picture = _productBlob.GetURLs(id);
            return Ok(dto);
        }
        // GET api/<ProductController>/name
        [HttpGet("name/{name}")]
        public async Task<IActionResult> Get([FromQuery] PagingRequest paging, [FromQuery] string name)
        {
            var list = await _productService.GetAsync(paging: paging, filter: p => p.Name.ToUpper().Contains(name.ToUpper()));
            if (list == null)
            {
                return NotFound();
            }
            var products = new ProductView(list);
            var json = JsonConvert.SerializeObject(products);
            return Ok(json);
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync(Product product, List<IFormFile> pictures)
        {
            try
            {
                /*
                var product = new Product()
                {
                    CategoryId = categoryId,
                    StoreId = storeId,
                    Name = name,
                    Price = price,
                    Quantity = quantity
                };
                */
                var create = await _productService.CreateAsync(product);
                if (create != null)
                {
                    //upload pictures
                    
                    return Ok(create);
                }
                else
                {
                    return BadRequest(StatusCodes.Status500InternalServerError);
                }
            }
            catch
            {
                return BadRequest(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT api/<ProductController>/5
        [HttpPut]
        public async Task<IActionResult> PutAsync(Product product)
        {
            try
            {
                /*
                var product = new Product()
                {
                    Id = id,
                    CategoryId = categoryId,
                    StoreId = storeId,
                    Name = name,
                    Price = price,
                    Quantity = quantity,
                    Status = true
                };
                */
                var updated = await _productService.UpdateAsync(product.Id, product);
                if (updated != null)
                {
                    return Ok(updated);
                }
                else
                {
                    return BadRequest(StatusCodes.Status500InternalServerError);
                }
            }
            catch
            {
                return BadRequest(StatusCodes.Status500InternalServerError);
            }
        }
        //PUT disable/enable 
        //change entity's status
        [HttpPut("enable-disable/{id}")]
        public async Task<IActionResult> SwitchStatusAsync(int id)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id);
                if (product.Status == false)
                {
                    product.Status = true;
                }
                else
                {
                    product.Status = false;
                }
                var updated = await _productService.UpdateAsync(id, product);
                return Ok(updated);
            }
            catch
            {
                return BadRequest(StatusCodes.Status500InternalServerError);
            }
        }
        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            //bool result = await _productService.DeleteAsync(id);
            //return Ok(result);
            return BadRequest(StatusCodes.Status501NotImplemented);//Ok("Not yet implemented!");
        }
    }
}
