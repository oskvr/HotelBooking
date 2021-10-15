

# Hotel Booking

![Picture of app](https://i.imgur.com/XZp5NgA.jpg)

## About

### Tech:

- WPF
- [Prism](https://github.com/PrismLibrary/Prism)
- Entity Framework Core
- SQL Server Express LocalDB
- [Fody.PropertyChanged](https://github.com/Fody/PropertyChanged)

UI libraries and controls:

- [ModernWpf](https://github.com/ghost1372/ModernWpf)
  

## Getting started

### Prerequisites

- [.NET 5.0 SDK or higher](https://dotnet.microsoft.com/download/dotnet/5.0)

  - `dotnet tool install --global dotnet-ef`

- [SQL Server Express LocalDB](http://www.hanselman.com/blog/download-sql-server-express)

- An IDE or Editor of your choice

  

```sh
> git clone https://github.com/oskvr/HotelBooking.git HotelBooking
> cd HotelBooking/HotelBooking.DAL
> dotnet ef migrations add Init
> dotnet ef database update
> cd ../HotelBooking.Presentation
> dotnet run
```

or 

Using the *Package Manager Console* in Visual Studio:

- Change the "Default Project" to HotelBooking.DAL and run:
```
> add-migration Init
> update-database
```

## License

MIT
