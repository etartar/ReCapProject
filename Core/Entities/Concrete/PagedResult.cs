using System.Collections.Generic;

namespace Core.Entities.Concrete
{
    public class PagedResult<T> : PagedResultBase where T : class, IEntity, new()
    {
        public IList<T> Results { get; set; }
        
        public PagedResult()
        {
            Results = new List<T>();
        }
    }
}
