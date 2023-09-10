using DataAccess.DbConfigureManagement;
using DataAccess.DbContext;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataAccess.Tests;

[TestClass]
public class DataAccessTests
{

    [TestMethod]
    public void ConfigurationTest()
    {
        var dbConfigurator = DbConfigurator.CreateDbConfiguratorWithAppData();
        Console.WriteLine($@"Строка подключения: ""{dbConfigurator.ProcessedConnectionString}""");
    }
    
    [TestMethod]
    public void AppDbContextTest()
    {
        var dbConfigurator = DbConfigurator.CreateDbConfiguratorWithAppData();
        using var dbContext = new AppDbContext(dbConfigurator.GetContextsOptions<AppDbContext>());
        
    }

}