using Catalog.BLL.Interfaces;
using Catalog.Controllers;
using Catalog.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Catalog.Tests.Presentation_Layer.Controllers
{
    [TestFixture]
    public class SearchControllerTest
    {
        private Mock<IUnitOfWork> mock;
        private SearchController controller;

        [SetUp]
        public void Setup()
        {
            mock = new Mock<IUnitOfWork>();
            controller = new SearchController(mock.Object);
        }

        [Test]
        public void SearchResult_HappyPath()
        {
            //Arrange
            mock.Setup(db => db.Facilities.Find(It.IsAny<Func<Facility, bool>>()))
                .Returns(GetFacilities());

            //Act
            var result = controller.SearchResult("string") as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void SearchResult_NullArgument_ReturnPartial()
        {
            //Arrange

            //Act
            var result = controller.SearchResult(null) as PartialViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void SearchResult_NotFoundInDB_ReturnsPartial()
        {
            //Arrange
            mock.Setup(db => db.Facilities.Find(It.IsAny<Func<Facility, bool>>()))
                .Returns(GetEmptyFacilities());

            //Act
            var result = controller.SearchResult("string") as PartialViewResult;

            //Assert
            Assert.IsNotNull(result);

        }



        [TearDown]
        public void TearDown()
        {
            mock = null;
            controller = null;
        }

        private List<Facility> GetFacilities()
        {
            var facilities = new List<Facility>
            {
                new Facility
                    {
                        Id = 1,
                        Name = "Name 1",
                        Price = 3,
                        Rating = 3.2,
                        Phone = "012345678",
                        FacilityType = "Bar",
                        FacilityOwnerId = 2
                    },
                new Facility
                {
                    Id = 2,
                    Name = "Name 2",
                    Price = 3,
                    Rating = 3.2,
                    Phone = "012345678",
                    FacilityType = "Bar",
                    FacilityOwnerId = 2
                },
                new Facility
                {
                    Id = 3,
                    Name = "Name 1",
                    Price = 3,
                    Rating = 3.2,
                    Phone = "012345678",
                    FacilityType = "Bar",
                    FacilityOwnerId = 2
                }
            };

            return facilities;
        }

        private List<Facility> GetEmptyFacilities()
        {
            return new List<Facility>();
        }

    }
}
