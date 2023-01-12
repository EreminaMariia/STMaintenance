using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLibrary
{
    public class ErrorCodeView : INameIdView
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public ErrorCodeView()
        { }

        public ErrorCodeView(ErrorCode code)
        {
            Name = code.Code != null? code.Code: "";
            Id = code.Id;
        }
    }
}
