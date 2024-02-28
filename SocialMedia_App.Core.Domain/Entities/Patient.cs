using SocialMedia_App.Core.Domain.Common;
using System;

namespace SocialMedia_App.Core.Domain.Entities
{
    public class Patient : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string IdentificationNumber { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public bool IsSmoker { get; set; }
        public bool HasAllergies { get; set; }
        public string Photo { get; set; }

        // relationships
        //public ICollection<Appointment> Appointments { get; set; }
    }
}

