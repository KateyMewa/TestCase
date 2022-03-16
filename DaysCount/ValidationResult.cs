using System;
using System.Collections.Generic;
using System.Text;

namespace DaysCount
{
    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public IEnumerable<string> ValidationErrors { get; set; }
    }
}
