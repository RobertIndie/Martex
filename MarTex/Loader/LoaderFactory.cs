using System;
using System.Collections.Generic;
using System.Text;

namespace MarTex.Loader
{
    public class LoaderFactory
    {
        public static ContentLoader CreateFileLoader(string path)
        {
            FileLoader loader = new FileLoader
            {
                filePath = path
            };
            return loader;
        }
    }
}
