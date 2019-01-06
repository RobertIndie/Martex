using System;
using Xunit;
using MarTex;
using MarTex.Loader;
using MarTex.Parser;

namespace UnitTest
{
    public class LoaderTest
    {
        [Fact]
        public Content LoadFile()
        {
            ContentLoader loader = LoaderFactory.CreateFileLoader(@"../test.md");
            Content content = loader.Load();
            return content;
        }

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
    }
}
