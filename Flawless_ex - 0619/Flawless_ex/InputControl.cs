using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flawless_ex
{
    class InputControl
    {
        string kana;

        public string Kana(string a)
        {
            kana = Microsoft.VisualBasic.Strings.StrConv(a, Microsoft.VisualBasic.VbStrConv.Katakana | Microsoft.VisualBasic.VbStrConv.Narrow, 0x411);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(kana);

            a = stringBuilder.ToString();

            return a;
        }
    }
}
