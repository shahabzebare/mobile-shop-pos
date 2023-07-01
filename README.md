# Phone Store Management System (2018)

[![License](https://img.shields.io/badge/license-MIT-blue.svg)](https://opensource.org/licenses/MIT)

The Phone Store Management System is an open-source software developed in C# using the WPF framework. It provides a comprehensive solution for managing purchases, sales, expenses, returns, and generating reports for a phone store. The system is built on top of SQL Server for data storage and utilizes Entity Framework Core for database operations. The user interface is designed using Material UI Design, providing an intuitive and modern user experience.

> **Please note that** this project was created in 2018 and may not reflect the latest advancements in software development practices and technologies.

## Features

- **Purchase Management:** The system allows you to manage purchases of phones, with support for tracking the IMEI of every phone acquired. It provides features for adding new purchases, updating purchase details, and searching for specific purchases.

- **Sale Management:** Efficiently manage the sales process with features such as adding new sales, updating sales information, and searching for specific sales. The system ensures accurate inventory management by deducting sold phones from the available stock.

- **Expenses Management:** Easily track and manage expenses related to the phone store. You can record various types of expenses, such as rent, utilities, salaries, and more. The system provides features for adding new expenses, updating expense details, and generating expense reports.

- **Return Management:** Handle returns efficiently by recording return transactions. The system allows you to track returned phones, update return details, and process refunds or exchanges as required.

- **Reports:** Generate comprehensive reports to gain insights into the store's performance. The system provides various types of reports, including profit reports, sales reports, expense reports, and more.

## Requirements

To run the Phone Store Management System, you need the following:

- Windows operating system
- .NET Framework 4.8 or later
- SQL Server (Express edition or higher)
- Visual Studio (or any other compatible C# development environment)

## Installation

1. Clone the repository to your local machine:

```shell
git clone https://github.com/shahabzebare/mobile-shop-pos.git
```

2. Open the project in Visual Studio or your preferred development environment.

3. Configure the SQL Server connection string in the `App.config` file. Replace the placeholders with your SQL Server details.

```XML

  <connectionStrings>
    <add name="Model1" connectionString="Server=YOUR_SERVER_NAME;Database=YOUR_DATABASE_NAME;User=YOUR_USERNAME;Password=YOUR_PASSWORD;" providerName="System.Data.SqlClient" />
  </connectionStrings>
```

4. Build the solution to restore NuGet packages and compile the project.

5. Run the application and start managing your phone store!

## Contributing

Contributions to the Phone Store Management System are welcome. However, please note that this project was created in 2018 and may not be actively maintained. If you still wish to contribute, please follow these steps:

1. Fork the repository.

2. Create a new branch for your feature or bug fix.

3. Make your changes and ensure the code passes all tests.

4. Commit your changes and push them to your fork.

5. Submit a pull request to the main repository.


## License

The Phone Store Management System is open-source software licensed under the [MIT license](LICENSE).

## Acknowledgments

This project makes use of the following open-source libraries and frameworks:

- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/) - Object-relational mapper for .NET.
- [Material UI Design](https://material-ui.com/) - Modern and intuitive UI design framework.
- [SQL Server](https://www.microsoft.com/en-us/sql-server/) - Relational database management system.

## Contact

For any questions or inquiries, please contact [me@shahabzebari.net](mailto:me@shahabzebari.net).

---

Feel free to customize this README file according to your project's specific details, guidelines, and best practices.
