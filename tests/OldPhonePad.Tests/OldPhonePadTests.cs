using System;
using Xunit;
using IronSoftware.PhoneKeypad;

namespace OldPhonePad.Tests
{
    public class OldPhonePadTests
    {
        [Theory]
        [InlineData("2#", "A")]
        [InlineData("22#", "B")]
        [InlineData("222#", "C")]
        [InlineData("2222#", "A")]
        [InlineData("33#", "E")]
        [InlineData("333#", "F")]
        public void Decode_SingleButton_ReturnsCorrectLetter(string input, string expected)
        {
            var result = IronSoftware.PhoneKeypad.OldPhonePad.Decode(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("4433555 555666#", "HELLO")]
        [InlineData("9999666 666 66#", "ZOON")]
        [InlineData("7777#", "S")]
        [InlineData("77777#", "P")]
        public void Decode_MultipleButtons_ReturnsCorrectWord(string input, string expected)
        {
            var result = IronSoftware.PhoneKeypad.OldPhonePad.Decode(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("2 2#", "AA")]
        [InlineData("222 2 22#", "CAB")]
        [InlineData("33 3#", "ED")]
        public void Decode_WithPauses_HandlesSameButtonCharacters(string input, string expected)
        {
            var result = IronSoftware.PhoneKeypad.OldPhonePad.Decode(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("227*#", "B")]
        [InlineData("222**#", "")]
        [InlineData("444433*33555#", "GEL")]
        [InlineData("2*#", "")]
        public void Decode_WithBackspace_DeletesLastCharacter(string input, string expected)
        {
            var result = IronSoftware.PhoneKeypad.OldPhonePad.Decode(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("8 88777444666*664#", "TURING")]
        [InlineData("0#", " ")]
        [InlineData("00#", " ")]
        [InlineData("1#", "&")]
        [InlineData("11#", "'")]
        [InlineData("111#", "(")]
        public void Decode_SpecialKeys_WorksCorrectly(string input, string expected)
        {
            var result = IronSoftware.PhoneKeypad.OldPhonePad.Decode(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("222333444#", "CFI")]
        [InlineData("4433555 555666#", "HELLO")]
        [InlineData("96667775553#", "WORLD")]
        public void Decode_ComplexInputs_WorksCorrectly(string input, string expected)
        {
            var result = IronSoftware.PhoneKeypad.OldPhonePad.Decode(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Decode_EmptyInput_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => IronSoftware.PhoneKeypad.OldPhonePad.Decode(""));
        }

        [Fact]
        public void Decode_NullInput_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => IronSoftware.PhoneKeypad.OldPhonePad.Decode(null!));
        }

        [Theory]
        [InlineData("222")]
        [InlineData("222*")]
        [InlineData("44433555 555666")]
        public void Decode_InputWithoutSend_ThrowsArgumentException(string input)
        {
            Assert.Throws<ArgumentException>(() => IronSoftware.PhoneKeypad.OldPhonePad.Decode(input));
        }

        [Theory]
        [InlineData("2a3#")]
        [InlineData("22!3#")]
        [InlineData("22@#")]
        public void Decode_InvalidCharacters_ThrowsArgumentException(string input)
        {
            Assert.Throws<ArgumentException>(() => IronSoftware.PhoneKeypad.OldPhonePad.Decode(input));
        }

        [Theory]
        [InlineData("#", "")]
        [InlineData("*#", "")]
        [InlineData("**#", "")]
        public void Decode_EdgeCases_HandlesGracefully(string input, string expected)
        {
            var result = IronSoftware.PhoneKeypad.OldPhonePad.Decode(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Decode_LongInput_WorksCorrectly()
        {
            string input = "4433555 555666 0 96667775553#";
            string expected = "HELLO WORLD";
            var result = IronSoftware.PhoneKeypad.OldPhonePad.Decode(input);
            Assert.Equal(expected, result);
        }
    }
}
