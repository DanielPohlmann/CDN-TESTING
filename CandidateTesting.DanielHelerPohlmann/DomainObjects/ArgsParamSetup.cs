using CandidateTesting.DanielHelerPohlmann.Core.Extenssions;
using CandidateTesting.DanielHelerPohlmann.Core.DomainObjects;
using CandidateTesting.DanielHelerPohlmann.Validations;
using FluentValidation.Results;

namespace CandidateTesting.DanielHelerPohlmann.Models
{
    public class ArgsParamSetup : ValidationModel
    {
        private readonly string[] _args;

        public ArgsParamSetup(string[] args)
        {
            _args = args;
        }

        public string? UrlFileImput => _args?.Get(0);

        public string? PatchFileOutput => _args?.Get(1);

        public override bool IsValid()
        {
            ValidationResult = new ArgsParamSetupValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}