using Core.Entities;

namespace Entities.Concrete
{
    public class CreditCard : IEntity
    {
        public int Id { get; set; }
        public string NameOnTheCard { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public int Cvv { get; set; }
    }
}
