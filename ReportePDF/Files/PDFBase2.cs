using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Utils;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.IO;

namespace ReportePDF.Files
{
    public abstract class PDFBase2
    {
        protected readonly Document document;
        private MemoryStream stream;
        protected Paragraph text;

        protected PdfDocument pdf;
        protected Style poppinsboldStyle;
        protected Style poppinsStyle;

        protected Style section;
        protected Style subsection;
        protected Style label;
        protected Style value;

        public PDFBase2(DateTime _date, string _user, string _reportType, string _personType)
        {
            stream = new MemoryStream();
            PdfWriter writer = new PdfWriter("demo.pdf");
            pdf = new PdfDocument(writer);
            document = new Document(pdf, PageSize.LETTER);
            document.SetMargins(140, 30, 20, 30);
            pdf.AddEventHandler(PdfDocumentEvent.START_PAGE,
                new HeaderEventHandler2(_date, _user, _reportType, _personType));

            DefineStyles();
        }

        private void DefineStyles()
        {
            poppinsboldStyle = new Style()
                .SetFont(PdfFontFactory.CreateFont(GlobalUrl2.POPPINSBOLD,
                    PdfEncodings.IDENTITY_H, true));

            poppinsStyle = new Style()
                .SetFontColor(new DeviceRgb(45, 66, 148))
                .SetFont(PdfFontFactory.CreateFont(GlobalUrl2.POPPINREGULAR,
                    PdfEncodings.IDENTITY_H, true));

            section = new Style()
                .SetFont(PdfFontFactory.CreateFont(GlobalUrl2.POPPINSBOLD,
                    PdfEncodings.IDENTITY_H, true))
                .SetFontSize(13);

            subsection = new Style()
                .SetFontColor(new DeviceRgb(45, 66, 148))
                .SetFont(PdfFontFactory.CreateFont(GlobalUrl2.POPPINREGULAR,
                    PdfEncodings.IDENTITY_H, true))
                .SetFontSize(12);

            label = new Style()
                .SetFont(PdfFontFactory.CreateFont(
                    StandardFonts.HELVETICA_BOLD))
                .SetFontSize(10);

            value = new Style()
                .SetFont(PdfFontFactory.CreateFont(GlobalUrl2.HELVETICALIGHT,
                    PdfEncodings.IDENTITY_H, true))
                .SetFontSize(10);
        }

        public void AttachedInfo(List<string> anexos)
        {
            var table = CreateTable(new float[] { 100f });
            var cell = CreateCell();
            table.SetMarginTop(-10);

            cell.SetBorder(new SolidBorder(new DeviceRgb(234, 234, 234), 1));
            cell.SetBorderTop(new SolidBorder(new DeviceRgb(45, 66, 148), 5));
            cell.Add(new Paragraph("TABLA DE CONTENIDO - ANEXOS").AddStyle(section).SetFontSize(11));
            table.AddCell(cell);

            document.Add(table);

            document.Add(new Paragraph().SetFontSize(10));

            int count = 1;
            foreach (var item in anexos)
            {
                document.Add(new Paragraph()
                    .SetFixedLeading(18)
                    .Add(new Text("ANEXO " + count)
                        .AddStyle(poppinsboldStyle)
                        .SetFontSize(17))
                    .Add(new Text("\n")
                        .AddStyle(poppinsboldStyle)
                        .SetFontSize(17))
                    .Add(new Text(item)
                        .AddStyle(subsection)));
                count++;
            }

            document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
        }

        public void AddAttached(int no, string type, string url)
        {
            document.Add(new AreaBreak(AreaBreakType.LAST_PAGE));
            document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));

            Paragraph pz = new Paragraph("ANEXO " + no)
                .Add(new Text("\n"))
                .Add(new Text(type)
                    .AddStyle(poppinsStyle)
                    .SetFontSize(27))
                .AddStyle(poppinsboldStyle)
                .SetFontSize(40)
                .SetFixedLeading(40)
                .SetTextAlignment(TextAlignment.CENTER);

            new Canvas(new PdfCanvas(pdf.GetLastPage()),
                new Rectangle(160, 230, 300, 200))
                    .Add(pz);


            PdfDocument pdfDocument = new PdfDocument(new PdfReader(url));

            PdfMerger merger = new PdfMerger(pdf);
            merger.Merge(pdfDocument, 1, pdfDocument.GetNumberOfPages());

            pdfDocument.Close();
        }

        public void CreateUserSection(string fullname, string username)
        {
            var table = CreateTable(new float[] { 65f, 35f });

            var cell = CreateCell();
            cell.SetBorder(new SolidBorder(new DeviceRgb(234, 234, 234), 1));
            cell.SetBorderTop(new SolidBorder(new DeviceRgb(45, 66, 148), 5));
            cell.SetPaddingLeft(10);
            cell.SetPaddingTop(10);
            cell.SetPaddingBottom(10);

            cell.Add(
                new Paragraph()
                    .Add(new Text("Nombre Completo").AddStyle(label))
                    .Add(new Text("\n").AddStyle(value))
                    .Add(new Text(fullname).AddStyle(value))
                    .Add(new Text("\n\n").AddStyle(value))
                    .Add(new Text("Nombre de Usuario").AddStyle(label))
                    .Add(new Text("\n").AddStyle(value))
                    .Add(new Text(username).AddStyle(value))
            );

            table.AddCell(cell);

            var tableAux = CreateTable(new float[] { 25f, 25f, 25f, 25f });
            cell = CreateCell()
                .SetHeight(10)
                .SetBackgroundColor(
                    new DeviceRgb(78, 114, 252));
            tableAux.AddCell(cell);
            cell = CreateCell()
                .SetHeight(10)
                .SetBackgroundColor(
                    new DeviceRgb(51, 75, 166));
            tableAux.AddCell(cell);
            cell = CreateCell()
                .SetHeight(10)
                .SetBackgroundColor(
                    new DeviceRgb(18, 35, 99));
            tableAux.AddCell(cell);
            cell = CreateCell()
                .SetHeight(10)
                .SetBackgroundColor(
                    new DeviceRgb(10, 15, 33));
            tableAux.AddCell(cell);

            cell = CreateCell(1, 4)
                .SetHeight(2)
                .SetBorderBottom(new SolidBorder(new DeviceRgb(10, 15, 33), 2));
            tableAux.AddCell(cell);

            cell = CreateCell(1, 4).SetTextAlignment(TextAlignment.CENTER);
            cell.Add(
                new Paragraph()
                    .SetFixedLeading(19)
                    .Add(new Text("10").AddStyle(poppinsboldStyle)
                        .SetFontSize(19).SetFontColor(new DeviceRgb(10, 15, 33)))
                    .Add(new Text("\n").AddStyle(value))
                    .Add(new Text("Nivel De Confianza").AddStyle(poppinsboldStyle)
                    .SetFontSize(8).SetFontColor(new DeviceRgb(10, 15, 33)))
            );
            tableAux.AddCell(cell);

            cell = CreateCell(1, 2).SetTextAlignment(TextAlignment.CENTER)
                .SetBorderRight(new SolidBorder(new DeviceRgb(10, 15, 33), 2));
            cell.Add(
                new Paragraph()
                    .SetFixedLeading(15)
                    .Add(new Text("10").AddStyle(poppinsboldStyle)
                        .SetFontSize(15).SetFontColor(new DeviceRgb(10, 15, 33)))
                    .Add(new Text("\n").AddStyle(value))
                    .Add(new Text("Score Por Nombre").AddStyle(poppinsboldStyle)
                    .SetFontSize(5).SetFontColor(new DeviceRgb(10, 15, 33)))
            );
            tableAux.AddCell(cell);

            cell = CreateCell(1, 2).SetTextAlignment(TextAlignment.CENTER);
            cell.Add(
                new Paragraph()
                    .SetFixedLeading(15)
                    .Add(new Text("10").AddStyle(poppinsboldStyle)
                        .SetFontSize(15).SetFontColor(new DeviceRgb(10, 15, 33)))
                    .Add(new Text("\n").AddStyle(value))
                    .Add(new Text("Score Por ID").AddStyle(poppinsboldStyle)
                    .SetFontSize(5).SetFontColor(new DeviceRgb(10, 15, 33)))
            );
            tableAux.AddCell(cell);


            cell = CreateCell().SetPaddingLeft(10).SetPaddingRight(10);
            cell.Add(tableAux);

            table.AddCell(cell);

            document.Add(table);

        }

        public Table CreateTable(float[] cellWidth)
        {
            return new Table(
                UnitValue.CreatePercentArray(cellWidth))
                .UseAllAvailableWidth()
                .SetBorder(Border.NO_BORDER);
        }

        public Cell CreateCell(int rowspan = 1, int colspan = 1)
        {
            return new Cell(rowspan, colspan)
                .SetBorder(Border.NO_BORDER);
        }

        public Cell CreateCellSection(string value, int rowspan = 1, int colspan = 1)
        {
            text = new Paragraph(value)
                .AddStyle(section);

            return new Cell(rowspan, colspan)
                .Add(text)
                .SetBorder(Border.NO_BORDER)
                .SetBorderBottom(new SolidBorder(ColorConstants.LIGHT_GRAY, 1));
        }

        public Cell CreateCellSectionWithBar(string value, string subvalue, int rowspan = 1, int colspan = 1)
        {
            text = new Paragraph()
                .SetFixedLeading(14);
            text.Add(new Text(value).AddStyle(section));
            if (string.IsNullOrEmpty(subvalue) == false) text.Add("\n");
            text.Add(new Text(subvalue).AddStyle(subsection));

            return new Cell(rowspan, colspan)
                .Add(text)
                .SetPaddingTop(15)
                .SetPaddingBottom(15)
                .SetBorder(Border.NO_BORDER)
                .SetBorderTop(new SolidBorder(new DeviceRgb(45, 66, 148), 2))
                .SetBorderBottom(new SolidBorder(ColorConstants.LIGHT_GRAY, 1));
        }

        public Cell CreateCellLabel(string value, int rowspan = 1, int colspan = 1)
        {
            text = new Paragraph(value)
                .AddStyle(label);

            return new Cell(rowspan, colspan)
                .Add(text)
                .SetPadding(0)
                .SetBorder(Border.NO_BORDER)
                .SetBorderRight(new SolidBorder(ColorConstants.LIGHT_GRAY, 1));
        }

        public Cell CreateCellValue(string val, int rowspan = 1, int colspan = 1)
        {
            text = new Paragraph(val)
                .AddStyle(value);

            return new Cell(rowspan, colspan)
                .Add(text)
                .SetPadding(0)
                .SetPaddingLeft(15)
                .SetBorder(Border.NO_BORDER);
        }

    }

    public class HeaderEventHandler2 : IEventHandler
    {
        protected readonly DateTime date;
        protected readonly string user, reportType, personType;

        public HeaderEventHandler2(DateTime _date, string _user, string _reportType, string _personType)
        {
            date = _date;
            user = _user;
            reportType = _reportType;
            personType = _personType;
        }

        public void HandleEvent(Event @event)
        {
            PdfDocumentEvent pdfDocumentEvent = (PdfDocumentEvent)@event;
            PdfDocument pdfDocument = pdfDocumentEvent.GetDocument();
            PdfPage pdfPage = pdfDocumentEvent.GetPage();

            PdfCanvas pdfCanvas = new PdfCanvas(
                pdfPage.NewContentStreamBefore(),
                pdfPage.GetResources(),
                pdfDocument);

            Rectangle rectangleArea = new Rectangle(
                0, pdfPage.GetPageSize().GetTop() - 140,
                pdfPage.GetPageSize().GetRight(), 140);

            new Canvas(pdfCanvas, rectangleArea)
                .Add(CreateHeader(pdfDocumentEvent))
                .Close();
        }

        public Table CreateHeader(PdfDocumentEvent documentEvent)
        {
            float[] cellWidth = { 20f, 80f };

            Image _img = new Image(ImageDataFactory
                .Create(GlobalUrl.LOGO));

            Table tableEvent = new Table(
                UnitValue.CreatePercentArray(cellWidth))
                .UseAllAvailableWidth()
                .SetMarginTop(8)
                .SetMarginLeft(30)
                .SetMarginRight(30);

            PdfFont HELVETICA = PdfFontFactory.CreateFont(
               StandardFonts.HELVETICA);

            PdfFont POPPINSBOLD = PdfFontFactory.CreateFont(
               GlobalUrl2.POPPINSBOLD, PdfEncodings.IDENTITY_H, true);

            PdfFont POPPINREGULAR = PdfFontFactory.CreateFont(
               GlobalUrl2.POPPINREGULAR, PdfEncodings.IDENTITY_H, true);

            Style styleCell = new Style()
                .SetBorder(Border.NO_BORDER);

            Cell cell = new Cell();
            cell.Add(_img.SetWidth(60));
            cell.AddStyle(styleCell);
            tableEvent.AddCell(cell);

            var tableAux = new Table(UnitValue.CreatePercentArray(new float[] { 50f, 50f }))
                .SetHorizontalAlignment(HorizontalAlignment.RIGHT);

            cell = new Cell();
            cell.AddStyle(styleCell);
            cell.SetTextAlignment(TextAlignment.RIGHT);
            cell.Add(new Paragraph("Identificación usuario:")
                .SetFont(HELVETICA)
                .SetFontSize(9));
            tableAux.AddCell(cell);

            cell = new Cell();
            cell.AddStyle(styleCell);
            cell.SetTextAlignment(TextAlignment.LEFT);
            cell.Add(new Paragraph(user)
                .SetFont(HELVETICA)
                .SetFontSize(9));
            tableAux.AddCell(cell);

            cell = new Cell();
            cell.AddStyle(styleCell);
            cell.SetTextAlignment(TextAlignment.RIGHT);
            cell.Add(new Paragraph("Identificación consulta:")
                .SetFont(HELVETICA)
                .SetFontSize(9));
            tableAux.AddCell(cell);

            cell = new Cell();
            cell.AddStyle(styleCell);
            cell.SetTextAlignment(TextAlignment.LEFT);
            cell.Add(new Paragraph(user)
                .SetFont(HELVETICA)
                .SetFontSize(9));
            tableAux.AddCell(cell);

            cell = new Cell();
            cell.SetTextAlignment(TextAlignment.RIGHT);
            cell.SetVerticalAlignment(VerticalAlignment.MIDDLE);
            cell.SetHorizontalAlignment(HorizontalAlignment.RIGHT);
            cell.AddStyle(styleCell);
            cell.Add(tableAux);
            tableEvent.AddCell(cell);


            cell = new Cell(1, 2);
            cell.SetHeight(35);
            cell.SetBorderBottom(new SolidBorder(ColorConstants.LIGHT_GRAY, 1));
            cell.AddStyle(styleCell);
            cell.SetPaddingTop(20);
            cell.SetTextAlignment(TextAlignment.LEFT);
            cell.Add(new Paragraph()
                .SetFixedLeading(10f)
                    .Add(new Text(reportType)
                    .SetFont(POPPINSBOLD)
                    .SetFontSize(17))
                    );
            cell.Add(new Paragraph()
                .Add(new Text(personType)
                    .SetFont(POPPINREGULAR)
                    .SetFontSize(12)
                    .SetFontColor(new DeviceRgb(45, 66, 148))));


            if (reportType.Contains("REPORTE"))
            {
                cell.SetPaddingBottom(10);
                cell.SetBorderBottom(new SolidBorder(new DeviceRgb(45, 66, 148), 2));
            }

            tableEvent.AddCell(cell);


            return tableEvent;
        }
    }

    public static class GlobalUrl2
    {
        public static string LOGO = "https://seifweb.azurewebsites.net/files/logo01.png";
        public static string POPPINSBOLD = "C:/Users/edgar/Desktop/Dinamica/Poppins-Bold.ttf";
        public static string POPPINREGULAR = "C:/Users/edgar/Desktop/Dinamica/Poppins-Regular.ttf";
        public static string HELVETICALIGHT = "C:/Users/edgar/Desktop/Dinamica/helvetica-light.ttf";
    }
}