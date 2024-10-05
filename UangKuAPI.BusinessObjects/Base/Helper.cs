using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using System.Globalization;
using System.Text;

namespace UangKuAPI.BusinessObjects.Base
{
    public class DateFormat
    {
        public static string DateTimeNow(string format, DateTime? dateTime)
        {
            DateTime _dateTime = dateTime ?? DateTime.Now;
            string _format = !string.IsNullOrEmpty(format) ? _dateTime.ToString(format) : _dateTime.ToString();
            return _format;
        }

        public static DateTime DateTimeNow(DateTime? dateTime)
        {
            var result = dateTime ?? DateTime.Now;
            return result;
        }

        public static DateTime DateTimeNow()
        {
            var result = DateTimeNow(null);
            return result;
        }

        public static DateTime DateTimeNowDate(int year, int month, int day)
        {
            var result = new DateTime(year, month, day);
            return result;
        }

        private static string shortyearpattern = "yyMMdd";
        private static string longyearpattern = "yyyy-MM-dd HH:mm:ss";
        private static string yearmonthdate = "yyyy-MM-dd";
        private static string date = "dd/MM/yyyy";
        private static string datetime = "dd/MM/yyyy HH:mm";
        private static string datetimesecond = "dd/MM/yyyy HH:mm:ss";
        private static string datelong = "dd MMM yyyy";
        private static string dateshortmonth = "dd-MMM-yyyy";
        private static string longdatepattern = "dd-MMM-yyyy HH:mm:ss";
        private static string datehourminute = "dd/MM/yyyy HH:mm";
        private static string dateshortmonthhourminute = "dd-MMM-yyyy HH:mm";
        private static string hourmin = "HH:mm";
        private static string month = "MMMM";
        private static string daydatemonthyear = "dddd, dd MMMM yyyy";
        public static string Shortyearpattern { get => shortyearpattern; set => shortyearpattern = value; }
        public static string Longyearpattern { get => longyearpattern; set => longyearpattern = value; }
        public static string Date { get => date; set => date = value; }
        public static string Datetime { get => datetime; set => datetime = value; }
        public static string Datetimesecond { get => datetimesecond; set => datetimesecond = value; }
        public static string Datelong { get => datelong; set => datelong = value; }
        public static string Dateshortmonth { get => dateshortmonth; set => dateshortmonth = value; }
        public static string Longdatepattern { get => longdatepattern; set => longdatepattern = value; }
        public static string Datehourminute { get => datehourminute; set => datehourminute = value; }
        public static string Dateshortmonthhourminute { get => dateshortmonthhourminute; set => dateshortmonthhourminute = value; }
        public static string Hourmin { get => hourmin; set => hourmin = value; }
        public static string Month { get => month; set => month = value; }
        public static string Yearmonthdate { get => yearmonthdate; set => yearmonthdate = value; }
        public static string Daydatemonthyear { get => daydatemonthyear; set => daydatemonthyear = value; }
    }

    public static class AppConstant
    {
        private static string foundMsg = "Data Found";
        private static string notFoundMsg = "Data Not Found";
        private static string requiredMsg = "{0} Is Required";
        private static string alreadyexistMsg = "{0} Already Exist";
        private static string createdsuccessMsg = "{0} {1} Created Successfully";
        private static string updatesuccessMsg = "{0} Updated Successfully";
        private static string failedMsg = "Failed To {0} Data For {1} {2}";

        public static string FoundMsg { get => foundMsg; set => foundMsg = value; }
        public static string NotFoundMsg { get => notFoundMsg; set => notFoundMsg = value; }
        public static string RequiredMsg { get => requiredMsg; set => requiredMsg = value; }
        public static string AlreadyExistMsg { get => alreadyexistMsg; set => alreadyexistMsg = value; }
        public static string CreatedSuccessMsg { get => createdsuccessMsg; set => createdsuccessMsg = value; }
        public static string UpdateSuccessMsg { get => updatesuccessMsg; set => updatesuccessMsg = value; }
        public static string FailedMsg { get => failedMsg; set => failedMsg = value; }
    }

    public static class Converter
    {
        public static int StringToInt(string dataString, int dataInt)
        {
            int result = !string.IsNullOrEmpty(dataString) ? int.Parse(dataString) : dataInt;

            return result;
        }

        public static string IntToString(int data)
        {
            var result = data.ToString();
            return result;
        }

        public static long IntToLong(int data)
        {
            long result = data * 1024 * 1024;
            return result;
        }

        public static int DecimalToInt(decimal value)
        {
            var values = (int)value;
            return values;
        }

        public static string DecimalToString(decimal value)
        {
            try
            {
                int result = (int)Math.Round(value);
                var values = result.ToString();
                return values;
            }
            catch (OverflowException)
            {
                return "Value too large";
            }
        }

        public static bool StringToBool(string value, bool defaultValue)
        {
            bool result;

            try
            {
                result = Convert.ToBoolean(value);
            }
            catch (Exception)
            {
                result = defaultValue;
            }

            return result;
        }

        public static DateTime StringToDateTime(string value, DateTime defaultValue)
        {
            DateTime result;

            try
            {
                result = DateTime.Parse(value);
            }
            catch
            {
                result = defaultValue;
            }

            return result;
        }

        //Class Untuk Convert Byte[] Ke Base64String
        public static string ByteToStringImg(byte[] imgPath)
        {
            try
            {
                string imgString = Convert.ToBase64String(imgPath);
                return imgString;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        //Class Untuk Convert Base64String Ke Byte[]
        public static byte[] StringToByteImg(string imgPath)
        {
            byte[] imgByte = Array.Empty<byte>();
            try
            {
                imgByte = Convert.FromBase64String(imgPath);
                return imgByte;
            }
            catch (Exception)
            {
                return imgByte;
            }
        }

        //Function Untuk Convert PDF Ke Byte[]
        public static byte[] PDFToByte(string filePath)
        {
            byte[] pdfBytes = File.ReadAllBytes(filePath);
            return pdfBytes;
        }

        //Class Untuk Decode Base64 Ke Byte[]
        public static byte[] DecodeBase64ToBytes(string base64String)
        {
            byte[] data = Array.Empty<byte>();
            try
            {
                data = Convert.FromBase64String(base64String);
                return data;
            }
            catch (Exception)
            {
                return data;
            }
        }

        //Class Untuk Decode Base64 Ke String
        public static string DecodeBase64ToString(string base64String)
        {
            try
            {
                byte[] data = Convert.FromBase64String(base64String);
                string decodedString = Encoding.UTF8.GetString(data);
                return decodedString;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        //Fungsi Format DateOnly Ke DateTime
        public static DateTime DateOnlyToDateTime(DateOnly? dateOnly, TimeOnly? timeOnly)
        {
            var date = dateOnly ?? DateOnly.MinValue;
            var time = timeOnly ?? TimeOnly.MinValue;
            var result = date.ToDateTime(time);
            return result;
        }

        //Function Untuk StringBuilder
        public static string BuilderString(params object[] items)
        {
            var builder = new StringBuilder();

            for (int i = 0; i < items.Length; i++)
            {
                builder.Append(items[i]);
            }
            var result = builder.ToString();
            return result;
        }

        //Function Untuk IList Jadi List
        public static List<T> ConvertIListToList<T>(IList<T> inputList)
        {
            var list = new List<T>(inputList);
            return list;
        }

        //Format Penomoran Tanggal
        public static string NumberingFormat(int number, string format)
        {
            var result = number.ToString(format);
            return result;
        }

        public static string Currency(decimal amount, string culture)
        {
            CultureInfo info = new CultureInfo(culture);
            return amount.ToString("C", info);
        }
    }

    public static class GeneratePDFFile
    {
        public static Paragraph SetParagraph(string title, int size, iText.Layout.Properties.TextAlignment alignment)
        {
            Paragraph paragraph = new Paragraph(title)
                .SetTextAlignment(alignment)
                .SetFontSize(size);
            return paragraph;
        }

        public static LineSeparator SetLine()
        {
            LineSeparator ls = new LineSeparator(new SolidLine());
            return ls;
        }

        public static Paragraph SetNewLine()
        {
            Paragraph paragraph = new Paragraph(new Text("\n"));
            return paragraph;
        }

        public static Image SetImage(string imgPath, iText.Layout.Properties.TextAlignment alignment)
        {
            var img = new Image(ImageDataFactory
               .Create(imgPath))
               .SetTextAlignment(alignment);
            return img;
        }

        public static Link SetLink(string title, string url)
        {
            var link = new Link(title,
                PdfAction.CreateURI(url));
            return link;
        }

        public static Table SetTable(int column, bool isLarge)
        {
            var tbl = new Table(column, isLarge)
                .SetKeepTogether(false);
            return tbl;
        }

        public static Cell SetCell(int rowsSpan, bool isAddBGColor, string title,
            iText.Layout.Properties.TextAlignment alignment)
        {
            var cell = new Cell(rowsSpan, rowsSpan)
                .SetTextAlignment(alignment)
                .Add(new Paragraph(title));

            if (isAddBGColor)
            {
                cell.SetBackgroundColor(iText.Kernel.Colors.ColorConstants.GRAY);
            }

            return cell;
        }

        public static iText.Kernel.Geom.PageSize SetPageSize(string size)
        {
            var pageSize = new Dictionary<string, iText.Kernel.Geom.PageSize>(StringComparer.OrdinalIgnoreCase)
            {
                { "A0", iText.Kernel.Geom.PageSize.A0 },
                { "A1", iText.Kernel.Geom.PageSize.A1 },
                { "A2", iText.Kernel.Geom.PageSize.A2 },
                { "A3", iText.Kernel.Geom.PageSize.A3 },
                { "A4", iText.Kernel.Geom.PageSize.A4 },
                { "A5", iText.Kernel.Geom.PageSize.A5 },
                { "A6", iText.Kernel.Geom.PageSize.A6 },
                { "A7", iText.Kernel.Geom.PageSize.A7 },
                { "A8", iText.Kernel.Geom.PageSize.A8 },
                { "A9", iText.Kernel.Geom.PageSize.A9 },
                { "A10", iText.Kernel.Geom.PageSize.A10 },
                { "B0", iText.Kernel.Geom.PageSize.B0 },
                { "B1", iText.Kernel.Geom.PageSize.B1 },
                { "B2", iText.Kernel.Geom.PageSize.B2 },
                { "B3", iText.Kernel.Geom.PageSize.B3 },
                { "B4", iText.Kernel.Geom.PageSize.B4 },
                { "B5", iText.Kernel.Geom.PageSize.B5 },
                { "B6", iText.Kernel.Geom.PageSize.B6 },
                { "B7", iText.Kernel.Geom.PageSize.B7 },
                { "B8", iText.Kernel.Geom.PageSize.B8 },
                { "B9", iText.Kernel.Geom.PageSize.B9 },
                { "B10", iText.Kernel.Geom.PageSize.B10 },
                { "executive", iText.Kernel.Geom.PageSize.EXECUTIVE },
                { "ledger", iText.Kernel.Geom.PageSize.LEDGER },
                { "legal", iText.Kernel.Geom.PageSize.LEGAL },
                { "letter", iText.Kernel.Geom.PageSize.LETTER },
                { "tabloid", iText.Kernel.Geom.PageSize.TABLOID }
            };
            var pages = pageSize.TryGetValue(size, out var page) ? page : iText.Kernel.Geom.PageSize.A4;
            return pages;
        }

        public static void SetPagesNumber(PdfDocument pdfDoc, Document doc)
        {
            int n = pdfDoc.GetNumberOfPages();
            for (int i = 1; i <= n; i++)
            {
                doc.ShowTextAligned(new Paragraph($"Page " + i + " of " + n),
                   559, 806, i, iText.Layout.Properties.TextAlignment.RIGHT,
                   iText.Layout.Properties.VerticalAlignment.TOP, 0);
            }
        }

        public static void SetFooterPages(Paragraph par, PdfDocument pdfdoc, Document doc)
        {
            for (int i = 1; i <= pdfdoc.GetNumberOfPages(); i++)
            {
                PdfPage pdfPage = pdfdoc.GetPage(i);
                pdfPage.SetIgnorePageRotationForContent(true);

                var pageSize = pdfPage.GetPageSize();
                float x;
                float y;
                if (pdfPage.GetRotation() % 180 == 0)
                {
                    x = pageSize.GetWidth() / 2;
                    y = pageSize.GetBottom() + 20;
                }
                else
                {
                    x = pageSize.GetHeight() / 2;
                    y = pageSize.GetRight() - 20;
                }

                doc.ShowTextAligned(par, x, y, i, iText.Layout.Properties.TextAlignment.CENTER, iText.Layout.Properties.VerticalAlignment.BOTTOM, 0);
            }
        }
    }
}
