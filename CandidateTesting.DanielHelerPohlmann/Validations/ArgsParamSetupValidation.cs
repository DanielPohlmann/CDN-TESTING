using CandidateTesting.DanielHelerPohlmann.Core.Extenssions;
using CandidateTesting.DanielHelerPohlmann.Models;
using CandidateTesting.DanielHelerPohlmann.Sources.Messages;
using FluentValidation;

namespace CandidateTesting.DanielHelerPohlmann.Validations
{
    internal class ArgsParamSetupValidation : AbstractValidator<ArgsParamSetup>
    {
        public ArgsParamSetupValidation()
        {
            RuleFor(c => c.UrlFileImput)
                .NotEmpty()
                .NotNull()
                .Must(UrlIsFileValid)
                .WithMessage(Message.UrlIsNotValid);

            RuleFor(c => c.PatchFileOutput)
                .NotEmpty()
                .NotNull()
                .Must(PathIsValid)
                .WithMessage(Message.PathIsNotValid);
        }

        protected static bool UrlIsFileValid(string? url) => url?.UrlIsFileValid() ?? false;

        protected static bool PathIsValid(string? path) => path?.IsValidPathAndFileName() ?? false;

    }
}
