using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarInsurance
{
    public class InternationalPassport
    {
        public string CountryCode { get; set; }
        public string IdNumber { get; set; }
        public string GivenNames { get; set; }
        public string Surname { get; set; }
        public string DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string Gender { get; set; }
        public string DateOfIssue { get; set; }
        public string ExpiryDate { get; set; }
        public string MrzLine1 { get; set; }
        public string MrzLine2 { get; set; }


        public static InternationalPassport ParseDocument(string input)
        {
            var page0Index = input.IndexOf("Page 0");
            if (page0Index == -1)
                throw new ArgumentException("Input does not contain 'Page 0' section");

            var relevantPart = input.Substring(page0Index);

            string GetField(string fieldName)
            {
                var search = $":{fieldName}:";
                var startIndex = relevantPart.IndexOf(search);
                if (startIndex == -1) return null;

                startIndex += search.Length;
                var endIndex = relevantPart.IndexOf('\n', startIndex);
                if (endIndex == -1) endIndex = relevantPart.Length;

                return relevantPart.Substring(startIndex, endIndex - startIndex).Trim();
            }

            return new InternationalPassport
            {
                CountryCode = GetField("Country Code"),
                IdNumber = GetField("ID Number"),
                GivenNames = GetField("Given Name(s)"),
                Surname = GetField("Surname"),
                DateOfBirth = GetField("Date of Birth"),
                PlaceOfBirth = GetField("Place of Birth"),
                Gender = GetField("Gender"),
                DateOfIssue = GetField("Date of Issue"),
                ExpiryDate = GetField("Expiry Date"),
                MrzLine1 = GetField("MRZ Line 1"),
                MrzLine2 = GetField("MRZ Line 2"),
            };
        }
        public string GetData()
        {
            return
            $"Country Code: {CountryCode}\n" +
            $"ID Number: {IdNumber}\n" +
            $"Given Name(s): {GivenNames}\n" +
            $"Surname: {Surname}\n" +
            $"Date of Birth: {DateOfBirth}\n" +
            $"Place of Birth: {PlaceOfBirth}\n" +
            $"Gender: {Gender}\n" +
            $"Date of Issue: {DateOfIssue}\n" +
            $"Expiry Date: {ExpiryDate}\n" +
            $"MRZ Line 1: {MrzLine1}\n" +
            $"MRZ Line 2: {MrzLine2}";
        }
    }
}
