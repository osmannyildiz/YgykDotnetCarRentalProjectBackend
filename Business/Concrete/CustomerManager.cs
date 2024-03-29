﻿using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Concrete {
    public class CustomerManager : ICustomerService {
        ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal) {
            _customerDal = customerDal;
        }

        [ValidationAspect(typeof(CustomerValidator))]
        [CacheRemoveAspect("ICustomerService.Get")]
        public IResult Add(Customer customer) {
            _customerDal.Add(customer);
            return new SuccessResult(Messages.CustomerAdded);
        }

        [ValidationAspect(typeof(CustomerValidator))]
        [CacheRemoveAspect("ICustomerService.Get", "IRentalService.GetAllRentalDetails", "IRentalService.GetRentalDetail")]
        public IResult Delete(Customer customer) {
            _customerDal.Delete(customer);
            return new SuccessResult(Messages.CustomerDeleted);
        }

        [CacheAspect]
        public IDataResult<List<Customer>> GetAll() {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll());
        }

        [CacheAspect]
        public IDataResult<Customer> GetByCompanyName(string companyName) {
            return new SuccessDataResult<Customer>(_customerDal.Get(c => c.CompanyName == companyName));
        }

        [CacheAspect]
        public IDataResult<Customer> GetById(int id) {
            return new SuccessDataResult<Customer>(_customerDal.Get(c => c.Id == id));
        }

        [ValidationAspect(typeof(CustomerValidator))]
        [CacheRemoveAspect("ICustomerService.Get", "IRentalService.GetAllRentalDetails", "IRentalService.GetRentalDetail")]
        public IResult Update(Customer customer) {
            _customerDal.Update(customer);
            return new SuccessResult(Messages.CustomerUpdated);
        }
    }
}
