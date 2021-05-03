using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using PieShop.Model;
namespace PieShop.Test
{
   public class Pies
    {
        [Fact]
        public void IsPiesInStock()
        {
            Product product = new Product();
            Assert.False(product.InStock);
        }
        [Fact]
        public void NameTest()
        {
            Product product = new Product();
            product.Name = "Cup Cake";
            Assert.Equal("Cup Cake", product.Name);
        }
        [Fact]
        public void NameRegxTest()
        {
            Product product = new Product();
            product.Name = "Cup Cake";
            Assert.Matches("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", product.Name);
        }
        [Fact]
        public void IsDescripNull()
        {
            Product product = new Product();
            Assert.Null(product.ShortDescription);
        }
    }
}
