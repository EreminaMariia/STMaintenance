using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLibrary.Services
{
    public class InnerArchiveViewService : ITableViewService<InnerArchiveView>
    {
        private PassportMaker techPassport;
        private bool canChange = true;
        public InnerArchiveViewService(PassportMaker passport)
        {
            techPassport = passport;
        }

        public int Add(ITableView view)
        {            
            return -1;
        }

        public bool Delete(int id)
        {
            return false;
        }

        public void Update(ITableView view)
        {}
    }

    public class OuterArchiveViewService : ITableViewService<OuterArchiveView>
    {
        private PassportMaker techPassport;
        private bool canChange = true;
        public OuterArchiveViewService()
        {
            
        }

        public int Add(ITableView view)
        {
            return -1;
        }

        public bool Delete(int id)
        {
            return false;
        }

        public void Update(ITableView view)
        { }
    }
}
