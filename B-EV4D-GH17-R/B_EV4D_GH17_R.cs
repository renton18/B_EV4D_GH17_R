﻿using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_EV4D_GH17_R
{
    public static class B_EV4D_GH17_R
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private extern static System.IntPtr CreateFont(int nHeight, int nWidth, int nEscapement, int nOrientation, int fnWeight, bool fdwItalic,
            bool fdwUnderline, bool fdwStrikeOut, int fdwCharSet, int fdwOutputPrecision, int fdwClipPrecision, int fdwQuality, int fdwPitchAndFamily, string lpszFace);
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private extern static System.IntPtr SelectObject(System.IntPtr hObject, System.IntPtr hFont);
        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        private extern static int TextOut(IntPtr hdc, int nXStart, int nYStart, string lpString, int cbString);
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private extern static bool DeleteObject(System.IntPtr hObject);

        public enum BARCODE_FONT
        {
            CODE39,
            CODE128,
            NW7
        }
        public enum PRINT_PATTERN
        {
            PATTERN1,
            PATTERN2
        }

        public static string printerName;
        public static string[] datas;
        public static BARCODE_FONT font;
        public static PRINT_PATTERN type;

        private static string barcodeFont;
        private static string printType;

        public static void Print()
        {
            switch (font)
            {
                case BARCODE_FONT.CODE39:
                    barcodeFont = "Code 39";
                    break;
                case BARCODE_FONT.CODE128:
                    barcodeFont = "Code 128";
                    break;
                case BARCODE_FONT.NW7:
                    barcodeFont = "NW-7";
                    break;
            }

            PrintDocument pd = new PrintDocument();
            pd.PrinterSettings.PrinterName = printerName;
            //PrintPageイベントハンドラーを登録　pd_PrintPageはメソッド名
            pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
            pd.Print();
        }

        private static void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            //デバイスコンテキスト取得
            IntPtr hdc = e.Graphics.GetHdc();

            //バーコードの印字
            //バーコードの場合は9番目の引数を0にする必要あり
            IntPtr mFont = CreateFont(300, 0, 0, 0, 0, false, false, false, 0, 0, 0, 0, 0, barcodeFont);
            SelectObject(hdc, mFont);
            TextOut(hdc, 100, 150, datas[0], datas[0].Length);
            DeleteObject(mFont);

            //文字列のフォント
            String stringFontName = "Meiryo UI";

            //文字列1
            IntPtr mFont1 = CreateFont(100, 0, 0, 0, 0, false, false, false, 128, 0, 0, 0, 0, stringFontName);
            SelectObject(hdc, mFont1);
            String printString1 = "文字列１";
            TextOut(hdc, 5, 5, printString1, Encoding.GetEncoding("Shift_JIS").GetByteCount(printString1));
            DeleteObject(mFont1);
            //文字列2
            IntPtr mFont2 = CreateFont(100, 0, 0, 0, 0, false, false, false, 128, 0, 0, 0, 0, stringFontName);
            SelectObject(hdc, mFont2);
            String printString2 = "文字列２";
            TextOut(hdc, 300, 5, printString2, Encoding.GetEncoding("Shift_JIS").GetByteCount(printString2));
            DeleteObject(mFont2);

            //文字列3
            IntPtr mFont3 = CreateFont(60, 0, 0, 0, 0, false, false, false, 128, 0, 0, 0, 0, stringFontName);
            SelectObject(hdc, mFont3);
            String printString3 = "文字列３";
            TextOut(hdc, 5, 105, printString3, Encoding.GetEncoding("Shift_JIS").GetByteCount(printString3));
            DeleteObject(mFont3);
            //文字列4
            IntPtr mFont4 = CreateFont(60, 0, 0, 0, 0, false, false, false, 128, 0, 0, 0, 0, stringFontName);
            SelectObject(hdc, mFont4);
            String printString4 = "文字列４";
            TextOut(hdc, 300, 105, printString4, Encoding.GetEncoding("Shift_JIS").GetByteCount(printString4));
            DeleteObject(mFont4);

            //文字列5
            IntPtr mFont5 = CreateFont(60, 0, 0, 0, 0, false, false, false, 128, 0, 0, 0, 0, stringFontName);
            SelectObject(hdc, mFont5);
            String printString5 = "文字列5";
            TextOut(hdc, 5, 165, printString5, Encoding.GetEncoding("Shift_JIS").GetByteCount(printString5));
            DeleteObject(mFont5);
            //文字列6
            IntPtr mFont6 = CreateFont(60, 0, 0, 0, 0, false, false, false, 128, 0, 0, 0, 0, stringFontName);
            SelectObject(hdc, mFont6);
            String printString6 = "文字列6";
            TextOut(hdc, 300, 165, printString6, Encoding.GetEncoding("Shift_JIS").GetByteCount(printString6));
            DeleteObject(mFont6);

            //文字列7
            IntPtr mFont7 = CreateFont(60, 0, 0, 0, 0, false, false, false, 128, 0, 0, 0, 0, stringFontName);
            SelectObject(hdc, mFont7);
            String printString7 = "文字列7";
            TextOut(hdc, 5, 225, printString7, Encoding.GetEncoding("Shift_JIS").GetByteCount(printString7));
            DeleteObject(mFont7);
            //文字列8
            IntPtr mFont8 = CreateFont(60, 0, 0, 0, 0, false, false, false, 128, 0, 0, 0, 0, stringFontName);
            SelectObject(hdc, mFont8);
            String printString8 = "文字列8";
            TextOut(hdc, 300, 225, printString8, Encoding.GetEncoding("Shift_JIS").GetByteCount(printString8));
            DeleteObject(mFont8);

            //デバイスコンテキストの開放　これは必ず必要
            e.Graphics.ReleaseHdc(hdc);
        }

    }
}
