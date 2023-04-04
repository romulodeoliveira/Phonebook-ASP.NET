using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactRegister.Models
{
    public class ContactModel
    {
        
        public Guid Id
        {
            get;
            set;
        } = Guid.NewGuid();

        public string Name
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public string PhoneNumber
        {
            get;
            set;
        }

    }
}