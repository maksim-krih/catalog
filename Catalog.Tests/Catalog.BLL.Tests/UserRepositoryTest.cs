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
    public class UserRepositoryTest
    {
        private Mock<CatalogContext> _catalogContext;
        private IRepository<User> _userRepository;

        [SetUp]
        public void SetUp()
        {
            _catalogContext = new Mock<CatalogContext>();
            _userRepository = new UserRepository(_catalogContext.Object);

        }

        [Test]
        public void Get_ReturnUserFoundById()
        {
            //Arrange
            int id = 1;
            var expectedUser = CreateUser();

            _catalogContext.Setup(x => x.Users.Find(id)).Returns(expectedUser);

            //Act
            var returnedUser = _userRepository.Get(id);

            //Assert
            Assert.AreEqual(expectedUser, returnedUser);
        }

        [Test]
        public void Get_NonExistingId_ReturnNull()
        {
            //Arrange
            int id = 1;

            User expectedUser = null;

            _catalogContext.Setup(context => context.Users.Find(id)).Returns(expectedUser);

            //Act
            var returnedUser = _userRepository.Get(id);

            //Assert
            Assert.AreEqual(expectedUser, returnedUser);
        }

        [Test]
        public void GetAll_ReturnAllUsersFromDataBase()
        {
            //Arrange
            IEnumerable<User> users = CreateUserList();

            var usersMock = CreateDbSetMock(users);
            _catalogContext.Setup(context => context.Users).Returns(usersMock.Object);

            //Act
            var returnedUsers = _userRepository.GetAll();

            //Assert
            Assert.AreEqual(usersMock.Object, returnedUsers);
        }

        [Test]
        public void Create_ShouldAddUserToDataBase()
        {
            //Arrange
            User user = CreateUser();

            List<User> users = new List<User>();

            _catalogContext.Setup(context => context.Users.Add(It.IsAny<User>())).Callback<User>(usr => users.Add(usr));

            //Act
            _userRepository.Create(user);

            //Assert
            Assert.AreEqual(1, users.Count());
            Assert.IsTrue(users.Contains(user));
        }

        [Test]
        public void Delete_ShouldDeleteUserFromDataBase()
        {
            //Arrange
            int id = 1;
            User user = CreateUser();

            List<User> users = new List<User>();

            _catalogContext.Setup(context => context.Users.Find(id)).Returns(user);

            users.Add(user);

            _catalogContext.Setup(context => context.Users.Remove(It.IsAny<User>())).Callback<User>(s => users.Remove(s));

            //Act
            _userRepository.Delete(id);

            //Assert
            Assert.AreEqual(0, users.Count());
            Assert.IsFalse(users.Contains(user));
        }

        [Test]
        public void Delete_NullArgument_ShouldDeleteNothing()
        {
            //Arrange
            int id = 2;
            User user = CreateUser();

            List<User> users = new List<User>();
            users.Add(user);

            _catalogContext.Setup(context => context.Users.Find(id)).Returns(user);

            //Act
            users.Add(user);

            _catalogContext.Setup(context => context.Users.Remove(It.IsAny<User>())).Callback<User>(s => users.Remove(s));

            _userRepository.Delete(id);

            //Assert
            Assert.AreEqual(1, users.Count());
        }

        private User CreateUser()
        {
            var user = new User()
            {
                Id = 1,
                Name = "ABC",
                Roleid = 2,
                Email = "user@gmail.com",
                Password = "1111"
            };

            return user;
        }

        private List<User> CreateUserList()
        {
            var users = new List<User>()
            {
                 new User
                {
                    Id = 1,
                    Name = "ABC",
                    Roleid = 1,
                    Email = "user@gmail.com",
                    Password = "1111"
                },
                new User
                {
                    Id = 2,
                    Name = "ABC",
                    Roleid = 2,
                    Email = "user@gmail.com",
                    Password = "1111"
                },
                  new User
                {
                    Id = 3,
                    Name = "ABC",
                    Roleid = 1,
                    Email = "admin@gmail.com",
                    Password = "1111"
                }
            };

            return users;
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
