using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using ATHRentalSystem.Controllers;
using ATHRentalSystem.Areas.Users.Controllers;
using ATHRentalSystem.Models;

namespace ATHRentalSystem.Tests
{
   // [TestClass]

    public class UnitTests
    {

        private IWebDriver driver;

       // [TestInitialize]
        public void PrzygotujPrzegladarke()
        {
            driver = new ChromeDriver();
        }

       // [TestMethod]
        public void TestRezerwacji()
        {
            driver.Navigate().GoToUrl("https://localhost:7272/users/Rezerwacje/create");

            Actions actions = new Actions(driver);
            actions
             .Click(driver.FindElement(By.Id("ImieRezerwanta")))
             .SendKeys("Michal" + Keys.Tab)
             .SendKeys("Cader" + Keys.Tab)
             .SendKeys("29062023" + Keys.Tab)
             .SendKeys("0000" + Keys.Tab);

            RezerwacjeViewModel rezerwacje = new();
            rezerwacje.ImieRezerwanta = driver.FindElement(By.Id("ImieRezerwanta")).GetAttribute("value");
            rezerwacje.NazwiskoRezerwanta = driver.FindElement(By.Id("NazwiskoRezerwanta")).GetAttribute("value");


            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("Michal", rezerwacje.ImieRezerwanta);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("Cader", rezerwacje.NazwiskoRezerwanta);


        }
        [TestCleanup]
        public void ZamknijPrzegladarke()
        {
            driver.Quit();
        }
    }
    
}

