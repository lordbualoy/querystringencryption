using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueryStringEncryption.Helper
{
    public class FormEncryptedStringAttribute : ModelBinderAttribute
    {
        public FormEncryptedStringAttribute() : base(typeof(EncryptedStringModelBinder)) { }
    }

    public class FormEncryptedInt32Attribute : ModelBinderAttribute
    {
        public FormEncryptedInt32Attribute() : base(typeof(EncryptedInt32ModelBinder)) { }
    }

    public class EncryptedStringModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.FieldName);
            if (valueProviderResult == ValueProviderResult.None)
                return Task.CompletedTask;

            var value = valueProviderResult.FirstValue;
            if (string.IsNullOrEmpty(value))
                return Task.CompletedTask;

            bindingContext.Result = ModelBindingResult.Success(Decrypt(value));
            return Task.CompletedTask;
        }

        protected virtual object Decrypt(string encryptedValue)
        {
            return Encryption.Decrypt(encryptedValue);
        }
    }

    public class EncryptedInt32ModelBinder : EncryptedStringModelBinder
    {
        protected override object Decrypt(string encryptedValue)
        {
            string decrypted = (string)base.Decrypt(encryptedValue);
            return Encryption.DecryptToInt(decrypted);
        }
    }
}
