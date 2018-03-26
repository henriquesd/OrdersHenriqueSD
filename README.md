# OrdersHenriqueSD
.NET Core WebApi using Entity Framework and Auth0 Authentication


## Non-authenticated service:

### To register a new user:
```
/api/signin (POST)

{
	"name": "inform_name",
	"Email": "email@email.com",
	"userName": "inform_userName",
	"password": "inform_password"
}
```
### For Login:
```
/api/signup (POST)

{
	"userName": "inform_userName",
	"password": "inform_password"
}
```

### Authorization
After Sign up, get the token string, and inform the Header Key: Authorization, Values:
```
Bearer Inform_your_Token
```

------------------------------------------------------------------------------------

## Authenticated service:

### To get users:
```
/api/users (GET)
```
  
You can filter the users by Name, UserName, E-mail or Creation Date.
#### Example of filter by name:
```
/api/users?name=inform_the_name
```

#### Example of filter by multiples filters:
```
/api/users?name=inform_the_name&username=inform_the_user_name
```

You can sort by any field, to ascending sorting you need to inform the field name, and for descending sorting, you need to inform the simbol "-" before the field name.

#### Example of ascending sort:
```
/api/users?sortby=Name
```

#### Example of descending sort:
```
/api/users?sortby=-Name
```


### To create a new product:
```
/api/product (POST)

Inform:
{
    "name": "Inform_Name",
    "description": "Inform_description",
    "price": 10
}
```

### To get products:
```
/api/products (GET)
```

You can filter the products by Name, Description, CreationDate.
Example of filter by name:
```
api/products?name=inform_the_product_name
```
Example of filter by multiples filters:
```
api/products?name=inform_the_product_name&description=inform_the_description
```

You can sort by any field, to ascending sorting you need to inform the field name;
	and for descending sorting, you need to inform the simbol "-" before the field name.
Example of ascending sort:
```
/api/products?sortby=Name
```
Example of descending sort:
```
/api/products?sortby=-Name
```


### To updated a product:
```
/api/product?Id=Inform_Id (PUT)

{
    "id": 3,
    "name": "Smartphone",
    "description": "Galaxy S8",
    "price": 10,
    "creationDate": "2018-01-22T17:42:10.7223334-03:00",
}
```

### To create a new Order:
```
/api/order (POST)

Inform:
{
  "userId": 1,
  "productOrderList": [
    {
      "productId": 1,
      "price": 10,
      "quantity": 5
    },
    {
      "productId": 2,
      "price": 50,
      "quantity": 2
    }
  ]
}
```

------------------------------------------------------------------------------------

## Docker

Docker is running in Linux.

### To run Sql:
```
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Henrique@151515' -p 1433:1433 -d microsoft/mssql-server-linux:latest
```

### Command to see images:
```
docker images
```

### Command to see containers:
```
docker ps
```
