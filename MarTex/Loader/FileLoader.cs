using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MarTex.Loader
{
    class FileLoader:ContentLoader
    {
        public string filePath;
        public override Content Load()
        {
            Content result = new Content();
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException();
            }
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            StreamReader streamReader = new StreamReader(fileStream);
            result.content.AddRange(streamReader.ReadToEnd().Split('\n'));
            streamReader.Close();
            fileStream.Close();
            return result;
        }
    }
}
