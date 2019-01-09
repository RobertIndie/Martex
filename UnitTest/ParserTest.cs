﻿using Xunit;
using MarTex;
using MarTex.Loader;
using MarTex.Parser;

namespace UnitTest
{
    public class ParserTest
    {
        [Fact]
        public void ParseContent()
        {
            Parser parser = new Parser()
            {
                content = new Content()
                {
                    rawText = @"\
Hello\nI am Aaron Robert.\
I use \\"
                }
            };
            Assert.Equal("Hello\nI am Aaron Robert.I use \\", parser.Escape());
            parser.content.rawText = @"There are no diagonal bars here.";
            Assert.Equal("There are no diagonal bars here.", parser.Escape());
        }

        [Fact]
        public void ParseText()
        {
            Parser parser = new Parser();
            string text = "Hello,This is **Bold**.**a**";
            var e = parser.ParseText(text);
        }
    }
}
