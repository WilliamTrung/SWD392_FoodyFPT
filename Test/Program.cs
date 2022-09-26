// See https://aka.ms/new-console-template for more information
using ApplicationCore.Context;
using ApplicationCore.Models;
using ApplicationCore.Repository;
using AutoMapper;
using Service;
using Service.Services.IService;
using Service.Services.Service;

string connection = "Server=tcp:foodyfpt.database.windows.net,1433;Initial Catalog=dbfoodyfpt;Persist Security Info=False;User ID=trungntse151134fpt;Password=Loliizabezt.1211;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
FoodyContext _context = new FoodyContext(connection);
IGenericRepository<Product> _repo = new GenericRepository<Product>(_context);
/*
Mapping mapping = new Mapping();
IMapper mapper = new Mapper(typeof(Mapping));

IProductService productService = new ProductService(mapping, _context);



var list = _repo.GetList();
foreach(var item in list.Result.ToList())
{
    Console.WriteLine(item);
}
*/
