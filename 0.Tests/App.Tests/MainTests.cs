using System.Globalization;
using App.Main.Controllers;
using DataAccess.DbConfigureManagement;
using DataAccess.DbContext;
using DataAccess.Entities.Enums;
using Domain.Models;
using Infrastructure.AppComponents.AppExceptions;
using Infrastructure.AppComponents.AppExceptions.CommunicationExceptions;
using Infrastructure.AppComponents.AppExceptions.CompanyExceptions;
using Infrastructure.BaseComponents.Components.Exceptions;
using Infrastructure.BaseExtensions;
using Infrastructure.BaseExtensions.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Tests;

[TestClass]
public class MainTests
{
    [TestMethod]
    public async Task CompanyControllerTest()
    {
        var dbConfigurator = DbConfigurator.CreateDbConfiguratorWithAppData(true);
        var dbContext = new AppDbContext(dbConfigurator.GetContextsOptions<AppDbContext>());

        var mainModel = new CompanyModel(dbContext);
        var controller = new CompanyController(mainModel);

        // Список всех компаний
        var companies = await controller.GetAll();
        
        var httpResult = controller.Get(companies.ElementAtOrDefault(0)?.Id ?? Guid.Empty);     // первая компания из списка
        var httpResult2 = controller.Get(Guid.Empty);                                       // несуществующий id

        Console.WriteLine(httpResult.Result.ToString());
        Console.WriteLine(httpResult2.Result.ToString());
    }

    [TestMethod]
    public void ExceptionTest()
    {
        var exList = new List<Exception>();
        
        exList.Add(BaseException.CreateException<OverflowException>());
        exList.Add(BaseException.CreateException<OverflowException>("Check"));
        exList.Add(BaseException.CreateException<OverflowException>("Check", new Exception("InnerEx")));
        exList.Add(BaseException.CreateException<OverflowException>("Check", null, 
            "ru", "Проверка"));
        exList.Add(BaseException.CreateException<OverflowException>("Check", new Exception("InnerEx"), 
            "ru", "Проверка"));
        
        exList.Add(CompanyAlreadyExistsException.Create());

        foreach (var e in exList)
        {
            Console.WriteLine(e.Message);
        }
    }

    [TestMethod]
    public void AppExceptionsTest()
    {
        var exList = new List<AppException>();

        CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("ru-RU");
        exList.Add(CommunicationTypeException.Create(CommunicationTypeEnm.Phone));
        exList.Add(CommunicationTypeException.Create(CommunicationTypeEnm.Email));
        exList.Add(CommunicationTypeException.Create(CommunicationTypeEnm.All));
        
        CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
        exList.Add(CommunicationTypeException.Create(CommunicationTypeEnm.Phone));
        exList.Add(CommunicationTypeException.Create(CommunicationTypeEnm.Email));
        exList.Add(CommunicationTypeException.Create(CommunicationTypeEnm.All));
        
        foreach (var e in exList)
        {
            Console.WriteLine(e.Message);
        }
    }

    [TestMethod]
    public void TmpTest()
    {
        var camelCaseStr = "one TWO three".ToCamelCase();
        var lowerCamelCaseStr = "one TWO three".ToLowerCamelCase();
    }
}