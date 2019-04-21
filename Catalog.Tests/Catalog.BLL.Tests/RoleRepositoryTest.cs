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
    class RoleRepositoryTest
    {
        private Mock<CatalogContext> _catalogContext;
        private IRepository<Role> _roleRepository;

        [SetUp]
        public void SetUp()
        {
            _catalogContext = new Mock<CatalogContext>();
            _roleRepository = new RoleRepository(_catalogContext.Object);

        }

        [Test]
        public void Get_ReturnRoleFoundById()
        {
            //Arrange
            int id = 1;
            var expectedRole = CreateRole();

            _catalogContext.Setup(x => x.Roles.Find(id)).Returns(expectedRole);

            //Act
            var returnedRole = _roleRepository.Get(id);

            //Assert
            Assert.AreEqual(expectedRole, returnedRole);
        }

        [Test]
        public void Get_NonExistingId_ReturnNull()
        {
            //Arrange
            int id = 1;

            Role expectedRole = null;

            _catalogContext.Setup(context => context.Roles.Find(id)).Returns(expectedRole);

            //Act
            var returnedRole = _roleRepository.Get(id);

            //Assert
            Assert.AreEqual(expectedRole, returnedRole);
        }

        [Test]
        public void GetAll_ReturnAllRolesFromDataBase()
        {
            //Arrange
            IEnumerable<Role> roles = CreateRoleList();

            var rolesMock = CreateDbSetMock(roles);
            _catalogContext.Setup(context => context.Roles).Returns(rolesMock.Object);

            //Act
            var returnedRoles = _roleRepository.GetAll();


            //Assert
            Assert.AreEqual(rolesMock.Object, returnedRoles);
        }

        [Test]
        public void Create_ShouldAddRoleToDataBase()
        {
            //Arrange
            Role role = CreateRole();

            List<Role> roles = new List<Role>();

            _catalogContext.Setup(context => context.Roles.Add(It.IsAny<Role>())).Callback<Role>(rl => roles.Add(rl));

            //Act
            _roleRepository.Create(role);

            //Assert
            Assert.AreEqual(1, roles.Count());
            Assert.IsTrue(roles.Contains(role));
        }

        [Test]
        public void Delete_ShouldDeleteRoleFromDataBase()
        {
            //Arrange
            int id = 1;
            Role role = CreateRole();

            List<Role> roles = new List<Role>();

            _catalogContext.Setup(context => context.Roles.Find(id)).Returns(role);

            roles.Add(role);

            _catalogContext.Setup(context => context.Roles.Remove(It.IsAny<Role>())).Callback<Role>(s => roles.Remove(s));

            //Act
            _roleRepository.Delete(id);

            //Assert
            Assert.AreEqual(0, roles.Count());
            Assert.IsFalse(roles.Contains(role));
        }

        [Test]
        public void Delete_NullArgument_ShouldDeleteNothing()
        {
            //Arrange
            int id = 2;
            Role role = CreateRole();

            List<Role> roles = new List<Role>();
            roles.Add(role);

            _catalogContext.Setup(context => context.Roles.Find(id)).Returns(role);

            //Act
            roles.Add(role);

            _catalogContext.Setup(context => context.Roles.Remove(It.IsAny<Role>())).Callback<Role>(s => roles.Remove(s));

            _roleRepository.Delete(id);

            //Assert
            Assert.AreEqual(1, roles.Count());
        }

        private Role CreateRole()
        {
            var role = new Role()
            {
                Id = 1,
                Name = "User"
            };

            return role;
        }

        private List<Role> CreateRoleList()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Id = 1,
                    Name = "User"                    
                },
                new Role()
                {
                    Id = 2,
                    Name = "Admin"
                },
                new Role()
                {
                    Id = 3,
                    Name = "User"
                },
            };

            return roles;
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
