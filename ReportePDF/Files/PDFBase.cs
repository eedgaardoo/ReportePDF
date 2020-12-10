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
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.IO;

namespace ReportePDF.Files
{
    public abstract class PDFBase
    {
        protected readonly Document document;
        private MemoryStream stream;
        protected Paragraph text;

        protected Style sectionStyle;
        protected Style subSectionStyle;
        protected Style boldTextStyle;
        protected Style textStyle;
        protected Style labelStyle;
        protected Style textBoxStyle;
        protected Style titleStyle;

        public PDFBase(DateTime _date, string _user, string _reportType, string _personType)
        {
            stream = new MemoryStream();
            PdfWriter writer = new PdfWriter("demo.pdf");
            PdfDocument pdf = new PdfDocument(writer);
            document = new Document(pdf, PageSize.LETTER);
            document.SetMargins(120, 0, 20, 0);
            pdf.AddEventHandler(PdfDocumentEvent.START_PAGE,
                new HeaderEventHandler(_date, _user, _reportType, _personType));
            DefineStyles();
        }

        private void DefineStyles()
        {
            sectionStyle = new Style()
                .SetFontSize(19)
                .SetFont(PdfFontFactory.CreateFont(GlobalUrl.ARIALBOLD,
                    PdfEncodings.IDENTITY_H, true))
                .SetFontColor(ColorConstants.WHITE)
                .SetPaddingLeft(25)
                .SetPaddingTop(2)
                .SetPaddingBottom(2)
                .SetBackgroundColor(new DeviceRgb(30, 18, 72));

            subSectionStyle = new Style()
                .SetFontSize(16)
                .SetFont(PdfFontFactory.CreateFont(
                    StandardFonts.HELVETICA_BOLD))
                .SetFontColor(new DeviceRgb(30, 18, 72))
                .SetPaddingLeft(10)
                .SetPaddingTop(2)
                .SetPaddingBottom(2)
                .SetBackgroundColor(new DeviceRgb(234, 234, 234));

            boldTextStyle = new Style()
                .SetFontSize(11)
                .SetFont(PdfFontFactory.CreateFont(
                    StandardFonts.HELVETICA_BOLD));

            textStyle = new Style()
                .SetFontSize(11)
                .SetFont(PdfFontFactory.CreateFont(
                    StandardFonts.HELVETICA));

            labelStyle = new Style()
                .SetFontSize(11)
                .SetFontColor(new DeviceRgb(83, 87, 92))
                .SetFont(PdfFontFactory.CreateFont(
                    StandardFonts.HELVETICA_BOLD));

            textBoxStyle = new Style()
                .SetFont(PdfFontFactory.CreateFont(
                    StandardFonts.HELVETICA))
                .SetFontSize(11)
                .SetPadding(2)
                .SetPaddingLeft(8)
                .SetBorderRadius(new BorderRadius(9))
                .SetBackgroundColor(new DeviceRgb(234, 234, 234));

            titleStyle = new Style()
                   .SetFontSize(12)
                    .SetFont(PdfFontFactory.CreateFont(
                    StandardFonts.HELVETICA_BOLD))
                   .SetFontColor(new DeviceRgb(30, 18, 72));
        }

        public void CreateInitialSection(string name, string id)
        {
            CreateSectionLabel("Datos de Usuario");

            text = new Paragraph("Nombre Completo");
            text.AddStyle(boldTextStyle)
                .SetFontSize(12)
                .SetMarginLeft(25);
            document.Add(text);

            text = new Paragraph(name);
            text.AddStyle(textBoxStyle)
                .SetMarginLeft(25)
                .SetMarginTop(-2)
                .SetWidth(200);
            document.Add(text);

            text = new Paragraph("Número de Identificación");
            text.AddStyle(boldTextStyle)
                .SetFontSize(12)
                .SetMarginTop(-1)
                .SetMarginLeft(25);
            document.Add(text);

            text = new Paragraph(id);
            text.AddStyle(textBoxStyle)
                .SetMarginLeft(25)
                .SetMarginTop(-2)
                .SetWidth(250);
            document.Add(text);
        }

        public void CreateSectionLabel(string title)
        {
            text = new Paragraph(title);
            text.AddStyle(sectionStyle);
            document.Add(text);
        }

        public void CreateSubsectionLabel(string subtitle)
        {
            text = new Paragraph(subtitle);
            text.AddStyle(subSectionStyle);
            document.Add(text);
        }

        public Table CreateTable(float[] cellWidth)
        {
            return new Table(
                UnitValue.CreatePercentArray(cellWidth))
                .UseAllAvailableWidth()
                .SetMarginLeft(25)
                .SetMarginTop(5)
                .SetBorder(Border.NO_BORDER);
        }

        public Cell CreateCell(int rowspan = 1, int colspan = 1)
        {
            return new Cell(rowspan, colspan)
                .SetBorder(Border.NO_BORDER)
                .SetPaddingTop(5);
        }

        public Cell CreateCellSubSection(string label, int rowspan = 1, int colspan = 1)
        {
            text = new Paragraph(label)
                .AddStyle(subSectionStyle);

            return new Cell(rowspan, colspan).Add(text)
                .SetBorder(Border.NO_BORDER)
                .SetPaddingTop(5)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE);
        }

        public Cell CreateCellSubSectionLabel(string label, bool reference = false, int fontSize = 11)
        {
            text = new Paragraph(label)
                .AddStyle(labelStyle);

            text.SetFontSize(fontSize);

            if (reference)
            {
                text.Add(new Text("__").SetFontColor(ColorConstants.WHITE));
                text.Add(new Text("?")
                .SetFontSize(9)
                .SetBackgroundColor(new DeviceRgb(234, 234, 234))
                .SetBorderRadius(new BorderRadius(9)));
            }


            return new Cell().Add(text)
                .SetBorder(Border.NO_BORDER)
                .SetBorderRight(new SolidBorder(new DeviceRgb(30, 18, 72), 1))
                .SetPaddingLeft(15);
        }

        public Cell CreateCellSubSectionValueBold(string label)
        {
            text = new Paragraph(label)
                .AddStyle(boldTextStyle);

            return new Cell().Add(text)
                .SetBorder(Border.NO_BORDER)
                .SetPaddingLeft(15);
        }

        public Cell CreateCellSubSectionValue(string label)
        {
            text = new Paragraph(label)
                .AddStyle(textStyle);

            return new Cell().Add(text)
                .SetBorder(Border.NO_BORDER)
                .SetPaddingLeft(15);
        }

        public Cell CreateCellHead(string label, int fontSize = 9, int rowspan = 1, int colspan = 1, TextAlignment textAlignment = TextAlignment.CENTER, int paddinleft = 0)
        {
            text = new Paragraph(label)
                .AddStyle(labelStyle)
                .SetFontSize(fontSize)
                .SetPaddingLeft(paddinleft)
                .SetFontColor(ColorConstants.WHITE)
                .SetBackgroundColor(new DeviceRgb(30, 18, 72));

            return new Cell(rowspan, colspan).Add(text)
                .SetBorder(Border.NO_BORDER)
                .SetBackgroundColor(new DeviceRgb(30, 18, 72))
                .SetTextAlignment(textAlignment);
        }

        public Cell CreateCellForImage(string url, string alternative, int width, int fontSize, int rowspan = 1, int colspan = 1)
        {
            var cell = CreateCell(rowspan, colspan)
                .SetTextAlignment(TextAlignment.CENTER);

            if (string.IsNullOrEmpty(url))
            {
                text = new Paragraph(alternative)
                .AddStyle(boldTextStyle)
                .SetFontSize(fontSize)
                .SetWidth(width)
                .SetHeight(width)
                .SetHorizontalAlignment(HorizontalAlignment.CENTER)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .SetBackgroundColor(new DeviceRgb(234, 234, 234))
                .SetFontColor(ColorConstants.WHITE);

                if (fontSize < 10)
                    text.SetFontColor(new DeviceRgb(159, 163, 169));

                cell.Add(text);
            }
            else
            {
                Image _img = new Image(ImageDataFactory
                .Create(url));
                cell.Add(_img.SetWidth(width));
            }

            return cell;
        }

        public Cell CreateCellSubSectionLink(string label, string url, Style style = null)
        {
            text = new Paragraph(new Link(label,
                PdfAction.CreateURI(url)))
                .AddStyle(style ?? textStyle)
                .SetUnderline();

            return new Cell().Add(text)
                .SetBorder(Border.NO_BORDER)
                .SetPaddingLeft(15);
        }

        public Cell CreateCellLabel(string label, int rowspan = 1, int colspan = 1, bool padding = true, bool center = false, Style style = null, int fontSize = 11)
        {
            text = new Paragraph(label)
                .AddStyle(style ?? labelStyle)
                .SetFontSize(fontSize);

            if (center) text.SetTextAlignment(TextAlignment.CENTER);

            var cell = new Cell(rowspan, colspan).Add(text)
                .SetBorder(Border.NO_BORDER)

                .SetVerticalAlignment(VerticalAlignment.MIDDLE);

            if (padding) cell.SetPaddingTop(5);
            return cell;
        }

        public Cell CreateCellValue(string value, int rowspan = 1, int colspan = 1, int paddingLeft = 0, int fontSize = 11)
        {
            text = new Paragraph(value)
                .AddStyle(textBoxStyle);

            text.SetFontSize(fontSize);

            if (string.IsNullOrEmpty(value))
            {
                text.Add("_").SetFontColor(new DeviceRgb(234, 234, 234));
            }

            return new Cell(rowspan, colspan).Add(text)
                .SetBorder(Border.NO_BORDER)
                .SetPaddingTop(5)
                .SetPaddingLeft(paddingLeft)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE);
        }

        public Cell CreateCellReference(string value, bool title, int width = 60, bool paddingTop = false, bool paddingBottom = false)
        {
            text = new Paragraph(value)
                .SetFontSize(6);

            text.SetWidth(width);

            if (title)
            {
                text.SetPaddingRight(10);
                text.SetPaddingLeft(5);
                text.SetPaddingTop(2);
                text.SetPaddingBottom(2);
                text.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));
                text.SetBorderRadius(new BorderRadius(9))
                    .SetBackgroundColor(new DeviceRgb(234, 234, 234));
            }
            else
                text.SetFont(PdfFontFactory.CreateFont(
                   StandardFonts.HELVETICA));

            var cell = new Cell().Add(text)
               .SetBorder(Border.NO_BORDER)
               .SetPaddingLeft(10);

            if (paddingTop) cell.SetPaddingTop(5);
            if (paddingBottom) cell.SetPaddingBottom(2);

            return cell;
        }

        public Cell CreateReference()
        {
            var cell = CreateCell(6, 1)
                .SetPaddingRight(5)
                .SetPaddingLeft(10);

            text = new Paragraph("Referencia")
                .AddStyle(labelStyle)
                .SetFontColor(new DeviceRgb(30, 18, 72));

            cell.Add(text);

            float[] cellWidth = { 70f, 30f };
            var table = new Table(
                UnitValue.CreatePercentArray(cellWidth))
                .UseAllAvailableWidth()
                .SetBorder(new SolidBorder(new DeviceRgb(30, 18, 72), 1))
                .SetMarginRight(15)
                .SetPadding(100);

            table.AddCell(CreateCellReference("Validado:", true, 50, true));
            table.AddCell(CreateCellReference("Validado/Coincide:", true, 65, true));
            table.AddCell(CreateCellReference("El APi de NUFI encontró información con las datos proporcionados", false, 80));
            table.AddCell(CreateCellReference("El APi de NUFI encontró información con las datos proporcionados y el dato encontrado coincide con el proporcionado", false, 100, false, true));

            table.AddCell(CreateCellReference("No Validado:", true, 50));
            table.AddCell(CreateCellReference("Validado/No Coincide:", true, 65));
            table.AddCell(CreateCellReference("El API de NUFI no encontró información con los datos proporcionados", false, 80));
            table.AddCell(CreateCellReference("El APi de NUFI encontró información con las datos proporcionados y el dato encontrado NO coincide con el proporcionado", false, 100, false, true));

            cell.Add(table);
            return cell;
        }

        public Cell CreateSubHeaderCell(string label, int rowspan = 1, int colspan = 1)
        {
            text = new Paragraph(label)
                .AddStyle(boldTextStyle)
                .SetFontSize(12);

            return new Cell(rowspan, colspan).Add(text)
                .SetBorder(Border.NO_BORDER)
                .SetBorderBottom(new SolidBorder(new DeviceRgb(30, 18, 72), 1));
        }

        public Cell CreateCheckboxCell(string label, bool value, int rowspan = 1, int colspan = 1)
        {
            text = new Paragraph()
                .AddStyle(labelStyle);

            if (value)
                text.Add(new Text("4")
                    .SetFontSize(12)
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.ZAPFDINGBATS))
                    .SetBorder(new SolidBorder(new DeviceRgb(234, 234, 234), 1)));
            else 
                text.Add(new Text("__")
                    .SetFontColor(ColorConstants.WHITE)
                    .SetBorder(new SolidBorder(new DeviceRgb(234, 234, 234), 1)));

            text.Add(new Text("_").SetFontColor(ColorConstants.WHITE));
            text.Add(" " + label);


            var cell = new Cell(rowspan, colspan).Add(text)
                .SetBorder(Border.NO_BORDER)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE);
           
            return cell;
        }
    }

    public class HeaderEventHandler : IEventHandler
    {
        protected readonly DateTime date;
        protected readonly string user, reportType, personType;

        public HeaderEventHandler(DateTime _date, string _user, string _reportType, string _personType)
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
                0, pdfPage.GetPageSize().GetTop() - 120,
                pdfPage.GetPageSize().GetRight(), 120);

            pdfCanvas.AddImageFittedIntoRectangle(
                ImageDataFactory.Create(GlobalUrl.BACKGROUND),
                rectangleArea, true);

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
                .UseAllAvailableWidth();

            Style styleCell = new Style()
                .SetBorder(Border.NO_BORDER);

            Cell cell = new Cell();
            cell.SetPaddingLeft(25);
            cell.SetPaddingTop(5);
            cell.Add(_img.SetWidth(60));
            cell.AddStyle(styleCell);
            tableEvent.AddCell(cell);

            PdfFont ARIAL = PdfFontFactory.CreateFont(
                GlobalUrl.ARIAL, PdfEncodings.IDENTITY_H, true);

            PdfFont HELVETICABOLD = PdfFontFactory.CreateFont(
                StandardFonts.HELVETICA_BOLD);

            cell = new Cell();
            cell.AddStyle(styleCell);
            cell.SetPaddingRight(20);
            cell.SetPaddingTop(15);
            cell.SetTextAlignment(TextAlignment.RIGHT);
            cell.Add(
                new Paragraph("Fecha Consulta:")
                .SetFont(ARIAL)
                .SetFontSize(9)
            );
            cell.Add(
                new Paragraph(date.ToString("dd/MM/yyyy HH:mm:ss tt"))
                .SetFont(ARIAL)
                .SetWidth(100)
                .SetHorizontalAlignment(HorizontalAlignment.RIGHT)
                .SetFontSize(8)
                .SetBackgroundColor(new DeviceRgb(207, 207, 207))
                .SetBorderRadius(new BorderRadius(9))
            );
            cell.Add(
                new Paragraph($"Usuario: {user}")
                .SetFont(ARIAL)
                .SetFontSize(8)
            );
            tableEvent.AddCell(cell);

            cell = new Cell();
            cell.AddStyle(styleCell);
            cell.SetPaddingLeft(20);
            cell.SetVerticalAlignment(VerticalAlignment.MIDDLE);
            cell.SetTextAlignment(TextAlignment.LEFT);
            if(string.IsNullOrEmpty(reportType) == false)
            cell.Add(
                new Paragraph(reportType)
                .SetFont(HELVETICABOLD)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(10)
                .SetWidth(75)
                .SetPaddingTop(5)
                .SetPaddingBottom(5)
                .SetFontColor(ColorConstants.WHITE)
                .SetBackgroundColor(new DeviceRgb(30, 18, 72))
                .SetBorderRadius(new BorderRadius(12))
            );
            tableEvent.AddCell(cell);

            cell = new Cell();
            cell.AddStyle(styleCell);
            cell.SetPaddingRight(20);
            cell.SetVerticalAlignment(VerticalAlignment.MIDDLE);
            cell.SetTextAlignment(TextAlignment.RIGHT);
            cell.Add(
                new Paragraph(personType)
                .SetFont(HELVETICABOLD)
                .SetHorizontalAlignment(HorizontalAlignment.RIGHT)
                .SetFontSize(20)
                .SetFontColor(new DeviceRgb(30, 18, 72))
            );
            tableEvent.AddCell(cell);

            return tableEvent;
        }
    }

    public static class GlobalUrl
    {
        public static string ARIAL = "https://seifweb.azurewebsites.net/files/arial.ttf";
        public static string ARIALBOLD = "https://seifweb.azurewebsites.net/files/arialbd.ttf";
        public static string LOGO = "https://seifweb.azurewebsites.net/files/logo01.png";
        public static string BACKGROUND = "https://seifweb.azurewebsites.net/files/header.png";
    }
}