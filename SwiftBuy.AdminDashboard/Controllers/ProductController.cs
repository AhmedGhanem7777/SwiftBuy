using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Mvc;
using SwiftBuy.AdminDashboard.Helper;
using SwiftBuy.AdminDashboard.Models;
using SwiftBuy.Core.Domain.Common.Entities;
using SwiftBuy.Core.Domain.Contracts;
using SwiftBuy.Core.Domain.Specifications.Products;

namespace SwiftBuy.AdminDashboard.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var specs = new ProductWithBrandAndCategorySpecifications();
            var products = await _unitOfWork.GetRepository<Product, int>().GetAllWithSpecAsync(specs);
            var mappedProducts = _mapper.Map<IReadOnlyList<ProductViewModel>>(products);
            return View(mappedProducts);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Image is not null)
                {
                    model.PictureUrl = PictureSettings.UploadFile(model.Image, "products");
                }
                else
                {
                    model.PictureUrl = "images/products/blueberry-cheesecake.png";
                }
                var mappedProduct = _mapper.Map<Product>(model);
                await _unitOfWork.GetRepository<Product, int>().AddAsync(mappedProduct);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(id);
            var mappedProduct = _mapper.Map<ProductViewModel>(product);
            return View(mappedProduct);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Image is not null)
                {
                    if (model.PictureUrl is not null)
                    {
                        PictureSettings.DeleteFile(model.PictureUrl, "products");
                    }
                    model.PictureUrl = PictureSettings.UploadFile(model.Image, "products");
                }
                var product = _mapper.Map<Product>(model);
                _unitOfWork.GetRepository<Product, int>().Update(product);
                var result = await _unitOfWork.CompleteAsync();
                if (result > 0)
                    return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(id);
            var mappedProduct = _mapper.Map<ProductViewModel>(product);
            return View(mappedProduct);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ProductViewModel model)
        {
            try
            {
                var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(model.Id);
                if (product.PictureUrl is not null)
                    PictureSettings.DeleteFile(product.PictureUrl, "products");
                _unitOfWork.GetRepository<Product, int>().Delete(product);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }
    }
}
