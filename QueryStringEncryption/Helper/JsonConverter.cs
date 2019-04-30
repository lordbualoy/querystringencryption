using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueryStringEncryption.Helper
{
    public class JsonEncryptedString : JsonConverter
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;
        public override bool CanConvert(Type objectType) => true;
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string encryptedValue = reader.Value.ToString();
            string decrypted = Encryption.Decrypt(encryptedValue);
            return CreateEncryptionWrapper(encryptedValue, decrypted);
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            string encryptedValue = GetEncryptedString(value);
            writer.WriteValue(encryptedValue);
        }
        protected virtual object CreateEncryptionWrapper(string encryptedValue, string decrypted)
        {
            return new EncryptionWrapper<string>(encryptedValue, decrypted);
        }
        protected virtual string GetEncryptedString(object encryptionWrapper)
        {
            return ((EncryptionWrapper<string>)encryptionWrapper).EncryptedValue;
        }
    }

    public class JsonEncryptedInt32 : JsonEncryptedString
    {
        protected override object CreateEncryptionWrapper(string encryptedValue, string decrypted)
        {
            return new EncryptionWrapper<int>(encryptedValue, int.Parse(decrypted));
        }
        protected override string GetEncryptedString(object encryptionWrapper)
        {
            return ((EncryptionWrapper<int>)encryptionWrapper).EncryptedValue;
        }
    }
}
