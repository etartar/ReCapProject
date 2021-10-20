﻿using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IBrandService
    {
        List<Brand> GetAll();
        Brand GetById(int brandId);
        void Create(Brand brand);
        void Update(int brandId, Brand brand);
        void Delete(int brandId);
    }
}