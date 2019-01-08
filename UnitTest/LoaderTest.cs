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
    }
}
