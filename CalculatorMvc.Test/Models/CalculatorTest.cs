using CalculatorMvc.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorMvc.Test.Models
{
    public class CalculatorTest
    {
        private string _expression;
        private string GetExpression;
        private Calculator _calculator;

        private Dictionary<char, int> operationPriority = new()
        {
            {'(', 0},
            {'+', 1},
            {'-', 1},
            {'*', 2},
            {'/', 2},
            {'^', 3},
            {'~', 4}
        };

        public CalculatorTest(string expression = "1+1")
        {
            _expression = expression;
            _calculator = new Calculator(_expression);
            GetExpression = _calculator.ToPostfix(_expression);
        }

        [Fact]
        public void Calculator_GetStringNumber_ReturnSuccess()
        {
            // Arrange
            int pos = 0;

            // Act
            var result = _calculator.GetStringNumber(_expression, ref pos);

            // Assert
            result.Should().BeOfType<string>();
            result.Should().Be("1");

        }

        [Fact]
        public void Calculator_ToPostfix_ReturnSuccess()
        {
            // Arrange

            // Act
            var result = _calculator.ToPostfix(_expression);

            // Assert
            result.Should().BeOfType<string>();
            result.Should().Be("1 1 +");
        }

        [Fact]
        public void Calculator_Execute_ReturnSuccess()
        {
            // Arrange
            var op = '+';
            var first = 1;
            var second = 1;

            // Act
            var result = _calculator.Execute(op, first, second);

            // Assert
            result.Should().Be(2);
        }
        [Fact]
        public void Calculator_Calculate_ReturnSuccess()
        {
            // Arrange

            // Act
            var result = _calculator.Calculate();

            // Assert
            result.Should().Be(2);
        }


    }
}
