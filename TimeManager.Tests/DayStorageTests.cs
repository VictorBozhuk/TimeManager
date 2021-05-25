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
    public class DayStorageTests
    {
        [Fact]
        public void WhenYouGetListOfDaysInWhichThereIsOneElementTheNumberOfElementsShouldBeEqualTo1()
        {
            var mock = new Mock<IDayStorage>();
            mock.Setup(a => a.GetAllDays()).Returns(new List<Day>() { new Day() });

            // Assert
            Assert.Equal(1, mock.Object.GetAllDays().Count);
        }

        [Fact]
        public void WhenYouGetListOfDaysInWhichThereIsOneElementTheNumberOfElementsShouldNotBeEqualOtherThenTo1()
        {
            var mock = new Mock<IDayStorage>();
            mock.Setup(a => a.GetAllDays()).Returns(new List<Day>() { new Day() });

            // Assert
            Assert.NotEqual(2, mock.Object.GetAllDays().Count);
        }

        [Fact]
        public void WhenYouGetDayByIdThenMustBeReturnedDayWithSameId()
        {
            var mock = new Mock<IDayStorage>();
            var guid = Guid.NewGuid();
            mock.Setup(a => a.GetDay(guid)).Returns(new Day() { Id = guid, });

            // Assert
            Assert.Equal(guid, mock.Object.GetDay(guid).Id);
        }

        [Fact]
        public void WhenYouCreatedTheDayThenDayMustBeInListDays()
        {
            var mock = new Mock<IDayStorage>();
            var guid = Guid.NewGuid();
            var day = new Day()
            {
                Id = Guid.NewGuid(),
            };
            mock.Setup(a => a.Create(day));
            mock.Setup(a => a.GetAllDays()).Returns(new List<Day>() { day });

            Assert.True(mock.Object.GetAllDays().Contains(day));
        }

        [Fact]
        public void WhenYouDeletedTheDayThenDayMustNotBeInListDays()
        {
            var mock = new Mock<IDayStorage>();
            var guid = Guid.NewGuid();
            var day = new Day()
            {
                Id = guid,
            };
            mock.Setup(a => a.Delete(guid));
            mock.Setup(a => a.GetAllDays()).Returns(new List<Day>() { });

            Assert.False(mock.Object.GetAllDays().Contains(day));
        }


        [Fact]
        public void WhenYouEditedTheDayDateThenDayMustBeHaveNewDate()
        {
            var mock = new Mock<IDayStorage>();
            var guid = Guid.NewGuid();
            var day = new Day()
            {
                Id = guid,
                Date = DateTime.Now,
            };
            mock.Setup(a => a.GetAllDays()).Returns(new List<Day>() { day });

            var day2 = new Day()
            {
                Id = guid,
                Date = DateTime.Now.AddDays(1),
            };
            mock.Setup(a => a.Edit(day2));
            mock.Setup(a => a.GetAllDays()).Returns(new List<Day>() { day2 });

            Assert.True(mock.Object.GetAllDays().FirstOrDefault(x => x.Date == day2.Date) != null);
        }
    }
}
