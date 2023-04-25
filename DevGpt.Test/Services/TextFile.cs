using Xunit;

namespace DevGpt.Console.SampleApplication.Tests
{
    public class CalculatorTests
    {
        [Fact]
        public void AddTest()
        {
            Calculator calculator = new Calculator();
            Assert.Equal(4, calculator.Add(2, 2));
        }

        [Fact]
        public void SubtractTest()
        {
            Calculator calculator = new Calculator();
            Assert.Equal(1, calculator.Subtract(3, 2));
        }

        [Fact]
        public void MultiplyTest()
        {
            Calculator calculator = new Calculator();
            Assert.Equal(6, calculator.Multiply(2, 3));
        }

        [Fact]
        public void DivideTest()
        {
            Calculator calculator = new Calculator();
            Assert.Equal(2, calculator.Divide(4, 2));
        }

        [Fact]
        public void DivideByZeroTest()
        {
            Calculator calculator = new Calculator();
            Assert.Throws<DivideByZeroException>(() => calculator.Divide(4, 0));
        }
    }
}