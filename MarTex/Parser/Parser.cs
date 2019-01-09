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

        public Dictionary<string, (string find_pattern, Func<Match,string,string> parse)> textPattern = new Dictionary<string, (string, Func<Match,string, string>)>()
        {
            { "Bold",(@"\*{2}(?=[^\s\*])(?<content>.*?)(?:<fuck>[^\s\*])\*{2}",(match,format)=> string.Format(format,match.Groups["fuck"]))}
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
                StringBuilder format_sb = new StringBuilder();
                format_sb.AppendFormat("<{0}>{{0}}</{0}>", pattern.Key);
                Regex rgx = new Regex(pattern.Value.find_pattern);
                var match = rgx.Match(text);
                element.InnerXml = rgx.Replace(text, pattern.Value.parse(match,format_sb.ToString()));
            }
            return element;
        }

        public void ParseFramework()
        {
            
        }
    }
}
