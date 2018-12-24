using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;

namespace TribalSvcPortal.AppLogic.BusinessLogicLayer
{
    internal class PDFReportGen
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public PDFReportGen(IHostingEnvironment env)
        {
            _hostingEnvironment = env;
        }

        internal byte[] MergePDFReport(string rptTemplatePDF, dynamic data)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // create a new PDF reader based on the PDF template document  
                PdfReader pdfReader = new PdfReader(Path.Combine(_hostingEnvironment.ContentRootPath, "Docs", "OpenDumpSurveyForm.pdf"));
                
                //stamper and memorystream
                PdfStamper pdfStamper = new PdfStamper(pdfReader, ms);

                pdfStamper.AcroFields.SetField("Site Name", data.GetType().GetProperty("SITE_IDX").GetValue(data, null));
                pdfStamper.AcroFields.SetField("Community", data.GetType().GetProperty("SITE_IDX").GetValue(data, null));
                pdfStamper.AcroFields.SetField("Tribe", data.GetType().GetProperty("SITE_IDX").GetValue(data, null));
                pdfStamper.AcroFields.SetField("Site Status", "Active");  //"Inactive"
                pdfStamper.AcroFields.SetField("Latitude N", "111");  //"Inactive"
                pdfStamper.AcroFields.SetField("Longitude W", "222");  //"Inactive"

                // flatten the form to remove editting options, set it to false  
                // to leave the form open to subsequent manual edits  
                pdfStamper.FormFlattening = false;

                foreach (var formField in pdfStamper.AcroFields.Fields)
                {
                    string dd = formField.Key;
                    //var merge_data = data.GetType().GetProperty("SITE_IDX").GetValue(data, null);
                }

                // close the pdf  
                pdfStamper.Close();

                return ms.ToArray();
            }
        }

        internal byte[] CreatePDFReport(string rptTemplateXML, dynamic data)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                //grab template XML from file
                var xml = XDocument.Load(Path.Combine(_hostingEnvironment.ContentRootPath, "Docs", "rpt_OpenDumpSurvey.xml"));

                //var ttt = data.GetType().GetProperty("SITE_IDX").GetValue(data, null); 

                // Create an instance of the document class which represents the PDF document itself.
                Document document = new Document(PageSize.A4, 25, 25, 25, 25);
                string title = xml.Element("report").Attribute("title").Value;

                // Create an instance to the PDF file by creating an instance of the PDF Writer class using the document and the filestrem in the constructor.
                PdfWriter writer = PdfWriter.GetInstance(document, ms);

                // Add meta information to the document
                document.AddAuthor("Muscogee Creek Nation");
                document.AddCreator("Open Environment Software");
                document.AddKeywords(title ?? "Report");
                document.AddSubject(title ?? "Report");
                document.AddTitle(title ?? "Report");

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
                if (title != null)
                    document.Add(new Paragraph(title + Environment.NewLine, font_head));

                // Add document subtitle
                string subtitle = xml.Element("report").Attribute("subtitle").Value;
                if (subtitle != null)
                    document.Add(new Paragraph(subtitle + Environment.NewLine + Environment.NewLine, font_small));


                //read document body
                foreach (XElement f in xml.Element("report").Element("reportfields").Elements("field"))
                {
                    if (f.Attribute("type").Value == "paragraph")
                    {
                        //merge in data from database
                        string prop_name = f.Attribute("value")?.Value;
                        if (prop_name != null)
                        {
                            var merge_data = data.GetType().GetProperty(prop_name).GetValue(data, null);
                            document.Add(new Paragraph(f.Attribute("label")?.Value + (merge_data ?? "") + Environment.NewLine, font_small));
                        }
                    }
                    else if (f.Attribute("type").Value == "table")
                    {
                        PdfPTable t = CreateTable(f, font_small);
                        document.Add(t);
                    }
                }

                // Close the document
                document.Close();

                // Close the writer instance
                writer.Close();

                return ms.ToArray();
            }
        }

        private static void MergeFields(IEnumerable<XElement> fields, Document document, Font font_small)
        {

        }


        private static PdfPTable CreateTable(XElement t_xml, Font font_small)
        {
            //create a PDF Table
            PdfPTable t = new PdfPTable(t_xml.Attribute("cols").Value.ConvertOrDefault<int>());
            t.WidthPercentage = 100;

            //iterate through tablecells 
            foreach (XElement td in t_xml.Elements("tablecell"))
            {
                PdfPCell cell = new PdfPCell() { Padding = 5 };

                //set optional cell background color
                if (td.Attribute("bgcolor")?.Value == "darkgrey")
                    cell.BackgroundColor = new BaseColor(128, 128, 128);

                //colspan of cell
                cell.Colspan = td.Attribute("colspan")?.Value.ConvertOrDefault<int?>() ?? 1;

                //cell horizontal alignment
                cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right

                //handling cell content

                //recursively call function to handle all subfields in table
                //MergeFields(td.Elements("field"), document, font_small);

                foreach (XElement td_cont in td.Elements("field"))
                {
                    if (td_cont.Attribute("type").Value == "paragraph")
                        cell.AddElement(new Phrase(td_cont.Attribute("label")?.Value, font_small));
                    else if (td_cont.Attribute("type").Value == "table")
                    {
                        PdfPTable subtable = CreateTable(td_cont, font_small);
                        cell.AddElement(subtable);
                        //PdfPCell cell3 = new PdfPCell(subTable1);
                    }
                }

                t.AddCell(cell);
            }

            return t;
        }
    }
}
