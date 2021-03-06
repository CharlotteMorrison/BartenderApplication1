﻿using System.Collections.Generic;
using System.Linq;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BartenderApp.Domain.Abstract;
using BartenderApp.Domain.Entities;
using BartenderApp.WebUI.Controllers;
using System;
using System.Web.Mvc;
using BartenderApp.WebUI.Models;
using BartenderApp.WebUI.HtmlHelpers;

namespace BartenderApp.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        //[TestMethod]
        //public void Can_Paginate()
        //{
        //    Mock<IProductRepository> mock = new Mock<IProductRepository>();
        //    mock.Setup(m => m.Products).Returns(new Product[]
        //    {
        //        new Product {ProductID = 1, Name = "P1" },
        //        new Product {ProductID = 2, Name = "P2" },
        //        new Product {ProductID = 3, Name = "P3" },
        //        new Product {ProductID = 4, Name = "P4" },
        //        new Product {ProductID = 5, Name = "P5" }
        //    });

        //    ProductController controller = new ProductController(mock.Object);
        //    controller.PageSize = 3;

        //    ProductListViewModel result = (ProductListViewModel)controller.List(null,2).Model;

        //    Product[] prodArray = Products.ToArray();
        //    Assert.IsTrue(prodArray.Length == 2);
        //    Assert.AreEqual(prodArray[0].Name, "P4");
        //    Assert.AreEqual(prodArray[1].Name, "P5");
        //}
        
        [TestMethod]

        public void Can_Generate_Page_Links()
        {
            HtmlHelper myHelper = null;

            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage= 10
            };

            Func<int, string> pageUrlDelegate = i => "Page" + i;

            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            Assert.AreEqual(@"<a class=""btn btn-default""href=""Page1"">1</a>"
                    + @"<a class=""btn btn-default btn-primary selected""href=""Page2"">2</a>"
                    + @"<a class=""btn btn-default""href=""Page3"">3</a>",
                     result.ToString());
        }

        [TestMethod]

    public void Can_Send_Pagination_View_Model()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1" },
                new Product {ProductID = 2, Name = "P2" },
                new Product {ProductID = 3, Name = "P3" },
                new Product {ProductID = 4, Name = "P4" },
                new Product {ProductID = 5, Name = "P5" }
            });
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;
            ProductListViewModel result = (ProductListViewModel)controller.List(null,2).Model;

            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }
           
        [TestMethod]

        public void Can_Filter_Products()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1", Category ="Cat1" },
                new Product {ProductID = 2, Name = "P2", Category ="Cat2"  },
                new Product {ProductID = 3, Name = "P3", Category ="Cat1"  },
                new Product {ProductID = 4, Name = "P4", Category ="Cat2"  },
                new Product {ProductID = 5, Name = "P5", Category ="Cat3"  }
            });
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            Product[] result = ((ProductListViewModel)controller.List("Cat2", 1).Model)
                .Products.ToArray();

            Assert.AreEqual(result.Length, 2);
            Assert.IsTrue(result[0].Name == "P2" && result[0].Category == "Cat2");
            Assert.IsTrue(result[1].Name == "P4" && result[1].Category == "Cat2");
        }
    }

}
