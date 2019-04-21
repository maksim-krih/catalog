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
    public class FacilityRepositoryTest
    {

        private Mock<CatalogContext> _catalogContext;
        private IRepository<Facility> _facilityRepository;

        [SetUp]
        public void SetUp()
        {
            _catalogContext = new Mock<CatalogContext>();
            _facilityRepository = new FacilityRepository(_catalogContext.Object);

        }

        [Test]
        public void Get_ReturnFacilityFoundById()
        {
            //Arrange
            int id = 1;
            var expectedFacility = CreateFacility();
            
            _catalogContext.Setup(x => x.Facilities.Find(id)).Returns(expectedFacility);

            //Act
            var returnedFacility = _facilityRepository.Get(id);

            //Assert
            Assert.AreEqual(expectedFacility, returnedFacility);
        }

        [Test]
        public void Get_NonExistingId_ReturnNull()
        {
            //Arrange
            int id = 1;

            Facility expectedFacility = null;

            _catalogContext.Setup(context => context.Facilities.Find(id)).Returns(expectedFacility);

            //Act
            var returnedFacility = _facilityRepository.Get(id);

            //Assert
            Assert.AreEqual(expectedFacility, returnedFacility);
        }

        [Test]
        public void GetAll_ReturnAllFacilitiesFromDataBase()
        {
            //Arrange
            IEnumerable<Facility> facilities = CreateFacilityList();

            var facilitiesMock = CreateDbSetMock(facilities);
            _catalogContext.Setup(context => context.Facilities).Returns(facilitiesMock.Object);

            //Act
            var returnedFacilities = _facilityRepository.GetAll();

            //Assert
            Assert.AreEqual(facilitiesMock.Object, returnedFacilities);
        }

        [Test]
        public void Create_ShouldAddFasilityToDataBase()
        {
            //Arrange
            Facility facility = CreateFacility();

            List<Facility> facilities = new List<Facility>();

            _catalogContext.Setup(context => context.Facilities.Add(It.IsAny<Facility>())).Callback<Facility>(facil => facilities.Add(facil));

            //Act
            _facilityRepository.Create(facility);

            //Assert
            Assert.AreEqual(1, facilities.Count());
            Assert.IsTrue(facilities.Contains(facility));
        }

        [Test]
        public void Delete_ShouldDeleteFacilityFromDataBase()
        {
            //Arrange
            int id = 1;
            Facility facility = CreateFacility();

            List<Facility> facilities = new List<Facility>();

            _catalogContext.Setup(context => context.Facilities.Find(id)).Returns(facility);

            facilities.Add(facility);

            _catalogContext.Setup(context => context.Facilities.Remove(It.IsAny<Facility>())).Callback<Facility>(s => facilities.Remove(s));

            //Act
            _facilityRepository.Delete(id);

            //Assert
            Assert.AreEqual(0, facilities.Count());
            Assert.IsFalse(facilities.Contains(facility));
        }

        [Test]
        public void Delete_NullArgumentShouldDeleteNothing()
        {
            //Arrange
            int id = 2;
            Facility facility = CreateFacility();

            List<Facility> facilities = new List<Facility>();
            facilities.Add(facility);

            _catalogContext.Setup(context => context.Facilities.Find(id)).Returns(facility);

            facilities.Add(facility);

            _catalogContext.Setup(context => context.Facilities.Remove(It.IsAny<Facility>())).Callback<Facility>(s => facilities.Remove(s));

            //Act
            _facilityRepository.Delete(id);

            //Assert
            Assert.AreEqual(1, facilities.Count());
        }

        private Facility CreateFacility()
        {
            var facility = new Facility()
            {
                Id = 1,
                Name = "ABC",
                Price = 3,
                Rating = 5,
                Phone = "012345678",
                FacilityType = "Bar",
                FacilityOwnerId = 1
            };

            return facility;
        }

        private List<Facility> CreateFacilityList()
        {
            var facilities = new List<Facility>()
            {
                new Facility()
                {
                     Id = 1,
                    Name = "ABC",
                    Price = 4,
                    Rating = 5,
                    Phone = "012345678",
                    FacilityType = "Bar",
                    FacilityOwnerId = 1
                },
               new Facility()
                {
                    Id = 2,
                    Name = "ABC",
                    Price = 3,
                    Rating = 4.5,
                    Phone = "012345678",
                    FacilityType = "Bar",
                    FacilityOwnerId = 3
                },
               new Facility()
                {
                    Id = 3,
                    Name = "ABC",
                    Price = 3,
                    Rating = 3.1,
                    Phone = "012345678",
                    FacilityType = "Bar",
                    FacilityOwnerId = 2
                }
            };

            return facilities;
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
