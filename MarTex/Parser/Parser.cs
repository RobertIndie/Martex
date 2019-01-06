using System;
using System.Collections.Generic;
using System.Text;
using MarTex.Loader;

namespace MarTex.Parser
{
    public class Parser
    {
        public Content content;
        public Dictionary<char, char> excapeCharList = new Dictionary<char, char>()
        {
            { 'n','\n'},
            { '\n','\0'},
            { '\\','\\'}
        };
        public void Escape()
        {
            char[] newStr = new char[content.rawText.Length];
            int p = 0;
            for(int i = 0; i < content.rawText.Length; i++)
            {
                char c = content.rawText[i];
                if (c == '\\' && i + 1 != content.rawText.Length) 
                {
                    char cp = content.rawText[i + 1];
                    if (excapeCharList.ContainsKey(c))
                    {
                        cp = excapeCharList[cp];
                        newStr[p] = cp;
                        p++;
                        i++;
                    }
                    c = char.MinValue;
                }
                else
                {
                    newStr[p] = c;
                    p++;
                }
            }
            content.rawText = new string(newStr).TrimEnd();
        }
    }
}
