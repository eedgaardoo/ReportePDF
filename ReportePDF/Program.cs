using ReportePDF.Files;
using System;

namespace ReportePDF
{
    class Program
    {
        static void Main(string[] args)
        {           
            SEIFBASIC pdf = new SEIFBASIC();
            pdf.CreatePDF();
            Console.WriteLine("Terminado!");
            Console.ReadLine();
        }
    }
}