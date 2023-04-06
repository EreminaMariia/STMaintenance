using Entities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LogicLibrary
{
    public class SupplierView : ITableView, INameIdView
    {
        private string name = string.Empty;
        private string address = string.Empty;
        private string phoneNumber = string.Empty;
        private string addPhoneNumber = string.Empty;
        private string email = string.Empty;
        private string person = string.Empty;
        private string comment = string.Empty;
        public int Id { get; set; }

        [System.ComponentModel.DisplayName("Название")]
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(nameof(Name)); }
        }

        [System.ComponentModel.DisplayName("Адрес")]
        public string Address
        {
            get { return address; }
            set { address = value; OnPropertyChanged(nameof(Address)); }
        }

        [System.ComponentModel.DisplayName("Телефон")]
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; OnPropertyChanged(nameof(PhoneNumber)); }
        }

        [System.ComponentModel.DisplayName("Доп.телефон")]
        public string AddPhoneNumber
        {
            get { return addPhoneNumber; }
            set { addPhoneNumber = value; OnPropertyChanged(nameof(AddPhoneNumber)); }
        }

        [System.ComponentModel.DisplayName("Email")]
        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged(nameof(Email)); }
        }
        [System.ComponentModel.DisplayName("Представитель")]
        public string Person
        {
            get { return person; }
            set { person = value; OnPropertyChanged(nameof(Person)); }
        }

        [System.ComponentModel.DisplayName("Комментарий")]
        public string Comment
        {
            get { return comment; }
            set { comment = value; OnPropertyChanged(nameof(Comment)); }
        }
        public SupplierView(){}

            public SupplierView (EquipmentSupplier sup)
        {
            Id = sup.Id;
            Name = sup.Name;
            Address = sup.Address;
            PhoneNumber = sup.PhoneNumber;
            AddPhoneNumber = sup.AdditionalPhoneNumber;
            Email = sup.Email;
            Person = sup.Person;
            Comment = sup.Commentary;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public event ITableView.DeleteHandler DeletingEvent;
    }
}
