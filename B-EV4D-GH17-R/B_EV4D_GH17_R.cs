using System;
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
            int xPos1 = 100;
            int yPos1 = 150;
            //バーコードの場合は9番目の引数を0にする必要あり
            IntPtr mFont1 = CreateFont(300, 0, 0, 0, 0, false, false, false, 0, 0, 0, 0, 0, barcodeFont);
            SelectObject(hdc, mFont1);
            TextOut(hdc, xPos1, yPos1, datas[0], datas[0].Length);
            DeleteObject(mFont1);

            //文字列のフォント
            String stringFontName = "Meiryo UI";

            //文字列1
            int xPos2 = 200;
            int yPos2 = 360;
            //文字列の場合は9番目の引数を128にする必要あり
            IntPtr mFont2 = CreateFont(50, 0, 0, 0, 0, false, false, false, 128, 0, 0, 0, 0, stringFontName);
            SelectObject(hdc, mFont2);
            TextOut(hdc, xPos2, yPos2, datas[1], Encoding.GetEncoding("Shift_JIS").GetByteCount(datas[1]));
            DeleteObject(mFont2);

            //文字列2
            int xPos3 = 10;
            int yPos3 = 10;
            IntPtr mFont3 = CreateFont(100, 0, 0, 0, 0, false, false, false, 128, 0, 0, 0, 0, stringFontName);
            SelectObject(hdc, mFont3);
            TextOut(hdc, xPos3, yPos3, datas[2], Encoding.GetEncoding("Shift_JIS").GetByteCount(datas[2]));
            DeleteObject(mFont3);

            //デバイスコンテキストの開放　これは必ず必要
            e.Graphics.ReleaseHdc(hdc);
        }

    }
}
