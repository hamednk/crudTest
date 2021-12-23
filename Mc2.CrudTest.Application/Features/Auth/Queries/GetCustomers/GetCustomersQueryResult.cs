using System;

namespace Mc2.CrudTest.Application.Features.Auth.Queries.GetCustomers
{
    public class GetCustomersQueryResult
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
