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

        public Dictionary<string, (string find_pattern, Func<Match,string> parse)> textPattern = new Dictionary<string, (string, Func<Match, string>)>()
        {
            { "Bold",(@"\*{2}(?=[^\s\*])(.*?)([^\s\*])\*{2}",(match)=> string.Format("<Bold>{0}{1}</Bold>","$1","$2"))},//搞不明白，为啥用(match)=> "<Bold>$0$1</Bold>"就乱套了？
                                                                                                                       //而且为啥用$0只能取到缺失最后一个字符的内容？
        };
        public XmlElement ParseText(string text)
        {
            //XmlDocument doc = new XmlDocument();
            //var root = doc.CreateElement("Root");
            //root.InnerXml = "<Fuck>you</Fuck>";
            //doc.AppendChild(root);
            //var s = root.InnerXml;
            XmlDocument doc = new XmlDocument();
            XmlElement element = doc.CreateElement("Text");
            foreach (var pattern in textPattern)
            {
                Regex rgx = new Regex(pattern.Value.find_pattern);
                MatchCollection matches = rgx.Matches(text);
                foreach(var match in matches)
                    text = element.InnerXml = rgx.Replace(text, pattern.Value.parse((Match)match),1);
            }
            return element;
        }

        public void ParseFramework()
        {
            
        }
    }
}
