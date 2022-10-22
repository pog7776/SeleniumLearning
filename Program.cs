using System;
using System.Collections;
using System.Linq;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
// using OpenQA.Selenium.Edge;
// using OpenQA.Selenium.Firefox;
// using OpenQA.Selenium.IE;

using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace SeleniumLearning {
    class Program {
        static void Main(string[] args) {
            //IWebDriver driver = new ChromeDriver();

            new DriverManager().SetUpDriver(new ChromeConfig());
            // Create Chrome driver
            ChromeDriver driver = new ChromeDriver();
            // Navigate to a webpage
            driver.Navigate().GoToUrl("https://www.selenium.dev/selenium/web/web-form.html");
            // Get the page title
            string title = driver.Title;
            Console.WriteLine(title);
            // Wait for page to load
            // NOTE - This is not a good practice just waiting but used to demonstrate
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);
            // Find elements on the page
            IWebElement textBox = driver.FindElement(By.Name("my-text"));
            IWebElement submitButton = driver.FindElement(By.TagName("button"));
            // Type in the text box
            textBox.SendKeys("Selenium");
            // Click the submit button
            //submitButton.Click();
            // Close the browser
            //driver.Quit();
        }
    }
}
