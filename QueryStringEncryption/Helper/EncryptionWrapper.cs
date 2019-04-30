using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryStringEncryption.Helper
{
    public struct EncryptionWrapper<T>
    {
        public T Value { get; set; }
        public string EncryptedValue { get; private set; }

        public EncryptionWrapper(T value)
        {
            Value = value;
            EncryptedValue = value != null ? Encryption.Encrypt(value.ToString()) : null;
        }

        public EncryptionWrapper(string encryptedValue, T value)
        {
            EncryptedValue = encryptedValue;
            if (encryptedValue != null)
                Value = value;
            else
                Value = default(T);
        }

        public override bool Equals(object other)
        {
            return base.Equals(other);
        }
        public override int GetHashCode() => base.GetHashCode();
        public override string ToString()
        {
            return Value.ToString();
        }
        public static implicit operator T(EncryptionWrapper<T> value) => value.Value;
        public static implicit operator EncryptionWrapper<T>(T value) => new EncryptionWrapper<T>(value);

        //public static bool operator ==(StambiaDateWrapper lhs, StambiaDateWrapper rhs)
        //{
        //    // Check for null on left side.
        //    if (ReferenceEquals(lhs, null))
        //    {
        //        if (ReferenceEquals(rhs, null))
        //        {
        //            // null == null = true.
        //            return true;
        //        }

        //        // Only the left side is null.
        //        return false;
        //    }
        //    // Equals handles case of null on right side.
        //    return lhs.Equals(rhs);
        //}

        //public static bool operator !=(StambiaDateWrapper lhs, StambiaDateWrapper rhs)
        //{
        //    return !(lhs == rhs);
        //}

        //public static bool operator <(StambiaDateWrapper lhs, StambiaDateWrapper rhs)
        //{
        //    return Comparer<StambiaDateWrapper>.Default.Compare(lhs.Value, rhs.Value) < 0;
        //}

        //public static bool operator >(StambiaDateWrapper lhs, StambiaDateWrapper rhs)
        //{
        //    return !(lhs < rhs);
        //}

        //public static bool operator <=(StambiaDateWrapper lhs, StambiaDateWrapper rhs)
        //{
        //    return (lhs < rhs) || (lhs == rhs);
        //}

        //public static bool operator >=(StambiaDateWrapper lhs, StambiaDateWrapper rhs)
        //{
        //    return (lhs > rhs) || (lhs == rhs);
        //}
    }
}
