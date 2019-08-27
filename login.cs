using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;

namespace TestProject1
{
    [TestFixture]
    public class NewseLetter
    {
        private IWebDriver _driver;
        string _base_url = TestContext.Parameters["BaseUrl"].ToString();
        string _tag_list1 = TestContext.Parameters["tag_list1"].ToString();
        
        
        //private readonly string _tag_list1 = TestContext.Parameters.Get("tag_list1");
        //private string _base_url = TestContext.Parameters.Get("base_url");

        //string base_url = TestContext.Parameters["base_url"];


        //private const string base_url = "https://athletictrainers.inloop.com";
        //private const string tag_list4 = "//ul[@class='tag-list clearfix ng-scope']//a[@class='ng-binding'][contains(text(),'NATA')]";
        //private const string tag_list3 = "//ul[@class='tag-list clearfix ng-scope']//a[@class='ng-binding'][contains(text(),'Injuries')]";
        //private const string tag_list2 = "//ul[@class='tag-list clearfix ng-scope']//a[@class='ng-binding'][contains(text(),'Head Athletic Trainers')]";
        //private const string tag_list1 = "//li[@class='ng-scope']//a[@class='ng-binding'][contains(text(),'Concussions')]";
        //private const string tag_list5 = "//ul[@class='tag-list clearfix ng-scope']//a[@class='ng-binding'][contains(text(),'Pain Relief')]";
        //private const string tag_list6 = "//a[contains(text(),'Range Of Motion')]";
        //private const string tag_list7 = "//a[contains(text(),'Rehabilitation')]";
        //private const string tag_list8 = "//a[contains(text(),'Strength Training')]";

        public static object IWebElement { get; private set; }

        
        private static void Main(string[] args)
        {
            var newseLetter = new NewseLetter();
            newseLetter._base_url = ConfigurationManager.AppSettings["base_url"];
            IWebDriver driver = newseLetter.SetUp();
            newseLetter.scroll_to(driver);
            newseLetter.coockie_false(driver);
            newseLetter.newsletter_picker(driver);
            //ReCaptcha(driver);
            newseLetter.tab_list_clicker(driver);
            newseLetter.finalizer(driver);
        }

        [Test]
        private void finalizer(IWebDriver driver)
        {
            Thread.Sleep(5000);
            driver.SwitchTo().Window(driver.WindowHandles[0]);
            Assert.IsTrue(driver.PageSource.Contains(_base_url));
            Thread.Sleep(10000);


            driver.Quit();
        }

        [Test]
        private void ReCaptcha()
        {
            _driver.FindElement(By.XPath("//input[@placeholder='Your Email']")).SendKeys("aa@aa.aa");
            IWebElement frame = _driver.FindElements(By.XPath("//iframe[contains(@src, 'recaptcha')]"))[0];
            _driver.SwitchTo().Frame(frame);
            IWebElement checkbox = _driver.FindElement(By.CssSelector("div.recaptcha-checkbox-border"));
            checkbox.Click();
            Thread.Sleep(5000);
        }

        [SetUp]
        private IWebDriver SetUp()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

            _driver.Manage().Window.Maximize();
            Thread.Sleep(10000);
            return _driver;
        }

        private void scroll_to(IWebDriver driver)
        {
            var element = driver.FindElement(By.CssSelector("#newsletters-archive article a"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(element);
            actions.Perform();
        }

        private void coockie_false(IWebDriver driver)
        {
            driver.FindElement(By.XPath("//a[@class='cc-btn cc-dismiss']")).Click();
            Thread.Sleep(1000);
        }

        private void newsletter_picker(IWebDriver driver)
        {
            List<IWebElement> news = driver.FindElements(By.CssSelector("#newsletters-archive article a")).ToList();
            for (int i = 0; i < news.Count; i++)
            {
                news[i].Click();
                Assert.IsTrue(driver.PageSource.Contains("api/feedlandingapi/getnewslettertemplatebody?id="));
                driver.SwitchTo().Window(driver.WindowHandles[1]);
                driver.Close();
                driver.SwitchTo().Window(driver.WindowHandles[0]);
                Thread.Sleep(2000);
            }

            driver.SwitchTo().Window(driver.WindowHandles[0]);
            driver.Navigate().GoToUrl(_base_url);
            Thread.Sleep(5000);
        }

        private void tab_list_clicker(IWebDriver driver)
        {
            driver.FindElement(By.XPath(_tag_list1)).Click();
            Assert.IsTrue(driver.PageSource.Contains("entity/7/"));
            driver.Navigate().Back();
            Thread.Sleep(2000);
            //driver.FindElement(By.XPath(tag_list2)).Click();
            //Assert.IsTrue(driver.PageSource.Contains("entity/17/"));
            //driver.Navigate().Back();
            //driver.FindElement(By.XPath(tag_list3)).Click();
            //Assert.IsTrue(driver.PageSource.Contains("entity/3/"));
            //driver.Navigate().Back();
            //driver.FindElement(By.XPath(tag_list4)).Click();
            //Assert.IsTrue(driver.PageSource.Contains("entity/152/"));
            //driver.Navigate().Back();
            //driver.FindElement(By.XPath(tag_list5)).Click();
            //Assert.IsTrue(driver.PageSource.Contains("entity/368/5"));
            //driver.Navigate().Back();
            //driver.FindElement(By.XPath(tag_list6)).Click();
            //Assert.IsTrue(driver.PageSource.Contains("entity/16/5"));
            //driver.Navigate().Back();
            //driver.FindElement(By.XPath(tag_list7)).Click();
            //Assert.IsTrue(driver.PageSource.Contains("entity/52/5"));
            //driver.Navigate().Back();
            //driver.FindElement(By.XPath(tag_list8)).Click();
            //Assert.IsTrue(driver.PageSource.Contains("entity/126/5"));
            //driver.Navigate().Back();
        }
    }
}
