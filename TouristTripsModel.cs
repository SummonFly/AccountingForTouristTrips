using AccountingForTouristTrips.Model;
using System;
using System.Data.Entity;
using System.Linq;

namespace AccountingForTouristTrips
{
    public class TouristTripsModel : DbContext
    {
        // Контекст настроен для использования строки подключения "TouristTripsModel" из файла конфигурации  
        // приложения (App.config или Web.config). По умолчанию эта строка подключения указывает на базу данных 
        // "AccountingForTouristTrips.TouristTripsModel" в экземпляре LocalDb. 
        // 
        // Если требуется выбрать другую базу данных или поставщик базы данных, измените строку подключения "TouristTripsModel" 
        // в файле конфигурации приложения.
        public TouristTripsModel()
            : base("name=TouristTripsModel")
        {
        }

        // Добавьте DbSet для каждого типа сущности, который требуется включить в модель. Дополнительные сведения 
        // о настройке и использовании модели Code First см. в статье http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Tour> Tours { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }

    }
}