using BusinessLogic.DtoModels;
using BusinessLogic.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineShop.Areas.Admin.Controllers;
using OnlineShop.Areas.Customer.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OnlineShop.Tests.AdminArea
{
    public class AdminAreaTests
    {
        [Fact]
        public void Products_GetFullProduct_NotNullCollection()
        {
            // Arrange
            var mock = new Mock<IProductManager>();
            var prodController = new ProductController(
                new Mock<IWebHostEnvironment>().Object, new Mock<IProductTypesManager>().Object,
                mock.Object, new Mock<ISpecialTagManager>().Object);
            mock.Setup(x => x.GetFullProducts()).Returns(GetTestProducts());

            // Act
            var result = prodController.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Dto_Product>>(viewResult.Model);
            model.Should().NotBeNull();
            model.Count().Should().Be(GetTestProducts().Count());
        }


        [Theory]
        [InlineData(0, 1000, true)]
        [InlineData(-10, 0, false)]
        [InlineData(7, 12, true)]
        public void Products_GetProductsBetweenPPrice_NotNullCollection(decimal? first, decimal? second, bool expected)
        {
            // Arrange
            var mock = new Mock<IProductManager>();
            var prodController = new ProductController(
                new Mock<IWebHostEnvironment>().Object, new Mock<IProductTypesManager>().Object,
                mock.Object, new Mock<ISpecialTagManager>().Object);
            mock.Setup(x => x.GetProductsBetweenPrice(first, second)).Returns(GetTestProducts());

            // Act
            var result = prodController.Index(first, second);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Dto_Product>>(viewResult.Model);
            model.Should().NotBeNull();
            model.Count().Should().Be(GetTestProducts().Count());
        }


        private IEnumerable<Dto_Product> GetTestProducts()
        {
            var products = new List<Dto_Product>()
            {
                new Dto_Product { Id = 1, Name = "name", Price = 14.234m, IsAvailable = true, Image = "image", ProductTypeId = 2, SpecialTagId = 2 },
                new Dto_Product { Id = 2, Name = "name", Price = 19.234m, IsAvailable = true, Image = "image", ProductTypeId = 5, SpecialTagId = 9 },
                new Dto_Product { Id = 3, Name = "name", Price = 2.1023m, IsAvailable = false, Image = "image", ProductTypeId = 3, SpecialTagId = 7 },
                new Dto_Product { Id = 4, Name = "name", Price = 4.1023m, IsAvailable = true, Image = "image", ProductTypeId = 1, SpecialTagId = 4 },
            };
            return products;
        }
    }
}
