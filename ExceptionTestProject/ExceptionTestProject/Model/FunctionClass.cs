using System;
using System.Collections.Generic;
using System.Text;

namespace ExceptionTestProject.Model
{
    public class FunctionClass
    {
        public void DoWork(bool isVallid)
        {
            if (!isVallid)
            {
                throw new InvalidOperationException("Invalid operation. The method call is invalid for the objects current state.");
            }
        }

        public void Process(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (input.Length < 3)
                throw new ArgumentException("Input too short");
        }
            
    }
}
