using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLibrary
{
    public interface INameIdView: IIdView
    {
        public string Name { get;}       
    }
}
