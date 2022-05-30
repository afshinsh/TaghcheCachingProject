using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaghcheCaching.InfraStructure.Interfaces.Caching;
using TaghcheCaching.InfraStructure.Models;

namespace TaghcheCaching.Application.Sevices.Caching
{
    public  class ManagerService : IManager
    {
        public IManager? NextManager { get; set; }


        public void SetNextManager(IManager manager) => NextManager = manager;

        public virtual Task<BookResponseModel> GetBook(string id) { throw new NotImplementedException(); }

        public virtual void SetBook(string id, object? value) { throw new NotImplementedException(); }


    }
}
