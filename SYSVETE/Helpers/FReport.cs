using FastReport;
using FastReport.Export.OoXML;
using FastReport.Export.Pdf;
using FastReport.Web;
using System.Data;

namespace SYSVETE.Helpers
{
    public class FReport
    {
        private readonly string _conexion;

        public FReport(string conexion)
        {
            _conexion = conexion;
        }
        public MemoryStream GenerarReporte(string nombreReporte)
        {
            using (Report report = new Report())
            {

                //if (nombreReporte.Length == 0)
                //{
                //    throw new Exception("Nombre de reporte no válido");
                //}

                //if (parametros == null)
                //{
                //    throw new Exception("Debe ingresar parametros al reporte");
                //}

                var pathReporte = $"Reportes\\{nombreReporte}.frx";

                report.Load(pathReporte);
                report.Dictionary.Connections[0].ConnectionString = _conexion;
                var pdfExport = new PDFExport();
                var stream = new System.IO.MemoryStream();
                report.Prepare();
                pdfExport.Export(report, stream);

                using (MemoryStream ms = new MemoryStream())
                {

                    //if (tipo == TipoArchivo.EXCEL)
                    //{
                    //    Excel2007Export excel = new Excel2007Export();
                    //    excel.ShowProgress = false;
                    //    webReport.Report.Export(excel, ms);
                    //    ms.Position = 0;
                    //    return ms;
                    //}

                    report.Export(new PDFExport(), ms);
                    ms.Position = 0;
                    report.Dispose();
                    return ms;
                }
            }
        }
        public MemoryStream GenerarReporteconParmetros(string nombreReporte, ParametrosReporteDto[] parametros)
        {
            using (Report report = new Report())
            {

                if (nombreReporte.Length == 0)
                {
                    throw new Exception("Nombre de reporte no válido");
                }

                if (parametros == null)
                {
                    throw new Exception("Debe ingresar parametros al reporte");
                }

                var pathReporte = $"Reportes\\{nombreReporte}.frx";

                report.Load(pathReporte);
                report.Dictionary.Connections[0].ConnectionString = _conexion;

                foreach (var param in parametros)
                {
                    report.SetParameterValue(param.NombreParametro,param.ValorParametro);
                }

                var pdfExport = new PDFExport();
                var stream = new System.IO.MemoryStream();
                report.Prepare();
                pdfExport.Export(report, stream);

                using (MemoryStream ms = new MemoryStream())
                {

                    report.Export(new PDFExport(), ms);
                    ms.Position = 0;
                    report.Dispose();
                    return ms;
                }
            }
        }
    }
    public class ParametrosReporteDto
    {
        public string NombreParametro { get; set; }
        public object ValorParametro { get; set; }
    }


    public enum TipoArchivo
    {
        PDF = 1,
        EXCEL = 2
    }

    public class ParametrosRangos
    {
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
    }
}
