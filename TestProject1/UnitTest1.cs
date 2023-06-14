using ATHRentalSystem.Areas.Identity.Pages.Account;
using ATHRentalSystem.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Globalization;

namespace TestProject1
{
    [TestClass]

    public class UnitTests
    {
        IWebDriver driver = new ChromeDriver();

        [TestMethod]

        public void TestRezerwacji()
        {

            driver.Navigate().GoToUrl("https://localhost:7272/users/rezerwacje/create");

            RezerwacjeViewModel rezerwacje = new RezerwacjeViewModel();
            rezerwacje.ImieRezerwanta = "John";
            rezerwacje.NazwiskoRezerwanta = "Johnowski";

            rezerwacje.IdMiasta = 1;
            rezerwacje.IdRoweru = 11;

            // Wyszukaj elementy formularza i uzupe³nij pola
            IWebElement imieInput = driver.FindElement(By.Id("ImieRezerwanta"));
            imieInput.SendKeys(rezerwacje.ImieRezerwanta);

            IWebElement nazwiskoInput = driver.FindElement(By.Id("NazwiskoRezerwanta"));
            nazwiskoInput.SendKeys(rezerwacje.NazwiskoRezerwanta);

            IWebElement IdMiastaInput = driver.FindElement(By.Id("IdMiasta"));
            IdMiastaInput.SendKeys(rezerwacje.IdMiasta.ToString() + Keys.Tab);

            IWebElement IdRoweruInput = driver.FindElement(By.Id("IdRoweru"));
            IdRoweruInput.SendKeys(rezerwacje.IdRoweru.ToString() + Keys.Tab);

            DateTime currentDate = DateTime.Now;

            string dateTimeString = currentDate.ToString("dd-MM-yyyyyyHH:mm");

            driver.FindElement(By.Id("RezerwacjaOd")).SendKeys(dateTimeString);

            DateTime currentDate2 = DateTime.Now;


            string dateTimeString2 = currentDate.ToString("dd-MM-yyyyyyHH:mm");

            driver.FindElement(By.Id("RezerwacjaDo")).SendKeys(dateTimeString2);



            //Wykonaj akcjê wys³ania formularza
            IWebElement submitButton = driver.FindElement(By.Id("create"));
            submitButton.Click();



        }

        [TestCleanup]
        public void TearDown()
        {
            driver.Quit();
        }
    } 
}




//rezerwacje.RezerwacjaOd =DateTime.ParseExact("01-01-2022 00:00:00", "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
//rezerwacje.RezerwacjaDo =DateTime.ParseExact("08-01-2022 00:00:00", "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
//IWebElement dataOdInput = driver.FindElement(By.Id("RezerwacjaOd"));
//dataOdInput.SendKeys(rezerwacje.RezerwacjaOd + Keys.Tab);

//IWebElement dataDoInput = driver.FindElement(By.Id("RezerwacjaDo"));
//dataDoInput.SendKeys(rezerwacje.RezerwacjaDo + Keys.Tab);