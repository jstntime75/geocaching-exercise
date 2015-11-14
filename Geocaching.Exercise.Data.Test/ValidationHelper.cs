using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Geocaching.Exercise.Data.Test
{
    internal class EntityValidationResult
    {
        public IList<ValidationResult> Errors { get; private set; }

        public bool HasError
        {
            get { return Errors.Count > 0; }
        }

        public EntityValidationResult(IList<ValidationResult> errors = null)
        {
            Errors = errors ?? new List<ValidationResult>();
        }
    }

    internal class EntityValidator<T> where T : class
    {
        public EntityValidationResult Validate(T entity)
        {
            var validationResults = new List<ValidationResult>();
            var vc = new ValidationContext(entity, null, null);
            var isValid = Validator.TryValidateObject(entity, vc, validationResults, true);

            return new EntityValidationResult(validationResults);
        }
    }

    internal class ValidationHelper
    {
        public static EntityValidationResult ValidateEntity<T>(T entity)
            where T : class
        {
            return new EntityValidator<T>().Validate(entity);
        }
    }
}
