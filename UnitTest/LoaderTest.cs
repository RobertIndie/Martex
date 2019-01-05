using System;
using Xunit;
using MarTex;
using MarTex.Loader;

namespace UnitTest
{
    public class LoaderTest
    {
        [Fact]
        public void LoadFile()
        {
            ContentLoader loader = LoaderFactory.CreateFileLoader(@"../test.md");
            Console.WriteLine(Environment.CurrentDirectory);
        }
    }
}
