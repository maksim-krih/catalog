using Catalog.BLL.Interfaces;
using Catalog.BLL.Repositories;
using Catalog.DAL.Data;
using Catalog.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Catalog.Tests
{
    [TestFixture]
    public class FeedbackRepositoryTest
    {
        private Mock<CatalogContext> _catalogContext;
        private IRepository<Feedback> _feedbackRepository;

        [SetUp]
        public void SetUp()
        {
            _catalogContext = new Mock<CatalogContext>();
            _feedbackRepository = new FeedbackRepository(_catalogContext.Object);

        }

        [Test]
        public void Get_ReturnFeedbackFoundById()
        {
            //Arrange
            int id = 1;
            var expectedFeedback = CreateFeedback();

            _catalogContext.Setup(x => x.Feedbacks.Find(id)).Returns(expectedFeedback);

            //Act
            var returnedFeedback = _feedbackRepository.Get(id);

            //Assert
            Assert.AreEqual(expectedFeedback, returnedFeedback);
        }

        [Test]
        public void Get_NonExistingId_ReturnNull()
        {
            //Arrange
            int id = 1;

            Feedback expectedFeedback = null;

            _catalogContext.Setup(context => context.Feedbacks.Find(id)).Returns(expectedFeedback);

            //Act
            var returnedFeedback = _feedbackRepository.Get(id);

            //Assert
            Assert.AreEqual(expectedFeedback, returnedFeedback);
        }

        [Test]
        public void GetAll_ReturnAllFeedbacksFromDataBase()
        {
            //Arrange
            IEnumerable<Feedback> feedbacks = CreateFeedbackList();

            var feedbacksMock = CreateDbSetMock(feedbacks);
            _catalogContext.Setup(context => context.Feedbacks).Returns(feedbacksMock.Object);

            //Act
            var returnedFeedbacks = _feedbackRepository.GetAll();


            //Assert
            Assert.AreEqual(feedbacksMock.Object, returnedFeedbacks);
        }

        [Test]
        public void Create_ShouldAddFeedbackToDataBase()
        {
            //Arrange
            Feedback feedback = CreateFeedback();

            List<Feedback> feedbacks = new List<Feedback>();

            _catalogContext.Setup(context => context.Feedbacks.Add(It.IsAny<Feedback>())).Callback<Feedback>(fdb => feedbacks.Add(fdb));

            //Act
            _feedbackRepository.Create(feedback);

            //Assert
            Assert.AreEqual(1, feedbacks.Count());
            Assert.IsTrue(feedbacks.Contains(feedback));
        }

        [Test]
        public void Delete_ShouldDeleteFeedbackFromDataBase()
        {
            //Arrange
            int id = 1;
            Feedback feedback = CreateFeedback();

            List<Feedback> feedbacks = new List<Feedback>();

            _catalogContext.Setup(context => context.Feedbacks.Find(id)).Returns(feedback);

            feedbacks.Add(feedback);

            _catalogContext.Setup(context => context.Feedbacks.Remove(It.IsAny<Feedback>())).Callback<Feedback>(s => feedbacks.Remove(s));

            //Act
            _feedbackRepository.Delete(id);

            //Assert
            Assert.AreEqual(0, feedbacks.Count());
            Assert.IsFalse(feedbacks.Contains(feedback));
        }

        [Test]
        public void Delete_NullArgument_ShouldDeleteNothing()
        {
            //Arrange
            int id = 2;
            Feedback feedback = CreateFeedback();

            List<Feedback> feedbacks = new List<Feedback>();
            feedbacks.Add(feedback);

            _catalogContext.Setup(context => context.Feedbacks.Find(id)).Returns(feedback);

            //Act
            feedbacks.Add(feedback);

            _catalogContext.Setup(context => context.Feedbacks.Remove(It.IsAny<Feedback>())).Callback<Feedback>(s => feedbacks.Remove(s));

            _feedbackRepository.Delete(id);

            //Assert
            Assert.AreEqual(1, feedbacks.Count());
        }

        private Feedback CreateFeedback()
        {
            var feedback = new Feedback()
            {
                Id = 1,
                FacilityId = 1,
                Author = "Anonynous",
                Date = DateTime.Now,
                Rating = 5,
                Message = "Message"
            };

            return feedback;
        }

        private List<Feedback> CreateFeedbackList()
        {
            var feedbacks = new List<Feedback>()
            { 
                new Feedback()
                {
                    Id = 1,
                    FacilityId = 1,
                    Author = "Unknown",
                    Date = DateTime.Now,
                    Rating = 3.5,
                    Message = "Message"
                },
                new Feedback()
                {
                    Id = 2,
                    FacilityId = 1,
                    Author = "Anonynous",
                    Date = DateTime.Now,
                    Rating = 5,
                    Message = "Message"
                },
                new Feedback()
                {
                    Id = 2,
                    FacilityId = 1,
                    Author = "Anonynous",
                    Date = DateTime.Now,
                    Rating = 4,
                    Message = "Message"
                },
            };

            return feedbacks;
        }

        private static Mock<DbSet<T>> CreateDbSetMock<T>(IEnumerable<T> elements) where T : class
        {
            var elementsAsQueryable = elements.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();

            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());

            return dbSetMock;
        }
    }
}
