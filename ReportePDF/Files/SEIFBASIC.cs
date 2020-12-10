using iText.Kernel.Colors;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;

namespace ReportePDF.Files
{
    public class SEIFBASIC : PDFBase2
    {
        public SEIFBASIC() : base(DateTime.Now, "49dc0d56-2714-4d2a-b535-3a962df06fab", "SEIF BASIC", "Persona Física")
        {

        }

        public void CreatePDF()
        {

            AttachedInfo(new List<string>() { "CURP" });

            CreateUserSection("Jesús Edgardo Cervantes Ríos", "edgardocervantes@outlook.com");

            CreateSearchParameterSection();

            CreateSearchResultSection();

            AddAttached(1, "CURP", "https://nufistorage001.blob.core.windows.net/seif/2020-12-02/20201202T074010954.pdf");

          
            document.Close();
        }

        public void CreateSearchParameterSection()
        {
            var table = CreateTable(new float[] { 22f, 78f });
            var cell = CreateCellSection("PARAMETROS DE BÚSQUEDA", 1, 2);
            table.AddCell(cell);

            cell = CreateCell(1, 2);
            cell.SetHeight(5);
            table.AddCell(cell);

            cell = CreateCellLabel("Nombre (s)", 1, 1);
            table.AddCell(cell);
            cell = CreateCellValue("Jesús Edgardo", 1, 1);
            table.AddCell(cell);

            cell = CreateCellLabel("Apellido Paterno", 1, 1);
            table.AddCell(cell);
            cell = CreateCellValue("Cervantes", 1, 1);
            table.AddCell(cell);

            cell = CreateCellLabel("Apellido Materno", 1, 1);
            table.AddCell(cell);
            cell = CreateCellValue("Ríos", 1, 1);
            table.AddCell(cell);

            cell = CreateCellLabel("Sexo", 1, 1);
            table.AddCell(cell);
            cell = CreateCellValue("Masculino", 1, 1);
            table.AddCell(cell);

            cell = CreateCellLabel("Estado de Nacimiento", 1, 1);
            table.AddCell(cell);
            cell = CreateCellValue("Sonora", 1, 1);
            table.AddCell(cell);

            cell = CreateCellLabel("Fecha de Nacimiento", 1, 1);
            table.AddCell(cell);
            cell = CreateCellValue("28/12/1990", 1, 1);
            table.AddCell(cell);

            cell = CreateCellLabel("RFC", 1, 1);
            table.AddCell(cell);
            cell = CreateCellValue("CERJ901228LY3", 1, 1);
            table.AddCell(cell);

            cell = CreateCellLabel("CURP", 1, 1);
            table.AddCell(cell);
            cell = CreateCellValue("CERJ901228HSRRSS01", 1, 1);
            table.AddCell(cell);

            document.Add(table);
        }

        public void CreateSearchResultSection()
        {
            var table = CreateTable(new float[] { 12f, 88f }).SetMarginTop(20);
            var cell = CreateCellSectionWithBar("RESULTATOS DE LA BÚSQUEDA", "Información general", 1, 2);
            table.AddCell(cell);

            cell = CreateCell(1, 2);
            cell.SetHeight(5);
            table.AddCell(cell);

            cell = CreateCell(1, 2);
            cell.Add(new Paragraph("CALCULAR CURP").AddStyle(section));
            table.AddCell(cell);

            cell = CreateCell(1, 2);
            table.AddCell(cell);

            cell = CreateCellLabel("Ingresada", 1, 1);
            table.AddCell(cell);
            cell = CreateCellValue("CERJ901228HSRRSS01", 1, 1);
            table.AddCell(cell);

            cell = CreateCellLabel("Generada", 1, 1);
            table.AddCell(cell);
            cell = CreateCellValue("CERJ901228HSRRSS01", 1, 1);
            table.AddCell(cell);

            cell = CreateCellLabel("Resultado", 1, 1);
            table.AddCell(cell);
            cell = CreateCellValue("CURP coincide", 1, 1);
            table.AddCell(cell);

            cell = CreateCell(1, 2);
            cell.SetHeight(5);
            table.AddCell(cell);

            cell = CreateCell(1, 2);
            cell.Add(new Paragraph("VALIDAR CURP").AddStyle(section));
            table.AddCell(cell);

            cell = CreateCell(1, 2);
            table.AddCell(cell);

            cell = CreateCellLabel("Ingresada", 1, 1);
            table.AddCell(cell);
            cell = CreateCellValue("CERJ901228HSRRSS01", 1, 1);
            table.AddCell(cell);

            cell = CreateCellLabel("Generada", 1, 1);
            table.AddCell(cell);
            cell = CreateCellValue("CERJ901228HSRRSS01", 1, 1);
            table.AddCell(cell);

            cell = CreateCellLabel("Resultado", 1, 1);
            table.AddCell(cell);
            cell = CreateCellValue("Válido", 1, 1);
            table.AddCell(cell);

            cell = CreateCellLabel("Documento", 1, 1);
            table.AddCell(cell);
            cell = CreateCellValue("Anexo 1", 1, 1);
            table.AddCell(cell);

            document.Add(table);
        }
    }
}
