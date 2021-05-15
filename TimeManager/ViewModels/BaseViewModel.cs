using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TimeManager.Storage.Storages.Abstracts;

namespace TimeManager.ViewModels
{
    public abstract class BaseViewModel
    {
        protected readonly IDayStorage _dayStorage;
        protected readonly IDailyTaskStorage _dailyTaskStorage;
        protected readonly IGlobalTaskStorage _globalTaskStorage;
        protected MainViewModel _main;

        public BaseViewModel(MainViewModel main, IDayStorage dayStorage, IDailyTaskStorage dailyTaskStorage, IGlobalTaskStorage globalTaskStorage)
        {
            _dayStorage = dayStorage;
            _dailyTaskStorage = dailyTaskStorage;
            _globalTaskStorage = globalTaskStorage;
            _main = main;
        }

        public BaseViewModel(MainViewModel main, IDayStorage dayStorage, IDailyTaskStorage dailyTaskStorage)
        {
            _dayStorage = dayStorage;
            _dailyTaskStorage = dailyTaskStorage;
            _main = main;
        }
    }
}
