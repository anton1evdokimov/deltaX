using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ПТК_КМ_Дельта
{

    public struct Av
    {
        public int porog_min;
        public int porog_max;
        public int kolvo_avariynih_datchikov;
        public int avariya1_predupregdenie0;
        public ushort value;
        public bool alarm;
    }
    public struct Data
    {
        public double absolutnoe;
        
        public double smeshenie;
        
    }
    public class global
    {     
        public bool isOPEN = false;
        public string save_file;
        public string save_file2;
        public string save_file3;
        public string open_file;
        public string config_file;
        public string event_file;
        public string vniim_file;
        public string mediana_file;
        public int predupregdenie = 0;
        public int skorost_posilok = 0;
        public int sekunda = 0;
        ////////////////
        public UInt16 version_proshivki;
        public bool obnovlenie_proshivki;
     
        public int lock_tcp=0;
        public int lock_otobragenie=0;
        public Int16 porog_max;
        public Int16 porog_min;
        public bool oshibka;
        public bool zakrito;

        public bool inversion_data;
        public bool otkl_panel;
        #region про сеть и прием
        public string IP="192.168.1.170";
        public string MASKA = "255.255.255.0";
        public int SPORT = 43690;
        public int DPORT = 80;
        public string IP1 = "";// нет
        public byte pred_posilka=0;
        #endregion
        #region график
        public double mashtab = 0.1;// 10 Hz
        public double[,] XY;
        public int XY_count;
        public bool otnositelnoe_otobragenie = false;
        #endregion
        public double timeout_alarm = 0.1;//
        public double period_oprosa_open = 0.1;// 10 Hz
        
        public Av[] avariya = new Av[4];
        public Data[] data = new Data[8];
        public int graph_memory = 100;
        public int graph_ind = 0;
        public int graph_memory_set = 0;
        public global()
        {
            for (int i = 0; i < 4; i++)
            {
                avariya[i] = new Av();
               
                avariya[i].alarm = false;
                avariya[i].kolvo_avariynih_datchikov = 0;
                avariya[i].porog_min = 0;
                avariya[i].porog_max = 0;
                avariya[i].avariya1_predupregdenie0 = 0;  
                avariya[i].alarm = false;
            }
            inversion_data = false;
            otkl_panel = false;
            for (int i = 0; i < 8; i++)
            {
                data[i] = new Data();
                data[i].smeshenie = 0;
                data[i].absolutnoe = 0;               
            }
        }
    }
}
