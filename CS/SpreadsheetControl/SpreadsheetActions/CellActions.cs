﻿using System;
using System.Drawing;
using DevExpress.Spreadsheet;

namespace SpreadsheetControl_API {
    public static class CellActions {
        #region Actions
        public static Action<IWorkbook> ChangeCellValueAction = ChangeCellValue;
        public static Action<IWorkbook> AddHyperlinkAction = AddHyperlink;
        public static Action<IWorkbook> CopyCellDataAndStyleAction = CopyCellDataAndStyle;
        public static Action<IWorkbook> MergeAndSplitCellsAction = MergeAndSplitCells;
        #endregion

        static void ChangeCellValue(IWorkbook workbook) {
            workbook.BeginUpdate();
            try {
                Worksheet worksheet = workbook.Worksheets[0];

                worksheet.Cells["A1"].Value = "dateTime:";
                worksheet.Cells["A2"].Value = "double:";
                worksheet.Cells["A3"].Value = "string:";
                worksheet.Cells["A4"].Value = "error constant:";
                worksheet.Cells["A5"].Value = "boolean:";
                worksheet.Cells["A6"].Value = "float:";
                worksheet.Cells["A7"].Value = "char:";
                worksheet.Cells["A8"].Value = "int32:";
                worksheet.Cells["A10"].Value = "Fill a range of cells:";
                worksheet.Columns["A"].WidthInCharacters = 20;
                worksheet.Columns["B"].WidthInCharacters = 20;
                worksheet.Range["A1:B8"].Alignment.Horizontal = HorizontalAlignment.Left;

                // Add data of different types to cells.
                worksheet.Cells["B1"].Value = DateTime.Now;
                worksheet.Cells["B1"].NumberFormat = "m/d/yy";
                worksheet.Cells["B2"].Value = Math.PI;
                worksheet.Cells["B3"].Value = "Have a nice day!";
                worksheet.Cells["B4"].Value = CellValue.ErrorReference;
                worksheet.Cells["B5"].Value = true;
                worksheet.Cells["B6"].Value = float.MaxValue;
                worksheet.Cells["B7"].Value = 'a';
                worksheet.Cells["B8"].Value = Int32.MaxValue;

                // Fill all cells in the range with 10.
                worksheet.Range["B10:E10"].Value = 10;
            }
            finally {
                workbook.EndUpdate();
            }

        }

        static void AddHyperlink(IWorkbook workbook) {
            workbook.BeginUpdate();
            try {
                Worksheet worksheet = workbook.Worksheets[0];
                worksheet.Range["A:C"].ColumnWidthInCharacters = 12;

                // Create a hyperlink to a web page.
                Cell cell = worksheet.Cells["A1"];
                worksheet.Hyperlinks.Add(cell, "http://www.devexpress.com/", true, "DevExpress");

                // Create a hyperlink to a cell range in a workbook.
                Range range = worksheet.Range["C3:D4"];
                Hyperlink cellHyperlink = worksheet.Hyperlinks.Add(range, "Sheet2!B2:E7", false, "Select Range");
                cellHyperlink.TooltipText = "Click Me";
            }
            finally {
                workbook.EndUpdate();
            }
        }

        static void CopyCellDataAndStyle(IWorkbook workbook) {
            workbook.BeginUpdate();
            try {
                Worksheet worksheet = workbook.Worksheets[0];
                worksheet.Columns["A"].WidthInCharacters = 32;
                worksheet.Columns["B"].WidthInCharacters = 20;
                Style style = workbook.Styles[BuiltInStyleId.Input];

                // Specify the content and formatting for a source cell.
                worksheet.Cells["A1"].Value = "Source Cell";

                Cell sourceCell = worksheet.Cells["B1"];
                sourceCell.Formula = "= PI()";
                sourceCell.NumberFormat = "0.0000";
                sourceCell.Style = style;
                sourceCell.Font.Color = Color.Blue;
                sourceCell.Font.Bold = true;
                sourceCell.Borders.SetOutsideBorders(Color.Black, BorderLineStyle.Thin);

                // Copy all information from the source cell to the "B3" cell. 
                worksheet.Cells["A3"].Value = "Copy All";
                worksheet.Cells["B3"].CopyFrom(sourceCell);

                // Copy only the source cell content (e.g., text, numbers, formula calculated values) to the "B4" cell.
                worksheet.Cells["A4"].Value = "Copy Values";
                worksheet.Cells["B4"].CopyFrom(sourceCell, PasteSpecial.Values);

                // Copy the source cell content (e.g., text, numbers, formula calculated values) 
                // and number formats to the "B5" cell.
                worksheet.Cells["A5"].Value = "Copy Values and Number Formats";
                worksheet.Cells["B5"].CopyFrom(sourceCell, PasteSpecial.Values | PasteSpecial.NumberFormats);

                // Copy only the formatting information from the source cell to the "B6" cell.
                worksheet.Cells["A6"].Value = "Copy Formats";
                worksheet.Cells["B6"].CopyFrom(sourceCell, PasteSpecial.Formats);

                // Copy all information from the source cell to the "B7" cell except for border settings.
                worksheet.Cells["A7"].Value = "Copy All Except Borders";
                worksheet.Cells["B7"].CopyFrom(sourceCell, PasteSpecial.All & ~PasteSpecial.Borders);

                // Copy information only about borders from the source cell to the "B8" cell.
                worksheet.Cells["A8"].Value = "Copy Borders";
                worksheet.Cells["B8"].CopyFrom(sourceCell, PasteSpecial.Borders);
            }
            finally {
                workbook.EndUpdate();
            }
        }

        static void MergeAndSplitCells(IWorkbook workbook) {
            workbook.BeginUpdate();
            try {
                Worksheet worksheet = workbook.Worksheets[0];

                worksheet.Cells["A1"].FillColor = Color.LightGray;

                worksheet.Cells["B2"].Value = "B2";
                worksheet.Cells["B2"].FillColor = Color.LightGreen;

                worksheet.Cells["C3"].Value = "C3";
                worksheet.Cells["C3"].FillColor = Color.LightSalmon;

                // Merge cells contained in the range.
                worksheet.MergeCells(worksheet.Range["A1:C5"]);
            }
            finally {
                workbook.EndUpdate();
            }
        }
    }
}
