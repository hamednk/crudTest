using Mc2.CrudTest.Application.Interfaces.Services;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Application.Features.Auth.Commands
{

    public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
    {
        private IGenericService Service { get; }

        public DeleteCustomerCommandValidator(IGenericService genericService)
        {
            Service = genericService;

            RuleFor(p => p.Email)
                .MustAsync(IsValidEmailAddress).WithMessage("Please enter correct email !")
                .Null().WithMessage("Please fill email !");
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
    }
}

