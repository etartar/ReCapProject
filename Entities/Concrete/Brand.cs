using Core.Entities;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public class Brand : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
