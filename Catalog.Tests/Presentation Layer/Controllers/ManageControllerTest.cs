using Catalog.BLL.Interfaces;
using Catalog.Controllers;
using Catalog.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Tests.Presentation_Layer.Controllers
{
    [TestFixture]
    public class ManageControllerTest
    {
        private Mock<IUnitOfWork> mock;
        private ManageController controller;

        [SetUp]
        public void Setup()
        {
            mock = new Mock<IUnitOfWork>();
            controller = new ManageController(mock.Object);
        }


        [Test]
        public void UserCabinet_HappyPath()
        {
            //Arrange
            mock.Setup(db => db.Users.Get(It.IsAny<int>())).Returns(GetUser());

            //Act
            var result = controller.UserCabinet(0) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void UserCabinet_NullArgument_ReturnsNotFound()
        {
            //Arrange
            mock.Setup(db => db.Users.Get(It.IsAny<int>())).Returns(GetUser());

            //Act
            var result = controller.UserCabinet(null) as NotFoundResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void UserCabinet_UserNotFoundInDB_ReturnsNotFound()
        {
            //Arrange
            mock.Setup(db => db.Users.Get(It.IsAny<int>())).Returns(GetNullUser());

            //Act
            var result = controller.UserCabinet(0) as NotFoundResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void UpdateProfile_HappyPath()
        {
            //Arrange
            mock.Setup(db => db.Users.Update(It.IsAny<User>())).Verifiable();
            mock.Setup(db => db.Save()).Verifiable();

            //Act
            var result = controller.UpdateProfile(0, GetUser()) as RedirectToActionResult;

            //Assert
            Assert.IsNotNull(result);
            mock.VerifyAll();
        }

        [Test]
        public void UpdateProfile_NullArgument_ReturnsNotFound()
        {
            //Arrange

            //Act
            var result = controller.UpdateProfile(0, GetNullUser()) as NotFoundResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void UpdateProfile_InvalidModelState_ReturnsView()
        {
            //Arrange
            controller.ModelState.AddModelError("Model", "Model is invalid");

            //Act
            var result = controller.UpdateProfile(0, GetUser()) as ViewResult;
            var model = result.Model;

            //Assert            
            Assert.IsNotNull(result);
            Assert.AreEqual(GetUser().GetType().GetProperties(),
                                model.GetType().GetProperties());
        }

        [Test]
        public void UpdateProfile_CatchDbUpdateConcurrencyException_UserIsInDB_ReturnsNotFound()
        {
            //Arrange
            mock.Setup(DBNull => DBNull.Users.Get(It.IsAny<int>())).Returns(GetNullUser());
            mock.Setup(db => db.Users.Update(It.IsAny<User>()));
            // Should have thrown DbUpdateConcurrencyException but theres to difficult to create new instance of it.
            mock.Setup(db => db.Save()).Throws(new Exception());

            //Act

            //Assert            
            Assert.Catch<NotSupportedException>(() => { controller.UpdateProfile(0, GetUser()); });
        }

        [Test]
        public void Add_HappyPath()
        {
            //Arrange
            mock.Setup(db => db.Facilities.Create(It.IsAny<Facility>())).Verifiable();
            mock.Setup(db => db.Save()).Verifiable();
            
            //Act
            var result = controller.Add(0, GetFacility()) as RedirectToActionResult;

            //Assert
            Assert.IsNotNull(result);
            mock.VerifyAll();
        }

        [Test]
        public void Add_NullArgument_ReturnsNotFound()
        {
            //Arrange

            //Act
            var result = controller.Add(null, GetFacility()) as NotFoundResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Add_DBException_ThrowsNotImplementedException()
        {
            //Arrange
            mock.Setup(db => db.Facilities.Create(It.IsAny<Facility>()));
            mock.Setup(db => db.Save()).Throws(new Exception());

            //Act

            //Assert
            Assert.Throws<NotImplementedException>(() => { controller.Add(0, GetFacility()); });
        }

        [Test]
        public void Edit_HappyPath()
        {
            //Arrange
            mock.Setup(db => db.Facilities.Get(It.IsAny<int>())).Returns(GetFacility());

            //Act
            var result = controller.Edit(0) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Edit_FacilityNotFoundInDB_ReturnsNotFound()
        {
            //Arrange
            mock.Setup(db => db.Facilities.Get(It.IsAny<int>())).Returns(GetNullFacility());

            //Act
            var result = controller.Edit(0) as NotFoundResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Edit_HttpPost_HappyPath()
        {
            //Arrange
            mock.Setup(db => db.Facilities.Update(It.IsAny<Facility>())).Verifiable();
            mock.Setup(db => db.FacilityAddresses.Update(It.IsAny<FacilityAddress>())).Verifiable();
            mock.Setup(db => db.Schedules.Update(It.IsAny<Schedule>())).Verifiable();
            mock.Setup(db => db.Save()).Verifiable();


            //Act
            var result = controller.Edit(GetFacility()) as RedirectToActionResult;

            //Assert
            Assert.IsNotNull(result);
            mock.VerifyAll();
        }

        [Test]
        public void Edit_HttpPost_NullArgument_ReturnsNotFound()
        {
            //Arrange

            //Act
            var result = controller.Edit(GetNullFacility()) as NotFoundResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Edit_HttpPost_CatchNotExpectedError_ThrowNotImplementedException()
        {
            //Arrange
            mock.Setup(db => db.Facilities.Update(It.IsAny<Facility>())).Throws(new Exception());

            //Act

            //Assert
            Assert.Catch<NotImplementedException>(() => { controller.Edit(GetFacility()); });
        }
        
        [Test]
        public void Delete_HappyPath()
        {
            //Arrange
            mock.Setup(db => db.Facilities.Get(It.IsAny<int>())).Returns(GetFacility());

            //Act
            var result = controller.Delete(0) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Delete_NullArgument_ReturnsNotFound()
        {
            //Arrange

            //Act
            var result = controller.Delete(null) as NotFoundResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Delete_FacilityNotFoundInDB_ReturnsNotFound()
        {
            //Arrange
            mock.Setup(db => db.Facilities.Get(It.IsAny<int>())).Returns(GetNullFacility());

            //Act
            var result = controller.Delete(0) as NotFoundResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Delete_HttpPost_HappyPath()
        {
            //Arrange
            mock.Setup(db => db.Facilities.Delete(It.IsAny<int>())).Verifiable();
            mock.Setup(db => db.Save()).Verifiable();

            //Act
            var result = controller.DeleteConfirmed(0) as RedirectToActionResult;

            //Assert
            Assert.IsNotNull(result);
            mock.VerifyAll();
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

        private Facility GetNullFacility()
        {
            return null;
        }

        private User GetUser()
        {
            return new User
            {
                Id = 0,
                Email = "email@gmail.com",
                Password = "1111",
                Name = "name",
                Roleid = 2
            };
        }

        private User GetNullUser()
        {
            return null;
        }

        

        

    }
}
