using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
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

namespace ReportePDF.Files
{
    public class PersonPDFReportBasic : PDFBase
    {
        public PersonPDFReportBasic() : base(DateTime.Now, "49dc0d56-2714-4d2a-b535-3a962df06fab", "", "Reporte de Auditoría")
        {

        }

        public void CreatePDF()
        {
            //CreateInitialSection("Jesús Edgardo Cervantes Rios", "49dc0d56-2714-4d2a-b535-3a962df06fab");

            //CreateSearchParameterSection();

            //CreateSearchResultSection();

            //CreateSearchParameterSectionAdvanced();
            //CreateAddressSectionAdvanced();
            //CreateINESectionAdvanced();
            //CreateRUGSectionAdvanced();
            //CreateEnrichmentSectionAdvanced();
            //CreateSigerSectionAdvanced();
            //CreateIMPISectionAdvanced();

            CreateReportSection();


            document.Close();
        }

        private void CreateReportSection()
        {
            var table = CreateTable(new float[] { 25f, 8f, 67f });
            var cell = CreateCell(1, 1);
            text = new Paragraph("Número Total de Reportes Ejecutados:")
                .AddStyle(labelStyle)
                .SetFontSize(13).SetFontColor(new DeviceRgb(30, 18, 72));
            cell.Add(text);
            table.AddCell(cell);

            table.AddCell(CreateCellValue("1000", 1, 1, 0, 13));
            document.Add(table);

            table = CreateTable(new float[] { 13f, 10f, 13f, 10f, 4f, 4f, 13, 10, 13, 10 });
            table.SetPaddingRight(20);
            table.SetPaddingTop(30);

            var marcador = new Paragraph(new Text("__").SetFontColor(new DeviceRgb(234, 234, 234))).AddStyle(textBoxStyle);
            marcador.Add("X");
            marcador.Add(new Text("_").SetFontColor(new DeviceRgb(234, 234, 234)));

            var vacio = new Paragraph(new Text("___").SetFontColor(new DeviceRgb(234, 234, 234))).AddStyle(textBoxStyle);

            table.AddCell(CreateCellLabel("SEIF Basic Persona Física", 1, 1, true, false, null, 8));
            cell = CreateCell(1, 1).SetPaddingRight(10).SetVerticalAlignment(VerticalAlignment.MIDDLE);
            cell.Add(true ? marcador : vacio);
            table.AddCell(cell);

            table.AddCell(CreateCellLabel("SEIF Advanced Persona Físico", 1, 1, true, false, null, 8));
            cell = CreateCell(1, 1).SetPaddingRight(10).SetVerticalAlignment(VerticalAlignment.MIDDLE);
            cell.Add(false ? marcador : vacio);
            table.AddCell(cell);

            table.AddCell(CreateCell(1, 1).SetBorderRight(new SolidBorder(new DeviceRgb(30, 18, 72), 1)));

            table.AddCell(CreateCell(1, 1));

            table.AddCell(CreateCellLabel("SEIF Basic Persona Moral", 1, 1, true, false, null, 8));
            cell = CreateCell(1, 1).SetPaddingRight(10).SetVerticalAlignment(VerticalAlignment.MIDDLE);
            cell.Add(false ? marcador : vacio);
            table.AddCell(cell);

            table.AddCell(CreateCellLabel("SEIF Advanced Persona Moral", 1, 1, true, false, null, 8));
            cell = CreateCell(1, 1).SetPaddingRight(10).SetVerticalAlignment(VerticalAlignment.MIDDLE);
            cell.Add(true ? marcador : vacio);
            table.AddCell(cell);

            table.AddCell(CreateCell(1,11).SetPaddingTop(20));

            document.Add(table);


            CreateSectionLabel("Historial de Búsquedas");


            text = new Paragraph("Filtros")
                .AddStyle(boldTextStyle)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(14)
                .SetWidth(75)
                .SetPaddingTop(5)
                .SetPaddingBottom(5)
                .SetFontColor(ColorConstants.WHITE)
                .SetBackgroundColor(new DeviceRgb(30, 18, 72))
                .SetBorderRadius(new BorderRadius(12))
                .SetMarginLeft(20);
            document.Add(text);           

            table = CreateTable(new float[] { 25f, 25f, 25f, 25f });

            table.AddCell(CreateCellLabel("Rango de Fechas").SetHorizontalAlignment(HorizontalAlignment.CENTER));
            table.AddCell(CreateCellLabel("Estatus").SetHorizontalAlignment(HorizontalAlignment.CENTER));
            table.AddCell(CreateCellLabel("Usuario SEIF").SetHorizontalAlignment(HorizontalAlignment.CENTER));
            table.AddCell(CreateCell(1, 1));

            table.AddCell(CreateCellValue("", 1, 1, 0).SetPaddingRight(5));
            table.AddCell(CreateCellValue("", 1, 1, 0).SetPaddingRight(5));
            table.AddCell(CreateCellValue("", 1, 1, 0).SetPaddingRight(5));
            table.AddCell(CreateCellValue("", 1, 1, 0).SetPaddingRight(5));

            document.Add(table);

            text = new Paragraph("Lista de Búsquedas")
              .AddStyle(labelStyle)
              .SetMarginLeft(25)
              .SetMarginTop(10)
              .SetFontSize(13).SetFontColor(new DeviceRgb(30, 18, 72));

            document.Add(text);


            float[] cellWidth = { 20f, 20f, 20f, 20f, 20f };
            table = CreateTable(cellWidth);
            table.SetPaddingRight(20);

            table.AddCell(CreateCellHead("Tipo de Reporte"));
            table.AddCell(CreateCellHead("Individuo / Empresa Consultado"));
            table.AddCell(CreateCellHead("Fecha / Hora Consulta"));
            table.AddCell(CreateCellHead("Estatus"));
            table.AddCell(CreateCellHead("Link PDF"));

            table.AddCell(CreateCellValue("", 1, 1, 0).SetPaddingRight(5));
            table.AddCell(CreateCellValue("", 1, 1, 0).SetPaddingRight(5));
            table.AddCell(CreateCellValue("", 1, 1, 0).SetPaddingRight(5));
            table.AddCell(CreateCellValue("", 1, 1, 0).SetPaddingRight(5));
            table.AddCell(CreateCellValue("", 1, 1, 0).SetPaddingRight(5));

            table.AddCell(CreateCellValue("", 1, 1, 0).SetPaddingRight(5));
            table.AddCell(CreateCellValue("", 1, 1, 0).SetPaddingRight(5));
            table.AddCell(CreateCellValue("", 1, 1, 0).SetPaddingRight(5));
            table.AddCell(CreateCellValue("", 1, 1, 0).SetPaddingRight(5));
            table.AddCell(CreateCellValue("", 1, 1, 0).SetPaddingRight(5));

            table.AddCell(CreateCellValue("", 1, 1, 0).SetPaddingRight(5));
            table.AddCell(CreateCellValue("", 1, 1, 0).SetPaddingRight(5));
            table.AddCell(CreateCellValue("", 1, 1, 0).SetPaddingRight(5));
            table.AddCell(CreateCellValue("", 1, 1, 0).SetPaddingRight(5));
            table.AddCell(CreateCellValue("", 1, 1, 0).SetPaddingRight(5));


            document.Add(table);

        }

        private void CreateSearchParameterSection()
        {
            CreateSectionLabel("Parametros de la Búsqueda");

            float[] cellWidth = { 28f, 12f, 12f, 12f, 36f };
            var table = CreateTable(cellWidth);

            table.AddCell(CreateCellLabel("Nombre (s) *"));
            table.AddCell(CreateCellValue("Jesus Edgardo", 1, 3));
            table.AddCell(CreateCellLabel(string.Empty));

            table.AddCell(CreateCellLabel("Apellido Paterno *"));
            table.AddCell(CreateCellValue("Cervantes", 1, 3));
            table.AddCell(CreateCellLabel(string.Empty));

            table.AddCell(CreateCellLabel("Apellido Materno *"));
            table.AddCell(CreateCellValue("Rios", 1, 3));
            table.AddCell(CreateCellLabel(string.Empty));

            table.AddCell(CreateCellLabel("Sexo"));
            table.AddCell(CreateCellValue("Hombre", 1, 3));
            table.AddCell(CreateCellLabel(string.Empty));

            table.AddCell(CreateCellLabel("Estado de Nacimiento"));
            table.AddCell(CreateCellValue("Sonora", 1, 3));
            table.AddCell(CreateCellLabel(string.Empty));

            table.AddCell(CreateCellLabel("Fecha de Nacimiento"));
            table.AddCell(CreateCellValue("1990"));
            table.AddCell(CreateCellValue("12", 1, 1, 10));
            table.AddCell(CreateCellValue("28", 1, 1, 10));
            table.AddCell(CreateCellLabel(string.Empty));

            table.AddCell(CreateCellLabel("RFC"));
            table.AddCell(CreateCellValue("CERJ901228LY3", 1, 3));
            table.AddCell(CreateCellLabel(string.Empty));

            table.AddCell(CreateCellLabel("CURP"));
            table.AddCell(CreateCellValue("CERJ901228HSRRSS01", 1, 3));
            table.AddCell(CreateCellLabel(string.Empty));

            document.Add(table);
        }

        private void CreateSearchResultSection()
        {
            CreateSectionLabel("Resultados de la Búsqueda");

            CreateCalculateCurpSubSection();

            CreateValidateCurpSubSection();

            CreateValidateRFCSubSection();

            CreateProfessionalCertificates();

            CreateSatSection();

            CreateInterpolSection();
        }

        private void CreateCalculateCurpSubSection()
        {
            float[] cellWidth = { 20f, 40f, 40f };
            var table = CreateTable(cellWidth);

            table.AddCell(CreateCellSubSection("CALCULAR CURP", 1, 2));
            table.AddCell(CreateReference());

            table.AddCell(CreateCell().SetPaddingTop(5));
            table.AddCell(CreateCell().SetPaddingTop(5));

            table.AddCell(CreateCellSubSectionLabel("Ingresada"));
            table.AddCell(CreateCellSubSectionValueBold("CERJ901228HSRRSS01"));

            table.AddCell(CreateCellSubSectionLabel("Generada"));
            table.AddCell(CreateCellSubSectionValueBold("CERJ901228HSRRSS01"));

            table.AddCell(CreateCellSubSectionLabel("Resultado", true));
            table.AddCell(CreateCellSubSectionValue("Validado/Coincide"));

            table.AddCell(CreateCell().SetPaddingTop(5));
            table.AddCell(CreateCell().SetPaddingTop(5));

            document.Add(table);
        }

        private void CreateValidateCurpSubSection()
        {
            document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
            float[] cellWidth = { 20f, 40f, 40f };
            var table = CreateTable(cellWidth);

            table.AddCell(CreateCellSubSection("VALIDAR CURP", 1, 2));
            table.AddCell(CreateCell(5, 1));

            table.AddCell(CreateCell().SetPaddingTop(5));
            table.AddCell(CreateCell().SetPaddingTop(5));

            table.AddCell(CreateCellSubSectionLabel("Ingresada"));
            table.AddCell(CreateCellSubSectionValueBold("CERJ901228HSRRSS01"));

            table.AddCell(CreateCellSubSectionLabel("Generada"));
            table.AddCell(CreateCellSubSectionValueBold("CERJ901228HSRRSS01"));

            table.AddCell(CreateCellSubSectionLabel("Resultado", true));
            table.AddCell(CreateCellSubSectionValue("Validado/Coincide"));

            table.AddCell(CreateCellSubSectionLabel("Ver Documento"));
            table.AddCell(CreateCellSubSectionLink("Documento CURP", "https://nufistorage001.blob.core.windows.net/seif/2020-11-27/20201127T024923546.pdf"));

            document.Add(table);
        }

        private void CreateValidateRFCSubSection()
        {
            float[] cellWidth = { 20f, 40f, 40f };
            var table = CreateTable(cellWidth);
            table.SetMarginTop(15);

            table.AddCell(CreateCellSubSection("VALIDAR RFC", 1, 2));
            table.AddCell(CreateCell(4, 1));

            table.AddCell(CreateCell().SetPaddingTop(5));
            table.AddCell(CreateCell().SetPaddingTop(5));

            table.AddCell(CreateCellSubSectionLabel("Ingresada"));
            table.AddCell(CreateCellSubSectionValueBold("CERJ901228LY3"));

            table.AddCell(CreateCellSubSectionLabel("Generada"));
            table.AddCell(CreateCellSubSectionValueBold("CERJ901228LY3"));

            table.AddCell(CreateCellSubSectionLabel("Resultado"));
            table.AddCell(CreateCellSubSectionValue("Validado/Coincide"));

            document.Add(table);
        }

        private void CreateProfessionalCertificates()
        {
            float[] cellWidth = { 25, 45, 30f };
            var table = CreateTable(cellWidth);
            table.SetMarginTop(15);
            table.SetPaddingRight(20);

            table.AddCell(CreateCellSubSection("CONSULTA DE CÉDULAS PROFESIONALES", 1, 3));

            //--foreach() ---------------------------------------------------->

            table.AddCell(CreateCell().SetPaddingTop(5));
            table.AddCell(CreateCell().SetPaddingTop(5));
            table.AddCell(CreateCell(8, 1));

            table.AddCell(CreateCellSubSectionLabel("Nombre"));
            table.AddCell(CreateCellValue("JESUS EDGARDO CERVANTES RIOS").SetPaddingLeft(20));

            table.AddCell(CreateCellSubSectionLabel("Género"));
            table.AddCell(CreateCellValue(string.Empty).SetPaddingLeft(20));

            table.AddCell(CreateCellSubSectionLabel("No. de Cédula"));
            table.AddCell(CreateCellValue("11932309").SetPaddingLeft(20));

            table.AddCell(CreateCellSubSectionLabel("Título"));
            table.AddCell(CreateCellValue("MAESTRÍA EN CIENCIAS DE LA COMPUTACIÓN").SetPaddingLeft(20));

            table.AddCell(CreateCellSubSectionLabel("Institución"));
            table.AddCell(CreateCellValue("INSTITUTO TECNOLÓGICO DE HERMOSILLO").SetPaddingLeft(20));

            table.AddCell(CreateCellSubSectionLabel("Año de Registro"));
            table.AddCell(CreateCellValue("2020").SetPaddingLeft(20));

            table.AddCell(CreateCellSubSectionLabel("Tipo"));
            table.AddCell(CreateCellValue(string.Empty).SetPaddingLeft(20));





            table.AddCell(CreateCell().SetPaddingTop(20));
            table.AddCell(CreateCell().SetPaddingTop(20));
            table.AddCell(CreateCell(7, 1));

            table.AddCell(CreateCellSubSectionLabel("Nombre"));
            table.AddCell(CreateCellValue("JESUS EDGARDO CERVANTES RIOS").SetPaddingLeft(20));

            table.AddCell(CreateCellSubSectionLabel("Género"));
            table.AddCell(CreateCellValue(string.Empty).SetPaddingLeft(20));

            table.AddCell(CreateCellSubSectionLabel("No. de Cédula"));
            table.AddCell(CreateCellValue("8580163").SetPaddingLeft(20));

            table.AddCell(CreateCellSubSectionLabel("Título"));
            table.AddCell(CreateCellValue("LICENCIATURA EN INGENIERÍA EN SISTEMAS COMPUTACIONALES").SetPaddingLeft(20));

            table.AddCell(CreateCellSubSectionLabel("Institución"));
            table.AddCell(CreateCellValue("INSTITUTO TECNOLÓGICO DE HERMOSILLO, SON. (I.T.R.)").SetPaddingLeft(20));

            table.AddCell(CreateCellSubSectionLabel("Año de Registro"));
            table.AddCell(CreateCellValue("2014").SetPaddingLeft(20));

            table.AddCell(CreateCellSubSectionLabel("Tipo"));
            table.AddCell(CreateCellValue(string.Empty).SetPaddingLeft(20));

            document.Add(table);


            text = new Paragraph("NO SE ENCONTRÓ REGISTRO DE LA PERSONA EN EL REGISTRO NACIONAL DE PROFESIONISTAS")
                .AddStyle(boldTextStyle)
                .SetFontSize(10);
            text.SetMarginTop(30);
            text.SetPaddingLeft(40);
            text.SetPaddingRight(100);

            document.Add(text);
        }

        private void CreateSatSection()
        {
            float[] cellWidth = { 35f, 45f, 20f };
            var table = CreateTable(cellWidth);
            table.SetPaddingRight(20);

            table.AddCell(CreateCellSubSection("CONSULTA DE CONTRIBUYENTES BOLETINADOS SAT (69 B)", 1, 3));

            table.AddCell(CreateCell().SetPaddingTop(5));
            table.AddCell(CreateCell().SetPaddingTop(5));
            table.AddCell(CreateCell(4, 1));

            table.AddCell(CreateCellSubSectionLabel("Nombre de Contribuyente"));
            table.AddCell(CreateCellValue("").SetPaddingLeft(20));

            table.AddCell(CreateCellSubSectionLabel("RFC"));
            table.AddCell(CreateCellValue("").SetPaddingLeft(20));

            table.AddCell(CreateCellSubSectionLabel("Situación de Contribuyente"));
            table.AddCell(CreateCellValue("").SetPaddingLeft(20));

            document.Add(table);

            var names = new string[]
            {
                "Presunto",
                "Desvirtuado",
                "Definitivo",
                "Con Sentencia Favorable"
            };

            foreach (var item in names)
            {
                var title = item.Contains("Con") ? item : item + "s";
                text = new Paragraph(title)
                    .AddStyle(boldTextStyle)
                    .SetMarginLeft(40)
                    .SetMarginTop(10)
                    .SetFontColor(new DeviceRgb(30, 18, 72));

                document.Add(text);

                table = CreateTable(new float[] { 30f, 30f, 30f });
                table.SetMarginLeft(40)
                    .SetMarginRight(20);

                var leyenda = item.Contains("Presunto") ? "de Presunción" : item.Contains("Desvirtuado") ? "de Desvirtuados" : item;

                table.AddCell(CreateCellHead("Número y Fecha de Oficio Global " + leyenda));
                table.AddCell(CreateCellHead("Publicación Página SAT " + title));
                table.AddCell(CreateCellHead("Publicación Diario Oficial de la Federación " + title));

                table.AddCell(CreateCellValue("").SetPaddingRight(10).SetPaddingTop(10));
                table.AddCell(CreateCellValue("").SetPaddingLeft(10).SetPaddingTop(10));
                table.AddCell(CreateCellValue("").SetPaddingLeft(10).SetPaddingTop(10));

                document.Add(table);
            }

            text = new Paragraph("NO SE ENCONTRÓ REGISTRO DE LA PERSONA EN LAS LISTAS DE CONTRIBUYENTES BOLETINADOS DEL SAT")
                .AddStyle(boldTextStyle)
                .SetFontSize(10);
            text.SetMarginTop(30);
            text.SetPaddingLeft(40);
            text.SetPaddingRight(100);

            document.Add(text);
        }

        private void CreateInterpolSection()
        {
            float[] cellWidth = { 25f, 30f, 45f };
            var table = CreateTable(cellWidth);
            table.SetPaddingRight(20);

            table.AddCell(CreateCellSubSection("CONSULTA DE LISTAS DE INTERPOL (FICHA ROJA)", 1, 3));

            table.AddCell(CreateCell(1, 3).SetPaddingTop(5));

            var url = "https://ws-public.interpol.int/notices/v1/red/2017-292912/images/60908874";
            table.AddCell(CreateCellForImage("", "FOTO", 150, 14, 11, 1))
                .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .SetHorizontalAlignment(HorizontalAlignment.CENTER);

            table.AddCell(CreateCellSubSectionLabel("Nombre (s)"));
            table.AddCell(CreateCellValue("").SetPaddingLeft(20).SetPaddingRight(10));

            table.AddCell(CreateCellSubSectionLabel("Apellido Paterno"));
            table.AddCell(CreateCellValue("").SetPaddingLeft(20).SetPaddingRight(10));

            table.AddCell(CreateCellSubSectionLabel("Apellido Materno"));
            table.AddCell(CreateCellValue("").SetPaddingLeft(20).SetPaddingRight(10));

            table.AddCell(CreateCellSubSectionLabel("Fecha de Nacimiento"));
            table.AddCell(CreateCellValue("").SetPaddingLeft(20).SetPaddingRight(10));

            table.AddCell(CreateCellSubSectionLabel("Peso"));
            table.AddCell(CreateCellValue("").SetPaddingLeft(20).SetPaddingRight(10));

            table.AddCell(CreateCellSubSectionLabel("Altura"));
            table.AddCell(CreateCellValue("").SetPaddingLeft(20).SetPaddingRight(10));

            table.AddCell(CreateCellSubSectionLabel("Sexo"));
            table.AddCell(CreateCellValue("").SetPaddingLeft(20).SetPaddingRight(10));

            table.AddCell(CreateCellSubSectionLabel("Marcas Distintivas"));
            table.AddCell(CreateCellValue("").SetPaddingLeft(20).SetPaddingRight(10));

            table.AddCell(CreateCellSubSectionLabel("Color de Ojos"));
            table.AddCell(CreateCellValue("").SetPaddingLeft(20).SetPaddingRight(10));

            table.AddCell(CreateCellSubSectionLabel("Color de Cabello"));
            table.AddCell(CreateCellValue("").SetPaddingLeft(20).SetPaddingRight(10));

            table.AddCell(CreateCellSubSectionLabel("Lugar de Nacimiento"));
            table.AddCell(CreateCellValue("").SetPaddingLeft(20).SetPaddingRight(10));

            document.Add(table);

            text = new Paragraph("NO SE ENCONTRÓ REGISTRO DE LA PERSONA EN LAS LISTAS DE INTERPOL")
                .AddStyle(boldTextStyle)
                .SetFontSize(10);
            text.SetMarginTop(30);
            text.SetPaddingLeft(40);
            text.SetPaddingRight(100);

            document.Add(text);
        }



        private void CreateSearchParameterSectionAdvanced()
        {
            document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));

            CreateSectionLabel("Parametros de la Búsqueda");

            float[] cellWidth = { 25f, 12f, 12f, 12f, 32f };
            var table = CreateTable(cellWidth);

            table.AddCell(CreateCellLabel("Nombre (s) *"));
            table.AddCell(CreateCellValue("", 1, 3));

            var tableimg = CreateTable(new float[] { 50f, 50f })
                .SetMarginRight(20);

            tableimg.AddCell(CreateCellLabel("Cargar imagen", 1, 2, true, true)
                .SetHorizontalAlignment(HorizontalAlignment.CENTER));

            tableimg.AddCell(CreateCellForImage("", "INE ANVERSO", 65, 6, 1, 1))
                .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .SetHorizontalAlignment(HorizontalAlignment.CENTER);

            tableimg.AddCell(CreateCellForImage("", "INE REVERSO", 65, 6, 1, 1))
                .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .SetHorizontalAlignment(HorizontalAlignment.CENTER);

            tableimg.AddCell(CreateCellForImage("", "COMPROBANTE", 65, 6, 1, 1))
                .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .SetHorizontalAlignment(HorizontalAlignment.CENTER);

            tableimg.AddCell(CreateCellForImage("", "FOTOGRAFÍA", 65, 6, 1, 1))
                .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .SetHorizontalAlignment(HorizontalAlignment.CENTER);

            tableimg.AddCell(CreateCellLabel("Tipo de Documento", 1, 2).SetPaddingTop(10));
            tableimg.AddCell(CreateCellValue("", 1, 2));

            var cell = CreateCell(10, 1).SetHorizontalAlignment(HorizontalAlignment.CENTER);
            cell.Add(tableimg);
            table.AddCell(cell);

            table.AddCell(CreateCellLabel("Apellido Paterno *"));
            table.AddCell(CreateCellValue("", 1, 3));

            table.AddCell(CreateCellLabel("Apellido Materno *"));
            table.AddCell(CreateCellValue("", 1, 3));

            table.AddCell(CreateCellLabel("Sexo"));
            table.AddCell(CreateCellValue("", 1, 3));

            table.AddCell(CreateCellLabel("Estado de Nacimiento"));
            table.AddCell(CreateCellValue("", 1, 3));

            table.AddCell(CreateCellLabel("Fecha de Nacimiento"));
            table.AddCell(CreateCellValue(""));
            table.AddCell(CreateCellValue("", 1, 1, 10));
            table.AddCell(CreateCellValue("", 1, 1, 10));

            table.AddCell(CreateCellLabel("RFC"));
            table.AddCell(CreateCellValue("", 1, 3));

            table.AddCell(CreateCellLabel("CURP"));
            table.AddCell(CreateCellValue("", 1, 3));

            table.AddCell(CreateCellLabel("Teléfono"));
            table.AddCell(CreateCellValue("", 1, 3));

            table.AddCell(CreateCellLabel("Correo electrónico"));
            table.AddCell(CreateCellValue("", 1, 3));

            document.Add(table);
        }

        private void CreateAddressSectionAdvanced()
        {
            float[] cellWidth = { 35f, 45f, 20f };
            var table = CreateTable(cellWidth);
            table.SetPaddingRight(20);

            table.AddCell(CreateCellSubSection("COMPROBANTE DE DOMICILIO", 1, 3));

            table.AddCell(CreateCell().SetPaddingTop(5));
            table.AddCell(CreateCell().SetPaddingTop(5));
            table.AddCell(CreateCell(4, 1));

            table.AddCell(CreateCellSubSectionLabel("Nombre"));
            table.AddCell(CreateCellValue("").SetPaddingLeft(20));

            table.AddCell(CreateCellSubSectionLabel("Domicilio"));
            table.AddCell(CreateCellValue("").SetPaddingLeft(20));

            table.AddCell(CreateCellSubSectionLabel("Tipo de documento"));
            table.AddCell(CreateCellValue("").SetPaddingLeft(20));

            document.Add(table);
        }

        private void CreateINESectionAdvanced()
        {
            float[] cellWidth = { 22f, 28f, 22f, 28f };
            var table = CreateTable(cellWidth);
            table.SetPaddingRight(20);
            table.AddCell(CreateCellSubSection("IDENTIFICACIÓN OFICIAL INE", 1, 4));
            document.Add(table);

            table = CreateTable(cellWidth);
            table.SetPaddingRight(20);
            table.SetMarginLeft(40)
                .SetMarginRight(20);

            table.AddCell(CreateSubHeaderCell("ANVERSO", 1, 4).SetPaddingTop(10));

            table.AddCell(CreateCellLabel("Nombre").SetPaddingTop(10));
            table.AddCell(CreateCellValue("").SetPaddingTop(10));

            table.AddCell(CreateCellLabel("Calle y Número").SetPaddingLeft(5).SetPaddingTop(10));
            table.AddCell(CreateCellValue("").SetPaddingTop(10));

            table.AddCell(CreateCellLabel("Apellido Paterno"));
            table.AddCell(CreateCellValue(""));

            table.AddCell(CreateCellLabel("C P").SetPaddingLeft(5));
            table.AddCell(CreateCellValue(""));

            table.AddCell(CreateCellLabel("Apellido Materno"));
            table.AddCell(CreateCellValue(""));

            table.AddCell(CreateCellLabel("Colonia").SetPaddingLeft(5));
            table.AddCell(CreateCellValue(""));

            table.AddCell(CreateCellLabel("Edad"));
            table.AddCell(CreateCellValue(""));

            table.AddCell(CreateCellLabel("Estado").SetPaddingLeft(5));
            table.AddCell(CreateCellValue(""));

            table.AddCell(CreateCellLabel("Sexo"));
            table.AddCell(CreateCellValue(""));

            table.AddCell(CreateCellLabel("Localidad").SetPaddingLeft(5));
            table.AddCell(CreateCellValue(""));

            table.AddCell(CreateCellLabel("Fecha de nacimiento"));
            table.AddCell(CreateCellValue(""));

            table.AddCell(CreateCellLabel("Municipio").SetPaddingLeft(5));
            table.AddCell(CreateCellValue(""));

            table.AddCell(CreateCellLabel("Credencial"));
            table.AddCell(CreateCellValue(""));

            table.AddCell(CreateCellLabel("Clave").SetPaddingLeft(5));
            table.AddCell(CreateCellValue(""));

            table.AddCell(CreateCellLabel("Instituto"));
            table.AddCell(CreateCellValue(""));

            table.AddCell(CreateCellLabel("Sección").SetPaddingLeft(5));
            table.AddCell(CreateCellValue(""));

            table.AddCell(CreateCellLabel("Registro"));
            table.AddCell(CreateCellValue(""));

            table.AddCell(CreateCellLabel("Año de Registro").SetPaddingLeft(5));
            table.AddCell(CreateCellValue(""));

            table.AddCell(CreateCellLabel("Emisión"));
            table.AddCell(CreateCellValue(""));

            table.AddCell(CreateCellLabel("Clave de Elector").SetPaddingLeft(5));
            table.AddCell(CreateCellValue(""));

            table.AddCell(CreateCellLabel("Vigencia"));
            table.AddCell(CreateCellValue(""));

            table.AddCell(CreateCellLabel("Folio").SetPaddingLeft(5));
            table.AddCell(CreateCellValue(""));

            table.AddCell(CreateCellLabel("CURP"));
            table.AddCell(CreateCellValue(""));

            document.Add(table);

            table = CreateTable(new float[] { 25f, 25f, 25f, 15f, 10f });
            table.SetPaddingRight(20)
                .SetMarginLeft(40);

            table.AddCell(CreateCheckboxCell("Escudo", true, 1, 1));
            table.AddCell(CreateCheckboxCell("Foto izquierda", false, 1, 1));
            table.AddCell(CreateCheckboxCell("Foto fantasma", true, 1, 1));
            table.AddCell(CreateCell());
            table.AddCell(CreateCell());

            table.AddCell(CreateCheckboxCell("Foto Marca Agua", false, 1, 1));
            table.AddCell(CreateCheckboxCell("Mapa México", true, 1, 1));
            table.AddCell(CreateCheckboxCell("Banda izquierda", false, 1, 1));
            table.AddCell(CreateCell());
            table.AddCell(CreateCell());

            document.Add(table);

            table = CreateTable(new float[] { 10f, 10f, 20f, 20f, 20f, 20f });
            table.SetPaddingRight(20);
            table.SetMarginLeft(40)
                .SetMarginRight(20);

            table.AddCell(CreateSubHeaderCell("REVERSO", 1, 6).SetPaddingTop(10));

            table.AddCell(CreateCellLabel("MRZ").SetPaddingTop(20));
            table.AddCell(CreateCellValue("", 1, 2).SetPaddingTop(20));
            table.AddCell(CreateCell(1, 3).SetPaddingTop(10));

            table.AddCell(CreateCheckboxCell("Código QR", true, 1, 2).SetPaddingTop(10));
            table.AddCell(CreateCheckboxCell("Firma", false, 1, 1));
            table.AddCell(CreateCheckboxCell("Firma Instituto", true, 1, 1));
            table.AddCell(CreateCheckboxCell("Huella Digital", true, 1, 2));

            table.AddCell(CreateCheckboxCell("Logo INE", true, 1, 2));
            table.AddCell(CreateCheckboxCell("Código Barras Unidimensional", false, 1, 2));
            table.AddCell(CreateCheckboxCell("Código Barras Bidimensional", true, 1, 2));

            document.Add(table);
        }

        private void CreateRUGSectionAdvanced()
        {
            float[] cellWidth = { 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f, 12.5f };
            var table = CreateTable(cellWidth);
            table.SetPaddingRight(20);

            table.AddCell(CreateCellSubSection("REGISTRO ÚNICO DE GARANTÍAS MOBILIARIAS (RUG)", 1, 8));
            table.AddCell(CreateCell(1, 8).SetPaddingTop(10));
            table.AddCell(CreateCellHead("Número de Garantía", 6));
            table.AddCell(CreateCellHead("Número de Operación", 6));
            table.AddCell(CreateCellHead("Tipo de Operación", 6));
            table.AddCell(CreateCellHead("Nombre del Otorgante", 6));
            table.AddCell(CreateCellHead("Folio Electrónico", 6));
            table.AddCell(CreateCellHead("Fecha de Inscripción", 6));
            table.AddCell(CreateCellHead("Tipo de Bienes Muebles", 6));
            table.AddCell(CreateCellHead("URL", 6));

            table.AddCell(CreateCellValue("", 1, 1, 0, 6).SetPaddingRight(5));
            table.AddCell(CreateCellValue("", 1, 1, 0, 6).SetPaddingRight(5));
            table.AddCell(CreateCellValue("", 1, 1, 0, 6).SetPaddingRight(5));
            table.AddCell(CreateCellValue("", 1, 1, 0, 6).SetPaddingRight(5));
            table.AddCell(CreateCellValue("", 1, 1, 0, 6).SetPaddingRight(5));
            table.AddCell(CreateCellValue("", 1, 1, 0, 6).SetPaddingRight(5));
            table.AddCell(CreateCellValue("", 1, 1, 0, 6).SetPaddingRight(5));
            table.AddCell(CreateCellValue("", 1, 1, 0, 6));


            document.Add(table);

            text = new Paragraph("NO SE ENCONTRÓ REGISTRO ÚNICO DE GARANTÍAS MOBILIARIAS")
                .AddStyle(boldTextStyle)
                .SetFontSize(10);
            text.SetMarginTop(20);
            text.SetPaddingLeft(40);
            text.SetPaddingRight(100);

            document.Add(text);
        }

        private void CreateEnrichmentSectionAdvanced()
        {
            document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
            float[] cellWidth = { 30f, 5f, 30f, 5f, 30f };
            var table = CreateTable(cellWidth);
            table.SetPaddingRight(20);

            table.AddCell(CreateCellSubSection("ENRIQUECIMIENTO DE DATOS", 1, 5));
            table.AddCell(CreateCell(1, 5).SetPaddingTop(10));

            table.AddCell(CreateCellLabel("Edad").SetPaddingLeft(10));
            table.AddCell(CreateCellValue("", 1, 2));
            table.AddCell(CreateCell(1, 2));

            table.AddCell(CreateCellLabel("Género").SetPaddingLeft(10));
            table.AddCell(CreateCellValue("", 1, 2));
            table.AddCell(CreateCell(1, 2));

            table.AddCell(CreateCellLabel("Fecha de Nacimiento").SetPaddingLeft(10));
            table.AddCell(CreateCellValue("", 1, 2));
            table.AddCell(CreateCell(1, 2));

            table.AddCell(CreateCell(1, 5).SetPaddingTop(10));

            table.AddCell(CreateCellHead("Nombres", 8).SetPaddingTop(5).SetPaddingBottom(5));
            table.AddCell(CreateCell(1, 1));

            table.AddCell(CreateCellHead("Teléfonos", 8));
            table.AddCell(CreateCell(1, 1));

            table.AddCell(CreateCellHead("Correos Electrónicos", 8));

            table.AddCell(CreateCellValue("", 1, 1, 0, 8).SetPaddingRight(5));
            table.AddCell(CreateCell(1, 1));

            table.AddCell(CreateCellValue("", 1, 1, 0, 8).SetPaddingRight(5));
            table.AddCell(CreateCell(1, 1));

            table.AddCell(CreateCellValue("", 1, 1, 0, 8).SetPaddingRight(5));

            table.AddCell(CreateCell(1, 5).SetPaddingTop(10));

            table.AddCell(CreateCellHead("Redes Sociales y Sitios Web", 8, 1, 3, TextAlignment.LEFT, 10).SetPaddingTop(5).SetPaddingBottom(5));
            table.AddCell(CreateCell(1, 1));

            table.AddCell(CreateCellHead("Nombres de Usuario", 8));

            table.AddCell(CreateCellValue("", 1, 3, 0, 8).SetPaddingRight(5));
            table.AddCell(CreateCell(1, 1));

            table.AddCell(CreateCellValue("", 1, 1, 0, 8).SetPaddingRight(5));

            table.AddCell(CreateCell(1, 5).SetPaddingTop(10));

            table.AddCell(CreateCellHead("Direcciónes", 8, 1, 5, TextAlignment.LEFT, 10).SetPaddingTop(5).SetPaddingBottom(5));

            table.AddCell(CreateCellValue("", 1, 5, 0, 8).SetPaddingRight(5));

            table.AddCell(CreateCell(1, 5).SetPaddingTop(10));

            table.AddCell(CreateCellHead("Trabajos", 8, 1, 5, TextAlignment.LEFT, 10).SetPaddingTop(5).SetPaddingBottom(5));

            table.AddCell(CreateCellValue("", 1, 5, 0, 8).SetPaddingRight(5));

            table.AddCell(CreateCell(1, 5).SetPaddingTop(10));

            table.AddCell(CreateCellHead("Estudios", 8).SetPaddingTop(5).SetPaddingBottom(5));
            table.AddCell(CreateCell(1, 1));

            table.AddCell(CreateCellHead("Relaciones", 8));
            table.AddCell(CreateCell(1, 1));

            table.AddCell(CreateCellHead("Países", 8));

            table.AddCell(CreateCellValue("", 1, 1, 0, 8).SetPaddingRight(5));
            table.AddCell(CreateCell(1, 1));

            table.AddCell(CreateCellValue("", 1, 1, 0, 8).SetPaddingRight(5));
            table.AddCell(CreateCell(1, 1));

            table.AddCell(CreateCellValue("", 1, 1, 0, 8).SetPaddingRight(5));

            table.AddCell(CreateCell(1, 5).SetPaddingTop(10));

            document.Add(table);

            table = CreateTable(new float[] { 20f, 5f, 20f, 5f, 20f, 5f, 20f, 5f });
            table.SetMarginRight(20).SetMarginLeft(20);

            table.AddCell(CreateCellHead("Imagenes", 8, 1, 8, TextAlignment.LEFT, 10).SetPaddingTop(5).SetPaddingBottom(5));
            table.AddCell(CreateCell(1, 8).SetPaddingTop(10));


            table.AddCell(CreateCellForImage("", "IMAGEN", 100, 6, 1, 1))
                .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .SetHorizontalAlignment(HorizontalAlignment.CENTER);
            table.AddCell(CreateCell(1, 1));

            table.AddCell(CreateCellForImage("", "IMAGEN", 100, 6, 1, 1))
                .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .SetHorizontalAlignment(HorizontalAlignment.CENTER);
            table.AddCell(CreateCell(1, 1));

            table.AddCell(CreateCellForImage("", "IMAGEN", 100, 6, 1, 1))
                .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .SetHorizontalAlignment(HorizontalAlignment.CENTER);
            table.AddCell(CreateCell(1, 1));

            table.AddCell(CreateCellForImage("", "IMAGEN", 100, 6, 1, 1))
                .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .SetHorizontalAlignment(HorizontalAlignment.CENTER);

            table.AddCell(CreateCell(1, 1));

            document.Add(table);

        }

        private void CreateSigerSectionAdvanced()
        {
            float[] cellWidth = { 22f, 28f, 22f, 28f };
            var table = CreateTable(cellWidth);
            table.SetPaddingRight(20);
            table.AddCell(CreateCellSubSection("REGISTRO PÚBLICO DE COMERCIO SIGER", 1, 4));
            document.Add(table);

            table.AddCell(CreateCellLabel("FME").SetPaddingTop(10));
            table.AddCell(CreateCellValue("").SetPaddingTop(10));

            table.AddCell(CreateCellLabel("Nombre de Socios").SetPaddingLeft(5).SetPaddingTop(10));
            table.AddCell(CreateCellValue("").SetPaddingTop(10));

            table.AddCell(CreateCellLabel("Nombre/Razón Social"));
            table.AddCell(CreateCellValue(""));

            table.AddCell(CreateCellLabel("Tipo de persona").SetPaddingLeft(5));
            table.AddCell(CreateCellValue(""));

            table.AddCell(CreateCellLabel("Objeto Social", 3, 1));
            table.AddCell(CreateCellValue("", 3, 1));

            table.AddCell(CreateCellLabel("Antecedente Registral").SetPaddingLeft(5));
            table.AddCell(CreateCellValue(""));

            table.AddCell(CreateCellLabel("Régimen jurídico"));
            table.AddCell(CreateCellValue(""));

            table.AddCell(CreateCellLabel("Giro").SetPaddingLeft(5));
            table.AddCell(CreateCellValue(""));

            table.AddCell(CreateCellLabel("Duración de la sociedad"));
            table.AddCell(CreateCellValue(""));

            table.AddCell(CreateCellLabel("Entidad Federativa").SetPaddingLeft(5));
            table.AddCell(CreateCellValue(""));

            table.AddCell(CreateCellLabel("Domicilio Social", 2, 1));
            table.AddCell(CreateCellValue("", 2, 1));

            table.AddCell(CreateCellLabel("Municipio").SetPaddingLeft(5));
            table.AddCell(CreateCellValue(""));

            table.AddCell(CreateCellLabel("Oficina Registral"));
            table.AddCell(CreateCellValue(""));

            table.AddCell(CreateCellLabel("Nacionalidad").SetPaddingLeft(5));
            table.AddCell(CreateCellValue(""));

            table.AddCell(CreateCellLabel("Fecha de Inscripción"));
            table.AddCell(CreateCellValue(""));

            table.AddCell(CreateCellLabel("RFC").SetPaddingLeft(5));
            table.AddCell(CreateCellValue(""));

            table.AddCell(CreateCellLabel("CURP"));
            table.AddCell(CreateCellValue(""));

            table.AddCell(CreateCellLabel("Estatus FME"));
            table.AddCell(CreateCellValue(""));

            table.AddCell(CreateCell(1, 2));

            table.AddCell(CreateCell(1, 4).SetBorderBottom(new SolidBorder(new DeviceRgb(30, 18, 72), 1)));
            table.AddCell(CreateCell(1, 4).SetPaddingTop(10));

            document.Add(table);
            table = CreateTable(new float[] { 10f, 30f, 5f, 10f, 30f });
            table.SetPaddingRight(20);

            table.AddCell(CreateCellHead("Forma precodificada").SetPaddingTop(10).SetPaddingBottom(10));
            table.AddCell(CreateCellValue("").SetPaddingLeft(5).SetPaddingRight(5));
            table.AddCell(CreateCell());
            table.AddCell(CreateCellHead("Fecha de Inscripción").SetPaddingTop(10).SetPaddingBottom(10));
            table.AddCell(CreateCellValue("").SetPaddingLeft(5).SetPaddingRight(5));

            table.AddCell(CreateCell(1, 5).SetPaddingTop(1));

            table.AddCell(CreateCellHead("Acto").SetPaddingTop(10).SetPaddingBottom(10));
            table.AddCell(CreateCellValue("").SetPaddingLeft(5).SetPaddingRight(5));
            table.AddCell(CreateCell());
            table.AddCell(CreateCellHead("Número de documento").SetPaddingTop(10).SetPaddingBottom(10));
            table.AddCell(CreateCellValue("").SetPaddingLeft(5).SetPaddingRight(5));

            table.AddCell(CreateCell(1, 5).SetPaddingTop(1));

            table.AddCell(CreateCellHead("NCI").SetPaddingTop(10).SetPaddingBottom(10));
            table.AddCell(CreateCellValue("").SetPaddingLeft(5).SetPaddingRight(5));
            table.AddCell(CreateCell());
            table.AddCell(CreateCellHead("Modalidad").SetPaddingTop(10).SetPaddingBottom(10));
            table.AddCell(CreateCellValue("").SetPaddingLeft(5).SetPaddingRight(5));

            table.AddCell(CreateCell(1, 5).SetPaddingTop(1));

            table.AddCell(CreateCellHead("Fecha de Ingreso").SetPaddingTop(10).SetPaddingBottom(10));
            table.AddCell(CreateCellValue("").SetPaddingLeft(5).SetPaddingRight(5));
            table.AddCell(CreateCell());
            table.AddCell(CreateCellHead("Ver PDF").SetPaddingTop(10).SetPaddingBottom(10));
            table.AddCell(CreateCellSubSectionLink("Descargar", "https://nufistorage001.blob.core.windows.net/seif/2020-11-27/20201127T024923546.pdf", textBoxStyle)
                .SetPaddingLeft(5));


            document.Add(table);

            text = new Paragraph("NO SE ENCONTRÓ REGISTRO DE LA PERSONA EN EL REGISTRO PUBLICO DE COMERCIO")
                .AddStyle(boldTextStyle)
                .SetFontSize(10);
            text.SetMarginTop(20);
            text.SetPaddingLeft(40);
            text.SetPaddingRight(100);

            document.Add(text);
        }

        private void CreateIMPISectionAdvanced()
        {
            document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
            float[] cellWidth = { 16.5f, 16.5f, 16.5f, 16.5f, 16.5f, 16.5f };
            var table = CreateTable(cellWidth);
            table.SetMarginRight(20);

            table.AddCell(CreateCellSubSection("SISTEMA DE INTELIGENCIA ARTIFICIAL PARA MARCAS DEL IMPI", 1, 6));
            table.AddCell(CreateCell(1, 6).SetPaddingTop(10));
            table.AddCell(CreateCellHead("Número de Expediente", 9));
            table.AddCell(CreateCellHead("Imagen", 9));
            table.AddCell(CreateCellHead("Marca", 9));
            table.AddCell(CreateCellHead("Tipo de Solicitud", 9));
            table.AddCell(CreateCellHead("Títular", 9));
            table.AddCell(CreateCellHead("Estatus", 9));

            table.AddCell(CreateCellValue("", 1, 1, 0, 9).SetPaddingRight(5));
            table.AddCell(CreateCellForImage("", "IMAGEN", 50, 6))
                .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .SetHorizontalAlignment(HorizontalAlignment.CENTER);
            table.AddCell(CreateCellValue("", 1, 1, 0, 9).SetPaddingRight(5));
            table.AddCell(CreateCellValue("", 1, 1, 0, 9).SetPaddingRight(5));
            table.AddCell(CreateCellValue("", 1, 1, 0, 9).SetPaddingRight(5));
            table.AddCell(CreateCellValue("", 1, 1, 0, 9).SetPaddingRight(5));

            table.AddCell(CreateCell(1, 6).SetPaddingTop(20));
            table.AddCell(CreateCellLabel("Datos Generales", 1, 4, true, false, titleStyle));
            table.AddCell(CreateCellLabel("Marca", 1, 2, true, false, titleStyle));

            table.AddCell(CreateCellLabel("Denominación", 1, 2));
            table.AddCell(CreateCellValue("", 1, 2).SetPaddingRight(30));

            table.AddCell(CreateCellValue("", 2, 2));

            table.AddCell(CreateCellLabel("Número de Expediente", 1, 2));
            table.AddCell(CreateCellValue("", 1, 2).SetPaddingRight(30));

            table.AddCell(CreateCellLabel("Fecha de Presentación", 1, 2));
            table.AddCell(CreateCellValue("", 1, 2).SetPaddingRight(30));

            table.AddCell(CreateCellLabel("Información del Titular", 1, 2, true, false, titleStyle));
            table.AddCell(CreateCellLabel("Fecha de Publicación de la solicitud", 1, 2));
            table.AddCell(CreateCellValue("", 1, 2).SetPaddingRight(30));

            table.AddCell(CreateCellLabel("Nombre"));
            table.AddCell(CreateCellValue(""));

            table.AddCell(CreateCellLabel("Tipo de Solicitud", 2, 2));
            table.AddCell(CreateCellValue("", 2, 2).SetPaddingRight(30));

            table.AddCell(CreateCellLabel("Dirección"));
            table.AddCell(CreateCellValue(""));

            table.AddCell(CreateCellLabel("País"));
            table.AddCell(CreateCellValue(""));

            table.AddCell(CreateCellLabel("Productos y Servicios", 1, 6, true, false, titleStyle));

            table.AddCell(CreateCellHead("Clase", 9).SetPaddingTop(5).SetPaddingBottom(5));
            table.AddCell(CreateCellHead("Descripción", 9, 1, 5).SetPaddingTop(5).SetPaddingBottom(5));
            table.AddCell(CreateCellValue("", 1, 1, 0, 9).SetPaddingRight(5));
            table.AddCell(CreateCellValue("", 1, 5, 0, 9).SetPaddingRight(5));

            table.AddCell(CreateCell(1, 6).SetPaddingTop(10));

            table.AddCell(CreateCellHead("Imagen", 9));
            table.AddCell(CreateCellHead("Folio de Entrada del Trámite", 9));
            table.AddCell(CreateCellHead("Año de Recepción", 9));
            table.AddCell(CreateCellHead("Descripción", 9));
            table.AddCell(CreateCellHead("Fecha de Inicio", 9));
            table.AddCell(CreateCellHead("Fecha de Conclusión", 9));

            table.AddCell(CreateCellForImage("", "IMAGEN", 50, 6))
                .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .SetHorizontalAlignment(HorizontalAlignment.CENTER);
            table.AddCell(CreateCellValue("", 1, 1, 0, 9).SetPaddingRight(5));
            table.AddCell(CreateCellValue("", 1, 1, 0, 9).SetPaddingRight(5));
            table.AddCell(CreateCellValue("", 1, 1, 0, 9).SetPaddingRight(5));
            table.AddCell(CreateCellValue("", 1, 1, 0, 9).SetPaddingRight(5));
            table.AddCell(CreateCellValue("", 1, 1, 0, 9).SetPaddingRight(5));

            document.Add(table);
        }

    }
}
