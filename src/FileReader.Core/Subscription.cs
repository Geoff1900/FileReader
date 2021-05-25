using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace FileReader.Core
{
    public class Subscription : IExcelReadable
    {
        public Subscription() { }

        public string Name { get; set; }
        public string Email { get; set; }
        public string NotificationEmail { get; set; }
        public bool AADGroup { get; set; }
        public string SubscriptionLevel { get; set; }
        public DateTimeOffset AssignedDate { get; set; }
        public bool Activated { get; set; }
        public DateTimeOffset Expiration { get; set; }
        public string Reference { get; set; }
        public bool Download { get; set; }
        public string Country { get; set; }
        public string Language { get; set; }
        public string SubscriptionStatus { get; set; }
        public Guid SubscriptionGuid { get; set; }
        public string UsageStatus { get; set; }

        public void DeSerialize(IExcelDataReader reader, Dictionary<string, int> columns, int count)
        {
            var properties = GetType().GetProperties();
            for (var i = 0; i < properties.Length; i++)
            {
                var propertyType = properties[i].PropertyType;
                var propertyValue = reader.GetString(i);
                if (string.IsNullOrWhiteSpace(propertyValue)) break;
                properties[i].SetValue(this, TypeDescriptor.GetConverter(propertyType).ConvertFrom(propertyValue));
                Console.WriteLine(TypeDescriptor.GetConverter(propertyType).ConvertFrom(propertyValue));
            }
        }
    }
}
    // TypeDescriptor.GetConverter(propertyType).ConvertFrom(propertyValue);
    //private dynamic ParsePropertyByType(Type type, string value) =>
    //    type.Name switch {
    //                 nameof(Boolean) => bool.Parse(value),
    //                 nameof(DateTimeOffset) => DateTimeOffset.Parse(value),
    //                 nameof(Guid) => Guid.Parse(value),
    //                 nameof(Int32) => int.Parse(value),
    //                 _ => value // or default to retuning a string value.
    //     };
    //properties[i].SetValue(this, propertyValue);
    //var properties = typeof(Subscription).GetProperties();
    //var email = GetType().GetProperty(nameof(Email));
    //email.SetValue(this, reader.GetString(columns["Email"]));
    //properties[i].SetValue(this, reader.GetString(i));
     
        
