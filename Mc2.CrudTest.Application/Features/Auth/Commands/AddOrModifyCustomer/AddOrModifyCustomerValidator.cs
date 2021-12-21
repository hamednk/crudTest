using Mc2.CrudTest.Application.Interfaces.Services;
using FluentValidation;
using PhoneNumbers;
using System;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Application.Features.Auth.Commands
{

    public class AddOrModifyCustomerValidator : AbstractValidator<AddOrModifyCustomerCommand>
    {
        private IGenericService Service { get; }

        public AddOrModifyCustomerValidator(IGenericService genericService)
        {
            Service = genericService;

            RuleFor(p => p.Email)
                .MustAsync(IsValidEmailAddress).WithMessage("Please enter correct email !")
                .MustAsync(IsUniqueEmail).WithMessage("Your email duplicate !, Please enter other email!");

            RuleFor(p => p.PhoneNumber)
                .MustAsync(IsValidPhoneNumber).WithMessage("Please enter correct phoneNumber !");
        }

        private async Task<bool> IsValidPhoneNumber(string phoneNumber, CancellationToken token)
        {
            PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();
            PhoneNumber queryPhoneNumber = phoneUtil.Parse(phoneNumber, "IR");

            var result = await Task.Run(() =>
                 {
                     return phoneUtil.IsValidNumber(queryPhoneNumber) ? true : false;
                 }).ConfigureAwait(false);

            return result;
        }
        private async Task<bool> IsValidEmailAddress(string emailaddress, CancellationToken token)
        {
            var result = await Task.Run(() =>
            {
                try
                {
                    MailAddress m = new MailAddress(emailaddress);
                    return true;
                }
                catch (FormatException)
                {
                    return false;
                }
            }).ConfigureAwait(false);

            return result;
        }

        private async Task<bool> IsUniqueEmail(string emailAddress, CancellationToken token)
        {
            var result = (await Service.ActionWithQueryAndParams<string>("SELECT [Email] FROM [Customer] WHERE [Email] = @Email", new { Email = emailAddress }).ConfigureAwait(false)).Data.Any();
            return !result;
        }
    }
}

