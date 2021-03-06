﻿Imports System
Imports System.IO
Imports System.Diagnostics
Imports DevExpress.Spreadsheet

Namespace SpreadsheetControl_API
	Public NotInheritable Class ImportExportActions

		Private Sub New()
		End Sub


		Public Shared ExportToPdfAction As Action(Of IWorkbook) = AddressOf ExportToPdf

		Private Shared Sub ExportToPdf(ByVal workbook As IWorkbook)
			workbook.Worksheets(0).Cells("D8").Value = "This document is exported to the PDF format."

'			#Region "#ExportToPdf"
			Using pdfFileStream As New FileStream("Documents\Document_PDF.pdf", FileMode.Create)
				workbook.ExportToPdf(pdfFileStream)
			End Using
'			#End Region ' #ExportToPdf
			Process.Start("Documents\Document_PDF.pdf")
		End Sub
	End Class
End Namespace
