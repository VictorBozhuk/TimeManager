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
    public class DailyTaskStorageTests
    {
        [Fact]
        public void WhenYouGetListOfDailyTasksInWhichThereIsOneElementTheNumberOfElementsShouldBeEqualTo1()
        {
            var mock = new Mock<IDailyTaskStorage>();
            mock.Setup(a => a.GetAllDailyTasks()).Returns(new List<DailyTask>() { new DailyTask() });

            // Assert
            Assert.Equal(1, mock.Object.GetAllDailyTasks().Count);
        }

        [Fact]
        public void WhenYouGetListOfDailyTasksInWhichThereIsOneElementTheNumberOfElementsShouldNotBeEqualOtherThenTo1()
        {
            var mock = new Mock<IDailyTaskStorage>();
            mock.Setup(a => a.GetAllDailyTasks()).Returns(new List<DailyTask>() { new DailyTask() });

            // Assert
            Assert.NotEqual(2, mock.Object.GetAllDailyTasks().Count);
        }

        [Fact]
        public void WhenYouGetDailyTaskByIdThenMustBeReturnedDailyTaskWithSameId()
        {
            var mock = new Mock<IDailyTaskStorage>();
            var guid = Guid.NewGuid();
            mock.Setup(a => a.GetDailyTask(guid)).Returns(new DailyTask() { Id = guid, });

            // Assert
            Assert.Equal(guid, mock.Object.GetDailyTask(guid).Id);
        }

        [Fact]
        public void WhenYouCreatedTheDailyTaskThenDayMustBeInListDailyTasks()
        {
            var mock = new Mock<IDailyTaskStorage>();
            var guid = Guid.NewGuid();
            var day = new DailyTask()
            {
                Id = Guid.NewGuid(),
            };
            mock.Setup(a => a.Create(day));
            mock.Setup(a => a.GetAllDailyTasks()).Returns(new List<DailyTask>() { day });

            Assert.True(mock.Object.GetAllDailyTasks().Contains(day));
        }

        [Fact]
        public void WhenYouDeletedTheDailyTaskThenDayMustNotBeInListDailyTasks()
        {
            var mock = new Mock<IDailyTaskStorage>();
            var guid = Guid.NewGuid();
            var day = new DailyTask()
            {
                Id = guid,
            };
            mock.Setup(a => a.Delete(guid));
            mock.Setup(a => a.GetAllDailyTasks()).Returns(new List<DailyTask>() { });

            Assert.False(mock.Object.GetAllDailyTasks().Contains(day));
        }


        [Fact]
        public void WhenYouEditedTheDailyTaskTitleThenDayMustBeHaveNewDate()
        {
            var mock = new Mock<IDailyTaskStorage>();
            var guid = Guid.NewGuid();
            var day = new DailyTask()
            {
                Id = guid,
                Title = "Title1",
            };
            mock.Setup(a => a.GetAllDailyTasks()).Returns(new List<DailyTask>() { day });

            var day2 = new DailyTask()
            {
                Id = guid,
                Title = "Title2",
            };
            mock.Setup(a => a.Edit(day2));
            mock.Setup(a => a.GetAllDailyTasks()).Returns(new List<DailyTask>() { day2 });

            Assert.True(mock.Object.GetAllDailyTasks().FirstOrDefault(x => x.Title == day2.Title) != null);
        }
    }
}
