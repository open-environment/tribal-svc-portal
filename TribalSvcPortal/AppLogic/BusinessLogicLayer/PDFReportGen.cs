using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace TribalSvcPortal.AppLogic.BusinessLogicLayer
{
    internal static class PDFReportGen
    {
        internal static byte[] CreatePDFReport()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Create an instance of the document class which represents the PDF document itself.
                Document document = new Document(PageSize.A4, 25, 25, 25, 25);

                // Create an instance to the PDF file by creating an instance of the PDF Writer class using the document and the filestrem in the constructor.
                PdfWriter writer = PdfWriter.GetInstance(document, ms);

                // Add meta information to the document
                document.AddAuthor("Muscogee Creek Nation");
                document.AddCreator("Open Environment Software");
                document.AddKeywords("Arbor Care");
                document.AddSubject("Arbor Care Work Order");
                document.AddTitle("Arbor Care Work Order");

                // Open the document to enable you to write to the document
                document.Open();

                //define standard font styles used in the document
                BaseFont bfHelv = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);
                Font font_main = new Font(bfHelv, 12, Font.NORMAL, BaseColor.BLACK);
                Font font_main_bold = new Font(bfHelv, 12, Font.BOLD, BaseColor.BLACK);
                Font font_head = new Font(bfHelv, 22, Font.NORMAL, BaseColor.BLACK);
                Font font_small = new Font(bfHelv, 9, Font.BOLD, BaseColor.DARK_GRAY);
                Font font_small_no_bold = new Font(bfHelv, 9, Font.NORMAL, BaseColor.DARK_GRAY);

                // Add document title
                document.Add(new Paragraph("Arbor Care Work Order Report" + Environment.NewLine, font_head));

                // Add document subtitle
                document.Add(new Paragraph("Print Date: " + System.DateTime.Today.ToShortDateString() + Environment.NewLine + Environment.NewLine, font_small));

                // Close the document
                document.Close();

                // Close the writer instance
                writer.Close();

                return ms.ToArray();
            }
        }
    }
}
