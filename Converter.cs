using System.Diagnostics;

namespace Html2Pdf
{
    public static class Converter
    {
        private const string PDF = "pdf";
        private const string HTML = "html";
        public static byte[] GeneratePdfBytes(string enginePath, string commandArgument, string html)
        {
            var fileTempate = $"{AppDomain.CurrentDomain.BaseDirectory}tmp.";
            var pdfFile = $"{fileTempate}{PDF}";
            var htmlFile = $"{fileTempate}{HTML}";

            File.WriteAllText(htmlFile, html);
            var cmd = string.Format(commandArgument, pdfFile, htmlFile);

            Process.Start(enginePath, cmd);

            while (!File.Exists(pdfFile))
            {
                Task.Delay(1000);
            }

            var resultButes = File.ReadAllBytes(pdfFile);
            File.Delete(pdfFile);
            File.Delete(htmlFile);

            return resultButes;
        }
    }
}
