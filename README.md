<h1>E-Commerce .NET Core Project</h1>

<p>This project contains the backend part of a personal development-oriented e-commerce web application built using .NET Core. The main goal of this project is to provide basic e-commerce functionality and enhance familiarity with .NET Core technologies.</p>

<h2>Technologies and Libraries Used</h2>

<ul>
    <li>C#.NET Core</li>
    <li>.NET Core</li>
    <li>FluentValidation, Autofac, Autofac.Extras.DynamicProxy</li>
    <li>Autofac</li>
    <li>Entity Framework Core</li>
    <li>Microsoft SQL Server</li>
    <li>JWT Authentication </li>
    <li>log4net</li>
</ul>

<h2>API Controllers</h2>

<h3>ProductsController</h3>
<p>This controller handles operations related to products.</p>

<h4>Endpoints:</h4>
<ul>
    <li><code>GET api/products/getall</code>: Retrieves a list of all products.</li>
    <li><code>GET api/products/getlistbycategory?categoryId={categoryId}</code>: Retrieves a list of products filtered by category.</li>
    <li><code>GET api/products/getbyid?productId={productId}</code>: Retrieves product details by product ID.</li>
    <li><code>POST api/products/add</code>: Adds a new product.</li>
    <li><code>POST api/products/update</code>: Updates an existing product.</li>
    <li><code>POST api/products/delete</code>: Deletes a product.</li>
    <li><code>POST api/products/transaction</code>: Performs a transactional operation on a product.</li>
</ul>

<h2>AuthController</h2>
<p>This controller manages user authentication and registration.</p>

<h3>Endpoints:</h3>
<ul>
    <li><code>POST api/auth/login</code>: Allows users to log in with their credentials.</li>
    <li><code>POST api/auth/register</code>: Registers new users.</li>
</ul>
<h2>How to Use</h2>
<ol>
    <li>Clone the repository to your local machine.</li>
    <li>Open the solution in Visual Studio or your preferred code editor.</li>
    <li>Make sure you have the necessary dependencies installed.</li>
    <li>Run the project.</li>
    <li>Use the provided endpoints to interact with the API.</li>
</ol>

<h3>Project Structure</h3>

<p>The project follows the principles of the Onion Architecture. The core layers are as follows:</p>

<ul>
    <li><strong>DataAccess</strong>: Contains code for database operations and Entity Framework Core usage.</li>
    <li><strong>Core</strong>: Includes main models, service interfaces, and helper classes.</li>
    <li><strong>Entities</strong>: Holds entity classes corresponding to database tables.</li>
    <li><strong>WebAPI</strong>: Contains HTTP endpoints, controllers, and JWT authentication setup.</li>
    <li><strong>Business</strong>: Houses business logic and service implementations.</li>
</ul>

<h2>Key Functions</h2>

<ul>
    <li>User registration and login</li>
    <li>Filtering and listing products</li>
    <li>Adding new products</li>
    <li>Authorization: Assigning roles like admin, manager to users</li>
</ul>
