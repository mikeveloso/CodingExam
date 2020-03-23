using System;
using System.Collections.Generic;
using System.Text;

namespace CodingExam.Common
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public class AppSettingSectionAttribute : Attribute
    {
        public AppSettingSectionAttribute(string sectionName, string sectionKey)
        {
            SectionName = sectionName;
            SectionKey = sectionKey;
        }

        public string SectionName { get; set; }

        public string SectionKey { get; set; }
    }
}
