using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private readonly ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        public async Task<IDataResult<List<Customer>>> GetAll()
        {
            var data = await _customerDal.GetAllAsync();
            return new SuccessDataResult<List<Customer>>(data);
        }

        public async Task<IDataResult<Customer>> GetById(int customerId)
        {
            var data = await _customerDal.GetAsync(b => b.Id == customerId);
            if (data is null) return new ErrorDataResult<Customer>(data, Messages.CustomerIsNull);
            else return new SuccessDataResult<Customer>(data);
        }

        public async Task<IResult> Create(Customer customer)
        {
            await _customerDal.AddAsync(customer);
            return new SuccessResult(Messages.CustomerAdded);
        }

        public async Task<IResult> Update(Customer customer)
        {
            await _customerDal.UpdateAsync(customer);
            return new SuccessResult(Messages.CustomerUpdated);
        }

        public IResult Delete(Customer customer)
        {
            _customerDal.Delete(customer);
            return new SuccessResult(Messages.CustomerDeleted);
        }
    }
}
