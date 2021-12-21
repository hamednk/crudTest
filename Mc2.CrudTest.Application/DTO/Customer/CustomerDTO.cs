using Mc2.CrudTest.Application.DTO.Base;
using System;

namespace Mc2.CrudTest.Application.DTO.Customer
{
    public class CustomerDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
        public DateTime? CreatedOn { get; set; }
    }

    public class CustomerSearchDTO : BaseSearchRequestDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
