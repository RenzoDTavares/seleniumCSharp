using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using RandomUserConsoleApp;
using Xunit;

namespace SeleniumProject
{
    public class Tests : DriverHelper
    {

        [SetUp]
        public void SetUp()
        {
            if (Driver == null)
            {
                var chromeDriverPath = "/home/ec2-user/";
                // Test
                // Tst actions yml
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("disable-infobars");
                options.AddArgument("--disable-extensions");
                options.AddArgument("--disable-gpu");
                options.AddArgument("--disable-dev-shm-usage");
                options.AddArgument("--no-sandbox");
                options.AddArgument("--headless"); 
                options.AddArgument("--user-data-dir=/tmp/chrome-profile");

                Driver = new ChromeDriver(chromeDriverPath, options);

                
                Driver.Navigate().GoToUrl("https://renzodtavares.github.io/simpleCRUD/main.html");

            }
        }

        [TearDown]
        public void TearDown()
        {
            //Driver.Quit();
        }

        /*[Test, Repeat(5), Order(1)]
       
        public void InserirItem()
        {

            // [Incluir novo funcionario]
            Driver.FindElement(By.XPath("//div[@class='container']//button[@data-target='#addEmployeeModal']")).Click();

            IWebElement nameInput = Driver.FindElement(By.Id("name"));
            IWebElement roleInput = Driver.FindElement(By.Id("role"));
            IWebElement salaryInput = Driver.FindElement(By.Id("salary"));
            IWebElement saveButton = Driver.FindElement(By.CssSelector("button[type='submit']"));

            var userData = Program.GetRandomUserDataFromApi();
                
            nameInput.SendKeys(userData.Name);
            roleInput.SendKeys(userData.City);

            Random random = new Random();
            int numeroAleatorio = random.Next(100, 20001); 
            salaryInput.SendKeys(numeroAleatorio.ToString());

            saveButton.Click();
            Thread.Sleep(500);

            string[] expectedValues = { userData.Name, userData.City, numeroAleatorio.ToString() };
            bool itemAdded = VerificarItemNaTabela(Driver, expectedValues);
            Assert.IsTrue(itemAdded, "O item foi adicionado com sucesso.");
        }

        [Test, Repeat(3), Order(2)]
        public void EditarItem()
        {
            IList<IWebElement> editButtons = Driver.FindElements(By.XPath("//table//button[@data-target='#editEmployeeModal']"));

            if (editButtons.Count > 0)
            {
                IList<IWebElement> rows = Driver.FindElements(By.XPath("//*[@id='employeeTable']/tr"));

                Random random = new Random();
                int randomIndex = random.Next(1, rows.Count);

                IWebElement editButton = rows[randomIndex].FindElement(By.CssSelector("button[data-target='#editEmployeeModal']"));

                editButton.Click();
                Thread.Sleep(500);

                IWebElement nameInput = Driver.FindElement(By.Id("editName"));
                string nameValue = nameInput.GetAttribute("value");

                var userData = Program.GetRandomUserDataFromApi();
                int numeroAleatorio = random.Next(100, 20001);

                EditarItem(userData.City, numeroAleatorio.ToString());

                IWebElement table = Driver.FindElement(By.ClassName("table"));
                string[] expectedValues = { nameValue, userData.City, numeroAleatorio.ToString() };
                bool itemAdded = VerificarItemNaTabela(Driver, expectedValues);

                Assert.IsTrue(itemAdded, "O item foi adicionado com sucesso.");
            }
            else
            {
                Assert.Fail("Nenhum botão 'Editar' encontrado.");
            }

            void EditarItem(string newRole, string newSalary)
            {
                IWebElement roleInput = Driver.FindElement(By.Id("editRole"));
                roleInput.Clear();
                roleInput.SendKeys(newRole);

                IWebElement salaryInput = Driver.FindElement(By.Id("editSalary"));
                salaryInput.Clear();
                salaryInput.SendKeys(newSalary);

                IWebElement saveButton = Driver.FindElement(By.CssSelector("#editEmployeeForm button[type='submit']"));
                saveButton.Click();
            }
        }
        */
        [Test, Repeat(2), Order(3)]
        public void ExcluirItemAleatorio()
        {
            // [Exclui um item aleatorio]
            var deleteButtons = Driver.FindElements(By.XPath("//button[contains(text(),'Delete')]"));

            if (deleteButtons.Count > 0)
            {
                Random random = new Random();
                int randomIndex = random.Next(deleteButtons.Count);
                IWebElement randomDeleteButton = deleteButtons[randomIndex];

                var row = randomDeleteButton.FindElement(By.XPath("./../../.."));
                var itemName = row.FindElement(By.XPath(".//td[1]")).Text;

                randomDeleteButton.Click();
                Thread.Sleep(1000);

                
                bool itemDeleted = Driver.PageSource.Contains(itemName);
                Assert.IsTrue(itemDeleted, "O item foi excluído com sucesso.");
            }
            else
            {
                Assert.Fail("Nenhum botão 'Delete' encontrado.");
            }
        }
        /*
        [Test]
        public void IncluirComValidacaoDeDuplicata()
        {
            //[Valida itens duplicados]
            var employeeNameElements = Driver.FindElements(By.XPath("//tbody[@id='employeeTable']/tr/td[1]"));

            if (employeeNameElements.Count > 0)
            {
                Random random = new Random();
                int randomIndex = random.Next(0, employeeNameElements.Count);
                var userName = employeeNameElements[randomIndex].Text;

                Driver.FindElement(By.XPath("//div[@class='container']//button[@data-target='#addEmployeeModal']")).Click();
                Thread.Sleep(500);

                var nameInput = Driver.FindElement(By.Id("name"));
                nameInput.Clear();
                nameInput.SendKeys(userName.ToLower());

                var userData = Program.GetRandomUserDataFromApi();
                int numeroAleatorio = random.Next(100, 20001);

                var roleInput = Driver.FindElement(By.Id("role"));
                roleInput.Clear();
                roleInput.SendKeys(userData.City);

                var salaryInput = Driver.FindElement(By.Id("salary"));
                salaryInput.Clear();
                salaryInput.SendKeys(numeroAleatorio.ToString());

                var saveButton = Driver.FindElement(By.CssSelector("#addEmployeeForm button[type='submit']"));
                saveButton.Click();

                var duplicatesMessage = Driver.FindElement(By.CssSelector(".duplicates-message"));
                //Assert.IsTrue(duplicatesMessage.Displayed, "A mensagem de erro não apareceu.");

                var employeeNames = Driver.FindElements(By.XPath("//tbody[@id='employeeTable']/tr/td[1]"));
                var duplicatedNameExists = employeeNames.Any(name => name.Text == userName);
                //Assert.IsFalse(duplicatedNameExists, "O nome duplicado ainda está na grade.");
            }
            else
            {
                Assert.Fail("Nenhum nome de funcionário encontrado na tabela.");
            }
        }

        static bool VerificarItemNaTabela(IWebDriver Driver, string[] expectedValues)
        {
            IList<IWebElement> rows = Driver.FindElements(By.XPath("//tbody[@id='employeeTable']/tr"));

            foreach (var row in rows)
            {
                var cells = row.FindElements(By.TagName("td"));

                if (cells.Count >= expectedValues.Length)
                {
                    bool valuesMatch = true;

                    for (int i = 0; i < expectedValues.Length; i++)
                    {
                        if (!cells[i].Text.Equals(expectedValues[i], StringComparison.OrdinalIgnoreCase))
                        {
                            valuesMatch = false;
                            break;
                        }
                    }

                    if (valuesMatch)
                    {
                        return true;
                    }
                }
            }
            Assert.Fail("Não foi possível encontrar verificar os itens na tabela");
            return false;
        }*/
    }
}
    


