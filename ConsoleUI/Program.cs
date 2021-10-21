using Business.Concrete;
using Core.Utilities.Results;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new EfCarDal());

            #region [Tüm Arabaları Listele]
            ListAllCars(carManager);
            #endregion

            #region [Id si 1 olan arabayı getir]

            Console.WriteLine("---- BMW yi getir ----");

            var getCar = carManager.GetById(1).ConfigureAwait(false).GetAwaiter().GetResult();

            Console.WriteLine($"Car Id : {getCar.Data.Id}, Daily Price : {getCar.Data.DailyPrice}, Model Year : {getCar.Data.ModelYear}, Description : {getCar.Data.Description}");

            Console.WriteLine("\n");
            #endregion

            #region [Yeni araba ekle]
            Car newCar = new Car
            {
                Id = 4,
                BrandId = 5,
                ColorId = 28,
                DailyPrice = 150,
                ModelYear = 1984,
                Description = "Tofaş Şahin"
            };

            carManager.Create(newCar);

            ListAllCars(carManager);
            #endregion

            #region [Tofaş şahinin fiyatını güncelle]
            IDataResult<Car> getTofas = carManager.GetById(4).ConfigureAwait(false).GetAwaiter().GetResult();
            getTofas.Data.DailyPrice = 120;

            carManager.Update(4, getTofas.Data);

            ListAllCars(carManager);
            #endregion

            #region [Tofaş şahini sil]
            carManager.Delete(4);

            ListAllCars(carManager);
            #endregion

            Console.WriteLine("Hello World!");
        }

        private static void ListAllCars(CarManager carManager)
        {
            Console.WriteLine("---- Tüm Arabaların Listesi ----");

            var allCarList = carManager.GetAll().ConfigureAwait(false).GetAwaiter().GetResult();

            foreach (var car in allCarList.Data)
            {
                Console.WriteLine($"Car Id : {car.Id}, Daily Price : {car.DailyPrice}, Model Year : {car.ModelYear}, Description : {car.Description}");
            }

            Console.WriteLine("\n");
        }
    }
}
