<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/newburner/CoffeeShop">
    <img src="https://github.com/dotnet/brand/blob/main/logo/dotnet-logo.svg" alt="Logo" width="80" height="80">
  </a>

<h3 align="center">Webapi and Identity Server</h3>

  <p align="center">
    Basic Webapi and Identity Server with Dotnet 6
    <br />
  </p>
</div>



### Built With

* [Dotnet 6](https://docs.microsoft.com/en-us/dotnet/core/whats-new/dotnet-6)
* [Npgsql](https://www.npgsql.org/)
* [Identity Server](https://duendesoftware.com/products/identityserver)



## Getting Started

This project requires Dotnet 6 , you can install Dotnet 6 from [here](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)




1. Clone the repo
   ```sh
   git clone https://github.com/newburner/CoffeeShop.git
   ```
2. Open the project in any IDE

3. Run efcore migrations for both projects (ef cli / Package Manager Console)

    CoffeeShop
   ```sh
   Add-Migration initialmigration 
   Update-Database 

   ```
    IdentityServer

   ```sh
   Add-Migration initialmigration -Context PersistedGrantDbContext
   Update-Database -Context PersistedGrantDbContext

   Add-Migration initialmigration -Context ConfigurationDbContext
   Update-Database -Context ConfigurationDbContext

   Add-Migration initialmigration -Context IdentityContext
   Update-Database -Context IdentityContext

   ```
4. Add `/seed` argument in launch configuration for IdentityServer project

5. Run the project

