using Xunit;
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
            Assert.Equal("Hello,This is <Bold>Bold</Bold>.<Bold>a</Bold>", parser.ParseText("Hello,This is **Bold**.**a**").InnerXml);
            Assert.Equal(@"Correct:
correct regex:
\\*{2}(?=[^\\s\\*])(.*?)([^\\s\\*])\\*{2}

Lorem **ipsum** dolor **A** *sit* amet, *consectetur *****
*adipiscing elit, sed do **eiusmod tempor incididunt ut.**


sion & **Test** to **matches sa d* *******@*(101 = sa iojads iojas opjkas    _** .
****TES   T******
**TEST * TEST**
**test**
**test.s  sddas ijdsoiaj 12@!@@#*43d**
**test test**
***ts**

Incorrect:

Multiline **bolded*
** test**
**test ***

One case to handle:
**g**


", parser.ParseText(@"Correct:
correct regex:
\\*{2}(?=[^\\s\\*])(.*?)([^\\s\\*])\\*{2}

Lorem <Bold>ipsum</Bold> dolor <Bold>A</Bold> *sit* amet, *consectetur *****
*adipiscing elit, sed do <Bold>eiusmod tempor incididunt ut.</Bold>


sion & <Bold>Test</Bold> to <Bold>matches sa d* *******@*(101 = sa iojads iojas opjkas    _</Bold> .
**<Bold>TES   T</Bold>****
<Bold>TEST * TEST</Bold>
<Bold>test</Bold>
<Bold>test.s  sddas ijdsoiaj 12@!@@#*43d</Bold>
<Bold>test test</Bold>
*<Bold>ts</Bold>

Incorrect:

Multiline **bolded*
** test**
**test ***

One case to handle:
<Bold>g</Bold>


").InnerXml);
        }
    }
}
