using Microsoft.AspNetCore.Mvc;
using SwiftBuy.Core.Domain.Contracts;
using SwiftBuy.Core.Domain.Entities.Product;

namespace SwiftBuy.AdminDashboard.Controllers
{
    public class BrandController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BrandController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            return View(brands);
        }

        public async Task<IActionResult> Create(ProductBrand brand)
        {
            try
            {
                await _unitOfWork.GetRepository<ProductBrand, int>().AddAsync(brand);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("brand", "Please Enter New Brand");
                return View(nameof(Index), await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync());
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var brand = await _unitOfWork.GetRepository<ProductBrand, int>().GetByIdAsync(id);
            _unitOfWork.GetRepository<ProductBrand, int>().Delete(brand);
            await _unitOfWork.CompleteAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
