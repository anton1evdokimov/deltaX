using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
namespace ПТК_КМ_Дельта
{
    public partial class Form1 : Form
    {
        byte[] send_data = new byte[30];
        public Form1()
        {
            InitializeComponent();
            send_data[0] = 1;//Идентификатор транзакции
            send_data[1] = 1;//Идентификатор транзакции
            send_data[2] = 0;//Идентификатор протокола
            send_data[3] = 0;//Идентификатор протокола
            send_data[4] = 0;//Длина сообщения
            send_data[5] = 6;//Длина сообщения
            send_data[6] = 9;//Адрес у
            send_data[8] = 0;//Адрес первого регистра Hi байт
            send_data[10] = 0;//Количество регистров Hi байт
        }
        global GL = new global(); tcp.tcp_client TCP;
        Thread potok;
        private void очиститьАрхивПоАлгоритмуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PASSWORD.Text != "3")
            {
                MessageBox.Show("Введите пароль!"); return;
            }
            PASSWORD.Text = "";
            send_data[0] = 1;//Идентификатор транзакции
            send_data[1] = 1;//Идентификатор транзакции
            send_data[2] = 0;//Идентификатор протокола
            send_data[3] = 0;//Идентификатор протокола
            send_data[4] = 0;//Длина сообщения
            send_data[5] = 6;//Длина сообщения
            send_data[6] = 0;//Адрес у
            send_data[7] = 3;//Функциональный код
            send_data[8] = 0x1;//Адрес первого регистра Архива //1
            send_data[9] = 0x4D;//Адрес первого регистра Архива 153;
            send_data[10] = 0xff;//Количество регистров Lo байт
            send_data[11] = 0x33;//Количество регистров Lo байт
            var dr = MessageBox.Show("Архив будет безвовратно удален. Подтверждаете удаление?", "", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                TCP = null;
                if (TCP == null)//8 байт на одну запись - 4 регистра
                    TCP = new tcp.tcp_client();
                if (TCP.tcp_start(GL.IP, GL.DPORT) < 0) { TCP.tcp_stop(); MessageBox.Show("Нет связи!"); return; }
                TCP.tcp_send(send_data, 12, GL.IP, GL.DPORT); Thread.Sleep(1300);
                byte[] rez = TCP.recive(); TCP.tcp_stop(); TCP = null;
                if (rez == null) { MessageBox.Show("Нет ответа при выполнении операции!"); return; }
                if (rez[1] == Convert.ToByte('O') && rez[2] == Convert.ToByte('K') && rez[3] == Convert.ToByte('!'))
                {
                    MessageBox.Show("Архив событий успешно очищен!");
                }
            }
        }
        private void очиститьАрхивМедианToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PASSWORD.Text != "3")
            {
                MessageBox.Show("Введите пароль!"); return;
            }
            PASSWORD.Text = "";
            send_data[0] = 1;//Идентификатор транзакции
            send_data[1] = 1;//Идентификатор транзакции
            send_data[2] = 0;//Идентификатор протокола
            send_data[3] = 0;//Идентификатор протокола
            send_data[4] = 0;//Длина сообщения
            send_data[5] = 6;//Длина сообщения
            send_data[6] = 0;//Адрес у
            send_data[7] = 3;//Функциональный код

            send_data[8] = 0xC3;//Адрес первого регистра Архива
            send_data[9] = 0x50;//

            send_data[10] = 0xff;//Количество регистров Lo байт
            send_data[11] = 0x33;//Количество регистров Lo байт
            var dr = MessageBox.Show("Архив будет безвовратно удален. Подтверждаете удаление?", "", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                TCP = null;
                if (TCP == null)//8 байт на одну запись - 4 регистра
                    TCP = new tcp.tcp_client();
                if (TCP.tcp_start(GL.IP, GL.DPORT) < 0) { TCP.tcp_stop(); MessageBox.Show("Нет связи!"); return; }
                TCP.tcp_send(send_data, 12, GL.IP, GL.DPORT); Thread.Sleep(1300);
                byte[] rez = TCP.recive(); TCP.tcp_stop();
                TCP = null;
                if (rez == null) { MessageBox.Show("Нет ответа при выполнении операции!"); return; }
                if (rez[1] == Convert.ToByte('O') && rez[2] == Convert.ToByte('K') && rez[3] == Convert.ToByte('!'))
                {
                    MessageBox.Show("Архив медиан успешно очищен!");
                }
            }
        }
        private void очиститьАрхивСобытийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            send_data[0] = 1;//Идентификатор транзакции
            send_data[1] = 1;//Идентификатор транзакции
            send_data[2] = 0;//Идентификатор протокола
            send_data[3] = 0;//Идентификатор протокола
            send_data[4] = 0;//Длина сообщения
            send_data[5] = 6;//Длина сообщения
            send_data[6] = 0;//Адрес у
            send_data[7] = 3;//Функциональный код

            send_data[8] = 1;//Адрес первого регистра Архива //1
            send_data[9] = 150;//Адрес первого регистра Архива 153;//

            send_data[10] = 0xff;//Количество регистров Lo байт
            send_data[11] = 0x33;//Количество регистров Lo байт
            TCP = null;
            if (TCP == null)//8 байт на одну запись - 4 регистра
                TCP = new tcp.tcp_client();
            if (TCP.tcp_start(GL.IP, GL.DPORT) < 0) { TCP.tcp_stop(); MessageBox.Show("Нет связи при получении архива!"); return; }
            TCP.tcp_send(send_data, 12, GL.IP, GL.DPORT); Thread.Sleep(1300);
            byte[] rez = TCP.recive(); TCP.tcp_stop(); TCP = null;
            if (rez == null) { MessageBox.Show("Нет ответа при выполнении операции!"); return; }
            if (rez[1] == Convert.ToByte('O') && rez[2] == Convert.ToByte('K') && rez[3] == Convert.ToByte('!'))
            {
                MessageBox.Show("Архив событий успешно очищен!");
            }
        }
        private void ВычислитьуставкиMenuItem_Click(object sender, EventArgs e)
        {
            send_data[0] = 1;//Идентификатор транзакции
            send_data[1] = 1;//Идентификатор транзакции
            send_data[2] = 0;//Идентификатор протокола
            send_data[3] = 0;//Идентификатор протокола
            send_data[4] = 0;//Длина сообщения
            send_data[5] = 6;//Длина сообщения
            send_data[6] = 0;//Адрес у
            send_data[7] = 3;//Функциональный код

            send_data[8] = 0x33;//Адрес первого регистра Архива //1
            send_data[9] = 0x33;//Адрес первого регистра Архива 153;//

            send_data[10] = 0x88;//Количество регистров Lo байт
            send_data[11] = 0x99;//Количество регистров Lo байт
            TCP = null;
            if (TCP == null)//8 байт на одну запись - 4 регистра
                TCP = new tcp.tcp_client();
            if (TCP.tcp_start(GL.IP, GL.DPORT) < 0) { TCP.tcp_stop(); MessageBox.Show("Нет связи при получении архива!"); return; }
            TCP.tcp_send(send_data, 12, GL.IP, GL.DPORT); Thread.Sleep(1300);
            byte[] rez = TCP.recive(); TCP.tcp_stop(); TCP = null;
            if (rez == null) { MessageBox.Show("Нет ответа при выполнении операции!"); return; }
            if (rez[1] == Convert.ToByte('O') && rez[2] == Convert.ToByte('K') && rez[3] == Convert.ToByte('!'))
            {
                MessageBox.Show("Команда выполнена!");
            }
        }
        #region операции с главной формой
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (TCP != null) { TCP.tcp_stop(); TCP = null; }
            if (potok != null) { potok.Abort(); potok = null; }
            this.Dispose();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text += Application.ProductVersion.ToString();
            textBox_avariya.Text = "";
            GL.save_file = Environment.CurrentDirectory + $"\\temp {DateTime.Now.ToShortDateString()}.csv";
            GL.save_file2 = Environment.CurrentDirectory + "\\temp2.csv";
            GL.save_file3 = Environment.CurrentDirectory + "\\temp3.csv";
            GL.config_file = Environment.CurrentDirectory + "\\config.ini";
            GL.event_file = Environment.CurrentDirectory + "\\Архив событий от " + DateTime.Now.ToShortDateString() + ".csv";
            GL.vniim_file = Environment.CurrentDirectory + "\\Архив алгоритма самоконтроля от " + DateTime.Now.ToShortDateString() + ".txt";
            GL.mediana_file = Environment.CurrentDirectory + "\\Медианы " + DateTime.Now.ToShortDateString() + ".txt";

            load_setting();
            int delta = GL.porog_max - GL.porog_min;
            if (delta < 10) { GL.porog_min = -330; GL.porog_max = 10700; }
            for (int i = 0; i < 8; i++)
            {
                chart3.ChartAreas[i].AxisY.Title = "Удлинение, мкм";
                chart3.ChartAreas[i].AxisY.Maximum = 1;
                chart3.Series[i].BorderColor = Color.Black;
                chart3.Series[i].Color = Color.Black;
                chart1.Series[i].BorderWidth = 2;
                chart3.ChartAreas[i].AxisY.Maximum = GL.porog_max;
                chart3.ChartAreas[i].AxisY.Minimum = GL.porog_min;
                for (int j = 0; j < 600; j++) { chart3.Series[i].Points.AddXY(0, 0); }
            }
            chart1.ChartAreas[0].AxisY.Minimum = GL.porog_min;
            chart1.ChartAreas[0].AxisY.Maximum = GL.porog_max;
            chart2.ChartAreas[0].AxisY.Minimum = GL.porog_min;
            chart2.ChartAreas[0].AxisY.Maximum = GL.porog_max;
            for (int i = 0; i < 8; i++)
            {
                chart3.ChartAreas[i].AxisY.Maximum = GL.porog_max;
                chart3.ChartAreas[i].AxisY.Minimum = GL.porog_min;
            }
            int L;
            switch (GL.graph_memory)
            {
                case 1: L = 1; break;
                case 2: L = 2; break;
                case 10: L = 10; break;
                case 20: L = 20; break;
                case 60: L = 60; break;
                case 120: L = 120; break;
                default: L = 1; GL.graph_memory = 1; break;
            }
            combo_memory.Text = combo_memory.Items[L].ToString();
            chart_graph_init(GL.graph_memory, true);
            switch ((int)(GL.mashtab))
            {
                case 1: L = 0; break;
                case 10: L = 1; break;
                case 30: L = 2; break;
                case 60: L = 3; break;
                default: L = 0; GL.mashtab = 1; return;
            }
            combo_period.Text = combo_period.Items[L].ToString();
        }
        private void Form1_Resize(object sender, EventArgs e)
        { panel1.Width = this.Width - panel1.Left; }
        #endregion
        #region операции с файлами
        void load_setting()
        {
            DialogResult dr = MessageBox.Show("Загрузить настройки ПТК КМ-Дельта из отдельного файла?", "", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                if (openFileDialog1.ShowDialog() != DialogResult.OK) { GL.open_file = GL.config_file; }// return;
                else { GL.open_file = openFileDialog1.FileName; }
            }
            else { GL.open_file = GL.config_file; }

            if (File.Exists(GL.open_file) != true) { return; }
            using (StreamReader rd = new StreamReader(GL.open_file))//System.IO.StreamReader rd= new System.IO.StreamReader(GL.config_file);
            {
                try
                {
                    string str = rd.ReadLine(); int ind = str.IndexOf(':'); ind++; GL.IP = str.Substring(ind, str.Length - ind);
                    str = rd.ReadLine(); ind = str.IndexOf(':'); ind++; GL.MASKA = str.Substring(ind, str.Length - ind);
                    str = rd.ReadLine(); ind = str.IndexOf(':'); ind++; GL.IP1 = str.Substring(ind, str.Length - ind);
                    str = rd.ReadLine(); ind = str.IndexOf(':'); ind++; GL.DPORT = Convert.ToInt32(str.Substring(ind, str.Length - ind));
                    str = rd.ReadLine(); ind = str.IndexOf(':'); ind++; GL.SPORT = Convert.ToInt32(str.Substring(ind, str.Length - ind));
                    str = rd.ReadLine(); ind = str.IndexOf(':'); ind++; GL.timeout_alarm = Convert.ToDouble(str.Substring(ind, str.Length - ind));
                    str = rd.ReadLine(); ind = str.IndexOf(':'); ind++; GL.mashtab = Convert.ToDouble(str.Substring(ind, str.Length - ind));
                    str = rd.ReadLine(); ind = str.IndexOf(':'); ind++; GL.otnositelnoe_otobragenie = Convert.ToBoolean(str.Substring(ind, str.Length - ind));
                    str = rd.ReadLine(); ind = str.IndexOf(':'); ind++; GL.data[0].smeshenie = Convert.ToDouble(str.Substring(ind, str.Length - ind));
                    str = rd.ReadLine(); ind = str.IndexOf(':'); ind++; GL.data[1].smeshenie = Convert.ToDouble(str.Substring(ind, str.Length - ind));
                    str = rd.ReadLine(); ind = str.IndexOf(':'); ind++; GL.data[2].smeshenie = Convert.ToDouble(str.Substring(ind, str.Length - ind));
                    str = rd.ReadLine(); ind = str.IndexOf(':'); ind++; GL.data[3].smeshenie = Convert.ToDouble(str.Substring(ind, str.Length - ind));
                    str = rd.ReadLine(); ind = str.IndexOf(':'); ind++; GL.data[4].smeshenie = Convert.ToDouble(str.Substring(ind, str.Length - ind));
                    str = rd.ReadLine(); ind = str.IndexOf(':'); ind++; GL.data[5].smeshenie = Convert.ToDouble(str.Substring(ind, str.Length - ind));
                    str = rd.ReadLine(); ind = str.IndexOf(':'); ind++; GL.data[6].smeshenie = Convert.ToDouble(str.Substring(ind, str.Length - ind));
                    str = rd.ReadLine(); ind = str.IndexOf(':'); ind++; GL.data[7].smeshenie = Convert.ToDouble(str.Substring(ind, str.Length - ind));
                    for (int i = 0; i < 4; i++)
                    {
                        str = rd.ReadLine(); ind = str.IndexOf(':'); ind++; GL.avariya[i].porog_max = Convert.ToInt32(str.Substring(ind, str.Length - ind));
                        str = rd.ReadLine(); ind = str.IndexOf(':'); ind++; GL.avariya[i].porog_min = Convert.ToInt32(str.Substring(ind, str.Length - ind));
                        str = rd.ReadLine(); ind = str.IndexOf(':'); ind++; GL.avariya[i].kolvo_avariynih_datchikov = Convert.ToInt32(str.Substring(ind, str.Length - ind));
                        str = rd.ReadLine(); ind = str.IndexOf(':'); ind++; GL.avariya[i].avariya1_predupregdenie0 = Convert.ToInt32(str.Substring(ind, str.Length - ind));
                    }
                    str = rd.ReadLine(); ind = str.IndexOf(':'); ind++; GL.porog_max = Convert.ToInt16(str.Substring(ind, str.Length - ind));
                    str = rd.ReadLine(); ind = str.IndexOf(':'); ind++; GL.porog_min = Convert.ToInt16(str.Substring(ind, str.Length - ind));
                    str = rd.ReadLine(); ind = str.IndexOf(':'); ind++; GL.inversion_data = Convert.ToBoolean(str.Substring(ind, str.Length - ind));
                    str = rd.ReadLine(); ind = str.IndexOf(':'); ind++; GL.otkl_panel = Convert.ToBoolean(str.Substring(ind, str.Length - ind));
                    str = rd.ReadLine(); ind = str.IndexOf(':'); ind++; GL.graph_memory = Convert.ToInt32(str.Substring(ind, str.Length - ind));
                    str = rd.ReadLine(); ind = str.IndexOf(':'); ind++; GL.version_proshivki = Convert.ToUInt16(str.Substring(ind, str.Length - ind));
                    rd.Close();
                }
                catch
                {
                    rd.Close();
                    MessageBox.Show("Ошибка чтения файла! Перезапустите программу.");
                    System.IO.File.Delete(GL.config_file);
                    this.Dispose();
                }
            }
        }
        public void save_setting(global G3 = null)
        {
            if (G3 != null) GL = G3;
            string sf = GL.config_file; saveFileDialog_ini.FileName = "";
            if (MessageBox.Show("Сохранить настройки ПТК КМ-Дельта в отдельный файл?", "", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                if (saveFileDialog_ini.ShowDialog() == DialogResult.OK)
                    if (saveFileDialog_ini.FileName != GL.save_file)
                    {
                        sf = saveFileDialog_ini.FileName;                   //System.IO.File.Copy(GL.save_file, saveFileDialog1.FileName, true);
                    }
            }
            else { MessageBox.Show("Параметры будут сохранены в файл по умолчанию"); }
            using (StreamWriter wr = new StreamWriter(sf, false))
            {
                wr.WriteLine("IP-адрес ПТК КМ-Дельта:" + GL.IP);
                wr.WriteLine("Маска подсети:" + GL.MASKA);
                wr.WriteLine("Удалённый IP1:" + GL.IP1);
                wr.WriteLine("Порт ПТК КМ-Дельта:" + GL.DPORT.ToString());
                wr.WriteLine("Порт программы:" + GL.SPORT.ToString());
                wr.WriteLine("Задержка срабатывания сигнализации:" + GL.timeout_alarm.ToString());
                wr.WriteLine("Масштаб отображения, с:" + GL.mashtab.ToString());
                wr.WriteLine("Отображение относительных данных:" + GL.otnositelnoe_otobragenie.ToString());
                wr.WriteLine("Смещение канала 1, мкм:" + GL.data[0].smeshenie.ToString());
                wr.WriteLine("Смещение канала 2, мкм:" + GL.data[1].smeshenie.ToString());
                wr.WriteLine("Смещение канала 3, мкм:" + GL.data[2].smeshenie.ToString());
                wr.WriteLine("Смещение канала 4, мкм:" + GL.data[3].smeshenie.ToString());
                wr.WriteLine("Смещение канала 5, мкм:" + GL.data[4].smeshenie.ToString());
                wr.WriteLine("Смещение канала 6, мкм:" + GL.data[5].smeshenie.ToString());
                wr.WriteLine("Смещение канала 7, мкм:" + GL.data[6].smeshenie.ToString());
                wr.WriteLine("Смещение канала 8, мкм:" + GL.data[7].smeshenie.ToString());
                for (int i = 0; i < 4; i++)
                {
                    string str = "Реле " + (i + 1).ToString() + " верхний порог:"; wr.WriteLine(str + GL.avariya[i].porog_max.ToString());
                    str = "Реле " + (i + 1).ToString() + " нижний порог:"; wr.WriteLine(str + GL.avariya[i].porog_min.ToString());
                    str = "Реле " + (i + 1).ToString() + " аварийные датчики:"; wr.WriteLine(str + GL.avariya[i].kolvo_avariynih_datchikov.ToString());
                    str = "Реле " + (i + 1).ToString() + " признак сигнализации:"; wr.WriteLine(str + GL.avariya[i].avariya1_predupregdenie0.ToString());
                }
                wr.WriteLine("Максимум шкалы:" + GL.porog_max.ToString());
                wr.WriteLine("Минимум шкалы:" + GL.porog_min.ToString());
                wr.WriteLine("Инверсия данных:" + GL.inversion_data.ToString());
                wr.WriteLine("Отключение реле:" + GL.otkl_panel.ToString());
                wr.WriteLine("Память, точек:" + GL.graph_memory.ToString());
                wr.WriteLine("Версия прошивки:" + GL.version_proshivki.ToString());
                wr.Close();
            }
        }
        private void остановкаТестаОтВНИИМToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool con = true;
            timer_VNIIM.Enabled = !con;
            Thread.Sleep(300);
            label_OK_VNIIM.Visible = !con;
            label_vniim.Visible = !con;
            label_catch.Visible = !con;
            GL.skorost_posilok = 0; GL.sekunda = 0; GL.pred_posilka = 0;

            запускТестаОтВНИИМToolStripMenuItem.Enabled = con;
            остановкаТестаОтВНИИМToolStripMenuItem.Enabled = !con;

            menu_open.Enabled = con;
            menu_setting.Enabled = con;
            menu_connect.Enabled = con;
            получитьАрхивСобытийToolStripMenuItem.Enabled = con;
            получитьАрхивАлгоритмаСамоконтроляToolStripMenuItem.Enabled = con;
            получитьАрхивТекущихЗначенийToolStripMenuItem.Enabled = con;
            menu_disconnect.Enabled = !con;
            разъединитьБезФайлаToolStripMenuItem.Enabled = !con;
            остановкаТестаОтВНИИМToolStripMenuItem.Enabled = !con;

            timer1.Enabled = !con; timer3.Enabled = true;
            label_speed.Text = "0";
        }
        private void запускТестаОтВНИИМToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool con = false;
            timer3.Enabled = true;
            //label_OK_VNIIM.Visible = !con;
            //label_vniim.Visible = !con;
            //label_catch.Visible = !con;

            запускТестаОтВНИИМToolStripMenuItem.Enabled = con;
            остановкаТестаОтВНИИМToolStripMenuItem.Enabled = !con;

            menu_open.Enabled = con;
            menu_setting.Enabled = con;
            menu_connect.Enabled = con;
            получитьАрхивСобытийToolStripMenuItem.Enabled = con;
            получитьАрхивАлгоритмаСамоконтроляToolStripMenuItem.Enabled = con;
            получитьАрхивТекущихЗначенийToolStripMenuItem.Enabled = con;
            menu_disconnect.Enabled = con;
            разъединитьБезФайлаToolStripMenuItem.Enabled = con;

            timer_VNIIM.Enabled = !con;
            timer1.Enabled = !con;
        }
        int count_vniim = 0;
        long NumberofCatсh = 0, zapros_ok = 0;
        private void timer_VNIIM_Tick(object sender, EventArgs e)
        {
            IX:
            if (TCP == null)
            {
                TCP = new tcp.tcp_client();
                if (TCP.tcp_start(GL.IP, GL.DPORT, true) < 0) { TCP.tcp_stop(); TCP = null; return; }
            }
            GL.isOPEN = false; GL.oshibka = false; GL.zakrito = false;

            int s = 0; recive_end = false;
            send_data[0] = 1;//Идентификатор транзакции
            send_data[1] = 1;//Идентификатор транзакции
            send_data[2] = 0;//Идентификатор протокола
            send_data[3] = 0;//Идентификатор протокола
            send_data[4] = 0;//Длина сообщения
            send_data[5] = 6;//Длина сообщения
            send_data[6] = 33;
            send_data[7] = 3;//Функциональный код
            send_data[8] = 0;
            send_data[9] = 0;//Адрес первого регистра Lo байт
            send_data[10] = 0;//Количество регистров: 11 без аварий
            send_data[11] = 13;//Количество регистров: 13 с авариями           
            s = TCP.tcp_send(send_data, 12, GL.IP, GL.DPORT); Thread.Sleep(50);
            if (s == 33)
            {
                NumberofCatсh++;
                //label_catch.Text = $"Number of Catсh: {NumberofCatсh}. {DateTime.Now.ToLongTimeString()}";
                TCP.tcp_stop(); TCP = null; count_vniim = 0; goto IX;
            }
            else
            {
                zapros_ok++;
                //label_OK_VNIIM.Text = zapros_ok.ToString();
            }
            byte[] bdata = TCP.recive();
            count_vniim++; GL.skorost_posilok++;
            if (count_vniim > 3000)
            { TCP.tcp_stop(); TCP = null; count_vniim = 0; }
            priem(bdata);
        }
        private void timer3_Tick(object sender, EventArgs e)
        {
            save_data();
        }
        void save_data()
        {
            try
            {
                DateTime dd = DateTime.Now;
                DateTime data = DateTime.SpecifyKind(dd, DateTimeKind.Local);
                for (int i = 0; i < 8; i++)
                {
                    if (GL.data[i].absolutnoe < -10000) return;
                }
                string str = data.ToShortDateString() + ";" + data.ToLongTimeString() + ";";
                using (StreamWriter wr = new StreamWriter(GL.save_file, true, Encoding.GetEncoding(1251)))
                {
                    for (int i = 0; i < 8; i++) { str += GL.data[i].absolutnoe.ToString() + ";"; }
                    wr.WriteLine(str);
                    wr.Close();
                }
            }
            catch { }
        }
        void _open_data()
        {
            try
            {
                StreamReader rd = new StreamReader(GL.open_file, Encoding.GetEncoding(1251));
                string str = rd.ReadLine();// читаем период
                int tzp1 = str.IndexOf(';'); tzp1++;
                int tzp2 = str.IndexOf(';', tzp1);
                int zapisi_count = 0;
                GL.period_oprosa_open = Convert.ToDouble(str.Substring(tzp1, tzp2 - tzp1));
                GL.XY_count = (int)(GL.mashtab / GL.period_oprosa_open);
                GL.XY_count = GL.XY_count * 10;
                GL.XY = null;
                GL.XY = new double[GL.XY_count, 9];
                chart1.ChartAreas[0].AxisX.Interval = GL.mashtab / 2.0;
                rd.ReadLine();// читаем шапку       // далее читаем собственно данные
                while (true)
                {
                    if (rd.Peek() == -1) { rd.Close(); break; }
                    string dd = rd.ReadLine();
                    zapisi_count++;
                    if (zapisi_count > GL.XY_count) { continue; }
                    double time = zapisi_count - 1; time = time * GL.period_oprosa_open;
                    time = Math.Round(time, 1);
                    string_to_data(dd, zapisi_count - 1, time);
                }
                rd.Close();
                for (int i = 0; i < 8; i++)
                {
                    GL.data[i].absolutnoe = GL.XY[0, i];
                }
                set_data_to_graph(); set_data_to_stolbiki();
                chart_scroll.Minimum = 0;
                chart_scroll.Maximum = zapisi_count - GL.XY_count - 1;
                chart_scroll.Value = chart_scroll.Minimum;
            }
            catch { }
        }
        void string_to_data(string data, int position, double time)
        {
            while (true)
            {
                if (0 == Interlocked.Exchange(ref GL.lock_otobragenie, 1))
                {
                    int sm; sm = 3;
                    int tzp1 = 0;
                    for (int i = 0; i < sm - 1; i++)
                    {
                        tzp1 = data.IndexOf(';', tzp1); tzp1++;
                    }
                    for (int j = 0; j < 8; j++)
                    {
                        tzp1 = data.IndexOf(';', tzp1); tzp1++;
                        int tzp2 = data.IndexOf(';', tzp1);
                        if (tzp2 == -1) { tzp2 = data.Length; }
                        string ss = data.Substring(tzp1, tzp2 - tzp1);
                        GL.XY[position, j] = Convert.ToDouble(ss);
                    }
                    GL.XY[position, 8] = time;
                    System.Threading.Interlocked.Exchange(ref GL.lock_otobragenie, 0);
                    break;
                }
            }
        }
        #endregion
        #region пунткы меню  
        #region о программе
        about.about AB;
        private void menu_about_Click(object sender, EventArgs e)
        {
            AB = new about.about(); this.Enabled = false;
            AB.Disposed += new EventHandler(AB_Disposed);
            AB.Left = this.Left + 100; AB.Top = this.Top + 100;
            AB.Show(); AB.Focus();
        }
        void AB_Disposed(object sender, EventArgs e)
        { this.Enabled = true; this.Focus(); }
        #endregion
        #region меню файл
        private void menu_exit_Click(object sender, EventArgs e)
        {
            if (TCP != null) { TCP.tcp_stop(); TCP = null; }
            if (potok != null) { potok.Abort(); potok = null; }
            Close();
        }

        private void menu_open_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK) { return; }
            GL.open_file = openFileDialog1.FileName;
            GL.isOPEN = true; _open_data();
        }

        bool recive_ok, recive_end;
        private void menu_connect_Click(object sender, EventArgs e)
        {
            try
            {
                //File.Delete(GL.save_file);
            }
            catch (Exception er)
            {
                MessageBox.Show($"Ошибка. Перезапустите программу или удалите файл temp.csv. {er.Message}"); return;
            }
            if (TCP == null) TCP = new tcp.tcp_client();
            if (menu_connect.Text == "Соединить")
            { if (TCP.tcp_start(GL.IP, GL.DPORT) < 0) { TCP.tcp_stop(); return; } }
            GL.isOPEN = false; GL.oshibka = false; GL.zakrito = false;
            GL.graph_ind = 0; con_disc(true);
            GL.obnovlenie_proshivki = false;
            StreamWriter wr = new StreamWriter(GL.save_file, true, Encoding.GetEncoding(1251));
            string tip_dannih = "значения абсолютые;";
            if (GL.otnositelnoe_otobragenie == true) { tip_dannih = "значения относительные;"; }
            //wr.WriteLine(tip_dannih);
            //wr.WriteLine("Дата;Время;МКЛП1,мкм;МКЛП2,мкм;МКЛП3,мкм;МКЛП4,мкм;МКЛП5,мкм;МКЛП6,мкм;МКЛП7,мкм;МКЛП8,мкм;");
            //wr.Close();
            recive_ok = true;
            potok = new Thread(recive); potok.Start();
        }
        private void menu_disconnect_Click(object sender, EventArgs e)
        {
            con_disc(false); recive_ok = false; while (!recive_end) ;
            TCP.tcp_stop();
            if (potok != null) { potok.Abort(); potok = null; }
            GL.open_file = GL.save_file; GL.zakrito = true;
            if (GL.obnovlenie_proshivki) { return; }
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) { return; }//open_data();
            if (saveFileDialog1.FileName != GL.save_file)
            {
                File.Copy(GL.save_file, saveFileDialog1.FileName, true);
            }// open_data();
        }
        private void разъединитьБезФайлаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now; double delta = 0;
            GL.zakrito = true; GL.open_file = GL.save_file; con_disc(false);
            recive_ok = false;
            while (!recive_end && (delta < 5)) { delta = (DateTime.Now - now).TotalSeconds; }
            try
            {
                TCP.tcp_stop();
                if (potok != null) { potok.Abort(); potok = null; }
            }
            catch { }
        }

        void con_disc(bool connect)
        {
            GL.skorost_posilok = 0; GL.sekunda = 0; GL.pred_posilka = 0;
            menu_open.Enabled = !connect;
            menu_setting.Enabled = !connect;
            menu_connect.Enabled = !connect;
            получитьАрхивСобытийToolStripMenuItem.Enabled = !connect;
            получитьАрхивАлгоритмаСамоконтроляToolStripMenuItem.Enabled = !connect;
            получитьАрхивТекущихЗначенийToolStripMenuItem.Enabled = !connect;

            menu_disconnect.Enabled = connect;
            разъединитьБезФайлаToolStripMenuItem.Enabled = connect;

            timer1.Enabled = connect; chart_scroll.Minimum = 0;
            chart_scroll.Maximum = 0; label_speed.Text = "0";
            if (connect == true)
            {
                chart1.ChartAreas[0].AxisY.Minimum = GL.porog_min;
                chart1.ChartAreas[0].AxisY.Maximum = GL.porog_max;
                chart2.ChartAreas[0].AxisY.Minimum = GL.porog_min;
                chart2.ChartAreas[0].AxisY.Maximum = GL.porog_max;
                for (int i = 0; i < 8; i++)
                {
                    chart3.ChartAreas[i].AxisY.Maximum = GL.porog_max;
                    chart3.ChartAreas[i].AxisY.Minimum = GL.porog_min;
                }
            }
        }
        #endregion
        #region параметры устройств
        parametry.parametry Settings;
        private void menu_ip_Click(object sender, EventArgs e)
        {
            Settings = new parametry.parametry();
            Settings.Top = this.Top + 100; Settings.Left = this.Left + 100;
            Settings.Disposed += new EventHandler(Settings_Disposed);
            Settings.GL = GL; this.Enabled = false;
            Settings.Show(); Settings.Focus();
        }
        void Settings_Disposed(object sender, EventArgs e)
        {
            GL = Settings.GL;//save_setting();
            chart1.ChartAreas[0].AxisY.Minimum = GL.porog_min;
            chart1.ChartAreas[0].AxisY.Maximum = GL.porog_max;
            chart2.ChartAreas[0].AxisY.Minimum = GL.porog_min;
            chart2.ChartAreas[0].AxisY.Maximum = GL.porog_max;
            for (int i = 0; i < 8; i++)
            {
                chart3.ChartAreas[i].AxisY.Maximum = GL.porog_max;
                chart3.ChartAreas[i].AxisY.Minimum = GL.porog_min;
            }
            this.Enabled = true; this.Focus();
        }
        #endregion
        #endregion
        private void установитьВремяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime GBV = DateTime.Now; int dx1, dx2, wdu = 1; //xf = Convert.ToInt16(xv);     //MessageBox.Show("0x"+GBV.Minute.ToString() + "  " + GBV.Minute.ToString());
            send_data[5] = 0xD;//Длина сообщения
            send_data[7] = 0x10;//Функциональный код
            send_data[9] = 0x8;//Адрес первого регистра Lo байт
            send_data[11] = 0x3;//Количество регистров Lo байт
            send_data[12] = (byte)(2 * send_data[11]); dx1 = GBV.Second / 10; dx2 = GBV.Second - dx1 * 10;
            send_data[13] = (byte)((dx1 << 4) + dx2); dx1 = GBV.Minute / 10; dx2 = GBV.Minute - dx1 * 10;
            send_data[14] = (byte)((dx1 << 4) + dx2); dx1 = GBV.Hour / 10; dx2 = GBV.Hour - dx1 * 10;
            send_data[15] = (byte)((dx1 << 4) + dx2); dx1 = GBV.Day / 10; dx2 = GBV.Day - dx1 * 10;
            send_data[16] = (byte)((dx1 << 4) + dx2);
            switch (GBV.DayOfWeek)
            {
                case DayOfWeek.Sunday: wdu = 7 << 5; break;
                case DayOfWeek.Monday: wdu = 1 << 5; break;
                case DayOfWeek.Tuesday: wdu = 2 << 5; break;
                case DayOfWeek.Wednesday: wdu = 3 << 5; break;
                case DayOfWeek.Thursday: wdu = 4 << 5; break;
                case DayOfWeek.Friday: wdu = 5 << 5; break;
                case DayOfWeek.Saturday: wdu = 6 << 5; break;
            }
            dx1 = GBV.Month / 10; dx2 = GBV.Month - dx1 * 10;
            send_data[17] = (byte)((dx1 << 4) + dx2 + wdu);
            dx1 = (GBV.Year - 2000) / 10; dx2 = (GBV.Year - 2000) - dx1 * 10;
            send_data[18] = (byte)((dx1 << 4) + dx2);
            if (TCP == null) TCP = new tcp.tcp_client();
            if (TCP.tcp_start(GL.IP, GL.DPORT) < 0) { TCP.tcp_stop(); MessageBox.Show("Нет связи при установке даты и времени!"); return; }
            byte[] bdata = null;
            try
            {
                TCP.tcp_send(send_data, 19, GL.IP, GL.DPORT); bdata = TCP.recive();
            }
            finally { TCP.tcp_stop(); }
            send_data[5] = 6;
            if (bdata == null) { MessageBox.Show("Нет ответа!"); return; }
            for (short k = 0; k < 12; k++) if (bdata[k] != send_data[k]) { MessageBox.Show("Ответ неверен!"); return; }
            { MessageBox.Show("Дата и время успешно установлены!"); }
        }
        /*
        0001	Идентификатор транзакции	Transaction Identifier
        0000	Идентификатор протокола	Protocol Identifier
        0006	Длина (6 байтов идут следом)	Message Length
        33	Адрес ус-ва (33 = 21 hex)	Unit Identifier
        03	Функциональный код (читаем Analog Output Holding Registers)	Function Code
        0000	Адрес первого регистра   
        0011	Количество требуемых регистров (11 данные от МКЛП + Дата и время)
         */
        #region прием данных  
        DateTime prevT, nextT; int ifLock = 0;
        void recive()
        {
            int s = 0; recive_end = false;
            send_data[0] = 1;//Идентификатор транзакции
            send_data[1] = 1;//Идентификатор транзакции
            send_data[2] = 0;//Идентификатор протокола
            send_data[3] = 0;//Идентификатор протокола
            send_data[4] = 0;//Длина сообщения
            send_data[5] = 6;//Длина сообщения
            send_data[6] = 33;
            send_data[7] = 3;//Функциональный код
            send_data[8] = 0;
            send_data[9] = 0;//Адрес первого регистра Lo байт
            send_data[10] = 0;//Количество регистров: 11 без аварий
            send_data[11] = 13;//Количество регистров: 13 с авариями
            try
            {
                while (recive_ok)
                {
                    //if (ifLock == 0)
                    {
                        s = TCP.tcp_send(send_data, 12, GL.IP, GL.DPORT); Thread.Sleep(50);
                        if (s == 1)
                        {
                            Thread.Sleep(3003);
                            s = TCP.tcp_send(send_data, 12, GL.IP, GL.DPORT); Thread.Sleep(503);
                            if (s == 1)
                            {
                                TCP = null; TCP = new tcp.tcp_client();
                                if (TCP.tcp_start(GL.IP, GL.DPORT) > 0)
                                {
                                    Thread.Sleep(1003);
                                    s = TCP.tcp_send(send_data, 12, GL.IP, GL.DPORT); Thread.Sleep(503);
                                }
                                if (s == 1)
                                {
                                    MessageBox.Show("Для возобновления связи нажмите повторно 'Соединить'");
                                    recive_end = true; recive_ok = false;
                                    //if (GL.zakrito == false) { GL.oshibka = true; }
                                    разъединитьБезФайлаToolStripMenuItem_Click(null, null);
                                }
                            }
                        }
                        byte[] bdata; bdata = TCP.recive();
                        Thread.Sleep(453);
                        if (bdata == null) { continue; }
                        GL.skorost_posilok++;
                        for (short k = 0; k < 5; k++) if (bdata[k] != send_data[k]) { continue; }
                        if (bdata[7] != send_data[7]) { continue; }
                        GL.pred_posilka = bdata[21];
                        int lversion_proshivki = bdata[17]; lversion_proshivki = lversion_proshivki << 8;
                        lversion_proshivki = lversion_proshivki | bdata[18]; lversion_proshivki = lversion_proshivki << 8;
                        lversion_proshivki = lversion_proshivki | bdata[19]; lversion_proshivki = lversion_proshivki << 8;
                        lversion_proshivki = lversion_proshivki | bdata[20];
                        priem(bdata);
                    }
                }
                recive_end = true;
            }
            catch (Exception er)
            {
                if (ifLock != 33)
                {
                    recive_end = true; recive_ok = false;
                    if (GL.zakrito == false) { GL.oshibka = true; }
                }
            }
        }
        int[] min_ch = new int[8]; int[] max_ch = new int[8]; string T_Date = "";
        void priem(byte[] dt)
        {
            try
            {
                int nr = 9;//9 - начало данных
                for (int i = 0; i < 8; i++)
                {
                    if (i == 2)
                    { }
                    uint d = (uint)(dt[nr]);
                    d = d << 8; nr++;
                    d = d | dt[nr]; nr++;
                    Int16 id = (Int16)d;
                    if (d == 0x8000) { GL.data[i].absolutnoe = -20000; }
                    else
                    {
                        double dd = id / 2.0;
                        /*if (dd < chart2.ChartAreas[0].AxisY.Minimum) min_ch[i] += 1; else min_ch[i] = 0;
                        if (min_ch[i] > 7){ chart2.ChartAreas[0].AxisY.Minimum = dd - 50; min_ch[i] = 0; }
                        if (dd > chart2.ChartAreas[0].AxisY.Maximum) max_ch[i] += 1; 
                        if (max_ch[i] > 7) { chart2.ChartAreas[0].AxisY.Maximum = dd + 500; max_ch[i] = 0; }  */
                        GL.data[i].absolutnoe = dd;
                    }
                }
                if (dt[32] != 0) { GL.avariya[1].alarm = true; GL.avariya[1].value = dt[32]; }
                else { GL.avariya[1].alarm = false; GL.avariya[1].value = 0; }
                if (dt[33] != 0) { GL.avariya[2].alarm = true; GL.avariya[2].value = dt[33]; }
                else { GL.avariya[2].alarm = false; GL.avariya[2].value = 0; }//Неисправность МКЛП
                if (dt[34] != 0) { GL.avariya[3].alarm = true; GL.avariya[3].value = dt[34]; }
                else { GL.avariya[3].alarm = false; GL.avariya[3].value = 0; }//Нет_связи_с МКЛП

                if (dt[28] != 0 && dt[29] != 0)///Дата и время
                {
                    string[] x = new string[5];
                    for (short h = 0; h < 5; h++) { x[h] = dt[25 + h].ToString("x"); if (x[h].Length == 1) x[h] = "0" + x[h]; }
                    T_Date = x[2] + ":" + x[1] + ":" + x[0] + " " + x[3] + "." + x[4] + ".20" + dt[30].ToString("x");
                }
                if (timer3.Enabled == false)
                {
                    timer3.Enabled = true;
                }
            }
            catch { }
        }

        public void poluchit_proshivku(ref global G3)
        {
            try
            {
                if (G3 != null) GL = G3;
                byte[] bdata = new byte[15];
                bdata[0] = Convert.ToByte('G');//Идификатор транзакции
                bdata[1] = Convert.ToByte('B');//Идификатор транзакции
                bdata[2] = Convert.ToByte(0x00);//Идификатор протокола
                bdata[3] = Convert.ToByte(0x00);//Идификатор протокола
                bdata[4] = Convert.ToByte(0);//Длина
                bdata[5] = Convert.ToByte(6);//Длина
                bdata[6] = Convert.ToByte('A');//Адрес
                bdata[7] = Convert.ToByte(0x03);//Функциональный код
                bdata[8] = Convert.ToByte(0);//Адрес первого регистра
                bdata[9] = Convert.ToByte(0x33);//Адрес первого регистра
                bdata[10] = Convert.ToByte(0);
                //for (int i = 0; i < 10; i++) { sdata[i] = Convert.ToByte('G'); }
                byte[] data = null;
                if (TCP == null) TCP = new tcp.tcp_client();
                if (TCP.tcp_start(GL.IP, GL.DPORT) < 0) { TCP.tcp_stop(); MessageBox.Show("Нет связи с ПТК КМ Дельта!"); return; }
                TCP.tcp_send(bdata, 12, GL.IP, GL.DPORT);
                data = TCP.recive(); TCP.tcp_stop();
                if (data[0] != Convert.ToByte('Q') || data[0x3D] != Convert.ToByte('G') || data[0x3E] != Convert.ToByte('B')) { throw new FormatException(); }
                if ((data[1] & 0x01) != 0) { GL.otnositelnoe_otobragenie = true; } else { GL.otnositelnoe_otobragenie = false; }
                if ((data[1] & 0x02) != 0) { GL.inversion_data = true; } else { GL.inversion_data = false; }
                if ((data[1] & 0x04) != 0) { GL.otkl_panel = true; } else { GL.otkl_panel = false; }
                if ((data[1] & 0x10) != 0) { GL.avariya[0].avariya1_predupregdenie0 = 1; } else { GL.avariya[0].avariya1_predupregdenie0 = 0; }
                if ((data[1] & 0x20) != 0) { GL.avariya[1].avariya1_predupregdenie0 = 1; } else { GL.avariya[1].avariya1_predupregdenie0 = 0; }
                if ((data[1] & 0x40) != 0) { GL.avariya[2].avariya1_predupregdenie0 = 1; } else { GL.avariya[2].avariya1_predupregdenie0 = 0; }
                if ((data[1] & 0x80) != 0) { GL.avariya[3].avariya1_predupregdenie0 = 1; } else { GL.avariya[3].avariya1_predupregdenie0 = 0; }
                int timeout_alarm;
                timeout_alarm = data[2]; timeout_alarm <<= 8; timeout_alarm |= data[3];
                GL.timeout_alarm = (double)timeout_alarm;
                int[] ipi = new int[4];
                int[] maska = new int[4];
                int[] t_ip2 = new int[4];
                int[] t_ip3 = new int[4];
                for (int i = 0; i < 4; i++)
                {
                    int j = i + 4; ipi[i] = data[j];
                    j = i + 8; maska[i] = data[j];
                    j = i + 12; t_ip2[i] = data[j];
                    j = i + 16; t_ip3[i] = data[j];
                }
                GL.IP1 = t_ip2[0].ToString() + "." + t_ip2[1].ToString() + "." + t_ip2[2].ToString() + "." + t_ip2[3].ToString();
                GL.SPORT = (t_ip3[0] << 8) | (t_ip3[1]); GL.DPORT = (t_ip3[2] << 8) | (t_ip3[3]);
                GL.MASKA = maska[0].ToString() + "." + maska[1].ToString() + "." + maska[2].ToString() + "." + maska[3].ToString();
                GL.IP = ipi[0].ToString() + "." + ipi[1].ToString() + "." + ipi[2].ToString() + "." + ipi[3].ToString();
                for (int i = 0; i < 8; i++)
                {
                    int j = i * 2; j = j + 20;
                    UInt16 smehenie = data[j]; j++;
                    smehenie = (UInt16)(smehenie << 8);
                    smehenie = (UInt16)(smehenie | data[j]);
                    Int16 re = (Int16)smehenie;
                    GL.data[i].smeshenie = re;
                    GL.data[i].smeshenie = GL.data[i].smeshenie / 2.0;
                }
                GL.avariya[0].kolvo_avariynih_datchikov = (data[40] >> 4) & 0x0F;
                GL.avariya[1].kolvo_avariynih_datchikov = data[40] & 0x0F;
                GL.avariya[2].kolvo_avariynih_datchikov = (data[41] >> 4) & 0x0F;
                GL.avariya[3].kolvo_avariynih_datchikov = data[41] & 0x0F;
                int sm = 42;
                for (int i = 0; i < 4; i++)
                {
                    Int16 porog = data[sm]; sm++;
                    porog = (Int16)((porog << 8) | data[sm]); sm++;
                    GL.avariya[i].porog_max = porog;
                    porog = data[sm]; sm++;
                    porog = (Int16)((porog << 8) | data[sm]); sm++;
                    GL.avariya[i].porog_min = porog;
                }
                GL.porog_max = data[36]; GL.porog_max = (Int16)((GL.porog_max << 8) + data[37]);
                GL.porog_min = data[38]; GL.porog_min = (Int16)((GL.porog_min << 8) + data[39]);
                GL.version_proshivki = data[58]; GL.version_proshivki = (UInt16)(GL.version_proshivki << 8);
                GL.version_proshivki = (UInt16)(GL.version_proshivki | data[59]);
                MessageBox.Show("Новые параметры получены");
                GL.obnovlenie_proshivki = true;
                save_setting();
            }
            catch { MessageBox.Show("Ошибка получения параметров"); }
        }
        #endregion
        #region отображение данных  
        const short delayAlarm = 12;
        short setAlarm = delayAlarm;
        private void timer1_Tick(object sender, EventArgs e)
        {
            GL.sekunda++;
            GL.graph_ind++;
            Time_Date.Text = T_Date;//Дата и Время
            //if (GL.obnovlenie_proshivki == true) { menu_disconnect.PerformClick(); return; }
            //if (GL.oshibka == true)
            //{
            //    menu_disconnect.PerformClick(); return;
            //}
            if (setAlarm > delayAlarm) { set_alarm(); setAlarm = 0; }
            setAlarm++;
            if (GL.sekunda == 20) { GL.sekunda = 0; label_speed.Text = GL.skorost_posilok.ToString(); GL.skorost_posilok = 0; }
            if (GL.graph_ind == GL.graph_memory) { chart_graph_set(); GL.graph_ind = 0; }
            if (tabControl1.SelectedIndex == 0) { set_data_to_stolbiki(); }
        }
        void set_data_to_stolbiki()
        {
            while (true)
            {
                if (0 != System.Threading.Interlocked.Exchange(ref GL.lock_otobragenie, 1))
                {
                    for (int i = 0; i < 8; i++)
                    {
                        chart2.Series[0].Points[i + 1].SetValueY(GL.data[i].absolutnoe);
                        string name = "МКЛП " + (i + 1).ToString();
                        if (GL.data[i].absolutnoe < -19000)
                        {
                            chart2.Series[0].Points[i + 1].SetValueY(0);
                            chart2.Series[0].Points[i + 1].Label = "откл.";
                        }
                        else
                        { chart2.Series[0].Points[i + 1].Label = GL.data[i].absolutnoe.ToString(); }
                    }
                    System.Threading.Interlocked.Exchange(ref GL.lock_otobragenie, 0); break;
                }
            }
            chart2.Invalidate();
        }
        void set_data_to_graph()
        {
            for (int i = 0; i < 8; i++)
            {
                chart1.Series[i].Points.Clear();
            }
            chart1.ChartAreas[0].Axes[0].Minimum = GL.XY[0, 8];
            int count = GL.XY_count;
            for (int gg = 0; gg < GL.XY_count - 2; gg++)
            {
                chart1.ChartAreas[0].Axes[0].Maximum = GL.XY[GL.XY_count - 1 - gg, 8];
                if (chart1.ChartAreas[0].Axes[0].Maximum > chart1.ChartAreas[0].Axes[0].Minimum)
                { break; }
                else
                { count--; }
            }
            for (int j = 0; j < count; j++)
            {
                for (int i = 0; i < 8; i++)
                {
                    chart1.Series[i].Points.AddXY(GL.XY[j, 8], GL.XY[j, i]);
                }
            }
            porogi_set(true);
        }
        void set_alarm()
        {
            int alarm = 0;
            int pred = 0;
            string message = "";
            //for (int i = 0; i < 4; i++)
            {
                //if (GL.avariya[i].kolvo_avariynih_datchikov == 0) { continue; }// сигнализация реле отключена
                //if (GL.avariya[1].alarm == false) { return; }// ok 
                if ((GL.avariya[1].value & 0x1) > 0) { message += "Предупреждение: 'Блокировка пуска ГА'" + Environment.NewLine; pred++; }
                if ((GL.avariya[1].value & 0x2) > 0) { message += "Авария: 'Аварийный останов ГА'" + Environment.NewLine; alarm++; }
                if ((GL.avariya[1].value & 0x4) > 0) { message += "Авария:'Сброс АРЗ'!" + Environment.NewLine; alarm++; }
                if ((GL.avariya[2].value & 0x1) > 0) { message += "Неисправность МКЛП1" + Environment.NewLine; }
                if ((GL.avariya[2].value & 0x2) > 0) { message += "Неисправность МКЛП2" + Environment.NewLine; }
                if ((GL.avariya[2].value & 0x4) > 0) { message += "Неисправность МКЛП3" + Environment.NewLine; }
                if ((GL.avariya[2].value & 0x8) > 0) { message += "Неисправность МКЛП4" + Environment.NewLine; }
                if ((GL.avariya[2].value & 0x10) > 0) { message += "Неисправность МКЛП5" + Environment.NewLine; }
                if ((GL.avariya[2].value & 0x20) > 0) { message += "Неисправность МКЛП6" + Environment.NewLine; }
                if ((GL.avariya[2].value & 0x40) > 0) { message += "Неисправность МКЛП7" + Environment.NewLine; }
                if ((GL.avariya[2].value & 0x80) > 0) { message += "Неисправность МКЛП8" + Environment.NewLine; }

                if ((GL.avariya[3].value & 0x1) > 0) { message += "Нет связи с МКЛП1" + Environment.NewLine; }
                if ((GL.avariya[3].value & 0x2) > 0) { message += "Нет связи с МКЛП2" + Environment.NewLine; }
                if ((GL.avariya[3].value & 0x4) > 0) { message += "Нет связи с МКЛП3" + Environment.NewLine; }
                if ((GL.avariya[3].value & 0x8) > 0) { message += "Нет связи с МКЛП4" + Environment.NewLine; }
                if ((GL.avariya[3].value & 0x10) > 0) { message += "Нет связи с МКЛП5" + Environment.NewLine; }
                if ((GL.avariya[3].value & 0x20) > 0) { message += "Нет связи с МКЛП6" + Environment.NewLine; }
                if ((GL.avariya[3].value & 0x40) > 0) { message += "Нет связи с МКЛП7" + Environment.NewLine; }
                if ((GL.avariya[3].value & 0x80) > 0) { message += "Нет связи с МКЛП8" + Environment.NewLine; }
            }
            if (textBox_avariya.Text != message)
                textBox_avariya.Text = message;
            if (alarm != 0) { textBox_avariya.ForeColor = Color.Red; return; }
            if (pred != 0) { textBox_avariya.ForeColor = Color.Green; return; }
        }
        void chart_graph_init(int new_memory, bool refresh)
        {
            while (true)
            {
                if (0 != System.Threading.Interlocked.Exchange(ref GL.graph_memory_set, 1))
                {
                    GL.graph_memory = new_memory;
                    for (int i = 0; i < 8; i++)
                    {
                        double min = 30 * ((double)GL.graph_memory);
                        double m;
                        double mn = min / 600; min = -min; m = min;
                        for (int j = 0; j < 600; j++)
                        {
                            min += mn;
                            chart3.Series[i].Points[j].YValues[0] = 0;
                            chart3.Series[i].Points[j].XValue = min;
                        }
                        chart3.ChartAreas[i].AxisX.Minimum = m;
                        chart3.ChartAreas[i].AxisX.Maximum = 0;
                    }
                    if (refresh == true) { chart3.Invalidate(); }
                    System.Threading.Interlocked.Exchange(ref GL.graph_memory_set, 0);
                    break;
                }
            }
            GL.graph_ind = 0;
        }
        void chart_graph_set()
        {
            while (true)
            {
                if (0 != System.Threading.Interlocked.Exchange(ref GL.graph_memory_set, 1))
                {
                    int L = 599;
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < L; j++)
                        {
                            chart3.Series[i].Points[j].YValues[0] = chart3.Series[i].Points[j + 1].YValues[0];
                        }

                        chart3.Series[i].Points[L].YValues[0] = GL.data[i].absolutnoe;
                    }
                    System.Threading.Interlocked.Exchange(ref GL.graph_memory_set, 0); break;
                }
            }
            chart3.Invalidate();
        }
        void porogi_set(bool dvigenie) { return; }
        private void chart_scroll_Scroll(object sender, ScrollEventArgs e)
        {
            int position = chart_scroll.Value;
            System.IO.StreamReader rd = new System.IO.StreamReader(GL.open_file, Encoding.GetEncoding(1251));
            string str = rd.ReadLine();// читаем период
            int zapisi_count = 0;
            rd.ReadLine();// читаем шапку// далее читаем собственно данные
            while (true)
            {
                if (rd.Peek() == -1) { rd.Close(); break; }
                string dd = rd.ReadLine();
                if (zapisi_count == position)
                {
                    for (int i = 0; i < GL.XY_count; i++)
                    {
                        double time = position + i; time = time * GL.period_oprosa_open;
                        time = Math.Round(time, 1);
                        string_to_data(dd, i, time);

                        if (rd.Peek() == -1) { rd.Close(); break; }
                        dd = rd.ReadLine();
                    }
                    break;
                }
                zapisi_count++;
            }
            rd.Close(); set_data_to_graph();
            if (GL.isOPEN == true)
            {
                for (int i = 0; i < 8; i++)
                {
                    GL.data[i].absolutnoe = GL.XY[0, i];
                }
                set_data_to_stolbiki();
            }
        }
        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (tabControl1.SelectedIndex == 2)
            {
                if (GL.isOPEN == false)
                {
                    while (true)
                    {
                        if (0 == System.Threading.Interlocked.Exchange(ref GL.lock_tcp, 1))
                        {
                            File.Copy(GL.save_file, GL.save_file2, true);
                            Interlocked.Exchange(ref GL.lock_tcp, 0);
                            break;
                        }
                    }
                    GL.open_file = GL.save_file2; //open_data();
                }
            }
            else
            {
                if (GL.isOPEN == false)
                {
                    GL.open_file = GL.save_file;//open_data();
                }
            }
        }
        private void b_memory_Click(object sender, EventArgs e)
        {
            int ind = combo_memory.Items.IndexOf(combo_memory.Text);
            int L; if (ind == -1) { MessageBox.Show("Неверный формат данных!"); return; }
            switch (ind)
            {
                case 0: L = 1; break;
                case 1: L = 2; break;
                case 2: L = 10; break;
                case 3: L = 20; break;
                case 4: L = 60; break;
                case 5: L = 120; break;
                default: return;
            }
            chart_graph_init(L, menu_setting.Enabled);
        }
        #endregion
        private void b_period_Click(object sender, EventArgs e)
        {
            int ind = combo_period.Items.IndexOf(combo_period.Text); int L;
            if (ind == -1) { MessageBox.Show("Неверный формат данных!"); return; }
            switch (ind)
            { case 0: L = 1; break; case 1: L = 10; break; case 2: L = 30; break; case 3: L = 60; break; default: return; }
            GL.mashtab = L;  //open_data();
        }
        #region  Адреса_для_МКЛП
        private void установитьНомерДляМКЛП(byte nb)
        {
            send_data[5] = 0x9;//Длина сообщения
            send_data[6] = 247;//Адрес у
            send_data[7] = 0x6;//Функциональный код
            send_data[9] = 153;//Адрес первого регистра Lo байт
            send_data[11] = 0x1;//Количество регистров Lo байт
            send_data[12] = (byte)(2 * send_data[11]);
            send_data[13] = 0; send_data[14] = nb;
            TCP = null;
            if (TCP == null)
                TCP = new tcp.tcp_client();
            if (TCP.tcp_start(GL.IP, GL.DPORT) < 0) { TCP.tcp_stop(); MessageBox.Show("Нет связи при установке адреса для МКЛП!"); return; }
            TCP.tcp_send(send_data, 15, GL.IP, GL.DPORT); byte[] bdata;
            bdata = TCP.recive(); TCP.tcp_stop(); TCP = null;
            send_data[5] = 6;
            if (bdata == null) { MessageBox.Show("Нет ответа!"); return; }
            for (short k = 0; k < 12; k++) if (bdata[k] != send_data[k]) { MessageBox.Show("Ответ неверен!"); return; }
            { MessageBox.Show("Адрес для МКЛП № " + nb.ToString() + " успешно установлен!"); }
            send_data[6] = 9;//
        }
        private void установитьНомерДляМКЛП1ToolStripMenuItem_Click(object sender, EventArgs e)
        { установитьНомерДляМКЛП(1); }
        private void установитьНомерДляМКЛП2ToolStripMenuItem_Click(object sender, EventArgs e)
        { установитьНомерДляМКЛП(2); }
        private void установитьНомерДляМКЛП3ToolStripMenuItem_Click(object sender, EventArgs e)
        { установитьНомерДляМКЛП(3); }
        private void установитьНомерДляМКЛП4ToolStripMenuItem_Click(object sender, EventArgs e)
        { установитьНомерДляМКЛП(4); }
        private void установитьНомерДляМКЛП5ToolStripMenuItem_Click(object sender, EventArgs e)
        { установитьНомерДляМКЛП(5); }
        private void установитьНомерДляМКЛП6ToolStripMenuItem_Click(object sender, EventArgs e)
        { установитьНомерДляМКЛП(6); }
        private void установитьНомерДляМКЛП7ToolStripMenuItem_Click(object sender, EventArgs e)
        { установитьНомерДляМКЛП(7); }
        private void установитьНомерДляМКЛП8ToolStripMenuItem_Click(object sender, EventArgs e)
        { установитьНомерДляМКЛП(8); }
        private void сбросВсехАдресоМКЛПToolStripMenuItem_Click(object sender, EventArgs e)
        {
            send_data[5] = 0x9;//Длина сообщения
            send_data[6] = 0;//Адрес у
            send_data[7] = 0x6;//Функциональный код
            send_data[9] = 150;//Адрес первого регистра Lo байт
            send_data[11] = 0x1;//Количество регистров Lo байт
            send_data[12] = (byte)(2 * send_data[11]);
            send_data[13] = 33;
            send_data[14] = 153;
            TCP = null;
            if (TCP == null)
                TCP = new tcp.tcp_client();
            if (TCP.tcp_start(GL.IP, GL.DPORT) < 0) { TCP.tcp_stop(); MessageBox.Show("Нет связи при сбросе адреса!"); return; }
            TCP.tcp_send(send_data, 15, GL.IP, GL.DPORT); Thread.Sleep(5000);
            byte[] bdata;
            bdata = TCP.recive(); TCP.tcp_stop(); TCP = null; send_data[5] = 6;
            if (bdata == null) { MessageBox.Show("Нет ответа!"); return; }
            for (short k = 0; k < 12; k++) if (bdata[k] != send_data[k]) { MessageBox.Show("Ответ неверен!"); return; }
            { MessageBox.Show("Все парамеры подключенных МКЛП сброшены до заводских!"); }
            send_data[6] = 9;//
        }
        #endregion      

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] x = new byte[50];
            x[9] = 0; x[10] = 121 * 2;
            x[11] = 0; x[12] = 122 * 2;
            x[13] = 0; x[14] = 123 * 2;
            x[15] = 0; x[16] = 124 * 2;
            x[17] = 0; x[18] = 125 * 2;
            x[19] = 0; x[20] = 126 * 2;
            x[21] = 0; x[22] = 127 * 2;
            x[23] = 0; x[24] = 128 * 2 - 1;

            x[25] = 0; x[26] = 125;
            x[27] = 0; x[28] = 125;
            x[29] = 0; x[30] = 125;

            x[31] = 0; x[32] = 0x07;
            x[33] = 0x33; x[34] = 0x33;

            priem(x);
            set_data_to_stolbiki(); set_alarm();
        }
        class DateCompare : IComparer<event_data>
        {
            public int Compare(event_data x1, event_data x2)
            {
                if (x1.date > x2.date) return -1;
                else if (x1.date < x2.date) return 1;
                return 0;
            }
        }
        class DateCompareR : IComparer<event_data>
        {
            public int Compare(event_data x1, event_data x2)
            {
                if (x1.date < x2.date) return -1;
                else if (x1.date > x2.date) return 1;
                return 0;
            }
        }
        class event_data
        {
            public int cod { get; set; }
            public double mediana { get; set; }
            public DateTime date { get; set; }
            public string message { get; set; }
            public string status { get; set; }
            public string uchastok { get; set; }
            public int number { get; set; }
            public int SAmed { get; set; }
        }
        private void получитьАрхивСобытийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            send_data[0] = 1;//Идентификатор транзакции
            send_data[1] = 1;//Идентификатор транзакции
            send_data[2] = 0;//Идентификатор протокола
            send_data[3] = 0;//Идентификатор протокола
            send_data[4] = 0;//Длина сообщения
            send_data[5] = 6;//Длина сообщения
            send_data[6] = 0;//Адрес у
            send_data[7] = 3;//Функциональный код
            send_data[8] = 1;//Адрес первого регистра Архива
            send_data[9] = 153;//Адрес первого регистра Архива
            send_data[10] = 0x07;//Количество регистров
            send_data[11] = 0xD9;//Количество регистров 2003 500 записей по 8 байт == 2000 регистров
            if (TCP == null)//8 байт на одну запись - 4 регистра
                TCP = new tcp.tcp_client();
            if (TCP.tcp_start(GL.IP, GL.DPORT) < 0) { TCP.tcp_stop(); MessageBox.Show("Нет связи при получении архива!"); return; }
            TCP.tcp_send(send_data, 12, GL.IP, GL.DPORT); Thread.Sleep(1300);
            byte[] bd;
            bd = TCP.recive(); TCP.tcp_stop();
            if (bd == null) { MessageBox.Show("Нет ответа с данными архива!"); return; }
            try
            {
                using (StreamWriter wr = new StreamWriter(GL.event_file, false, Encoding.GetEncoding(1251)))
                {
                    wr.WriteLine("Архив событий от ПТК КМ-Дельта от " + DateTime.Now.ToLongDateString() + " IP-адрес: " + GL.IP);
                    List<event_data> str = new List<event_data>();
                    int Leng = ((bd[4] << 8) + bd[5]) / 8; int y, m, d, h, min, sec, ix = 8;
                    for (short k = 0; k < Leng; k++)
                    {
                        try
                        {
                            if (bd[ix + 1] == '+')
                            {
                                ix += 2; str.Add(new event_data());
                                d = (bd[ix] >> 4) * 10 + (bd[ix] & 0x0F); ++ix;
                                m = (bd[ix] >> 4) * 10 + (bd[ix] & 0x0F); ++ix;
                                y = (bd[ix] >> 4) * 10 + (bd[ix] & 0x0F); ++ix;
                                h = (bd[ix] >> 4) * 10 + (bd[ix] & 0x0F); ++ix;
                                min = (bd[ix] >> 4) * 10 + (bd[ix] & 0x0F); ++ix;
                                sec = (bd[ix] >> 4) * 10 + (bd[ix] & 0x0F); ++ix;
                                str[k].date = new DateTime(y + 2000, m, d, h, min, sec);
                                str[k].cod = bd[ix]; str[k].status = ";   + ;";
                                switch (bd[ix])
                                {
                                    case 1: str[k].message = "Нет связи с МКЛП №1"; break;
                                    case 2: str[k].message = "Нет связи с МКЛП №2"; break;
                                    case 3: str[k].message = "Нет связи с МКЛП №3"; break;
                                    case 4: str[k].message = "Нет связи с МКЛП №4"; break;
                                    case 5: str[k].message = "Нет связи с МКЛП №5"; break;
                                    case 6: str[k].message = "Нет связи с МКЛП №6"; break;
                                    case 7: str[k].message = "Нет связи с МКЛП №7"; break;
                                    case 8: str[k].message = "Нет связи с МКЛП №8"; break;
                                    case 0x21: str[k].message = "Нет связи с МКЛП №1"; str[k].status = ";   - ;"; break;
                                    case 0x22: str[k].message = "Нет связи с МКЛП №2"; str[k].status = ";   - ;"; break;
                                    case 0x23: str[k].message = "Нет связи с МКЛП №3"; str[k].status = ";   - ;"; break;
                                    case 0x24: str[k].message = "Нет связи с МКЛП №4"; str[k].status = ";   - ;"; break;
                                    case 0x25: str[k].message = "Нет связи с МКЛП №5"; str[k].status = ";   - ;"; break;
                                    case 0x26: str[k].message = "Нет связи с МКЛП №6"; str[k].status = ";   - ;"; break;
                                    case 0x27: str[k].message = "Нет связи с МКЛП №7"; str[k].status = ";   - ;"; break;
                                    case 0x28: str[k].message = "Нет связи с МКЛП №8"; str[k].status = ";   - ;"; break;
                                    case 0x51: str[k].message = "Предупреждение от МКЛП №1"; break;
                                    case 0x52: str[k].message = "Предупреждение от МКЛП №2"; break;
                                    case 0x53: str[k].message = "Предупреждение от МКЛП №3"; break;
                                    case 0x54: str[k].message = "Предупреждение от МКЛП №4"; break;
                                    case 0x55: str[k].message = "Предупреждение от МКЛП №5"; break;
                                    case 0x56: str[k].message = "Предупреждение от МКЛП №6"; break;
                                    case 0x57: str[k].message = "Предупреждение от МКЛП №7"; break;
                                    case 0x58: str[k].message = "Предупреждение от МКЛП №8"; break;
                                    case 0x71: str[k].message = "Авария от МКЛП №1"; break;
                                    case 0x72: str[k].message = "Авария от МКЛП №2"; break;
                                    case 0x73: str[k].message = "Авария от МКЛП №3"; break;
                                    case 0x74: str[k].message = "Авария от МКЛП №4"; break;
                                    case 0x75: str[k].message = "Авария от МКЛП №5"; break;
                                    case 0x76: str[k].message = "Авария от МКЛП №6"; break;
                                    case 0x77: str[k].message = "Авария от МКЛП №7"; break;
                                    case 0x78: str[k].message = "Авария от МКЛП №8"; break;
                                    case 0x11: str[k].message = "Неисправность МКЛП №1"; break;
                                    case 0x12: str[k].message = "Неисправность МКЛП №2"; break;
                                    case 0x13: str[k].message = "Неисправность МКЛП №3"; break;
                                    case 0x14: str[k].message = "Неисправность МКЛП №4"; break;
                                    case 0x15: str[k].message = "Неисправность МКЛП №5"; break;
                                    case 0x16: str[k].message = "Неисправность МКЛП №6"; break;
                                    case 0x17: str[k].message = "Неисправность МКЛП №7"; break;
                                    case 0x18: str[k].message = "Неисправность МКЛП №8"; break;
                                    case 0xA0: str[k].message = "Включение ПТК КМ-Дельта"; break;
                                    case 0xB0: str[k].message = "Неисправность Блока управления"; break;

                                    case 0xA1: str[k].message = "Блокировка пуска ГА"; break;
                                    case 0xB1: str[k].message = "Аварийный останов ГА"; break;
                                    case 0xC1: str[k].message = "Сброс АРЗ"; break;
                                    case 0xA3: str[k].message = "Блокировка пуска ГА"; str[k].status = ";   - ;"; break;//после нажатия кнопки «сброс сигнализации»
                                    case 0xB3: str[k].message = "Аварийный останов ГА"; str[k].status = ";   - ;"; break;//после нажатия кнопки «сброс сигнализации»
                                    case 0xC3: str[k].message = "Сброс АРЗ"; str[k].status = ";   - ;"; break;//после нажатия кнопки «сброс сигнализации»

                                    case 0xBC: str[k].message = "Отсутствие связи с САУ ГА"; break;
                                    case 0xBF: str[k].message = "Отсутствие связи с САУ ГА"; str[k].status = ";   - ;"; break;

                                    case 0x33: str[k].message = "Установка даты и времени"; break;
                                    case 0xCC: str[k].message = "Установка настроеек ПТК КМ-Дельта"; break;
                                }
                            }
                        }
                        catch { continue; }//MessageBox.Show(w.ToString());
                    }
                    DateCompare cx = new DateCompare();
                    str.Sort(cx);
                    wr.WriteLine("№; Дата; Событие; Статус сигнала;"); short kx = 1;
                    for (short k = 0; k < str.Count(); k++)
                    {
                        if (str[k].date.Year < 2000)
                        { continue; }
                        wr.WriteLine(kx + "; " + str[k].date + ".000; " + str[k].message + str[k].status); kx += 1;
                    }
                    wr.Close();    //System.Diagnostics.Process.Start(GL.event_file);
                }
                MessageBox.Show("Архив успешно сформирован!");
                send_data[6] = 9;
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
        }


        private void получитьАрхивТекущихЗначенийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            send_data[0] = 1;//Идентификатор транзакции
            send_data[1] = 1;//Идентификатор транзакции
            send_data[2] = 0;//Идентификатор протокола
            send_data[3] = 0;//Идентификатор протокола
            send_data[4] = 0;//Длина сообщения
            send_data[5] = 6;//Длина сообщения
            send_data[6] = 0;//Адрес у
            send_data[7] = 3;//Функциональный код
            send_data[8] = 0x79;//Адрес первого регистра Архива
            send_data[9] = 0x18;//Адрес первого регистра Архива
            send_data[10] = 0x02;//Количество регистров 0x280
            send_data[11] = 0x80;//Количество регистров 640; 
            if (TCP == null)//8 байт на одну запись - 4 регистра
                TCP = new tcp.tcp_client();
            if (TCP.tcp_start(GL.IP, GL.DPORT) < 0) { TCP.tcp_stop(); MessageBox.Show("Нет связи при получении архива!"); return; }
            TCP.tcp_send(send_data, 12, GL.IP, GL.DPORT); Thread.Sleep(1300);
            byte[] bd;
            bd = TCP.recive(); TCP.tcp_stop();
            if (bd == null) { MessageBox.Show("Нет ответа с данными архива!"); }
            try
            {
                int[,] array = new int[80, 8]; string fileX = Environment.CurrentDirectory + "\\Архив за 10 мин.csv";
                using (StreamWriter wr = new StreamWriter(fileX, false, Encoding.GetEncoding(1251)))
                {
                    short ix = 0;
                    wr.WriteLine("Архив за 10 мин от ПТК КМ-Дельта от " + DateTime.Now.ToLongDateString() + " IP-адрес: " + GL.IP);
                    for (short k = 0; k < 8; k++)
                    {
                        try
                        {
                            for (short z = 0; z < 80; z++)
                            {
                                array[z, k] = (bd[ix] << 8) + (bd[ix + 1]); ix += 2;
                            }
                        }
                        catch (Exception w) { }//MessageBox.Show(w.ToString());
                    }
                    wr.WriteLine("Датчик №1;Датчик №2;Датчик №3;Датчик №4;Датчик №5;Датчик №6;Датчик №7;Датчик №8;"); short kx = 1;
                    for (short z = 0; z < 80; z++)
                    {
                        wr.WriteLine($"{array[z, 0]};{array[z, 1]};{array[z, 2]};{array[z, 3]};{array[z, 4]};{array[z, 5]};{array[z, 6]};{array[z, 7]};");
                    }
                    wr.Close();
                }
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
        }

        private void получитьАрхивАлгоритмаСамоконтроляToolStripMenuItem_Click(object sender, EventArgs e)
        {
            send_data[0] = 1;//Идентификатор транзакции
            send_data[1] = 1;//Идентификатор транзакции
            send_data[2] = 0;//Идентификатор протокола
            send_data[3] = 0;//Идентификатор протокола
            send_data[4] = 0;//Длина сообщения
            send_data[5] = 6;//Длина сообщения
            send_data[6] = 0;//Адрес у
            send_data[7] = 3;//Функциональный код
            send_data[8] = 1;//Адрес первого регистра Архива
            send_data[9] = 0x4D;//Адрес первого регистра Архива
            send_data[10] = 0x2E;//Количество регистров 0x2E
            send_data[11] = 0xE3;//Количество регистров 12000; 3000 записей по 8 байт == 12000 регистров

            if (TCP == null)
            {
                TCP = new tcp.tcp_client(); Thread.Sleep(333);
                if (!TCP.tcp_status())
                {
                    if (TCP.tcp_start(GL.IP, GL.DPORT) < 0)
                    {
                        TCP.tcp_stop(); TCP = null;
                        MessageBox.Show("Нет связи при получении архива!"); return;
                    }
                }
            }
            Thread.Sleep(300); TCP.tcp_send(send_data, 12, GL.IP, GL.DPORT); Thread.Sleep(800);
            byte[] bd;
            try
            {
                bd = TCP.recive();
                if (bd == null)
                {
                    MessageBox.Show("Нет ответа с данными архива по алгоритму самоконтроля!");
                    goto INRI;
                }
                using (StreamWriter wr = new StreamWriter(GL.vniim_file, false, Encoding.GetEncoding(1251)))
                {
                    wr.WriteLine("Архив по алгоритму самоконтроля от ПТК КМ-Дельта от " + DateTime.Now.ToLongDateString() + " IP-адрес: " + GL.IP);
                    List<event_data> str = new List<event_data>();
                    int Leng = ((bd[4] << 8) + bd[5]) / 8; int y, m, d, h, min, sec, ix = 8;
                    for (short k = 0; k < 12000; k++)
                    {
                        try
                        {
                            if (bd[ix + 1] == '+')
                            {
                                ix += 2; str.Add(new event_data());
                                d = (bd[ix] >> 4) * 10 + (bd[ix] & 0x0F); ++ix;
                                m = (bd[ix] >> 4) * 10 + (bd[ix] & 0x0F); ++ix;
                                y = (bd[ix] >> 4) * 10 + (bd[ix] & 0x0F); ++ix;
                                h = (bd[ix] >> 4) * 10 + (bd[ix] & 0x0F); ++ix;
                                min = (bd[ix] >> 4) * 10 + (bd[ix] & 0x0F); ++ix;
                                sec = (bd[ix] >> 4) * 10 + (bd[ix] & 0x0F); ++ix;
                                str[k].date = new DateTime(y + 2000, m, d, h, min, sec);
                                str[k].cod = bd[ix];
                                switch (bd[ix])
                                {
                                    case 1: str[k].message = "Старт калибровки"; break;
                                    case 2: str[k].message = "Конец калибровки"; break;
                                    case 3: str[k].message = "Cтарт текущего мониторинга 'мёртвого нуля'"; break;
                                    case 4: str[k].message = "'Большой скачок' не обнаружен"; break;
                                    case 5: str[k].message = "Старт поиска 'Большого' скачка"; break;
                                    case 7: str[k].message = "Обнаружен 'Большой скачок'."; break;//Все рабочие датчики превысили Rdz
                                    case 0xA0: str[k].message = "Включение ПТК КМ-Дельта"; break;
                                    case 0xEE: str[k].message = "Тревога! Более трех МКЛП неисправны."; break;

                                    case 0x31: str[k].message = "Конец 10-ти минутного окна №1"; str[k].status = ";   - ;"; break;
                                    case 0x32: str[k].message = "Конец 10-ти минутного окна №2"; str[k].status = ";   - ;"; break;
                                    case 0x33: str[k].message = "Конец 10-ти минутного окна №3"; str[k].status = ";   - ;"; break;
                                    case 0x34: str[k].message = "Конец 10-ти минутного окна №4"; str[k].status = ";   - ;"; break;
                                    case 0x35: str[k].message = "Конец 10-ти минутного окна №5"; str[k].status = ";   - ;"; break;
                                    case 0x36: str[k].message = "Конец 10-ти минутного окна №6"; str[k].status = ";   - ;"; break;
                                    case 0x37: str[k].message = "Конец 10-ти минутного окна №7"; str[k].status = ";   - ;"; break;
                                    case 0x38: str[k].message = "Конец 10-ти минутного окна №8"; str[k].status = ";   - ;"; break;
                                    case 0x39: str[k].message = "Конец 10-ти минутного окна №9"; str[k].status = ";   - ;"; break;
                                    case 0x3A: str[k].message = "Конец 10-ти минутного окна №10"; str[k].status = ";   - ;"; break;
                                    case 0x3B: str[k].message = "Конец 10-ти минутного окна №11"; str[k].status = ";   - ;"; break;
                                    case 0x3C: str[k].message = "Конец 10-ти минутного окна №12"; str[k].status = ";   - ;"; break;
                                    case 0x3D: str[k].message = "Конец 10-ти минутного окна №13"; str[k].status = ";   - ;"; break;
                                    case 0x3E: str[k].message = "Конец 10-ти минутного окна №14"; str[k].status = ";   - ;"; break;
                                    case 0x3F: str[k].message = "Конец 10-ти минутного окна №15"; str[k].status = ";   - ;"; break;
                                    case 0x40: str[k].message = "Конец 10-ти минутного окна №16"; str[k].status = ";   - ;"; break;
                                    case 0x41: str[k].message = "Конец 10-ти минутного окна №17"; str[k].status = ";   - ;"; break;
                                    case 0x42: str[k].message = "Конец 10-ти минутного окна №18"; str[k].status = ";   - ;"; break;

                                    case 0x11: str[k].message = "МКЛП №1 ожил"; break;
                                    case 0x12: str[k].message = "МКЛП №2 ожил"; break;
                                    case 0x13: str[k].message = "МКЛП №3 ожил"; break;
                                    case 0x14: str[k].message = "МКЛП №4 ожил"; break;
                                    case 0x15: str[k].message = "МКЛП №5 ожил"; break;
                                    case 0x16: str[k].message = "МКЛП №6 ожил"; break;
                                    case 0x17: str[k].message = "МКЛП №7 ожил"; break;
                                    case 0x18: str[k].message = "МКЛП №8 ожил"; break;

                                    case 0xA1: str[k].message = "Одиночное залипание МКЛП №1"; break;
                                    case 0xA2: str[k].message = "Одиночное залипание МКЛП №2"; break;
                                    case 0xA3: str[k].message = "Одиночное залипание МКЛП №3"; break;
                                    case 0xA4: str[k].message = "Одиночное залипание МКЛП №4"; break;
                                    case 0xA5: str[k].message = "Одиночное залипание МКЛП №5"; break;
                                    case 0xA6: str[k].message = "Одиночное залипание МКЛП №6"; break;
                                    case 0xA7: str[k].message = "Одиночное залипание МКЛП №7"; break;
                                    case 0xA8: str[k].message = "Одиночное залипание МКЛП №8"; break;

                                    case 0xC1: str[k].message = "Полное залипание МКЛП №1"; str[k].status = ";   - ;"; break;
                                    case 0xC2: str[k].message = "Полное залипание МКЛП №2"; str[k].status = ";   - ;"; break;
                                    case 0xC3: str[k].message = "Полное залипание МКЛП №3"; str[k].status = ";   - ;"; break;
                                    case 0xC4: str[k].message = "Полное залипание МКЛП №4"; str[k].status = ";   - ;"; break;
                                    case 0xC5: str[k].message = "Полное залипание МКЛП №5"; str[k].status = ";   - ;"; break;
                                    case 0xC6: str[k].message = "Полное залипание МКЛП №6"; str[k].status = ";   - ;"; break;
                                    case 0xC7: str[k].message = "Полное залипание МКЛП №7"; str[k].status = ";   - ;"; break;
                                    case 0xC8: str[k].message = "Полное залипание МКЛП №8"; str[k].status = ";   - ;"; break;

                                    case 0xD1: str[k].message = "Ошибка Е1 положительный выход за порог МКЛП №1"; break;
                                    case 0xD2: str[k].message = "Ошибка Е1 положительный выход за порог МКЛП №2"; break;
                                    case 0xD3: str[k].message = "Ошибка Е1 положительный выход за порог МКЛП №3"; break;
                                    case 0xD4: str[k].message = "Ошибка Е1 положительный выход за порог МКЛП №4"; break;
                                    case 0xD5: str[k].message = "Ошибка Е1 положительный выход за порог МКЛП №5"; break;
                                    case 0xD6: str[k].message = "Ошибка Е1 положительный выход за порог МКЛП №6"; break;
                                    case 0xD7: str[k].message = "Ошибка Е1 положительный выход за порог МКЛП №7"; break;
                                    case 0xD8: str[k].message = "Ошибка Е1 положительный выход за порог МКЛП №8"; break;

                                    case 0xF1: str[k].message = "Ошибка Е2 положительный выход за порог МКЛП №1"; break;
                                    case 0xF2: str[k].message = "Ошибка Е2 положительный выход за порог МКЛП №2"; break;
                                    case 0xF3: str[k].message = "Ошибка Е2 положительный выход за порог МКЛП №3"; break;
                                    case 0xF4: str[k].message = "Ошибка Е2 положительный выход за порог МКЛП №4"; break;
                                    case 0xF5: str[k].message = "Ошибка Е2 положительный выход за порог МКЛП №5"; break;
                                    case 0xF6: str[k].message = "Ошибка Е2 положительный выход за порог МКЛП №6"; break;
                                    case 0xF7: str[k].message = "Ошибка Е2 положительный выход за порог МКЛП №7"; break;
                                    case 0xF8: str[k].message = "Ошибка Е2 положительный выход за порог МКЛП №8"; break;

                                    case 0xB1: str[k].message = "Ошибка Е2 отрицательный выход за порог МКЛП №1"; break;
                                    case 0xB2: str[k].message = "Ошибка Е2 отрицательный выход за порог МКЛП №2"; break;
                                    case 0xB3: str[k].message = "Ошибка Е2 отрицательный выход за порог МКЛП №3"; break;
                                    case 0xB4: str[k].message = "Ошибка Е2 отрицательный выход за порог МКЛП №4"; break;
                                    case 0xB5: str[k].message = "Ошибка Е2 отрицательный выход за порог МКЛП №5"; break;
                                    case 0xB6: str[k].message = "Ошибка Е2 отрицательный выход за порог МКЛП №6"; break;
                                    case 0xB7: str[k].message = "Ошибка Е2 отрицательный выход за порог МКЛП №7"; break;
                                    case 0xB8: str[k].message = "Ошибка Е2 отрицательный выход за порог МКЛП №8"; break;

                                    case 0xE0: str[k].message = "Участок стационарности не найден"; break;

                                    case 0xE1: str[k].message = "Ошибка Е1 отрицательный выход за порог МКЛП №1"; break;
                                    case 0xE2: str[k].message = "Ошибка Е1 отрицательный выход за порог МКЛП №2"; break;
                                    case 0xE3: str[k].message = "Ошибка Е1 отрицательный выход за порог МКЛП №3"; break;
                                    case 0xE4: str[k].message = "Ошибка Е1 отрицательный выход за порог МКЛП №4"; break;
                                    case 0xE5: str[k].message = "Ошибка Е1 отрицательный выход за порог МКЛП №5"; break;
                                    case 0xE6: str[k].message = "Ошибка Е1 отрицательный выход за порог МКЛП №6"; break;
                                    case 0xE7: str[k].message = "Ошибка Е1 отрицательный выход за порог МКЛП №7"; break;
                                    case 0xE8: str[k].message = "Ошибка Е1 отрицательный выход за порог МКЛП №8"; break;
                                    default: str[k].message = bd[ix].ToString(); break;
                                }
                            }
                            else
                            {
                                str.Add(new event_data());
                                str[k].message = bd[ix].ToString(); ix += 1;
                            }
                        }
                        catch (Exception w) { str[k].message = w.ToString(); }//MessageBox.Show(w.ToString());
                    }
                    ///DateCompareR cx = new DateCompareR();str.Sort(cx);
                    wr.WriteLine("№; Дата; Событие;"); short kx = 1, okno = 1;
                    for (short k = 0; k < str.Count(); k++)
                    {
                        if (str[k].date.Year < 2000) { continue; }
                        string line = $"{kx} ; {str[k].date} ; {str[k].message}";
                        if (str[k].message.Contains("Старт калибровки")) okno = 1;
                        if (str[k].message.Contains("Конец 10-ти") || str[k].message.Contains("Участок стационарности не найден"))
                        {
                            line += $"; Окно №{okno}"; okno += 1;
                        }
                        wr.WriteLine(line); kx += 1;
                    }
                    wr.Close();
                }
                INRI:
                send_data[8] = 0xC3;//Адрес первого регистра Архива Медиан 
                send_data[9] = 0x50;//Адрес первого регистра Архива Медиан 
                send_data[10] = 0x0F;//Количество регистров
                send_data[11] = 0xA3;//Количество регистров 4000 1000 записей по 8 байт == 4000 регистров

                TCP.tcp_send(send_data, 12, GL.IP, GL.DPORT);
                Thread.Sleep(800);
                int countB = 0;
                bd = TCP.recive(ref countB);

                TCP.tcp_stop(); TCP = null;

                if (bd == null) { MessageBox.Show("Нет ответа с данными архива Медиан!"); return; }

                //Получение архива Медиан
                using (StreamWriter wr = new StreamWriter(GL.mediana_file, false, Encoding.GetEncoding(1251)))
                {
                    wr.WriteLine("Архив вычисленных Медиан от " + DateTime.Now.ToLongDateString() + " IP-адрес: " + GL.IP);
                    List<event_data> str = new List<event_data>();
                    int Leng = ((bd[4] << 8) + bd[5]) / 8; int y, m, d, h, min, sec, ix = 8;
                    for (short k = 0; k < 3000; k++)
                    {
                        try
                        {
                            IX:
                            ix += 1;
                            if (ix > countB)
                                break;
                            if ((bd[ix] & 0x0F) == 0)
                                goto IX;
                            str.Add(new event_data());
                            str[k].SAmed = bd[ix]; ++ix;
                            d = (bd[ix] >> 4) * 10 + (bd[ix] & 0x0F); ++ix;
                            m = (bd[ix] >> 4) * 10 + (bd[ix] & 0x0F); ++ix;
                            y = (bd[ix] >> 4) * 10 + (bd[ix] & 0x0F); ++ix;
                            h = (bd[ix] >> 4) * 10 + (bd[ix] & 0x0F); ++ix;
                            min = (bd[ix] >> 4) * 10 + (bd[ix] & 0x0F); ++ix;
                            sec = (bd[ix] >> 4) * 10 + (bd[ix] & 0x0F); ++ix;
                            str[k].date = new DateTime(y + 2000, m, d, h, min, sec);

                            str[k].number = bd[ix] + 1;
                            ++ix;
                            switch (bd[ix])
                            {
                                case 1: str[k].uchastok = "Первый участок стационнарности"; break;
                                case 2: str[k].uchastok = "Второй участок стационнарности"; break;
                                case 3: str[k].uchastok = "Третий участок стационнарности"; break;
                                case 4: str[k].uchastok = "Результат калибровки"; break;
                                default: str[k].uchastok = ""; break;
                            }
                            ++ix;
                            str[k].cod = bd[ix];
                            str[k].cod = str[k].cod << 8; ++ix;
                            str[k].cod += bd[ix];
                            str[k].cod = str[k].cod << 8; ++ix;
                            str[k].cod += bd[ix];
                            str[k].cod = str[k].cod << 8; ++ix;
                            str[k].cod += bd[ix];
                            if (str[k].cod > 30000)
                            {
                                str[k].mediana = (double)str[k].cod / 1000;
                            }
                        }
                        catch { continue; }
                    }
                    DateCompare cx = new DateCompare(); str.Sort(cx);
                    wr.WriteLine("№; Дата;Участок;Окно;Номер датчика;Величина медианы;"); short kx = 1;
                    for (short k = 0; k < str.Count(); k++)
                    {
                        if (str[k].date.Year < 2000)
                        { continue; }
                        if (str[k].SAmed > 10)
                        {
                            wr.WriteLine($"{kx}; {str[k].date}; {str[k].uchastok};Средняя медиана датчика №{str[k].number}; {str[k].mediana}");
                            kx += 1;
                        }
                    }
                    wr.Close();
                }
                MessageBox.Show("Архивы успешно сформированы!");
                send_data[6] = 9;
            }
            catch (Exception er) { MessageBox.Show(er.Message); }
        }

    }
}
