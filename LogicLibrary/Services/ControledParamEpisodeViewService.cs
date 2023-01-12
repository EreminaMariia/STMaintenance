using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLibrary.Services
{
    public class ControledParamEpisodeViewService : ITableViewService<ControledParametrEpisodeView>
    {
        private PassportMaker techPassport;
        private bool canChange = true;
        public ControledParamEpisodeViewService(PassportMaker passport)
        {
            techPassport = passport;
        }
        public int Add(ITableView view)
        {
            var item = (ControledParametrEpisodeView)view;
            int id = 1;

            if (techPassport.ControledParametrEpisodes == null)
            {
                techPassport.ControledParametrEpisodes = new List<ControledParametrEpisodeView>();
            }
            if (techPassport.ControledParametrEpisodes.Count > 0)
            {
                id = techPassport.ControledParametrEpisodesId++;
            }

            techPassport.ControledParametrEpisodes.Add(new ControledParametrEpisodeView
            {
                Id = id,
                //Name = item.Name,
                //Nominal = item.Nominal,
                //Unit = item.Unit
                Date = item.Date,
                Count = item.Count

        });
            return id;
        }

        public bool Delete(int id)
        {
            return false;
        }

        public void Update(ITableView view)
        {
            if (canChange)
            {
                canChange = false;
                var item = (ControledParametrEpisodeView)view;
                var oldItem = techPassport.ControledParametrEpisodes.First(x => x.Id == item.Id);
                //oldItem.Name = item.Name;
                //oldItem.Nominal = item.Nominal;
                //oldItem.Unit = item.Unit;
                oldItem.Date = item.Date;
                oldItem.Count = item.Count;
                oldItem.MarkChanged();
                canChange = true;
            }
        }
    }
}
