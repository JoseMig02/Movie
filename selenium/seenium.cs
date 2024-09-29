using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
class Program
{
    static void Main(string[] args)
    {
        IWebDriver driver = new ChromeDriver();
        driver.Navigate().GoToUrl("http://localhost:9000/#/movie");
        try
        {
            Thread.Sleep(2000);
            var addMovieButton = driver.FindElement(By.Id("AddMovie"));
            addMovieButton.Click();

            driver.FindElement(By.Name("title")).SendKeys("Prison Break");

            var genres = driver.FindElement(By.Id("genres"));
            var genresElement = new SelectElement(genres);
            genresElement.SelectByValue("action");

            var clasification = driver.FindElement(By.Id("clasification"));
            var clasificationElement = new SelectElement(clasification);
            clasificationElement.SelectByValue("pg");


            var checkboxField = driver.FindElement(By.Id("inTheater"));
            checkboxField.Click();

            var dateInput = driver.FindElement(By.Name("dateRelease"));
            dateInput.Clear();
            dateInput.SendKeys("10/02/2024");
            driver.FindElement(By.Name("Image")).SendKeys("https://imagenes.20minutos.es/files/image_1920_1080/uploads/imagenes/2022/06/20/prison-break.jpeg");
            driver.FindElement(By.Name("synopsis")).SendKeys("http://example.com/imagen.jpg");
            driver.FindElement(By.Name("trailerUrl")).SendKeys("https://www.youtube.com/watch?v=AL9zLctDJaU");
            driver.FindElement(By.Name("audioPromotional")).SendKeys("https://www.youtube.com/watch?v=AL9zLctDJaU");

            var submitButton = driver.FindElement(By.XPath("//button[@type='submit']"));
            submitButton.Click();

        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
        finally
        {
            driver.Quit();
        }
    }
}
