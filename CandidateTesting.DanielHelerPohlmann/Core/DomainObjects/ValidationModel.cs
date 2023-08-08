using FluentValidation.Results;

namespace CandidateTesting.DanielHelerPohlmann.Core.DomainObjects
{
    public abstract class ValidationModel
    {
        protected ValidationModel()
        {
            Timestamp = DateTime.Now;
        }

        public DateTime Timestamp { get; private set; }

        public ValidationResult? ValidationResult { get; set; }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
