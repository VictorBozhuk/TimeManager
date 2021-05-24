using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManager.Models;

namespace TimeManager.Storage.Storages.Abstracts
{
    public interface IUserStorage
    {
        void Create(User args);
        User GetUser(Guid id);
        List<User> GetAllUsers();
        void Edit(User args);
        void Delete(Guid id);
    }
}
