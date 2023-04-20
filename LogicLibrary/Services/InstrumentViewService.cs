using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLibrary.Services
{
    public class InstrumentViewService: ITableViewService<InstrumentView>
    {
        private PassportMaker techPassport;
        private bool canChange = true;
        public InstrumentViewService(PassportMaker passport)
        {
            techPassport = passport;
        }

        public int Add(ITableView view)
        {
            var item = (InstrumentView)view;
            int id = 1;
            if (techPassport.Instruments == null)
            {
                techPassport.Instruments = new List<InstrumentView>();
            }
            if (techPassport.Instruments.Count > 0)
            {                
                id = techPassport.InstrumentsId++;
            }
            techPassport.Instruments.Add(new InstrumentView
            {
                Id = id,
                Name = item.Name,

            });
            item.MarkChanged();
            return id;
        }

        public bool Delete(int id)
        {
            //var instrument = techPassport.Instruments.FirstOrDefault(m => m.Id == id);
            //if (instrument != null)
            //{
            //    instrument.ChangeIfInWork();
            //    instrument.MarkChanged();
            //}
            //return true;
            return false;
        }

        public void Update(ITableView view)
        {
            if (canChange)
            {
                canChange = false;
                var item = (InstrumentView)view;
                var oldItem = techPassport.Instruments.First(x => x.Id == item.Id);
                oldItem.Name = item.Name;
                oldItem.Art = item.Art;
                oldItem.CreateDate = item.CreateDate;
                oldItem.Count = item.Count;
                oldItem.Commentary = item.Commentary;
                oldItem.RemoveDate = item.RemoveDate;
                oldItem.RemoveReason = item.RemoveReason;
                oldItem.MarkChanged();
                canChange = true;
            }
        }
    }
}
