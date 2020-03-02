# Faker

[![Build status](https://ci.appveyor.com/api/projects/status/72staf3ivfet3699?svg=true)](https://ci.appveyor.com/project/oriches/faker-cs)

As with all my 'important' stuff it builds using the amazing [AppVeyor](https://ci.appveyor.com/project/oriches/faker-cs).

C# port of the Ruby Faker gem (http://faker.rubyforge.org/) and is used to easily generate fake data:

	addresses (UK, US),
	boolean,
	companies,
	countries,
	currencies,
	enums,
	finance (isin, ticker, coupon, maturity, bond name),
	internet (email, domain names, user names),
	names,
	lorem ipsum,
	phone numbers

Available as a NuGet package (https://nuget.org/packages/Faker.Net).

Get the code via git:

    git clone git://github.com/slashdotdash/faker-cs.git

Example code 
```csharp
var name = Faker.Name.FullName(); // Tod Yundt
var firstName = Faker.Name.First(); // Orlando
var lastName = Faker.Name.Last(); // Brekke
var address = Faker.Address.StreetAddress(); // 713 Pfeffer Bridge
var city = Faker.Address.City(); // Reynaton
var number = Faker.RandomNumber.Next(100); // 30
```

Supported versions:

	.NET framework 4.0,
	.NET framework 4.5,
	.NET framework 4.6,
	.NET framework 4.7,
	.NET framework 4.8,
	.NET Standard 2.0,
	.NET Standard 2.1,
	.NET Core 3.0,
	.NET Core 3.1

No longer supported in nuGet package (1.1 going forward):

	.NET framework 3.5 SP1,
	Silverlight 3.0,
	Silverlight 4.0,
	Silverlight 5.0,
	Windows Phone 7,
	Windows Phone 7.1
