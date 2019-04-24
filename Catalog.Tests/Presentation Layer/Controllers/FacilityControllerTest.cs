using Catalog.BLL.Interfaces;
using Catalog.BLL.ViewModels.DTO;
using Catalog.Controllers;
using Catalog.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Tests.Presentation_Layer.Controllers
{
    [TestFixture]
    public class FacilityControllerTest
    {
        [Test]
        public void Place_HappyPath()
        {
            //Assign
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(db => db.Facilities.Get(It.IsAny<int>())).Returns(GetFacility());
            var controller = new FacilityController(mock.Object);

            //Act
            var result = controller.Place(0) as ViewResult;


            //Assert
            Assert.IsNotNull(result);
            var model = result.Model as Facility;
            Assert.AreEqual(GetFacility().Id, model.Id);

        }

        [Test]
        public void Place_RecordNotFoundInDB_ReturnsNotFound()
        {
            //Assign
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(db => db.Facilities.Get(It.IsAny<int>())).Returns(GetNullFacility());
            var controller = new FacilityController(mock.Object);

            //Act
            var result = controller.Place(0);

            //Assert           
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void AddFeedback_HappyPath()
        {
            //Assign
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(db => db.Facilities.Get(It.IsAny<int>())).Returns(GetFacility());
            mock.Setup(db => db.Feedbacks.Create(It.IsAny<Feedback>())).Verifiable();
            mock.Setup(db => db.Save()).Verifiable();
            var controller = new FacilityController(mock.Object);

            //Act
            var result = controller.AddFeedback(0, GetFeedbackDTO()) as RedirectToActionResult;

            //Assert      
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ActionName == "Place");
            mock.Verify(a => a.Feedbacks.Create(It.IsAny<Feedback>()));
            mock.Verify(a => a.Save());
        }

        [Test]
        public void AddFeedback_NullArgument_ReturnsNotFound()
        {
            //Assign
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(db => db.Facilities.Get(It.IsAny<int>())).Returns(GetFacility());
            var controller = new FacilityController(mock.Object);

            //Act
            var result = controller.AddFeedback(0, GetNullFeedbackDTO());

            //Assert      
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void AddFeedback_FacilityNotFoundInDB_ReturnsNotFound()
        {
            //Assign
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(db => db.Facilities.Get(It.IsAny<int>())).Returns(GetNullFacility());
            var controller = new FacilityController(mock.Object);

            //Act
            var result = controller.AddFeedback(0, GetFeedbackDTO());

            //Assert      
            Assert.IsInstanceOf<NotFoundResult>(result);
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

        private Feedback GetFeedback()
        {
            return new Feedback
            {
                Id = 0,
                FacilityId = 0,
                Author = "Anonynous",
                Date = DateTime.Now,
                Rating = 4,
                Message = "Feedback message"
            };
        }

        private FeedbackDTO GetFeedbackDTO()
        {
            return new FeedbackDTO
            {      
                Rating = 0,
                Price = 0,
                Message = "Feedback message",
                Author = "Author"
            };
        }

        private FeedbackDTO GetNullFeedbackDTO()
        {
            return null;            
        }

        private Feedback GetNullFeedback()
        {
            return null;
        }

    }
}
