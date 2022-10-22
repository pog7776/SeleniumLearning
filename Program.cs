using System;
using System.Collections;
using System.Linq;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

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

            DriverManager dm = new DriverManager();

            foreach(WebDriver driver in SetupDrivers(dm)) {
                driver.Manage().Window.Maximize();

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Testing: " + driver.GetType());
                Console.ResetColor();

                try {
                    PersonalSite(driver);
                }
                catch (Exception e){
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(driver.GetType() + " Failed");
                    Console.WriteLine("Error:" + e.Message);
                    Console.ResetColor();
                    driver.Quit();
                }
            }
        }

        [TestInitialize]
        public static List<WebDriver> SetupDrivers(DriverManager dm) {
            List<WebDriver> drivers = new List<WebDriver>();
            
            // Hide USB errors that aren't important
            // ?? dont work
            // ChromeOptions options = new ChromeOptions();
            // options.AddAdditionalChromeOption("excludeSwitches", new List<string> { "enable-logging" });
            dm.SetUpDriver(new ChromeConfig());
            drivers.Add(new ChromeDriver());

            dm.SetUpDriver(new FirefoxConfig());
            drivers.Add(new FirefoxDriver());
            
            dm.SetUpDriver(new EdgeConfig());
            drivers.Add(new EdgeDriver());

            drivers.ForEach(driver => {
                driver.Manage().Window.Minimize();
                Console.WriteLine("Created: " + driver.GetType());
            });

            return drivers;
        }
        
        [TestMethod]
        public static void PersonalSite(WebDriver driver) {
            driver.Navigate().GoToUrl("https://jackcooper.dev");

            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(20);
            driver.FindElement(By.Id("projects")).Click();

            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(20);
            driver.FindElement(By.Id("quote")).Click();

            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(20);
            IWebElement quote = driver.FindElement(By.Id("quoteContainer"));

            WaitForStableText(quote, 0.5f);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Quote: " + quote.Text);
            Console.ResetColor();

            driver.Quit();
        }

        /// <summary>
        /// Polls an elements text until it hasn't changed since last poll.
        /// </summary>
        /// <param name="element">The element to watch for changes.</param>
        /// <param name="pollRate">Times per second to check the element.</param>
        /// <param name="ttl">Time in ms until it determines the text is not stable.</param>
        /// <returns>true: When no change is detected since last poll. false: When ttl is exceeded.</returns>
        public static bool WaitForStableText(IWebElement element, float pollRate = 5, int ttl = 5000) {
            // Store previous text
            string previous = "";
            // Count attempts
            int time = 0;

            // While no changes
            while(element.Text != previous) {
                // Update the temp text to the curreent text
                previous = element.Text;

                // Wait for time
                Thread.Sleep(Convert.ToInt32(1000/pollRate));
                time += Convert.ToInt32(1000/pollRate);

                // Check if gone over ttl
                if(time >= ttl) {
                    return false;
                }
            }
            return true;
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
    }
}
