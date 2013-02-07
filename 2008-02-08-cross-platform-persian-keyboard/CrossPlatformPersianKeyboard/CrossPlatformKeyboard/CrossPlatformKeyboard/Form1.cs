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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CrossPlatformKeyboard; // Not necessary in this case, because the namespace of project is the same as the namespace of PersianKeyboard.cs

namespace CrossPlatformKeyboard
{
    public partial class Form1 : Form
    {
        PersianKeyboard kb = new PersianKeyboard();

        public Form1()
        {
            InitializeComponent();

            kb.usePersianNums = false;
        }

        private void txtPersian_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            kb.shiftPressed = e.Shift;
        }

        private void txtPersian_KeyPress(object sender, KeyPressEventArgs e)
        {
            kb.TransformInputChar(txtPersian, e);
        }
    }
}

