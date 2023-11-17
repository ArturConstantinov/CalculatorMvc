using CalculatorMvc.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorMvc.Test.Controller
{
    public class HomeControllerTest
    {
        private HomeController _homeController;
        public HomeControllerTest()
        {
            _homeController = new HomeController();
        }

        [Fact]
        public void HomeController_Index_ReturnSuccess()
        {
            // Arrange
            string res = "0";

            // Act
            var result = _homeController.Index(res);

            // Assert
            result.Should().BeOfType<JsonResult>();
            result.Should().BeAssignableTo<IActionResult>();
        }
    }
}
