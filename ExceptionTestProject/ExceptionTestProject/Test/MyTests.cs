using ExceptionTestProject.Model;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ExceptionTestProject.Test
{
    public class MyTests
    {
        FunctionClass methods;
        [SetUp]
        public void initFunctionClass()
        {
            methods = new FunctionClass(); 
        }

        [Test]
        public void DoWork_WhenInvalid_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => methods.DoWork(false));
        }

        [Test]
        public void DoWork_throwException_WithCorrectMessage()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => methods.DoWork(false));
            Assert.That(ex.Message, Is.EqualTo("Invalid operation. The method call is invalid for the objects current state."));
        }

        [Test]
        public void DoWork_WhenValid_DoesNotThrow()
        {
            Assert.Ignore("Method is not yet implemented.");
            Assert.DoesNotThrow(() => methods.DoWork(true));
        }

        [Test]
        public void process_WhenNull_throwsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => methods.Process(null));
        }
        [Test]
        public void process_WhenTooShort_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => methods.Process("ab"));
        }
        [Test]
        public void process_WhenGivenCorrectvalue_DoesNotThrow()
        {
            Assert.Ignore("this is not part of the exercises");
            Assert.DoesNotThrow(() => methods.Process("Luhlelo"));
        }
    }
}
