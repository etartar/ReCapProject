using Core.Utilities.Results;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        Task<IDataResult<List<Customer>>> GetAll();
        Task<IDataResult<Customer>> GetById(int customerId);
        Task<IResult> Create(Customer customer);
        Task<IResult> Update(Customer customer);
        IResult Delete(Customer customer);
    }
}
