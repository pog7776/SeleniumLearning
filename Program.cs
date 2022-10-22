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

using SeleniumDocs.GettingStarted;
using System.Collections.Generic;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace SeleniumLearning {
    class Program {
        static void Main(string[] args) {
            // FirstScriptTest test = new FirstScriptTest();
            // test.CreateDriver();
            // test.ChromeSession();
            //Test();
            PersonalSite();
        }

        protected static void Test() {
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
            //IWebElement textBox = driver.FindElement(By.Name("my-text"));
            IWebElement textBox = driver.FindElement(By.Id("my-text-id"));
            IWebElement submitButton = driver.FindElement(By.TagName("button"));

            // Type in the text box
            textBox.SendKeys("Selenium");

            // Click the submit button
            //submitButton.Click();

            // Close the browser
            //driver.Quit();
        }

        public static void PersonalSite() {
            new DriverManager().SetUpDriver(new ChromeConfig());
            ChromeDriver driver = new ChromeDriver();

            //driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl("https://jackcooper.dev");

            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            driver.FindElement(By.Id("projects")).Click();

            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            driver.FindElement(By.Id("quote")).Click();

            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            IWebElement quote = driver.FindElement(By.Id("quoteContainer"));

            WaitForStableText(quote);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Quote: " + quote.Text);
            Console.ResetColor();

            driver.Quit();
        }

        public static bool WaitForStableText(IWebElement element, int pollRate = 5, int ttl = 5000) {
            // Store previous text
            string temp = "";
            // Count attempts
            int time = 0;

            // INFO "..." is specifically for my site
            while(element.Text != temp || element.Text == "...") {
                // Update the temp text to the curreent text
                temp = element.Text;

                // Wait for time
                Thread.Sleep(1000/pollRate);
                time += 1000/pollRate;

                // Check if gone over ttl
                if(time >= ttl) {
                    Console.WriteLine("No change detected.");
                    return false;
                }
            }
            return true;
        }
    }
}
