using System;
using System.Collections.Generic;
using System.Text;

using Excel = Microsoft.Office.Interop.Excel;

namespace Snitch_Map
{
    class ExcelReader
    {
        private Excel.Application xlApp;
        private Excel.Workbook xlWBook;
        private Excel.Worksheet xlSheet;
        private Excel.Range range;
        private int lastRow = 0;
        private List<int> _x;
        private List<int> _y;
        private StringBuilder sb;

        public List<int> X { get { return _x; } }
        public List<int> Y { get { return _y; } }

        public ExcelReader(string path)
        {
            xlApp = new Excel.Application();
            xlWBook = xlApp.Workbooks.Open(path);
            xlSheet = (Excel.Worksheet)xlWBook.Worksheets.get_Item(1);
            range = xlSheet.UsedRange;
            lastRow = xlSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Row;
            _x = new List<int>();
            _y = new List<int>();
            sb = new StringBuilder();
        }

        public void Scan()
        {
            try
            {
                for (int row = 1; row <= lastRow; row++)
                {
                    _x.Add(Int32.Parse((range.Cells[row, 1] as Excel.Range).Value2.ToString()));
                    _y.Add(Int32.Parse((range.Cells[row, 2] as Excel.Range).Value2.ToString()));
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }
    }
}
