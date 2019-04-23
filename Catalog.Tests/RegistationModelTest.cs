using Catalog.BLL.Interfaces;
using Catalog.BLL.ViewModels;
using Catalog.DAL.Models;
using Catalog.Views;
using Microsoft.AspNetCore.WebSockets.Internal;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Catalog.Tests
{
    [TestFixture]
    public class RegistationModelTest
    {

        [Test]
        public void ValidateRegistrationModel_CorrectDataGiven_ShouldReturnNoErrors()
        {
            //Arrange
            var facilityInstance = GetRegistationModel();

            //Act
            var errorcount = ValidationResult(facilityInstance).Count();

            //Assert
            Assert.AreEqual(0,errorcount);
        }

        [Test]
        public void ValidateRegistrationModel_CorrectDataGiven_ShouldReturnApropriateMessage()
        {
            //Arrange
            var facilityInstance = GetRegistationModel();

            //Act
            var validationResults = ValidationResult(facilityInstance);
            var actual = Validator.TryValidateObject(facilityInstance, new ValidationContext(facilityInstance), validationResults, true);


            //Assert
            Assert.IsTrue(actual, "Expected validation to succeed.");
            Assert.AreEqual(0, validationResults.Count, "Unexpected number of validation errors.");
            
        }

        [Test]
        public void ValidateRegistrationModel_NameFieldMissing_ShouldReturnError()
        {
            //Arrange
            var facilityInstance = GetWrongRegistationModel();
            var validationResults = new List<ValidationResult>();

            //Act
            var actual = Validator.TryValidateObject(facilityInstance, new ValidationContext(facilityInstance), validationResults, true);
            var message = validationResults[0];

            //Assert
            Assert.IsFalse(actual, "Expected validation to fail.");
            Assert.AreEqual(1, validationResults.Count, "Unexpected number of validation errors.");
            Assert.AreEqual(1, message.MemberNames.Count(), "Unexpected number of member names.");

        }


        private IList<ValidationResult> ValidationResult(object model)
        {
            var result = new List<ValidationResult>();
            Validator.TryValidateObject(model, new ValidationContext(model), result, true);

            return result;
        }

        private Registration GetRegistationModel()
        {
            var registationModel = new Registration
            {
                Name = "Bob",
                Email = "bob@gmail.com",
                Password = "12345"
            };

            return registationModel;
        }

        private Registration GetWrongRegistationModel()
        {
            var registationModel = new Registration
            {
                Email = "bob@gmail.com",
                Password = "12345"
            };

            return registationModel;
        }

    }


}
