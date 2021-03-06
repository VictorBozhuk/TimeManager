﻿using GalaSoft.MvvmLight.CommandWpf;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TimeManager.Abstract;
using TimeManager.Abstracts;
using TimeManager.Models;
using TimeManager.Storage.Storages.Abstracts;
using TimeManager.ViewModels.Base;

namespace TimeManager.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class CreateEditGlobalTaskViewModel : CreateEditBaseViewModel
    {
        private ComboBoxItem selectedType;
        private ComboBoxItem selectedStatus;
        public ComboBoxItem SelectedType
        {
            get { return selectedType; }
            set
            {
                selectedType = value;
                if (value != null)
                {
                    GlobalTask.Type = value.Content.ToString();
                }
            }
        }
        public bool IsTemplateChecked { get; set; }
        public GlobalTaskModel GlobalTask { get; set; }
        public ComboBoxItem SelectedStatus
        {
            get { return selectedStatus; }
            set
            {
                selectedStatus = value;
                GlobalTask.Status = value.Content.ToString();
            }
        }
        public CreateEditGlobalTaskViewModel(MainViewModel main, IDayStorage dayStorage, IDailyTaskStorage dailyTaskStorage, IGlobalTaskStorage globalTaskStorage, GlobalTaskModel globalTask = null, bool isTemplate = false)
            : base(main, dayStorage, dailyTaskStorage, globalTaskStorage)
        {
            if(globalTask != null)
            {
                IsTemplateChecked = isTemplate;
                GlobalTask = globalTask;
                NamePage = Texts.Edit;
                LoadTypes();
                SelectedStatus = TaskStatuses.FirstOrDefault(x => x.Content.ToString() == globalTask.Status);
                SelectedType = Types.FirstOrDefault(x => x.Content.ToString() == globalTask.Type);
            }
            else
            {
                Load();
                SelectedStatus = TaskStatuses.FirstOrDefault();
                NamePage = Texts.Create;
            }

            ExecuteCommand = new RelayCommand(Execute);
            CancelCommand = new RelayCommand(_main.GoToGlobalTasks);
        }

        public RelayCommand ExecuteCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public void PrepareEditDailyTask(GlobalTaskModel task)
        {
            GlobalTask = task;
            NamePage = Texts.Edit;
        }

        private void Execute()
        {
            if (IsTemplateChecked == true)
            {
                SetGlobalTask(Periods.TemplateDateTime);
            }
            else
            {
                SetGlobalTask(Convert.ToDateTime($"{GlobalTask.DeadLineShortDate} {GlobalTask.DeadLineTime}"));
            }

            _main.GoToGlobalTasks();
        }

        private void SetGlobalTask(DateTime deadLine)
        {
            if (NamePage == Texts.Create)
            {
                CreateGlobalTask(deadLine);
            }
            else if (NamePage == Texts.Edit)
            {
                EditGlobalTask(deadLine);
            }
        }

        private void CreateGlobalTask(DateTime deadLine)
        {
            var taskArgs = new GlobalTask()
            {
                Title = GlobalTask.Title,
                Type = GlobalTask.Type,
                IsPlan = true,
                Description = GlobalTask.Description,
                DeadLine = deadLine,
                Status = GlobalTask.Status,
            };
            _globalTaskStorage.Create(taskArgs);
        }

        private void EditGlobalTask(DateTime deadLine)
        {
            var taskArgs = new GlobalTask()
            {
                Id = GlobalTask.Id,
                Title = GlobalTask.Title,
                Status = GlobalTask.Status,
                Type = GlobalTask.Type,
                IsPlan = GlobalTask.IsPlan,
                Description = GlobalTask.Description,
                DeadLine = deadLine,
            };

            _globalTaskStorage.Edit(taskArgs);
        }

        private void Load()
        {
            NamePage = Texts.Create;
            GlobalTask = new GlobalTaskModel();
            LoadTypes();
        }
    }
}
