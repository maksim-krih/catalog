using Catalog.BLL.Interfaces;
using Catalog.BLL.ViewModels;
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
            mock.Setup(db => db.Facilities.GetAll()).Returns(GetFacilities()).Verifiable();
            mock.Setup(db => db.Users.GetAll()).Returns(GetUsers()).Verifiable();
            mock.Setup(db => db.Roles.GetAll()).Returns(GetRoles()).Verifiable();

            //Act
            var result = controller.Index() as ViewResult;

            //Assert
            var viewResult = result as ViewResult;
            var model = viewResult.Model as FacilitiesUsersRoles;

            var expected = new FacilitiesUsersRoles
            {
                Facilities = GetFacilities(),
                Users = GetUsers(),
                Roles = GetRoles()
            };

            Assert.AreEqual(expected.GetType().GetProperties(), 
                                      model.GetType().GetProperties());
            mock.VerifyAll();
        }

        #region Facility CRUD
        [Test]
        public void CreateFacility_HappyPath()
        {
            //Arrange      
            mock.Setup(db => db.Facilities.Create(It.IsAny<Facility>())).Verifiable();
            mock.Setup(db => db.Save()).Verifiable();
            //Act
            var result = controller.CreateFacility(GetFacility()) as RedirectToActionResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void CreateFacility_NullArgument_ThrowsNotImplementedException()
        {
            //Arrange      

            //Act

            //Assert
            Assert.Throws<NotImplementedException>(() => controller.CreateFacility(ReturnNullFacility()));
        }
                
        [Test]
        public void EditFacility_HttpGet_HappyPath()
        {
            //Arrange
            mock.Setup(db => db.Facilities.Get(It.IsAny<int>())).Returns(GetFacility());

            //Act
            var result = controller.EditFacility(0) as ViewResult;

            //Assert
            var model = result.Model as Facility;
            Assert.IsNotNull(result);
            Assert.AreEqual(GetFacility().GetType().GetProperties(), 
                                    model.GetType().GetProperties());
        }

        [Test]
        public void EditFacility_HttpGet_NullArgument_ReturnsNotFound()
        {
            //Arrange

            //Act
            int? i = null;
            var result = controller.EditFacility(i);

            //Assert            
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void EditFacility_HttpGet_RecordNotFoundInDB_ReturnsNotFound()
        {
            //Arrange
            mock.Setup(db => db.Facilities.Get(It.IsAny<int>())).Returns(ReturnNullFacility());         

            //Act
            var result = controller.EditFacility(0);

            //Assert            
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
               
        [Test]
        public void EditFacility_HttpPost_HappyPath()
        {
            //Arrange            
            mock.Setup(db => db.Facilities.Update(It.IsAny<Facility>())).Verifiable();
            mock.Setup(db => db.FacilityAddresses.Update(It.IsAny<FacilityAddress>())).Verifiable();
            mock.Setup(db => db.Schedules.Update(It.IsAny<Schedule>())).Verifiable();
            mock.Setup(db => db.Save()).Verifiable();

            //Act
            var result = controller.EditFacility(GetFacility()) as RedirectToActionResult;

            //Assert
            Assert.IsNotNull(result);
            mock.VerifyAll();
        }

        [Test]
        public void Edit_HttpPost_NullArgument_ThrowsNotImplementedException()
        {
            //Arrange

            //Act

            //Assert
            Assert.Throws<NotImplementedException>(() => controller.EditFacility(ReturnNullFacility()));
        }

        [Test]
        public void Edit_HttpPost_NotExpectedException_ThrowsNotImplementedException()
        {
            //Arrange
            mock.Setup(db => db.Facilities.Update(It.IsAny<Facility>())).Throws(new Exception());
            //Act

            //Assert
            Assert.Throws<NotImplementedException>(() => controller.EditFacility(GetFacility()));
        }
               
        [Test]
        public void DeleteFacility_HttpGet_HappyPath()
        {
            //Arrange
            mock.Setup(db => db.Facilities.Get(It.IsAny<int>())).Returns(GetFacility());

            //Act
            var result = controller.DeleteFacility(0) as ViewResult;

            //Assert
            var model = result.Model as Facility;
            Assert.IsNotNull(result);
            Assert.AreEqual(GetFacility().GetType().GetProperties(),
                                    model.GetType().GetProperties());
        }

        [Test]
        public void DeleteFacility_HttpGet_NullArgument_ReturnsNotFound()
        {
            //Arrange

            //Act
            var result = controller.DeleteFacility(null);

            //Assert            
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void DeleteFacility_HttpGet_RecordNotFoundInDB_ReturnsNotFound()
        {
            //Arrange
            mock.Setup(db => db.Facilities.Get(It.IsAny<int>())).Returns(ReturnNullFacility());

            //Act
            var result = controller.DeleteFacility(0);

            //Assert            
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void Delete_HttpPost_HappyPath()
        {
            //Arrange
            mock.Setup(db => db.Facilities.Delete(It.IsAny<int>())).Verifiable();
            mock.Setup(db => db.Save()).Verifiable();

            //Act
            var result = controller.DeleteConfirmedFacility(0);

            //Assert
            mock.VerifyAll();
            Assert.IsInstanceOf<RedirectToActionResult>(result);
        }
        #endregion

        #region User CRUD

        [Test]
        public void CreateUser_HappyPath()
        {
            //Arrange
            mock.Setup(db => db.Users.Create(It.IsAny<User>())).Verifiable();
            mock.Setup(db => db.Save()).Verifiable();

            //Act
            var result = controller.CreateUser(GetUser()) as RedirectToActionResult;

            //Assert
            Assert.IsNotNull(result);
            mock.VerifyAll();

        }

        [Test]
        public void CreateUser_NullArgument_ReturnsNotFound()
        {
            //Arrange

            //Act
            var result = controller.CreateUser(GetNullUser()) as NotFoundResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void CreateUser_InvalidModelState_ReturnsView()
        {
            //Arrange
            mock.Setup(db => db.Roles.GetAll()).Returns(GetRoles());
            controller.ModelState.AddModelError("Model", "Model is invalid");

            //Act
            var result = controller.CreateUser(GetUser()) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void EditUser_HappyPath()
        {
            //Arrange
            mock.Setup(db => db.Users.Get(It.IsAny<int>())).Returns(GetUser());
            mock.Setup(db => db.Roles.GetAll()).Returns(GetRoles());

            //Act
            var result = controller.EditUser(0) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void EditUser_NullArgument_ReturnsNotFound()
        {
            //Arrange

            //Act
            var result = controller.EditUser(null) as NotFoundResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void EditUser_RecordNotFoundInDB_ReturnNotFound()
        {
            //Arrange
            mock.Setup(db => db.Users.Get(It.IsAny<int>())).Returns(GetNullUser());

            //Act
            var result = controller.EditUser(0) as NotFoundResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void EditUser_HttpPost_HappyPath()
        {
            //Arrange
            mock.Setup(db => db.Users.Update(It.IsAny<User>())).Verifiable();
            mock.Setup(db => db.Save()).Verifiable();

            //Act
            var result = controller.EditUser(0, GetUser()) as RedirectToActionResult;

            //Assert
            Assert.IsNotNull(result);
            mock.VerifyAll();
        }

        [Test]
        public void EditUser_HttpPost_NullArgument_ReturnsNotFound()
        {
            //Arrange

            //Act
            var result = controller.EditUser(0, GetNullUser()) as NotFoundResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void EditUser_HttpPost_InvalidModelState_ReturnsView()
        {
            //Arrange
            mock.Setup(db => db.Roles.GetAll()).Returns(GetRoles());
            controller.ModelState.AddModelError("Model", "Model is invalid");

            //Act
            var result = controller.EditUser(0, GetUser()) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void DeleteUser_HappyPath()
        {
            //Arrange
            mock.Setup(db => db.Users.Get(It.IsAny<int>())).Returns(GetUser());

            //Act
            var result = controller.DeleteUser(0) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void DeleteUser_NullArgument_ReturnsNotFound()
        {
            //Arrange

            //Act
            var result = controller.DeleteUser(null) as NotFoundResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void DeleteUser_UserNotFoundInDB_ReturnsNotFound()
        {
            //Arrange
            mock.Setup(db => db.Users.Get(It.IsAny<int>())).Returns(GetNullUser());

            //Act
            var result = controller.DeleteUser(0) as NotFoundResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void DeleteUserConfirmed_HappyPath()
        {
            //Arrange
            mock.Setup(db => db.Users.Delete(It.IsAny<int>())).Verifiable();
            mock.Setup(db => db.Save()).Verifiable();

            //Act
            var result = controller.DeleteConfirmedUser(0) as RedirectToActionResult;

            //Assert
            Assert.IsNotNull(result);
            mock.VerifyAll();
        }



        #endregion








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

        private User GetUser()
        {
            return new User
            {
                Id = 0,
                Email = "",
                Name = "",
                Password = "",
                Roleid = 2
            };
        }

        private User GetNullUser()
        {
            return null;
        }

        private List<User> GetUsers()
        {
            return new List<User>
            {
                new User
                    {
                        Email = "",
                        Name = "",
                        Password = "",
                        Roleid = 2

                    },
                new User
                    {
                        Email = "",
                        Name = "",
                        Password = "",
                        Roleid = 2

                    }
            };
        }

        private List<Role> GetRoles()
        {
            return new List<Role>
            {
                new Role
                    {
                        Name = ""
                    },
                new Role
                    {
                        Name = ""
                    }
            };
        }

    }

}
