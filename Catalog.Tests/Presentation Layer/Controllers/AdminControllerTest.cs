using Catalog.BLL.Interfaces;
using Catalog.DAL.Models;
using Catalog.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalog.Tests.Presentation_Layer.Controllers
{
    [TestFixture]
    public class AdminControllerTest
    {
        private Mock<IUnitOfWork> mock;
        private AdminController controller;

        [SetUp]
        public void Setup()
        {
            mock = new Mock<IUnitOfWork>();
            controller = new AdminController(mock.Object);
        }


        [Test]
        public void Index_HappyPath()
        {
            //Arrange   
            mock.Setup(db => db.Facilities.GetAll()).Returns(GetFacilities());

            //Act
            var result = controller.Index();

            //Assert
            var viewResult = result as ViewResult;
            var model = viewResult.Model as IEnumerable<Facility>;            

            Assert.AreEqual(GetFacilities().Count, model.Count());
        }

        [Test]
        public void Details_HappyPath()
        {
            //Arrange
            mock.Setup(db => db.Facilities.Get(It.IsAny<int>())).Returns(GetFacility());
            
            //Act
            var result = controller.Details(0);

            //Assert
            var viewResult = result as ViewResult;
            var model = viewResult.Model as Facility;

            Assert.AreEqual(GetFacility().GetType().GetProperties(),
                                    model.GetType().GetProperties());
        }

        [Test]
        public void Details_NullArgument_ReturnsNotFound()
        {
            //Arrange
            mock.Setup(db => db.Facilities.Get(It.IsAny<int>())).Returns(GetFacility());

            //Act
            var result = controller.Details(null);

            //Assert            
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void Details_RecordNotFoundInDB_ReturnsNotFound()
        {
            mock.Setup(db => db.Facilities.Get(It.IsAny<int>())).Returns(ReturnNullFacility());

            //Act
            var result = controller.Details(0);

            //Assert            
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void Create_HttpGet_HappyPath()
        {
            //Arrange      
            
            //Act
            var result = controller.Create();

            //Assert
            Assert.IsTrue(result is ViewResult);
        }

        [Test]
        public void Create_HttpPost_HappyPath()
        {
            //Arrange
            mock.Setup(db => db.Facilities.Create(GetFacility()));

            //Act
            var result = controller.Create(GetFacility());

            //Assert            
            Assert.IsInstanceOf<RedirectToActionResult>(result);
        }

        [Test]
        public void Create_NullArgument_ReturnsView()
        {
            //Arrange
            mock.Setup(db => db.Facilities.Create(GetFacility()));
            controller.ModelState.AddModelError("Model", "Model is invalid");

            //Act
            var result = controller.Create(ReturnNullFacility()) as ViewResult;
            var model = result.Model;

            //Assert            
            Assert.IsNotNull(result);
            Assert.AreEqual(model, ReturnNullFacility());
        }

        [Test]
        public void Create_SaveModel()
        {
            //Arrange
            mock.Setup(db => db.Facilities.Create(GetFacility())).Verifiable();

            //Act
            var result = controller.Create(GetFacility());

            //Assert                       
            mock.Verify(a => a.Save());
        }

        [Test]
        public void Edit_HttpGet_HappyPath()
        {
            //Arrange
            mock.Setup(db => db.Facilities.Get(It.IsAny<int>())).Returns(GetFacility());

            //Act
            var result = controller.Edit(0) as ViewResult;

            //Assert
            var model = result.Model as Facility;
            Assert.IsNotNull(result);
            Assert.AreEqual(model.Id, GetFacility().Id);
        }

        [Test]
        public void Edit_HttpGet_NullArgument_ReturnsNotFound()
        {
            //Arrange

            //Act
            var result = controller.Edit(null);

            //Assert            
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void Edit_HttpGet_RecordNotFoundInDB_ReturnsNotFound()
        {
            //Arrange
            mock.Setup(db => db.Facilities.Get(It.IsAny<int>())).Returns(ReturnNullFacility());         

            //Act
            var result = controller.Edit(0);

            //Assert            
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void Edit_HttpPost_HappyPath()
        {
            //Arrange            
            mock.Setup(db => db.Facilities.Update(It.IsAny<Facility>())).Verifiable();
            mock.Setup(db => db.Save()).Verifiable();

            //Act
            var result = controller.Edit(0, GetFacility()) as RedirectToActionResult;

            //Assert
            Assert.IsNotNull(result);
            mock.VerifyAll();

        }

        [Test]
        public void Edit_HttpPost_NotMatchingArguments_ReturnsNotFound()
        {
            //Arrange
            var mock = new Mock<IUnitOfWork>();
            var id = 5;
            var controller = new AdminController(mock.Object);

            //Act
            var result = controller.Edit(id, GetFacility());

            //Assert
            Assert.AreNotEqual(id, GetFacility().Id);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void Edit_HttpPost_NullArgument_ReturnsView()
        {
            //Arrange

            //Act
            var result = controller.Edit(0, ReturnNullFacility()) as NotFoundResult;

            //Assert
            Assert.IsNotNull(result);
        }

        //[Test]
        //public void Edit_HttpPost_DbUpdateConcurrencyException_NotFoundInDB()
        //{
        //    //Arrange
        //    mock.Setup(db => db.Facilities.Update(GetFacility())).Throws(new DbUpdateConcurrencyException())

        //    //Act

        //    //Assert
        //    Assert.Catch<NotImplementedException>( () => { controller.Edit(0, GetFacility()); });
        //}

        [Test]
        public void Delete_HttpGet_HappyPath()
        {
            //Arrange
            var mock = new Mock<IUnitOfWork>();
            var id = 0;
            mock.Setup(db => db.Facilities.Get(id)).Returns(GetFacility());           
            var controller = new AdminController(mock.Object);

            //Act
            var result = controller.Delete(id) as ViewResult;

            //Assert
            var model = result.Model as Facility;
            Assert.IsNotNull(model);
        }

        [Test]
        public void Delete_HttpGet_NullArgument_ReturnsNotFound()
        {
            //Arrange
            var mock = new Mock<IUnitOfWork>();
            var controller = new AdminController(mock.Object);

            //Act
            var result = controller.Delete(null);

            //Assert            
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void Delete_HttpGet_RecordNotFoundInDB_ReturnsNotFound()
        {
            //Arrange
            var mock = new Mock<IUnitOfWork>();
            var id = 0;
            mock.Setup(db => db.Facilities.Get(id)).Returns(ReturnNullFacility());
            var controller = new AdminController(mock.Object);

            //Act
            var result = controller.Delete(id);

            //Assert            
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void Delete_HttpPost_HappyPath()
        {
            //Arrange
            var mock = new Mock<IUnitOfWork>();
            var id = 0;
            mock.Setup(db => db.Facilities.Delete(id)).Verifiable();
            mock.Setup(db => db.Save()).Verifiable();
            var controller = new AdminController(mock.Object);

            //Act
            var result = controller.DeleteConfirmed(id);

            //Assert
            mock.VerifyAll();
            Assert.IsInstanceOf<RedirectToActionResult>(result);
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
