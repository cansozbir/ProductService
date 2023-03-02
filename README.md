# Project Name
This is a .NET Web API developed using CQRS and Clean Architecture. The purpose of this API is to provide an API that users can perform basic CRUD operations. 

![CleanArchitecture](https://blog.cleancoder.com/uncle-bob/images/2012-08-13-the-clean-architecture/CleanArchitecture.jpg)

The application has been dockerized for ease of deployment. 
When you run the docker image, it will run a postgre server in the background, create databases & tables, and fill the tables with the sample data provided with the case. 


## Company Requirements 
The requirements for the project were:
- [x] Get sales history 
- [x] Add new sales history record
- [x] Delete sales history record
- [x] Update sales history record
- [x] Get profit for given store
- [x] Get the most profitable store
- [x] Get the best seller product by sales quantity
- [x] You can use table schemas specified in csv files. Please specify any changes you make - if any - in the database design. 
- [x] You should not use any ORM tool. All database operations should be done by queries.


## Technologies Used
- .NET 7
- Docker
- PostgreSQL

## Getting Started
### Prerequisites
- .NET 7 SDK
- Docker

## Installation
Clone the repository
```
git clone https://github.com/cansozbir/ProductService.git
```
Change to the project directory
```
cd ProductService
```

Run docker image
```
docker-compose up --build
```

If you want to avoid docker, you can use following commands to build & run the project, but in this case, please don't forget to create the database yourself.

Build the project
```
dotnet build
```
Run the project
```
dotnet ./src/ProductService.WebAPI/bin/Debug/net7.0/ProductService.WebAPI.dll
```

## Usage
When you run the application with docker. You can access to the swagger with the following url:
```
http://localhost:8088/swagger/index.html
```


## Endpoints
Most of the endpoints won't do anything with the specified Id. (eg. sending e request with id=0, storeName="Test" will only update the storeName, but not the id)

Every controller in this project has a documentation in it. You can visit them to see what each method does, which exceptions it throws, etc..
<details>
  <summary>If you want to see the list of endpoints:</summary>
  
  GET
/api/InventorySale <br>

POST
/api/InventorySale

GET
/api/InventorySale/{id}

PUT
/api/InventorySale/{id}

DELETE
/api/InventorySale/{id}

GET
/api/InventorySale/store/{storeId}
Product


GET
/api/Product

POST
/api/Product

GET
/api/Product/{id}

PUT
/api/Product/{id}

DELETE
/api/Product/{id}

GET
/api/Product/bestSellerByQuantity
Store


GET
/api/Store

POST
/api/Store

GET
/api/Store/{id}

PUT
/api/Store/{id}

DELETE
/api/Store/{id}

GET
/api/Store/{id}/profit

GET
/api/Store/mostProfitable
</details>


## Architecture
This project is based on the Clean Architecture principles proposed by Robert C. Martin (Uncle Bob). The architecture is divided into the following layers:

Domain: This layer contains the business logic of the application. <br>
Application: This layer contains the use cases of the application. <br>
Infrastructure: This layer contains the implementation details of the application (e.g. data access, external services).<br>
WebAPI: This layer exposes the application to the outside world through RESTful APIs. <br>

## Database Structure
Database names, column names, and the data was already provided in the case.<br>
I only modified `InventorySales` table, to have a Id column. Becase I wanted to have unique rows, so I can perform delete as well.

The database structure for this application is as follows:

### Stores
| Column Name | Data Type |
|-------------|-----------|
| Id          | int       |

### Products
|Column Name  |	Data Type |
|-------------|-----------|
| Id          | int       |
| ProductName | varchar   |
| Cost        | decimal   |
| SalesPrice  | decimal   |

### InventorySales
|Column Name    |	Data Type |
|---------------|-------------|
| Id            | int         |
| ProductId     | int         |
| StoreId       | int         |
| Date          | datetime    |
| SalesQuantity | int         |
| Stock         | int         |


## TODOs
- [ ] Deploy to web, and make it publicly accessible.
- [ ] Write unit tests & architectural tests.

## Contributing
1. Fork it (https://github.com/cansozbir/ProductService)
2. Create your feature branch (git checkout -b feature/fooBar)
3. Commit your changes (git commit -am 'Add some fooBar')
4. Push to the branch (git push origin feature/fooBar)
5. Create a new Pull Request 

## License
[MIT License](https://github.com/cansozbir/ProductService/blob/master/LICENSE)
