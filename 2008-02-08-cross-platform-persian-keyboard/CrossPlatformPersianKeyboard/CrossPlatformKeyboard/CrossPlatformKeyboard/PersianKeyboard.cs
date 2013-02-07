/// <summary>
///   (The MIT License)
///   
///   Copyright (c) 2008 M.S. Babaei
///   
///   Permission is hereby granted, free of charge, to any person obtaining a copy
///   of this software and associated documentation files (the "Software"), to deal
///   in the Software without restriction, including without limitation the rights
///   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
///   copies of the Software, and to permit persons to whom the Software is
///   furnished to do so, subject to the following conditions:
///   
///   The above copyright notice and this permission notice shall be included in
///   all copies or substantial portions of the Software.
///   
///   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
///   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
///   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
///   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
///   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
///   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
///   THE SOFTWARE.
/// </summary>


using System;
using System.Windows.Forms;

namespace CrossPlatformKeyboard
{
    public class PersianKeyboard
    {
        public bool shiftPressed = false;
        public bool usePersianNums = false;

        public PersianKeyboard()
        {

        }

        public void TransformInputChar(TextBox textBox, KeyPressEventArgs e)
        {
            int pos = textBox.SelectionStart;
            e.Handled = true;

            if (!shiftPressed)
            {
                #region Normal Keys

                switch (e.KeyChar.ToString().ToUpper())
                {
                    case "'":   //  گ
                        textBox.Text = textBox.Text.Insert(pos, "\u06AF");
                        break;
                    case ",":   //  و
                        textBox.Text = textBox.Text.Insert(pos, "\u0648");
                        break;
                    case ";":   //  ك
                        textBox.Text = textBox.Text.Insert(pos, "\u0643");
                        break;
                    case "A":   //  ش
                        textBox.Text = textBox.Text.Insert(pos, "\u0634");
                        break;
                    case "B":   //  ذ
                        textBox.Text = textBox.Text.Insert(pos, "\u0630");
                        break;
                    case "C":   //  ز
                        textBox.Text = textBox.Text.Insert(pos, "\u0632");
                        break;
                    case "D":   //  ي
                        textBox.Text = textBox.Text.Insert(pos, "\u064A");
                        break;
                    case "E":   //  ث
                        textBox.Text = textBox.Text.Insert(pos, "\u062B");
                        break;
                    case "F":   //  ب
                        textBox.Text = textBox.Text.Insert(pos, "\u0628");
                        break;
                    case "G":   //  ل
                        textBox.Text = textBox.Text.Insert(pos, "\u0644");
                        break;
                    case "H":   //  ا
                        textBox.Text = textBox.Text.Insert(pos, "\u0627");
                        break;
                    case "I":   //  ه
                        textBox.Text = textBox.Text.Insert(pos, "\u0647");
                        break;
                    case "J":   //  ت
                        textBox.Text = textBox.Text.Insert(pos, "\u062A");
                        break;
                    case "K":   //  ن
                        textBox.Text = textBox.Text.Insert(pos, "\u0646");
                        break;
                    case "L":   //  م
                        textBox.Text = textBox.Text.Insert(pos, "\u0645");
                        break;
                    case "M":   //  ئ
                        textBox.Text = textBox.Text.Insert(pos, "\u0626");
                        break;
                    case "N":   //  د
                        textBox.Text = textBox.Text.Insert(pos, "\u062F");
                        break;
                    case "O":   //  خ
                        textBox.Text = textBox.Text.Insert(pos, "\u062E");
                        break;
                    case "P":   //  ح
                        textBox.Text = textBox.Text.Insert(pos, "\u062D");
                        break;
                    case "Q":   //  ض
                        textBox.Text = textBox.Text.Insert(pos, "\u0636");
                        break;
                    case "R":   //  ق
                        textBox.Text = textBox.Text.Insert(pos, "\u0642");
                        break;
                    case "S":   //  س
                        textBox.Text = textBox.Text.Insert(pos, "\u0633");
                        break;
                    case "T":   //  ف
                        textBox.Text = textBox.Text.Insert(pos, "\u0641");
                        break;
                    case "U":   //  ع
                        textBox.Text = textBox.Text.Insert(pos, "\u0639");
                        break;
                    case "V":   //  ر
                        textBox.Text = textBox.Text.Insert(pos, "\u0631");
                        break;
                    case "W":   //  ص
                        textBox.Text = textBox.Text.Insert(pos, "\u0635");
                        break;
                    case "X":   //  ط
                        textBox.Text = textBox.Text.Insert(pos, "\u0637");
                        break;
                    case "Y":   //  غ
                        textBox.Text = textBox.Text.Insert(pos, "\u063A");
                        break;
                    case "Z":   //  ظ
                        textBox.Text = textBox.Text.Insert(pos, "\u0638");
                        break;
                    case "[":   //  ج
                        textBox.Text = textBox.Text.Insert(pos, "\u062C");
                        break;
                    case "\\":   // ژ
                        // or use
                        // case @"\":
                        textBox.Text = textBox.Text.Insert(pos, "\u0698");
                        break;
                    case "]":   //  چ
                        textBox.Text = textBox.Text.Insert(pos, "\u0686");
                        break;
                    case "`":   //  پ
                        textBox.Text = textBox.Text.Insert(pos, "\u067E");
                        break;
                    case "0":   //  ٠ | ۰
                        textBox.Text = textBox.Text.Insert(pos, !usePersianNums ? "\u0660" : "\u06F0");
                        break;
                    case "1":   //  ١ | ۱
                        textBox.Text = textBox.Text.Insert(pos, !usePersianNums ? "\u0661" : "\u06F1");
                        break;
                    case "2":   //  ٢ | ۲
                        textBox.Text = textBox.Text.Insert(pos, !usePersianNums ? "\u0662" : "\u06F2");
                        break;
                    case "3":   //  ٣ | ۳
                        textBox.Text = textBox.Text.Insert(pos, !usePersianNums ? "\u0663" : "\u06F3");
                        break;
                    case "4":   //  ٤ | ۴
                        textBox.Text = textBox.Text.Insert(pos, !usePersianNums ? "\u0664" : "\u06F4");
                        break;
                    case "5":   //  ٥ | ۵
                        textBox.Text = textBox.Text.Insert(pos, !usePersianNums ? "\u0665" : "\u06F5");
                        break;
                    case "6":   //  ٦ | ۶
                        textBox.Text = textBox.Text.Insert(pos, !usePersianNums ? "\u0666" : "\u06F6");
                        break;
                    case "7":   //  ٧ | ۷
                        textBox.Text = textBox.Text.Insert(pos, !usePersianNums ? "\u0667" : "\u06F7");
                        break;
                    case "8":   //  ٨ | ۸
                        textBox.Text = textBox.Text.Insert(pos, !usePersianNums ? "\u0668" : "\u06F8");
                        break;
                    case "9":   //  ٩ | ۹
                        textBox.Text = textBox.Text.Insert(pos, !usePersianNums ? "\u0669" : "\u06F9");
                        break;
                    default:
                        e.Handled = false;
                        break;
                }

                #endregion
            }
            else
            {
                #region Mixed Keys with Shift

                switch (e.KeyChar.ToString().ToUpper())
                {
                    case ",":   //  ؤ
                        textBox.Text = textBox.Text.Insert(pos, "\u0624");
                        break;
                    case "/":   //  ؟
                        textBox.Text = textBox.Text.Insert(pos, "\u061F");
                        break;
                    case "A":   //   ََ
                        textBox.Text = textBox.Text.Insert(pos, "\u064E");
                        break;
                    case "B":   //  إ
                        textBox.Text = textBox.Text.Insert(pos, "\u0625");
                        break;
                    case "C":   //  Nothing
                        break;
                    case "D":   //   ِِ
                        textBox.Text = textBox.Text.Insert(pos, "\u0650");
                        break;
                    case "E":   //   ٍٍ
                        textBox.Text = textBox.Text.Insert(pos, "\u064D");
                        break;
                    case "F":   //   ّّ
                        textBox.Text = textBox.Text.Insert(pos, "\u0651");
                        break;
                    case "G":   //   ْْ
                        textBox.Text = textBox.Text.Insert(pos, "\u0652");
                        break;
                    case "H":   //  آ
                        textBox.Text = textBox.Text.Insert(pos, "\u0622");
                        break;
                    case "I":   //  ة
                        textBox.Text = textBox.Text.Insert(pos, "\u0629");
                        break;
                    case "J":   //  ـ
                        textBox.Text = textBox.Text.Insert(pos, "\u0640");
                        break;
                    case "K":   //  »
                        textBox.Text = textBox.Text.Insert(pos, "\u00BB");
                        break;
                    case "L":   //  «
                        textBox.Text = textBox.Text.Insert(pos, "\u00AB");
                        break;
                    case "M":   //  ء
                        textBox.Text = textBox.Text.Insert(pos, "\u0621");
                        break;
                    case "N":   //  أ
                        textBox.Text = textBox.Text.Insert(pos, "\u0623");
                        break;
                    case "O":   //  ×
                        textBox.Text = textBox.Text.Insert(pos, "\u00D7");
                        break;
                    case "P":   //  ÷
                        textBox.Text = textBox.Text.Insert(pos, "\u00F7");
                        break;
                    case "Q":   //   ًً
                        textBox.Text = textBox.Text.Insert(pos, "\u064b");
                        break;
                    case "R":   //  Nothing
                        break;
                    case "S":   //   ُُ
                        textBox.Text = textBox.Text.Insert(pos, "\u064F");
                        break;
                    case "T":   //  ،
                        textBox.Text = textBox.Text.Insert(pos, "\u060C");
                        break;
                    case "U":   //  Nothing
                        break;
                    case "V":   //  ی
                        textBox.Text = textBox.Text.Insert(pos, "\u06CC");
                        break;
                    case "W":   //   ٌٌ
                        textBox.Text = textBox.Text.Insert(pos, "\u064C");
                        break;
                    case "X":   //  ]
                        textBox.Text = textBox.Text.Insert(pos, "\u005D");
                        break;
                    case "Y":   //  ؛
                        textBox.Text = textBox.Text.Insert(pos, "\u061B");
                        break;
                    case "Z":   //  [
                        textBox.Text = textBox.Text.Insert(pos, "\u005B");
                        break;
                    case "[":   //  {
                        textBox.Text = textBox.Text.Insert(pos, "\u007B");
                        break;
                    case "]":   //  }
                        textBox.Text = textBox.Text.Insert(pos, "\u007D");
                        break;
                    case "`":   //  ى
                        textBox.Text = textBox.Text.Insert(pos, "\u0060");
                        break;
                    default:
                        e.Handled = false;
                        break;
                }
                shiftPressed = false;

                #endregion
            }

            if (e.Handled)
                textBox.Select(pos + 1, 0);
        }

    }
}

