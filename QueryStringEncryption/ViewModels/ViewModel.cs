using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QueryStringEncryption.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueryStringEncryption.ViewModels
{
    public class JsonViewModel
    {
        [JsonConverter(typeof(JsonEncryptedInt32))]
        public EncryptionWrapper<int> Num { get; set; }
        [JsonConverter(typeof(JsonEncryptedString))]
        public EncryptionWrapper<string> Text { get; set; }
    }

    public class UrlEncodedViewModel
    {
        [FormEncryptedInt32]
        public int Num { get; set; }
        [FormEncryptedString]
        public string Text { get; set; }
    }
}
