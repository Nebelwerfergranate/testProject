using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sandbox_Products.DAL.Core;
using Sandbox_Products.BLL.Product;
using Sandbox_Products.Models.Product;
using Sandbox_Products.Models;
using Newtonsoft.Json;
using Sandbox_Products.Utils.CrudHelpers;
using Sandbox_Products.Constants.Messages;

namespace Sandbox_Products.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Product
        public ActionResult Index()
        {          
            return View();
        }

        public string GetProducts(Int32 rows, Int32 page, String sidx, String sord)
        {
            PagingModel pagin = new PagingModel(rows, page, sidx, sord);

            ProductManager productManager = new ProductManager(Db);
            ItemsResult<GetProductsViewModel> result = productManager.GetProducts(pagin);

            return JsonConvert.SerializeObject(new {
                rows = result.Items,
                total = result.Total,
                page = result.Page
            });
        }

        public ActionResult GetProductOperationsTypes()
        {
            ProductManager productManager = new ProductManager(Db);

            ItemsResult<GetProductsOperationsTypesViewModel> result = productManager.GetProductOperationsTypes();

            return Json(new
            {
                result = result.Success,
                message = result.Message,
                items = result.Items
            });
        }

        public ActionResult GetProductOperationsTypesHtml()
        {
            ProductManager productManager = new ProductManager(Db);

            ItemsResult<GetProductsOperationsTypesViewModel> result = productManager.GetProductOperationsTypes();

            return PartialView(result);
        }

        public ActionResult GetProductDetail(int id)
        {
            ProductManager productManager = new ProductManager(Db);

            ModelResult<ProductDetailViewModel> result = productManager.GetProductDetail(id);

            return Json(new
            {
                result = result.Success,
                message = result.Message,
                model = result.Model
            });
        }

        public string GetProductOperations(int productId, Int32 rows, Int32 page, String sidx, String sord, GetProductOperationsViewModel filter)
        {
            PagingModel paging = new PagingModel(rows, page, sidx, sord); // todo - grab from http context

            ProductManager productManager = new ProductManager(Db);

            ItemsResult<GetProductOperationsViewModel> result = productManager.GetProductOperations(productId, paging, filter);

            return JsonConvert.SerializeObject(new JqGridResult<GetProductOperationsViewModel>
            {
                rows = result.Items,
                total = result.Total,
                page = result.Page,
                result = result.Success,
                message = result.Message
            });
        }

        public string GetPerUseroperationsSum(int productId, Int32 rows, Int32 page, String sidx, String sord, GetPerUseroperationsSumViewModel filter)
        {
            PagingModel paging = new PagingModel(rows, page, sidx, sord);

            ProductManager productManager = new ProductManager(Db);

            ItemsResult<GetPerUseroperationsSumViewModel> result = productManager.GetPerUseroperationsSum(productId, paging, filter);

            return JsonConvert.SerializeObject(new JqGridResult<GetPerUseroperationsSumViewModel>
            {
                rows = result.Items,
                total = result.Total,
                page = result.Page,
                result = result.Success,
                message = result.Message
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(ProductModel model)
        {
            OperationResult result = new OperationResult();

            if (ModelState.IsValid)
            {
                ProductManager productManager = new ProductManager(Db);
                result = productManager.CreateProduct(model);
            }
            else
            {
                result.Message = Shared.VALIDATION_ERROR_MESSAGE;
            }

            return Json(new ActionJsonViewModel
            {
                result = result.Success,
                message = result.Message
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProduct(UpdateProductModel model)
        {
            OperationResult result = new OperationResult();
            List<String> errorMessages = new List<String>();

            if (ModelState.IsValid)
            {
                ProductManager productManager = new ProductManager(Db);
                result = productManager.UpdateProduct(model);
            }
            else
            {
                GrabModelErrors(errorMessages);
                result.Message = Shared.VALIDATION_ERROR_MESSAGE;
            }

            return Json(new ActionJsonViewModel
            {
                result = result.Success,
                message = result.Message,
                errors = errorMessages
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProduct (Int32 id)
        {
            ProductManager productManager = new ProductManager(Db);

            OperationResult result = productManager.DeleteProduct(id);

            return Json(new ActionJsonViewModel
            {
                result = result.Success,
                message = result.Message
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveProductOperation(ProductOperationModel model)
        {
            OperationResult result = new OperationResult();

            if (ModelState.IsValid)
            {
                ProductManager productManager = new ProductManager(Db);
                result = productManager.SaveProductOperation(model, User.Identity.Name);
            }
            else
            {
                result.Message = Shared.VALIDATION_ERROR_MESSAGE;
            }

            return Json(new ActionJsonViewModel
            {
                result = result.Success,
                message = result.Message
            });
        }
    }
}