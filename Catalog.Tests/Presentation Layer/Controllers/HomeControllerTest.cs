using Catalog.BLL.Interfaces;
using Catalog.Controllers;
using Catalog.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace Catalog.Tests.Presentation_Layer.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        private Mock<IUnitOfWork> mock;
        private HomeController controller;

        [SetUp]
        public void Setup()
        {
            mock = new Mock<IUnitOfWork>();
            controller = new HomeController(mock.Object);
        }


        [Test]
        public void Index_HappyPath()
        {
            //Arrange
            mock.Setup(db => db.Facilities.GetAll()).Returns(GetFacilities());

            //Act
            var result = controller.Index("price_desc");

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

        private Facility GetFacility()
        {
            return new Facility
            {
                Id = 0,
                Name = "Name 1",
                Price = 3,
                Rating = 3.2,
                Phone = "012345678",
                FacilityType = "Bar",
                FacilityOwnerId = 2
            };
        }

        private Facility ReturnNullFacility()
        {
            return null;
        }


    }
}
