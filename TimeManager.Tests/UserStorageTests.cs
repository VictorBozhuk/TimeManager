using System;
using Xunit;

using Moq;
using TimeManager.Storage;
using System.Collections.Generic;
using System.Data.Entity;
using TimeManager.Models;
using TimeManager.Storage.Storages.Abstracts;
using System.Linq;


namespace TimeManager.Tests
{
    public class UserStorageTests
    {
        [Fact]
        public void WhenYouGetListOfUsersInWhichThereIsOneElementTheNumberOfElementsShouldBeEqualTo1()
        {
            var mock = new Mock<IUserStorage>();
            mock.Setup(a => a.GetAllUsers()).Returns(new List<User>() { new User() });

            // Assert
            Assert.Equal(1, mock.Object.GetAllUsers().Count);
        }

        [Fact]
        public void WhenYouGetListOfUsersInWhichThereIsOneElementTheNumberOfElementsShouldNotBeEqualOtherThenTo1()
        {
            var mock = new Mock<IUserStorage>();
            mock.Setup(a => a.GetAllUsers()).Returns(new List<User>() { new User() });

            // Assert
            Assert.NotEqual(2, mock.Object.GetAllUsers().Count);
        }

        [Fact]
        public void WhenYouGetUserByIdThenMustBeReturnedUserWithSameId()
        {
            var mock = new Mock<IUserStorage>();
            var guid = Guid.NewGuid();
            mock.Setup(a => a.GetUser(guid)).Returns(new User() { Id = guid, });

            // Assert
            Assert.Equal(guid, mock.Object.GetUser(guid).Id);
        }

        [Fact]
        public void WhenYouCreatedTheUserThenUserMustBeInListUsers()
        {
            var mock = new Mock<IUserStorage>();
            var guid = Guid.NewGuid();
            var User = new User()
            {
                Id = Guid.NewGuid(),
            };
            mock.Setup(a => a.Create(User));
            mock.Setup(a => a.GetAllUsers()).Returns(new List<User>() { User });

            Assert.True(mock.Object.GetAllUsers().Contains(User));
        }

        [Fact]
        public void WhenYouDeletedTheUserThenUserMustNotBeInListUsers()
        {
            var mock = new Mock<IUserStorage>();
            var guid = Guid.NewGuid();
            var User = new User()
            {
                Id = guid,
            };
            mock.Setup(a => a.Delete(guid));
            mock.Setup(a => a.GetAllUsers()).Returns(new List<User>() { });

            Assert.False(mock.Object.GetAllUsers().Contains(User));
        }


        [Fact]
        public void WhenYouEditedTheUserDateThenUserMustBeHaveNewDate()
        {
            var mock = new Mock<IUserStorage>();
            var guid = Guid.NewGuid();
            var User = new User()
            {
                Id = guid,
                Login = "Login1",
            };
            mock.Setup(a => a.GetAllUsers()).Returns(new List<User>() { User });

            var User2 = new User()
            {
                Id = guid,
                Login = "Login2",
            };
            mock.Setup(a => a.Edit(User2));
            mock.Setup(a => a.GetAllUsers()).Returns(new List<User>() { User2 });

            Assert.True(mock.Object.GetAllUsers().FirstOrDefault(x => x.Login == User2.Login) != null);
        }
    }
}
