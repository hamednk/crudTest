﻿using System;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Mc2.CrudTest.Application.Interfaces.Services;
using Mc2.CrudTest.Domain;
using PhoneNumbers;

namespace Mc2.CrudTest.Application.Features.Auth.Commands.AddOrModifyCustomer
{

    public class AddOrModifyCustomerValidator : AbstractValidator<AddOrModifyCustomerCommand>
    {
        private IGenericService Service { get; }

        public AddOrModifyCustomerValidator(IGenericService genericService)
        {
            Service = genericService;

            RuleFor(p => p)
                .MustAsync(IsValidEmailAddress).WithMessage("Please enter correct email !")
                .MustAsync(IsUniqueEmail).WithMessage("Your email duplicate !, Please enter other email!");

            RuleFor(p => p.PhoneNumber)
                .MustAsync(IsValidPhoneNumber).WithMessage("Please enter correct phoneNumber !");
        }

        private async Task<bool> IsValidPhoneNumber(string phoneNumber, CancellationToken token)
        {
            PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();
            PhoneNumber queryPhoneNumber = phoneUtil.Parse(phoneNumber, "IR");

            bool result = await Task.Run(() =>
                 {
                     return phoneUtil.IsValidNumber(queryPhoneNumber) ? true : false;
                 }).ConfigureAwait(false);

            return result;
        }
        private async Task<bool> IsValidEmailAddress(Customer model, CancellationToken token)
        {
            bool result = await Task.Run(() =>
            {
                try
                {
                    MailAddress m = new(model.Email);
                    return true;
                }
                catch (FormatException)
                {
                    return false;
                }
            }).ConfigureAwait(false);

            return result;
        }

        private async Task<bool> IsUniqueEmail(Customer model, CancellationToken token)
        {
            if (model.Id.HasValue)
                return true;

            bool result = (await Service.ActionWithQueryAndParams<string>("SELECT [Email] FROM [Customer] WHERE [Email] = @Email", new { Email = model.Email }).ConfigureAwait(false)).Data.Any();
            return !result;
        }
    }
}

