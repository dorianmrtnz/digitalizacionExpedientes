using System;
using System.Data;
using System.Text.RegularExpressions;
using Entity;
using System.Web.UI;
using ClosedXML.Excel;
using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;



namespace mobileBO
{
    internal class Utilerias
    {
        #region proposito en general

        internal void Utilerias_ExportExcel(Page pPagina, DataSet dataSet)
        {
            //UserManager manager = new UserManager();
            //DataSet dataSet = manager.GetProductDataToExport();

            string attachment = "attachment; filename=Report.xls";
            pPagina.Response.ClearContent();
            pPagina.Response.AddHeader("content-disposition", attachment);
            pPagina.Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            string tab1 = "";
            foreach (DataTable table in dataSet.Tables)
            {
                foreach (DataColumn column in table.Columns)
                {
                    pPagina.Response.Write(tab1 + column.ColumnName);
                    tab1 = "\t";
                }
            }
            tab = "\n";
            foreach (DataRow dr in dataSet.Tables[0].Rows)
            {
                for (int i = 0; i < dataSet.Tables[0].Columns.Count; i++)
                {
                    pPagina.Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                tab = "\n";
            }
            pPagina.Response.End();
        }

        internal void Utilerias_ExportExcel_2(Page pPagina, DataSet dataSet, string nombreArchivo)
        {
            var wb = new XLWorkbook();


            foreach (DataTable dt in dataSet.Tables)
            {
                wb.Worksheets.Add(dt); //Add a DataTable as a worksheet
            }

            HttpResponse httpResponse = pPagina.Response;

            //httpResponse.Response httpResponse = pPagina.Response.ou;
            httpResponse.Clear();
            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            httpResponse.AddHeader("content-disposition", "attachment;filename=\"" + nombreArchivo + ".xlsx\"");
            // Flush the workbook to the Response.OutputStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wb.SaveAs(memoryStream);
                memoryStream.WriteTo(httpResponse.OutputStream);
                memoryStream.Close();
            }
            httpResponse.End();
        }

        internal void Utilerias_Mensaje(Page pPagina, string sAviso)
        {
            String codigoJs = "<script type='text/javascript'>alert('" + sAviso + "');</script>";
            ScriptManager.RegisterStartupScript(pPagina, pPagina.GetType(), "tempJS", codigoJs, false);

        }


        internal bool Utilerias_DataSetValido(DataSet DS)
        {
            if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        internal decimal Utilerias_Truncate(decimal number, int digits)
        {
            decimal stepper = (decimal)(Math.Pow(10.0, (double)digits));
            int temp = (int)(stepper * number);
            return (decimal)temp / stepper;
            
        }


        internal string GenerarCadenaSeparadaPorPipes(DataSet DS, string Campo, int tabla)
        {
            string Cadena = string.Empty;
            if (DS != null && DS.Tables.Count > 0 && DS.Tables[tabla].Rows.Count > 0)
            {
                foreach (DataRow row in DS.Tables[tabla].Rows)
                {
                    if (Cadena == string.Empty)
                        Cadena += row[Campo].ToString();
                    else
                        Cadena += "|" + row[Campo].ToString();
                }
                return Cadena;
            }
            else
            {
                return "";
            }

        }

        internal bool Utilerias_EmailValido(string strIn)
        {
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strIn,
                    @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");

        }

        internal bool Utilerias_CodigoUniqueValido(string strIn)
        {
            // Return true si la cadena es un uniqueidentifier
            return Regex.IsMatch(strIn, @"^[0-9A-F]{8}\-[0-9A-F]{4}\-[0-9A-F]{4}\-[0-9A-F]{4}\-[0-9A-F]{12}$");

        }

        public string Utilerias_generarStringXml(DataSet xml)
        {
            if (Utilerias_DataSetValido(xml))
                return xml.GetXml();
            else
                return string.Empty;
        }
             

        public DataSet Utilerias_generarDataSet(String cadena)
        {
            DataSet DS = new DataSet();
            System.IO.StringReader sr = new System.IO.StringReader(cadena);
            DS.ReadXml(sr);
            return DS;

        }

        internal DateTime Utileria_convierteQuincenaFechaBO(int? _desde, int? _hasta)
        {

            string _convertir = "";
            
            if (_desde != null  && _desde != 999999)
            {
                int _año = Convert.ToInt32(_desde / 100);
                int _cmes = Convert.ToInt32(_desde - _año * 100);
                int _mes = Convert.ToInt32((_cmes / 2) + (_cmes % 2));
                
                int _dia = _cmes % 2 == 1 ? 1 : 16;
                _convertir = _año.ToString() + "/" + _mes.ToString().PadLeft(2, '0') + "/" + _dia.ToString().PadLeft(2, '0');
            }
            else
            {
                if (_hasta == 999999)
                {
                    _convertir = "5000/01/15";
                }
                else 
                {
                    int _año = Convert.ToInt32(_hasta / 100);
                    int _cmes = Convert.ToInt32(_hasta - _año * 100);
                    int _mes = Convert.ToInt32((_cmes / 2) + (_cmes % 2));
                    int _dia;
                    if (_mes == 2)//febrero
                    {
                        if (_año % 4 == 0 && _año % 100 != 0 || _año % 400 == 0)
                        {
                            _dia = 29;
                        }
                        else 
                        {
                            _dia = 28;
                        }
                    }
                    else
                    {
                        _dia = _cmes % 2 == 1 ? 15 : 30;
                    }
                    _convertir = _año.ToString() + "/" + _mes.ToString().PadLeft(2, '0') + "/" + _dia.ToString().PadLeft(2, '0');
                }
            }
            return Convert.ToDateTime(_convertir);
        }


        internal string Utileria_convierteFechaQuincenaBO(DateTime _Fecha)
        {
            if (_Fecha.Year >= 5000)
            {
                return "999999";
            }
            else
            {
                if (_Fecha.Day < 16)
                {
                    return _Fecha.Year.ToString() + ((_Fecha.Month * 2) - 1).ToString().PadLeft(2, '0');
                }
                else
                {
                    return _Fecha.Year.ToString() + (_Fecha.Month * 2).ToString().PadLeft(2, '0');
                }
            }
        }
        #endregion

        #region tramite Seguimiento

        internal DataSet tramiteSeguimientoGridDatos()
        {
            DataSet _ds = new DataSet();

            return _ds;
        }
        #endregion

        #region imagenes

        internal byte[] utileriasResizeImage(Stream streamImage)
        {
            Bitmap originalImage = new Bitmap(streamImage);
            MemoryStream _MemStream = new MemoryStream();
            int newWidth = originalImage.Width;//Convert.ToInt32(originalImage.Width * aspectRatio);
            int newHeight = originalImage.Height;// Convert.ToInt32(originalImage.Height * aspectRatio);
            Double aspectRatio;
            if (originalImage.Width > originalImage.Height)
            {
                aspectRatio = Double.Parse(originalImage.Width.ToString()) / Double.Parse(originalImage.Height.ToString());
                newWidth = 1080;
                newHeight = int.Parse(Math.Round(newWidth / aspectRatio).ToString());
            }
            else
            {
                aspectRatio = Double.Parse(originalImage.Height.ToString()) / Double.Parse(originalImage.Width.ToString());
                newHeight = 1080;
                newWidth = int.Parse(Math.Round(newHeight / aspectRatio).ToString());
            }
            byte[] _binImage;
            Bitmap newimage = new Bitmap(originalImage, newWidth, newHeight); // crea la nueva imagen con las nuevas dimensiones
            Graphics myGraphics = Graphics.FromImage(newimage);

            myGraphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
            myGraphics.DrawImage(originalImage, 0, 0, newimage.Width, newimage.Height);
            originalImage.Dispose();
            newimage.Save(_MemStream, System.Drawing.Imaging.ImageFormat.Jpeg);//'la nueva imagen es guardad en el stream
            _MemStream.Position = 0;//'mueve la posicion del cursor del stream al inicio
            _binImage = new byte[_MemStream.Length]; // 'dimenciona el arrego del bytes
            _MemStream.Read(_binImage, 0, (int)_MemStream.Length);//'convierte a bytes el contenido del stream
            return _binImage;
        }


        #endregion

    }
}