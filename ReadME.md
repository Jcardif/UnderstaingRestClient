# Understanding Rest Client In C#
A simple console application demonstrating how to build a Rest Client in C#
#### HTTP Client
<p>HttpClient is a modern HTTP client for .NET. It provides a flexible and extensible API for accessing all things exposed through HTTP. 

#### Setting the Web-API(Rest Service)
Download the [Database File](https://github.com/Jcardif/UnderstaingRestClient/blob/master/Database/productdb.sql) to create your MySQL database.

Create an Empty WebApi Project and include it in your current solution
Next Create a class named Product.cs  under the model folder and define the class as follows:
```CSharp
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }
    }
```   
Next Create a class to handle the controller's operations and subsequently create a controller.  

Click [here](https://github.com/Jcardif/RestService) for more help on how create a rest service

#### Dependencies
Install the following nuget Packages in your .NET console application.
<li>System.Runtime.Serialization.Json
<li>Microsoft.AspNet.WebApi.Client
<li>NewtonSoft.Json

Add a reference to your rest service project

#### Building your Rest Client
Initialize your Rest Client as follows
```CSharp
 private static readonly HttpClient Client=new HttpClient();
```


