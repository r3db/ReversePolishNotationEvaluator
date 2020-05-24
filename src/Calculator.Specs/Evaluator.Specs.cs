using System;
using System.IO;
using System.Text;
using Xunit;

namespace Calculator
{
    // Todo: 90% of the tests are missing!
    public class EvaluatorSpecs
    {
        [Fact]
        public void ShouldEvaluateCorrectlyWhenUsingAddCommand()
        {
            // Arrange
            var input = "   8   7 +  ";

            // Act
            var result = Evaluate(input);

            // Assert
            Assert.Equal(15, (decimal)result);
        }

        [Fact]
        public void ShouldEvaluateCorrectlyWhenUsingMultipleAddCommands()
        {
            // Arrange
            var input = "  99   11      + 8 7 + +";

            // Act
            var result = Evaluate(input);

            // Assert
            Assert.Equal(125, (decimal)result);
        }

        [Fact]
        public void ShouldEvaluateCorrectlyWhenUsingMultiplyCommand()
        {
            // Arrange
            var input = "4 7 *";

            // Act
            var result = Evaluate(input);

            // Assert
            Assert.Equal(28, (decimal)result);
        }

        [Fact]
        public void ShouldEvaluateCorrectlyWhenUsingMultipleMultiplyCommands()
        {
            // Arrange
            var input = "  4 7 * 5 2 * * ";

            // Act
            var result = Evaluate(input);

            // Assert
            Assert.Equal(280, (decimal)result);
        }

        [Fact]
        public void ShouldEvaluateCorrectlyWhenUsingSubtractCommand()
        {
            // Arrange
            var input = "8 3 -";

            // Act
            var result = Evaluate(input);

            // Assert
            Assert.Equal(5, (decimal)result);
        }

        [Fact]
        public void ShouldEvaluateCorrectlyWhenUsingMultipleSubtractCommands()
        {
            // Arrange
            var input = "33 3 - 10 6 - -";

            // Act
            var result = Evaluate(input);

            // Assert
            Assert.Equal(26, (decimal)result);
        }

        [Fact]
        public void ShouldEvaluateCorrectlyWhenUsingDivideCommand()
        {
            // Arrange
            var input = "36 9 / ";

            // Act
            var result = Evaluate(input);

            // Assert
            Assert.Equal(4, (decimal)result);
        }

        [Fact]
        public void ShouldEvaluateCorrectlyWhenUsingMultipleDivideCommands()
        {
            // Arrange
            var input = "33 3 - 10 6 - -";

            // Act
            var result = Evaluate(input);

            // Assert
            Assert.Equal(26, (decimal)result);
        }

        // Helpers
        private static object Evaluate(string input)
        {
            return new Evaluator(new Lexer(new MemoryStream(Encoding.UTF8.GetBytes(input)))).Evaluate();
        }
    }
}