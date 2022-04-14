using System;
using Alura.LeilaoOnline.Selenium.Fixtures;
using Alura.LeilaoOnline.Selenium.PageObjects;
using OpenQA.Selenium;
using Xunit;

namespace Alura.LeilaoOnline.Selenium.Tests
{
    [Collection("Chrome Driver")]
    public class AoEfetuarRegistro
    {
        private IWebDriver driver;

        public AoEfetuarRegistro(TestFixture fixture)
        {
            driver = fixture.Driver;
        }

        [Fact]
        public void DadoInfoValidasDeveIrParaPaginaDeAgradecimento()
        {
            // Arrange - dado chrome aberto, página inicial do sist. de leilões,
            // E informado dados válidos para registro
            driver.Navigate().GoToUrl("http://localhost:5000");

            // Nome
            var inputNome = driver.FindElement(By.Id("Nome"));

            // Email
            var inputEmail = driver.FindElement(By.Id("Email"));

            // Password
            var inputSenha = driver.FindElement(By.Id("Password"));

            // ConfirmPassword
            var inputConfirmaSenha = driver.FindElement(By.Id("ConfirmPassword"));

            // Botão de Registro
            var botaoRegistro = driver.FindElement(By.Id("btnRegistro"));

            inputNome.SendKeys("Raphilske Koi");
            inputEmail.SendKeys("koi@koi.com");
            inputSenha.SendKeys("123");
            inputConfirmaSenha.SendKeys("123");

            // Act - efetuo o registro
            botaoRegistro.Click();

            // Assert - devo ser direcionado para uma página de agradecimento
            Assert.Contains("Obrigado", driver.PageSource);
        }

        [Theory]
        [InlineData("", "koi@hotmail.com", "123", "123")]
        [InlineData("Raphilske Koi", "koi", "123", "123")]
        [InlineData("Raphilske Koi", "koi@hotmail.com", "123", "456")]
        [InlineData("", "koi@hotmail.com", "123", "")]
        public void DadoInfoInvalidasDevePermanecerNaHome(
            string nome,
            string email,
            string senha,
            string confirmaSenha)
        {
            // Arrange - dado chrome aberto, página inicial do sist. de leilões,
            // E informado dados válidos para registro
            driver.Navigate().GoToUrl("http://localhost:5000");

            // Nome
            var inputNome = driver.FindElement(By.Id("Nome"));

            // Email
            var inputEmail = driver.FindElement(By.Id("Email"));

            // Password
            var inputSenha = driver.FindElement(By.Id("Password"));

            // ConfirmPassword
            var inputConfirmaSenha = driver.FindElement(By.Id("ConfirmPassword"));

            // Botão de Registro
            var botaoRegistro = driver.FindElement(By.Id("btnRegistro"));

            inputNome.SendKeys(nome);
            inputEmail.SendKeys(email);
            inputSenha.SendKeys(senha);
            inputConfirmaSenha.SendKeys(confirmaSenha);

            // Act - efetuo o registro
            botaoRegistro.Click();

            // Assert - devo ser direcionado para uma página de agradecimento
            Assert.Contains("section-registro", driver.PageSource);
        }

        [Fact]
        public void DadoNomeEmBrancoDeveMostrarMensagemDeErro()
        {
            // Arrange
            driver.Navigate().GoToUrl("http://localhost:5000");

            // Botão de registro
            var botaoRegistro = driver.FindElement(By.Id("btnRegistro"));

            // Act
            botaoRegistro.Click();

            // Assert
            IWebElement elemento = driver.FindElement(By.CssSelector("span.msg-erro[data-valmsg-for=Nome]"));
            Assert.Equal("The Nome field is required.", elemento.Text);
        }

        [Fact]
        public void DadoEmailInvalidoDeveMostrarMensagemDeErro()
        {
            // Arrange
            var registroPO = new RegistroPO(driver);
            registroPO.Visitar();

            registroPO.PreencheFormulario(
                nome: "",
                email: "raphilske",
                senha: "",
                confirmSenha: ""
                );

            // Act
            registroPO.SubmeteFormulario();

            // Assert
            
            Assert.Equal("Please enter a valid email address.",registroPO.EmailMensagemErro);
        }
    }
}
