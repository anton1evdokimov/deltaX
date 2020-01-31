using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;


namespace excel_work
{
     public class excel_work
    {
        private Excel.Application m_objExcel = null;
        private Excel.Workbooks m_objBooks = null;
        private Excel._Workbook m_objBook = null;
        private Excel.Sheets m_objSheets = null;
        private Excel._Worksheet m_objSheet = null;
        private Excel.Range m_objRange = null;
        private Excel.Font m_objFont = null;

        string alfavit = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        // Frequenty-used variable for optional arguments.
        private object m_objOpt = System.Reflection.Missing.Value;



        public void init_excel()
        {
            //Создание новой книги Excel
            m_objExcel = new Excel.Application();
            m_objBooks = (Excel.Workbooks)m_objExcel.Workbooks;
            m_objBook = (Excel._Workbook)(m_objBooks.Add(m_objOpt));
            m_objSheets = (Excel.Sheets)m_objBook.Worksheets;
            m_objSheet = (Excel._Worksheet)(m_objSheets.get_Item(1));
        }
        public void open_excel(string file)
        {
            m_objExcel = new Excel.Application();
            m_objExcel.Workbooks._Open(file,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing);
            m_objSheets = m_objExcel.Worksheets;
            m_objSheet = (Excel.Worksheet)m_objSheets.get_Item(1);
        }
        public void visible_excel(bool f) 
        {
            m_objExcel.Visible = f;
        }


       
        public void close_excel()
        {
            if (m_objBooks != null)
            {
                m_objBook.Close(false, m_objOpt, m_objOpt);
            }
            m_objExcel.Quit();
            m_objFont = null;
            m_objRange = null;
            m_objSheet = null;
            m_objSheets = null;
            m_objBooks = null;
            m_objBook = null;
            m_objExcel = null;
            GC.Collect();

        }
        public void wrtie_data(double[] M, int stroka, int stolbec, int kolvo_strok, int kolvo_stolbcov)
        {
            string str = cifra_to_bukva(stolbec) + stroka.ToString();
            m_objRange = m_objSheet.get_Range(str, m_objOpt);

            m_objRange = m_objRange.get_Resize(kolvo_strok, kolvo_stolbcov);
            m_objRange.set_Value(m_objOpt, M);


        }
        public void wrtie_string_data(string[,] M, int nachalo, int kolvo_strok, int kolvo_stolbcov)
        {
             string str = cifra_to_bukva(nachalo) + "5";
            m_objRange = m_objSheet.get_Range(str, m_objOpt);
            
            m_objRange = m_objRange.get_Resize(kolvo_strok, kolvo_stolbcov);
            m_objRange.set_Value(m_objOpt, M);


        }
        public void write_cell(int stroka,int stolbec,object data)
        {
            string str = cifra_to_bukva(stolbec) + stroka.ToString();
            m_objRange = m_objSheet.get_Range(str, m_objOpt);
         m_objRange.set_Value(m_objOpt, data);
        }
        public string read_cell(int stroka, int stolbec)
        {
            string str = cifra_to_bukva(stolbec) + stroka.ToString();
            m_objRange = m_objSheet.get_Range(str, Type.Missing);
            return m_objRange.Value2.ToString();
        }
         string cifra_to_bukva(int i)
        {
            int c = alfavit.Length;
            string r;
            if (i < c) { return alfavit[i].ToString(); }
            if (i < (2 * c)) { r = "A" + alfavit[i - c].ToString(); return r; }
            if (i < (3 * c)) { r = "B" + alfavit[i - 2 * c].ToString(); return r; }
            if (i < (4 * c)) { r = "C" + alfavit[i - 3 * c].ToString(); return r; }
            return "";
        }

         int chart_x = 10;
         int chart_y = 200;
         int chart_w = 500;
         int chart_h = 400;
         double  chart_max = 1;
         public void chart_config(int x, int y, int width, int height, double max)
         {
             chart_x = x;
             chart_y = y;
             chart_w = width;
             chart_h = height;
             chart_max = max;
         }

         public void write_chart( string chart_name, string podpis_x,string podpis_y, string diapason_nachalo,string diapason_end)
         {
             Excel.ChartObjects chartobj = (Excel.ChartObjects)m_objSheet.ChartObjects(Type.Missing);
             Excel.ChartObject chart1 = chartobj.Add(chart_x, chart_y, chart_w, chart_h);
             chart1.Chart.ChartWizard(m_objSheet.get_Range(diapason_nachalo, diapason_end), Excel.XlChartType.xlLine, 0, Excel.XlRowCol.xlColumns, 1, false, "", chart_name, podpis_x, podpis_y, "");
             chart1.Chart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlLine;
             


             /*    Excel.SeriesCollection seriesCollection =
       (Excel.SeriesCollection)m_objExcel.ActiveChart.SeriesCollection(Type.Missing);
                 Excel.Series series = seriesCollection.Item(1);
                 series.Name = "к-т отражения";
               
                  series = seriesCollection.Item(2);
                 series.Name = "к-т прохождения";
                
                  series = seriesCollection.Item(3);
                 series.Name = "ЭПР";*/

                

         
         }
        public void save_file(string file)
        {
            m_objBook.SaveAs(file, m_objOpt, m_objOpt,
                            m_objOpt, m_objOpt, m_objOpt, Excel.XlSaveAsAccessMode.xlNoChange,
                            m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt);//m_strSampleFolder + "1.xlsx"



        }
        public void save()
        {
            m_objBook.Save();
        }
    }
}


