using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IColorService
    {
        List<Color> GetAll();
        Color GetById(int colorId);
        void Create(Color color);
        void Update(int colorId, Color color);
        void Delete(int colorId);
    }
}
