using Business.Concrete;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new InMemoryCarDal());

            #region [Tüm Arabaları Listele]
            ListAllCars(carManager);
            #endregion

            #region [Id si 1 olan arabayı getir]

            Console.WriteLine("---- BMW yi getir ----");

            var getCar = carManager.GetCarById(1);

            Console.WriteLine($"Car Id : {getCar.Id}, Daily Price : {getCar.DailyPrice}, Model Year : {getCar.ModelYear}, Description : {getCar.Description}");

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

            carManager.CreateCar(newCar);

            ListAllCars(carManager);
            #endregion

            #region [Tofaş şahinin fiyatını güncelle]
            Car getTofas = carManager.GetCarById(4);
            getTofas.DailyPrice = 120;

            carManager.UpdateCar(getTofas);

            ListAllCars(carManager);
            #endregion

            #region [Tofaş şahini sil]
            Car getTofasForDelete = carManager.GetCarById(4);

            carManager.DeleteCar(getTofasForDelete);

            ListAllCars(carManager);
            #endregion

            Console.WriteLine("Hello World!");
        }

        private static void ListAllCars(CarManager carManager)
        {
            Console.WriteLine("---- Tüm Arabaların Listesi ----");

            foreach (var car in carManager.GetAllCars())
            {
                Console.WriteLine($"Car Id : {car.Id}, Daily Price : {car.DailyPrice}, Model Year : {car.ModelYear}, Description : {car.Description}");
            }

            Console.WriteLine("\n");
        }
    }
}
