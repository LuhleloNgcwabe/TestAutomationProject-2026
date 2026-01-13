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

        [Test,Category("regression")]
        public void DoWork_WhenInvalid_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => methods.DoWork(false));
        }

        [Test, Category("regression")]
        public void DoWork_throwException_WithCorrectMessage()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => methods.DoWork(false));
            Assert.That(ex.Message, Is.EqualTo("Invalid operation. The method call is invalid for the objects current state."));
        }

        [Test, Category("regression")]
        public void DoWork_WhenValid_DoesNotThrow()
        {
            Assert.Ignore("Method is not yet implemented.");
            Assert.DoesNotThrow(() => methods.DoWork(true));
        }

        [Test, Category("regression")]
        public void process_WhenNull_throwsArgumentNullException()
        {
            //Assert.Throws<ArgumentNullException>(() => methods.Process(null));
            Assert.That(() => methods.Process(null), Throws.TypeOf<ArgumentNullException>());
        }
        [Test, Category("regression")]
        public void process_WhenTooShort_ThrowsArgumentException()
        {
            //Assert.Throws<ArgumentException>(() => methods.Process("ab"));
            Assert.That(()=>methods.Process("ab"),Throws.TypeOf<ArgumentException>());
        }
        [Test, Category("regression")]
        public void process_WhenGivenCorrectvalue_DoesNotThrow()
        {
            Assert.Ignore("this is not part of the exercises");
            Assert.DoesNotThrow(() => methods.Process("Luhlelo"));
        }
        //Retry and Exceptions
        private static int _attempt = 0;
        [Test]
        [Retry(2)]
        [Category("smoke")]
        public void RetryExampleTest()
        {
            //Thsi test 2 fails on the first 2 attempt. and it passes on third attempt
            _attempt++;
            TestContext.Out.WriteLine($"Attemp {_attempt}");
            if (_attempt < 3) 
            {
              Assert.Throws<ArgumentException>(()=> new Exception("Temporary failure"));
            }
        }

        [Test,Category("smoke")]
        [Retry(3)]
        public void RetryExampleTest2() {
            TestContext.Out.WriteLine($"Attempts: {TestContext.CurrentContext.CurrentRepeatCount+1}");
            Assert.Fail("For retry demo");
        }
    }
}
