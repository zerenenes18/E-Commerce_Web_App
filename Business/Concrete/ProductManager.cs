using Business.Abstract;
using Business.BusinessAspects;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        [SecuredOperation("admin,manager")]
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            //validation
            /*
            var context = new ValidationContext<Product>(product);
            ProductValidator productValidator = new ProductValidator();

            var result = productValidator.Validate(context);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            */
            
            _productDal.Add(product);

            return new Result(true,Messages.ProductAdded);
        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new Result(true, "product deleted");
        }

        public IDataResult<List<Product>> GetAll()
        {
            //if (DateTime.Now.Hour == 3)
            //{
            //    return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            //}

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id),Messages.GetData);
        }

        public IDataResult<Product> GetById(int id)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == id),Messages.GetData);
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice  >= min && p.UnitPrice <= max),Messages.GetData);
        }

        public IDataResult<List<ProductDetailDto>> GetPrnoductDetails()
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            //if (DateTime.Now.Hour == 2)
            //{
            //    return new ErrorDataResult<List<ProductDetailDto>>(Messages.MaintenanceTime);
            //}

            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails(),Messages.GetData);
        }
    }
}
