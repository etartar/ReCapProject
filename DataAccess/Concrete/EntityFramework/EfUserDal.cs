using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, CarRentalContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using var context = new CarRentalContext();
            return GetClaimsQuery(context, user.Id).ToList();
        }

        public async Task<List<OperationClaim>> GetClaimsAsync(User user)
        {
            using var context = new CarRentalContext();
            return await GetClaimsQuery(context, user.Id).ToListAsync();
        }

        private IQueryable<OperationClaim> GetClaimsQuery(CarRentalContext context, int userId)
        {
            return from operationClaim in context.OperationClaims
                   join userOperationClaim in context.UserOperationClaims on operationClaim.Id equals userOperationClaim.OperationClaimId
                   where userOperationClaim.UserId == userId
                   select new OperationClaim
                   {
                       Id = operationClaim.Id,
                       Name = operationClaim.Name
                   };
        }
    }
}
