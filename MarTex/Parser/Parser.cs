using System;
using System.Collections.Generic;
using System.Text;
using MarTex.Loader;
using System.Text.RegularExpressions;
using System.Xml;

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
        public string Escape()
        {
            char[] newStr = new char[content.rawText.Length];
            int p = 0;
            for(int i = 0; i < content.rawText.Length; i++)
            {
                char c = content.rawText[i];
                if (c == '\\' && i + 1 != content.rawText.Length)
                {
                    char cp = content.rawText[i + 1];
                    if (cp == '\r') //windows system end line
                    {
                        i++;
                        cp = content.rawText[i + 1];
                    }
                    if (excapeCharList.ContainsKey(cp))
                    {
                        cp = excapeCharList[cp];
                    }
                    if (cp != '\0')//remove \0
                    {
                        newStr[p] = cp;
                        p++;
                    }
                    i++;
                    c = char.MinValue;
                }
                else if (c != '\0') 
                {
                    newStr[p] = c;
                    p++;
                }
            }
            content.rawText = p == content.rawText.Length ? new string(newStr) : new string(newStr).Remove(p);
            return content.rawText;
        }

        public Dictionary<string, (string find_pattern, string replace_patter)> textPattern = new Dictionary<string, (string find_pattern, string replace_patter)>()
        {
            { "Bold",(@"\*{2}(.*)\*{2}",@"$1")}
        };
        public XmlElement ParseText(string text)
        {
            XmlDocument doc = new XmlDocument();
            var root = doc.CreateElement("Root");
            root.InnerXml = "<Fuck>you</Fuck>";
            doc.AppendChild(root);
            var s = root.InnerXml;
            foreach (var pattern in textPattern)
            {
                Regex rgx = new Regex(pattern.Key);
                
            }
            return null;
        }

        public void ParseFramework()
        {
            
        }
    }
}
