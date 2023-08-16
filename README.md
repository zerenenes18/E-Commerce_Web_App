<h1>E-Commerce .NET Core Project</h1>

<p>This project contains the backend part of a personal development-oriented e-commerce web application built using .NET Core. The main goal of this project is to provide basic e-commerce functionality and enhance familiarity with .NET Core technologies.</p>

<h2>Technologies and Libraries Used</h2>

<ul>
    <li>C# and .NET Core</li>
    <li>FluentValidation, Autofac, Autofac.Extras.DynamicProxy</li>
    <li>Entity Framework Core and Microsoft SQL Server</li>
    <li>JWT Authentication (Microsoft.IdentityModel.Tokens)</li>
    <li>Logging (log4net, Microsoft.Extensions.Logging.Debug)</li>
    <li>MicroKnights.Log4NetAdoNetAppender</li>
    <li>Microsoft.AspNetCore.Authentication.JwtBearer</li>
</ul>

<h2>Running the Project</h2>

<p>You can interact with the project's API endpoints by making requests. The project is not actively hosted yet, but it will be made available soon.</p>

<h2>Project Structure</h2>

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

<h2>Security</h2>

<p>The project employs JWT token-based authentication for secure user login.</p>

<h2>License</h2>

<p>This project does not have a specified license. However, it is recommended to adhere to good faith practices and ensure your created code complies with licensing requirements.</p>
