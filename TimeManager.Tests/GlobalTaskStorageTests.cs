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
    public class GlobalTaskStorageTests
    {
        [Fact]
        public void WhenYouGetListOfGlobalTasksInWhichThereIsOneElementTheNumberOfElementsShouldBeEqualTo1()
        {
            var mock = new Mock<IGlobalTaskStorage>();
            mock.Setup(a => a.GetAllGlobalTasks()).Returns(new List<GlobalTask>() { new GlobalTask() });

            // Assert
            Assert.Equal(1, mock.Object.GetAllGlobalTasks().Count);
        }

        [Fact]
        public void WhenYouGetListOfGlobalTasksInWhichThereIsOneElementTheNumberOfElementsShouldNotBeEqualOtherThenTo1()
        {
            var mock = new Mock<IGlobalTaskStorage>();
            mock.Setup(a => a.GetAllGlobalTasks()).Returns(new List<GlobalTask>() { new GlobalTask() });

            // Assert
            Assert.NotEqual(2, mock.Object.GetAllGlobalTasks().Count);
        }

        [Fact]
        public void WhenYouGetGlobalTaskByIdThenMustBeReturnedGlobalTaskWithSameId()
        {
            var mock = new Mock<IGlobalTaskStorage>();
            var guid = Guid.NewGuid();
            mock.Setup(a => a.GetGlobalTask(guid)).Returns(new GlobalTask() { Id = guid, });

            // Assert
            Assert.Equal(guid, mock.Object.GetGlobalTask(guid).Id);
        }

        [Fact]
        public void WhenYouCreatedTheGlobalTaskThenDayMustBeInListGlobalTasks()
        {
            var mock = new Mock<IGlobalTaskStorage>();
            var guid = Guid.NewGuid();
            var day = new GlobalTask()
            {
                Id = Guid.NewGuid(),
            };
            mock.Setup(a => a.Create(day));
            mock.Setup(a => a.GetAllGlobalTasks()).Returns(new List<GlobalTask>() { day });

            Assert.True(mock.Object.GetAllGlobalTasks().Contains(day));
        }

        [Fact]
        public void WhenYouDeletedTheGlobalTaskThenDayMustNotBeInListGlobalTasks()
        {
            var mock = new Mock<IGlobalTaskStorage>();
            var guid = Guid.NewGuid();
            var day = new GlobalTask()
            {
                Id = guid,
            };
            mock.Setup(a => a.Delete(guid));
            mock.Setup(a => a.GetAllGlobalTasks()).Returns(new List<GlobalTask>() { });

            Assert.False(mock.Object.GetAllGlobalTasks().Contains(day));
        }


        [Fact]
        public void WhenYouEditedTheGlobalTaskDateThenDayMustBeHaveNewDate()
        {
            var mock = new Mock<IGlobalTaskStorage>();
            var guid = Guid.NewGuid();
            var day = new GlobalTask()
            {
                Id = guid,
                DeadLine = DateTime.Now,
            };
            mock.Setup(a => a.GetAllGlobalTasks()).Returns(new List<GlobalTask>() { day });

            var day2 = new GlobalTask()
            {
                Id = guid,
                DeadLine = DateTime.Now.AddDays(1),
            };
            mock.Setup(a => a.Edit(day2));
            mock.Setup(a => a.GetAllGlobalTasks()).Returns(new List<GlobalTask>() { day2 });

            Assert.True(mock.Object.GetAllGlobalTasks().FirstOrDefault(x => x.DeadLine == day2.DeadLine) != null);
        }
    }
}
