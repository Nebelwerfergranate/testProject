using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sandbox_Products.DAL.Core;
using Sandbox_Products.DAL;
using Sandbox_Products.Models.Product;
using Sandbox_Products.Utils;
using System.Data.Entity.Infrastructure;
using Sandbox_Products.Models;
using Sandbox_Products.Utils.CrudHelpers;
using System.Data;
using Messages = Sandbox_Products.Constants.Messages;

namespace Sandbox_Products.BLL.Product
{
    public class ProductManager
    {
        UnitOfWork _db;

        public ProductManager(UnitOfWork db)
        {
            _db = db;
        }

        public ItemsResult<GetProductsViewModel> GetProducts(PagingModel paging)
        {
            ItemsResult<GetProductsViewModel> result = new ItemsResult<GetProductsViewModel>();
            // todo try catch
            IQueryable<GetProductsViewModel> resAsQueryable = GetProductsAsQueryable();

            IOrderedQueryable<GetProductsViewModel> resAsOrderedQueryable;
            switch (paging.Sidx)
            {
                case nameof(GetProductsViewModel.Name):
                    resAsOrderedQueryable = resAsQueryable.OrderBy(p => p.Name, paging.Sord);
                    break;
                case nameof(GetProductsViewModel.Price):
                    resAsOrderedQueryable = resAsQueryable.OrderBy(p => p.Price, paging.Sord);
                    break;
                default:
                    resAsOrderedQueryable = resAsQueryable.OrderBy(p => p.Id);
                    break;
            }

            Int32 total;
            result.Items = resAsOrderedQueryable.Paging(paging, out total).ToList();
            result.Total = total;
            result.Page = paging.Page;
            result.Success = true;

            return result;
        }

        public ItemsResult<GetProductsOperationsTypesViewModel> GetProductOperationsTypes()
        {
            ItemsResult<GetProductsOperationsTypesViewModel> result = new ItemsResult<GetProductsOperationsTypesViewModel>();

            try
            {
                //todo cache
                result.Items = _db.SandboxRepository
                    .Get<ProductsOperationsTypes>()
                    .OrderByDescending(op => op.DisplayOrder.HasValue)
                    .ThenBy(op => op.DisplayOrder)                                       
                    .Select(op => new GetProductsOperationsTypesViewModel()
                    {
                        Id = op.Id,
                        Name = op.DisplayName
                    })                    
                    .ToList();
                result.Success = true;
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
                result.Message = Messages.Shared.INTERNAL_ERROR_MESSAGE;
            }            

            return result;
        }

        public ModelResult<ProductDetailViewModel> GetProductDetail(int id)
        {
            ModelResult<ProductDetailViewModel> result = new ModelResult<ProductDetailViewModel>();

            try
            {
                Products product = _db.SandboxRepository.Get<Products>(p => p.Id == id);

                if (product == null)
                {
                    result.Message = Messages.Product.PRODUCT_NOT_FOUND_ERROR_MESSAGE;
                    result.Success = false;

                    return result;
                }

                if (product.IsDeleted)
                {
                    result.Message = Messages.Product.GetProductIsDeletedErrorMessage(product.Name);
                    result.Success = false;

                    return result;                    
                }

                ProductDetailViewModel model = new ProductDetailViewModel();
                model.Name = product.Name;
                model.Price = product.Price;
                model.Count = _db.ProductRepository.GetProductCountById(id);

                result.Model = model;
                result.Success = true;  
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }

            return result;
        }

        public ItemsResult<GetProductOperationsViewModel> GetProductOperations(int productId, PagingModel paging, GetProductOperationsViewModel filter)
        {
            ItemsResult<GetProductOperationsViewModel> result = new ItemsResult<GetProductOperationsViewModel>();

            String productName;
            if(CheckProductIsDeletedByIdAndGetProductName(productId, out productName))
            {                
                result.Message = Messages.Product.GetProductExistDeletedErrorMessage(productName);

                return result;
            }

            try
            {
                IQueryable<GetProductOperationsViewModel> resAsQueryable = GetProductsOperationsAsQueryable(productId);

                if (!String.IsNullOrEmpty(filter.OperatorStrict))
                {
                    resAsQueryable = resAsQueryable.Where(po => po.Operator == filter.OperatorStrict);
                }

                if (!String.IsNullOrEmpty(filter.Operation))
                {
                    resAsQueryable = resAsQueryable.Where(po => po.Operation == filter.Operation);
                }

                resAsQueryable = resAsQueryable.Where(po => po.DateRaw >= filter.DateFrom);
                resAsQueryable = resAsQueryable.Where(po => po.DateRaw <= filter.DateTo);

                IOrderedQueryable<GetProductOperationsViewModel> resAsOrderedQueryable;

                switch (paging.Sidx)
                {
                    case nameof(GetProductOperationsViewModel.Operator):
                        resAsOrderedQueryable = resAsQueryable.OrderBy(po => po.Operator, paging.Sord);
                        break;
                    case nameof(GetProductOperationsViewModel.Operation):
                        resAsOrderedQueryable = resAsQueryable.OrderBy(po => po.Operation, paging.Sord);
                        break;
                    case nameof(GetProductOperationsViewModel.Count):
                        resAsOrderedQueryable = resAsQueryable.OrderBy(po => po.Count, paging.Sord);
                        break;
                    case nameof(GetProductOperationsViewModel.Date):
                        resAsOrderedQueryable = resAsQueryable.OrderBy(po => po.DateRaw, paging.Sord);
                        break;
                    default:
                        resAsOrderedQueryable = resAsQueryable.OrderBy(po => po.Operator);
                        break;
                }
                Int32 totalPages;

                result.Items = resAsOrderedQueryable.Paging(paging, out totalPages).ToList();
                result.Total = totalPages;
                result.Page = paging.Page;
                result.Success = true;
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
                result.Message = Messages.Shared.INTERNAL_ERROR_MESSAGE;
            }

            return result;
        }

        public ItemsResult<GetPerUseroperationsSumViewModel> GetPerUseroperationsSum (int productId, PagingModel paging, GetPerUseroperationsSumViewModel filter)
        {
            ItemsResult<GetPerUseroperationsSumViewModel> result = new ItemsResult<GetPerUseroperationsSumViewModel>();

            String productName;
            if (CheckProductIsDeletedByIdAndGetProductName(productId, out productName))
            {
                result.Message = Messages.Product.GetProductExistDeletedErrorMessage(productName);

                return result;
            }

            try
            {
                IQueryable<GetPerUseroperationsSumViewModel> resAsQueryable = GetPerUseroperationsSumAsQueryable(productId);

                if (!String.IsNullOrEmpty(filter.Operator))
                {
                    resAsQueryable = resAsQueryable.Where(op => op.Operator.Contains(filter.Operator));
                }

                IOrderedQueryable<GetPerUseroperationsSumViewModel> resAsOrderedQueryable;

                switch (paging.Sidx)
                {
                    case nameof(GetPerUseroperationsSumViewModel.Operator):
                        resAsOrderedQueryable = resAsQueryable.OrderBy(po => po.Operator, paging.Sord);
                        break;
                    case nameof(GetPerUseroperationsSumViewModel.Sum):
                        resAsOrderedQueryable = resAsQueryable.OrderBy(po => po.Sum, paging.Sord);
                        break;
                    default:
                        resAsOrderedQueryable = resAsQueryable.OrderBy(po => po.Operator);
                        break;
                }
                Int32 totalPages;

                result.Items = resAsOrderedQueryable.Paging(paging, out totalPages).ToList();
                result.Total = totalPages;
                result.Page = paging.Page;
                result.Success = true;
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
                result.Message = Messages.Shared.INTERNAL_ERROR_MESSAGE;
            }

            return result;
        }

        public OperationResult CreateProduct(ProductModel model)
        {
            OperationResult result = new OperationResult();

            Products product = new Products()
            {
                Name = model.ProductName,
                Price = model.ProductPrice,
                IsDeleted = false,
            };
            _db.SandboxRepository.Add(product);

            try
            {
                _db.Save();
                result.Success = true;
            }
            catch(DbUpdateException ex)
            {               
                if (CheckProductIsDeletedByName(model.ProductName))
                {
                    result.Message = Messages.Product.GetProductExistDeletedErrorMessage(model.ProductName);
                }
                else
                {
                    result.Message = Messages.Product.GetProductExistsErrorMessage(model.ProductName);
                }

                Debug.LogError(ex, true);
            }

            return result;    
        }

        public OperationResult UpdateProduct(UpdateProductModel model)
        {
            OperationResult result = new OperationResult();

            Products product = _db.SandboxRepository.Get<Products>(p => p.Id == model.Id);
            product.Name = model.Name;
            product.Price = model.Price;

            if (product.IsDeleted)
            {
                result.Success = false;
                result.NeedRefresh = true;
                result.Message = Messages.Product.GetProductIsDeletedErrorMessage(product.Name);

                return result;
            }

            _db.SandboxRepository.Update(product);

            try
            {
                _db.Save();
                result.Success = true;
            }
            catch (DbUpdateException ex)
            {
                result.Message = Messages.Product.GetProductExistsErrorMessage(model.Name);
                Debug.LogError(ex, true);
            }

            return result;
        }

        public OperationResult DeleteProduct(Int32 id)
        {
            OperationResult result = new OperationResult();

            Products product = _db.SandboxRepository.Get<Products>(p => p.Id == id);

            if(product.IsDeleted)
            {
                result.Success = false;
                result.NeedRefresh = true;
                result.Message = Messages.Product.GetProductIsDeletedErrorMessage(product.Name);

                return result;
            }
            product.IsDeleted = true;

            _db.SandboxRepository.Update(product);

            try
            {
                _db.Save();
                result.Success = true;
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }

            return result;
        }

        public OperationResult SaveProductOperation(ProductOperationModel model, String username)
        {
            OperationResult result = new OperationResult();

            // Question: is Isolation level ok?
            using (System.Data.Entity.DbContextTransaction dbTran = _db.GetTransaction(IsolationLevel.Serializable))
            {

                if (model.ProductOperationId == Constants.Product.EXPENSE_ID)
                {
                    Int64 productsCount = _db.ProductRepository.GetProductCountById(model.ProductId);

                    if (productsCount < model.ProductsCount)
                    {
                        result.Success = false;
                        result.Message = Messages.Product.GetNotEnoughProductsCountMessage(productsCount);

                        return result;
                    }
                }

                String productName = String.Empty;

                if (CheckProductIsDeletedByIdAndGetProductName(model.ProductId, out productName))
                {
                    result.Success = false;
                    result.NeedRefresh = true;
                    result.Message = Messages.Product.GetProductIsDeletedErrorMessage(productName);

                    return result;
                }

                ProductsOperations operation = new ProductsOperations();
                operation.ProductId = model.ProductId;
                operation.OperationTypeId = model.ProductOperationId;
                operation.ProductsCount = model.ProductsCount;
                operation.DateTimeCreated = DateTime.Now;
                operation.OperatorName = username;
                _db.SandboxRepository.Add(operation);

                try
                {
                    _db.Save();
                    dbTran.Commit();
                    result.Success = true;
                }
                catch (Exception ex)
                {
                    Debug.LogError(ex);
                }         
            }            

            return result;
        }

        public Boolean CheckProductIsDeletedByName(String name)
        {
            Products product = _db.SandboxRepository.Get<Products>(p => p.Name == name);

            return product.IsDeleted;
        }

        public Boolean CheckProductIsDeletedByIdAndGetProductName(int id, out String name)
        {
            Products product = _db.SandboxRepository.Get<Products>(p => p.Id == id);
            name = product.Name;

            return product.IsDeleted;
        }

        private IQueryable<GetProductsViewModel> GetProductsAsQueryable()
        {
           return _db.SandboxRepository
                .Get<Products>()
                .Where(p => !p.IsDeleted)
                .Select(p => new GetProductsViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                });
        }

        private IQueryable<GetProductOperationsViewModel> GetProductsOperationsAsQueryable(int productId)
        {
            return _db.SandboxRepository
                    .Get<ProductsOperations>()
                    .Where(po => po.ProductId == productId)
                    .Select(po => new GetProductOperationsViewModel()
                    {
                        Operator = po.OperatorName,
                        Count = po.ProductsCount,
                        Operation = po.ProductsOperationsTypes.DisplayName,
                        DateRaw = po.DateTimeCreated,
                        Color = po.ProductsOperationsTypes.DisplayColor
                    });
        }

        private IQueryable<GetPerUseroperationsSumViewModel> GetPerUseroperationsSumAsQueryable(int productId)
        {
            return _db.SandboxRepository
                .Get<vw_ProductsOperationsDetail>()
                .Where(po => po.ProductId == productId)
                .GroupBy(po => new {
                    OperatorName = po.OperatorName,
                    ProductPrice = po.ProductPrice
                })
                .Select(g => new GetPerUseroperationsSumViewModel()
                {
                    Operator = g.Key.OperatorName,
                    Count = g.Sum(po => (Int64)po.ProductsCountDelta),
                    Price = g.Key.ProductPrice
                });
        }
    }
}