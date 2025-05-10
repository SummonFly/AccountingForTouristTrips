namespace AccountingForTouristTrips.Migrations
{
    using AccountingForTouristTrips.Model;
    using AccountingForTouristTrips.View;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AccountingForTouristTrips.TouristTripsModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AccountingForTouristTrips.TouristTripsModel context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            context.Roles.Add(new Model.Role() { Id = 1, Name="Администратор"});
            context.Roles.Add(new Model.Role() { Id = 2, Name = "Бухгалтер" });
            context.Roles.Add(new Model.Role() { Id = 3, Name = "Менеджер" });
            context.Roles.Add(new Model.Role() { Id = 4, Name = "Клиент" });

            context.Countries.Add(new Model.Country() { Id = 1, Name = "Казахстан" });
            context.Countries.Add(new Model.Country() { Id = 2, Name = "Россия" });
            context.Countries.Add(new Model.Country() { Id = 3, Name = "Кыргызстан" });
            context.Countries.Add(new Model.Country() { Id = 4, Name = "Узбекистан" });
            context.Countries.Add(new Model.Country() { Id = 5, Name = "Таджикистан" });
            context.Countries.Add(new Model.Country() { Id = 6, Name = "Беларусь" });
            context.Countries.Add(new Model.Country() { Id = 7, Name = "Украина" });
            context.Countries.Add(new Model.Country() { Id = 8, Name = "Грузия" });

            context.Clients.Add(new Model.Client()
            {
                Id = 1,
                FirstName = "Иван",
                LastName = "Иванов",
                Email = "ivan.ivanov@example.com",
                Phone = "+7 (123) 456-78-90",
                DateOfBirth = new DateTime(1990, 1, 15)
            });

            context.Clients.Add(new Model.Client()
            {
                Id = 2,
                FirstName = "Петр",
                LastName = "Петров",
                Email = "petr.petrov@example.com",
                Phone = "+7 (123) 234-56-78",
                DateOfBirth = new DateTime(1985, 4, 20)
            });

            context.Clients.Add(new Model.Client()
            {
                Id = 3,
                FirstName = "Светлана",
                LastName = "Сидорова",
                Email = "svetlana.sidorova@example.com",
                Phone = "+7 (123) 345-67-89",
                DateOfBirth = new DateTime(1992, 6, 10)
            });

            context.Clients.Add(new Model.Client()
            {
                Id = 4,
                FirstName = "Алексей",
                LastName = "Алексеев",
                Email = "aleksey.alekseev@example.com",
                Phone = "+7 (123) 456-12-34",
                DateOfBirth = new DateTime(1988, 11, 25)
            });

            context.Clients.Add(new Model.Client()
            {
                Id = 5,
                FirstName = "Мария",
                LastName = "Маркова",
                Email = "maria.markova@example.com",
                Phone = "+7 (123) 678-90-12",
                DateOfBirth = new DateTime(1995, 3, 5)
            });

            context.Clients.Add(new Model.Client()
            {
                Id = 6,
                FirstName = "Дмитрий",
                LastName = "Дмитриев",
                Email = "dmitriy.dmitriev@example.com",
                Phone = "+7 (123) 789-01-23",
                DateOfBirth = new DateTime(1991, 9, 30)
            });

            context.Clients.Add(new Model.Client()
            {
                Id = 7,
                FirstName = "Елена",
                LastName = "Еленина",
                Email = "elena.elenina@example.com",
                Phone = "+7 (123) 890-12-34",
                DateOfBirth = new DateTime(1987, 12, 14)
            });

            context.Clients.Add(new Model.Client()
            {
                Id = 8,
                FirstName = "Анастасия",
                LastName = "Николаева",
                Email = "anastasia.nikolaeva@example.com",
                Phone = "+7 (123) 901-23-45",
                DateOfBirth = new DateTime(1993, 8, 22)
            });


            context.Tours.Add(new Model.Tour()
            {
                Id = 1,
                Name = "Тур в Алмату",
                CountryId = 1,
                Price = 50000,
                StartDate = new DateTime(2025, 6, 1),
                EndDate = new DateTime(2025, 6, 7),
                MaxSeats = 25
            });

            context.Tours.Add(new Model.Tour()
            {
                Id = 2,
                Name = "Тур в Москву",
                CountryId = 2,
                Price = 60000,
                StartDate = new DateTime(2025, 7, 10),
                EndDate = new DateTime(2025, 7, 15),
                MaxSeats = 30
            });

            context.Tours.Add(new Model.Tour()
            {
                Id = 3,
                Name = "Тур в Бишкек",
                CountryId = 3,
                Price = 45000,
                StartDate = new DateTime(2025, 8, 5),
                EndDate = new DateTime(2025, 8, 10),
                MaxSeats = 20
            });

            context.Tours.Add(new Model.Tour()
            {
                Id = 4,
                Name = "Тур в Ташкент",
                CountryId = 4,
                Price = 55000,
                StartDate = new DateTime(2025, 9, 1),
                EndDate = new DateTime(2025, 9, 6),
                MaxSeats = 15
            });

            context.Tours.Add(new Model.Tour()
            {
                Id = 5,
                Name = "Тур в Душанбе",
                CountryId = 5,
                Price = 48000,
                StartDate = new DateTime(2025, 9, 20),
                EndDate = new DateTime(2025, 9, 25),
                MaxSeats = 18
            });

            context.Tours.Add(new Model.Tour()
            {
                Id = 6,
                Name = "Тур в Минск",
                CountryId = 6,
                Price = 70000,
                StartDate = new DateTime(2025, 10, 10),
                EndDate = new DateTime(2025, 10, 15),
                MaxSeats = 22
            });

            context.Tours.Add(new Model.Tour()
            {
                Id = 7,
                Name = "Тур в Киев",
                CountryId = 7,
                Price = 65000,
                StartDate = new DateTime(2025, 10, 25),
                EndDate = new DateTime(2025, 10, 30),
                MaxSeats = 28
            });

            context.Tours.Add(new Model.Tour()
            {
                Id = 8,
                Name = "Тур в Батумі",
                CountryId = 8,
                Price = 52000,
                StartDate = new DateTime(2025, 11, 1),
                EndDate = new DateTime(2025, 11, 5),
                MaxSeats = 24
            });

            context.Tours.Add(new Model.Tour()
            {
                Id = 9,
                Name = "Тур в Санкт-Петербург",
                CountryId = 2,
                Price = 75000,
                StartDate = new DateTime(2025, 11, 10),
                EndDate = new DateTime(2025, 11, 15),
                MaxSeats = 30
            });

            context.Tours.Add(new Model.Tour()
            {
                Id = 10,
                Name = "Тур в Алматы и Астану",
                CountryId = 1,
                Price = 80000,
                StartDate = new DateTime(2025, 12, 1),
                EndDate = new DateTime(2025, 12, 7),
                MaxSeats = 35
            });

            context.Bookings.Add(new Model.Booking()
            {
                Id = 1,
                TourId = 1,
                ClientId = 1,
                BookingDate = new DateTime(2025, 5, 15),
                NumberOfPeople = 2,
                Statys = BookingStatys.Confirmed
            });

            context.Bookings.Add(new Model.Booking()
            {
                Id = 2,
                TourId = 2,
                ClientId = 2,
                BookingDate = new DateTime(2025, 6, 10),
                NumberOfPeople = 3,
                Statys = BookingStatys.Pending
            });

            context.Bookings.Add(new Model.Booking()
            {
                Id = 3,
                TourId = 3,
                ClientId = 3,
                BookingDate = new DateTime(2025, 7, 1),
                NumberOfPeople = 1,
                Statys = BookingStatys.Canceled
            });

            context.Bookings.Add(new Model.Booking()
            {
                Id = 4,
                TourId = 4,
                ClientId = 4,
                BookingDate = new DateTime(2025, 8, 5),
                NumberOfPeople = 4,
                Statys = BookingStatys.Confirmed
            });

            context.Bookings.Add(new Model.Booking()
            {
                Id = 5,
                TourId = 5,
                ClientId = 5,
                BookingDate = new DateTime(2025, 9, 12),
                NumberOfPeople = 2,
                Statys = BookingStatys.Pending
            });

            context.Bookings.Add(new Model.Booking()
            {
                Id = 6,
                TourId = 6,
                ClientId = 6,
                BookingDate = new DateTime(2025, 10, 18),
                NumberOfPeople = 5,
                Statys = BookingStatys.Confirmed
            });

            context.Bookings.Add(new Model.Booking()
            {
                Id = 7,
                TourId = 7,
                ClientId = 7,
                BookingDate = new DateTime(2025, 11, 22),
                NumberOfPeople = 3,
                Statys = BookingStatys.Canceled
            });

            context.Bookings.Add(new Model.Booking()
            {
                Id = 8,
                TourId = 8,
                ClientId = 8,
                BookingDate = new DateTime(2025, 12, 28),
                NumberOfPeople = 2,
                Statys = BookingStatys.Confirmed
            });

            context.Bookings.Add(new Model.Booking()
            {
                Id = 9,
                TourId = 1,
                ClientId = 2,
                BookingDate = new DateTime(2025, 5, 20),
                NumberOfPeople = 1,
                Statys = BookingStatys.Pending
            });

            context.Bookings.Add(new Model.Booking()
            {
                Id = 10,
                TourId = 2,
                ClientId = 3,
                BookingDate = new DateTime(2025, 6, 15),
                NumberOfPeople = 2,
                Statys = BookingStatys.Confirmed
            });

            context.Bookings.Add(new Model.Booking()
            {
                Id = 11,
                TourId = 3,
                ClientId = 4,
                BookingDate = new DateTime(2025, 7, 10),
                NumberOfPeople = 4,
                Statys = BookingStatys.Canceled
            });

            context.Bookings.Add(new Model.Booking()
            {
                Id = 12,
                TourId = 4,
                ClientId = 5,
                BookingDate = new DateTime(2025, 8, 15),
                NumberOfPeople = 3,
                Statys = BookingStatys.Confirmed
            });

            context.Bookings.Add(new Model.Booking()
            {
                Id = 13,
                TourId = 5,
                ClientId = 6,
                BookingDate = new DateTime(2025, 9, 5),
                NumberOfPeople = 2,
                Statys = BookingStatys.Pending
            });

            context.Bookings.Add(new Model.Booking()
            {
                Id = 14,
                TourId = 6,
                ClientId = 7,
                BookingDate = new DateTime(2025, 10, 25),
                NumberOfPeople = 3,
                Statys = BookingStatys.Confirmed
            });

            context.Bookings.Add(new Model.Booking()
            {
                Id = 15,
                TourId = 7,
                ClientId = 8,
                BookingDate = new DateTime(2025, 11, 10),
                NumberOfPeople = 4,
                Statys = BookingStatys.Pending
            });

            context.Bookings.Add(new Model.Booking()
            {
                Id = 16,
                TourId = 8,
                ClientId = 1,
                BookingDate = new DateTime(2025, 12, 5),
                NumberOfPeople = 2,
                Statys = BookingStatys.Canceled
            });

            context.Bookings.Add(new Model.Booking()
            {
                Id = 17,
                TourId = 1,
                ClientId = 2,
                BookingDate = new DateTime(2025, 5, 25),
                NumberOfPeople = 5,
                Statys = BookingStatys.Confirmed
            });

            context.Bookings.Add(new Model.Booking()
            {
                Id = 18,
                TourId = 2,
                ClientId = 3,
                BookingDate = new DateTime(2025, 6, 25),
                NumberOfPeople = 3,
                Statys = BookingStatys.Pending
            });

            context.Bookings.Add(new Model.Booking()
            {
                Id = 19,
                TourId = 3,
                ClientId = 4,
                BookingDate = new DateTime(2025, 7, 15),
                NumberOfPeople = 2,
                Statys = BookingStatys.Confirmed
            });

            context.Bookings.Add(new Model.Booking()
            {
                Id = 20,
                TourId = 4,
                ClientId = 5,
                BookingDate = new DateTime(2025, 8, 18),
                NumberOfPeople = 4,
                Statys = BookingStatys.Canceled
            });

            context.Users.Add(new User() 
            {
                ClientId=1, 
                Login="admin", PasswordHash=AuthorizationWindow.HashPassword("admin"), 
                RoleId= 1
            });
            context.Users.Add(new User()
            {
                ClientId = 2,
                Login = "bul",
                PasswordHash = AuthorizationWindow.HashPassword("bul"),
                RoleId = 2
            });
            context.Users.Add(new User()
            {
                ClientId = 3,
                Login = "manager",
                PasswordHash = AuthorizationWindow.HashPassword("manager"),
                RoleId = 3
            });

            context.Users.Add(new User()
            {
                ClientId = 4,
                Login = "client1",
                PasswordHash = AuthorizationWindow.HashPassword("client1"),
                RoleId = 4
            });
            context.Users.Add(new User()
            {
                ClientId = 5,
                Login = "client2",
                PasswordHash = AuthorizationWindow.HashPassword("client2"),
                RoleId = 4
            });

            context.SaveChanges();
        }
    }
}
