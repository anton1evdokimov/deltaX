using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using КМ_Дельта_М;
namespace parametry
{
    public partial class parametry : Form
    {
        public global GL; tcp.tcp_client TCP = new tcp.tcp_client();       
        private void settings_copy(){
            while (true)//Исправить коллекцию 
            {
                if (domainUpDown2.Text == domainUpDown2.Items[0].ToString()) { GL.timeout_alarm = 0; break; }
                if (domainUpDown2.Text == domainUpDown2.Items[1].ToString()) { GL.timeout_alarm = 1; break; }
                if (domainUpDown2.Text == domainUpDown2.Items[2].ToString()) { GL.timeout_alarm = 4; break; }
                if (domainUpDown2.Text == domainUpDown2.Items[3].ToString()) { GL.timeout_alarm = 9; break; }
                if (domainUpDown2.Text == domainUpDown2.Items[4].ToString()) { GL.timeout_alarm = 12; break; }
                if (domainUpDown2.Text == domainUpDown2.Items[5].ToString()) { GL.timeout_alarm = 17; break; }
                MessageBox.Show("Ошибка выбора");
                return;
            }
            GL.otnositelnoe_otobragenie = radioButton1.Checked;
            GL.data[0].smeshenie = (double)numericUpDown1.Value;
            GL.data[1].smeshenie = (double)numericUpDown2.Value;
            GL.data[2].smeshenie = (double)numericUpDown3.Value;
            GL.data[3].smeshenie = (double)numericUpDown4.Value;
            GL.data[4].smeshenie = (double)numericUpDown5.Value;
            GL.data[5].smeshenie = (double)numericUpDown6.Value;
            GL.data[6].smeshenie = (double)numericUpDown7.Value;
            GL.data[7].smeshenie = (double)numericUpDown8.Value;

            GL.avariya[0].porog_max = (int)(r1_top.Value * 2);
            GL.avariya[1].porog_max = (int)(r2_top.Value * 2);
            //GL.avariya[2].porog_max = (int)(r3_top.Value * 2);
            //GL.avariya[3].porog_max = (int)(r4_top.Value * 2);
            GL.avariya[0].porog_min = (int)(r1_down.Value * 2);
            GL.avariya[1].porog_min = (int)(r2_down.Value * 2);
            //GL.avariya[2].porog_min = (int)(r3_down.Value * 2);
            //GL.avariya[3].porog_min = (int)(r4_down.Value * 2);
            GL.avariya[0].kolvo_avariynih_datchikov = (int)n_kolvo_datchikov.Value;
            GL.avariya[1].kolvo_avariynih_datchikov = (int)n_kolvo_datchikov2.Value;
            //GL.avariya[2].kolvo_avariynih_datchikov = (int)n_kolvo_datchikov3.Value;
            //GL.avariya[3].kolvo_avariynih_datchikov = (int)n_kolvo_datchikov4.Value;
            if (r1_alarm.SelectedIndex == 2) { GL.avariya[0].avariya1_predupregdenie0 = 0; GL.avariya[0].kolvo_avariynih_datchikov = 0; } else { GL.avariya[0].avariya1_predupregdenie0 = 1 - r1_alarm.SelectedIndex; }
            if (r2_alarm.SelectedIndex == 2) { GL.avariya[1].avariya1_predupregdenie0 = 0; GL.avariya[1].kolvo_avariynih_datchikov = 0; } else { GL.avariya[1].avariya1_predupregdenie0 = 1 - r2_alarm.SelectedIndex; }
            //     if (r3_alarm.SelectedIndex == 2) { GL.avariya[2].avariya1_predupregdenie0 = 0; GL.avariya[2].kolvo_avariynih_datchikov = 0; } else { GL.avariya[2].avariya1_predupregdenie0 =1- r3_alarm.SelectedIndex; }
            //     if (r4_alarm.SelectedIndex == 2) { GL.avariya[3].avariya1_predupregdenie0 = 0; GL.avariya[3].kolvo_avariynih_datchikov = 0; } else { GL.avariya[3].avariya1_predupregdenie0 =1- r4_alarm.SelectedIndex; }
            GL.inversion_data = checkBox_inv.Checked;
            GL.inversion_rele = checkBox_rele.Checked;
            GL.porog_min = (int)numeric_minimum.Value;
            GL.porog_max = (int)numeric_maximum.Value; 
        }
        private void button1_Click(object sender, EventArgs e)
        {
            settings_copy();
            int f = send_data();
            if (f == 0) 
            {
                GL.IP = text_ip_new.Text;
                GL.MASKA = text_maska_new.Text;//GL.IP1 = text_ip1.Text;
                GL.DPORT = Convert.ToInt32(text_dport_new.Text); //GL.SPORT = Convert.ToInt32(text_sport_new.Text);
                this.Close(); return; 
            }
            if (f == 1) { MessageBox.Show("Ошибка формата данных"); return; }
            if (f == 2) { MessageBox.Show("Нет связи!"); return; }
        }
        private void parametry_Load(object sender, EventArgs e)
        {
            try
            {
                if (GL.avariya[0].kolvo_avariynih_datchikov == 0)
                { r1_alarm.SelectedIndex = 2; n_kolvo_datchikov.Value = 1; }
                else 
                { r1_alarm.SelectedIndex = 1-GL.avariya[0].avariya1_predupregdenie0; 
                  n_kolvo_datchikov.Value = GL.avariya[0].kolvo_avariynih_datchikov; 
                }
                if (GL.avariya[1].kolvo_avariynih_datchikov == 0)
                { r2_alarm.SelectedIndex = 2; n_kolvo_datchikov2.Value = 1; }
                else
                {
                    r2_alarm.SelectedIndex =1- GL.avariya[1].avariya1_predupregdenie0;
                    n_kolvo_datchikov2.Value = GL.avariya[1].kolvo_avariynih_datchikov;
                }                             
                text_ip_old.Text = GL.IP;
                text_ip_new.Text = GL.IP;
                text_maska_old.Text = GL.MASKA;
                text_maska_new.Text = GL.MASKA;
                //text_ip1.Text = GL.IP1;
                text_dport_old.Text = GL.DPORT.ToString();
                text_dport_new.Text = GL.DPORT.ToString();
                //chart2.ChartAreas[0].AxisY.Minimum = GL.porog_min;
                numeric_maximum.Value = GL.porog_max;
                numeric_minimum.Value = GL.porog_min;
                
                checkBox_inv.Checked = GL.inversion_data;
                checkBox_rele.Checked = GL.inversion_rele;
                /* для задержек                   
                    0 мс
                    117 мс
                    468 мс
                    1054 мс
                    ~1400 мс
                    1992 мс
                  */             
                int zapis;
                while (true)
                {
                    if (GL.timeout_alarm == 0)  { zapis = 0; break; }
                    if (GL.timeout_alarm == 1)  { zapis = 1; break; }
                    if (GL.timeout_alarm == 4)  { zapis = 2; break; }
                    if (GL.timeout_alarm == 9)  { zapis = 3; break; }
                    if (GL.timeout_alarm == 12) { zapis = 4; break; }
                    if (GL.timeout_alarm == 17) { zapis = 5; break; }
                    zapis = 4; break;
                }
                domainUpDown2.Text = domainUpDown2.Items[zapis].ToString();

                r1_top.Value = ((decimal)GL.avariya[0].porog_max) / 2;
                r2_top.Value = ((decimal)GL.avariya[1].porog_max) / 2;
                r1_down.Value = ((decimal)GL.avariya[0].porog_min) / 2;
                r2_down.Value = ((decimal)GL.avariya[1].porog_min) / 2;
               
                numericUpDown1.Value = (decimal)GL.data[0].smeshenie;
                numericUpDown2.Value = (decimal)GL.data[1].smeshenie;
                numericUpDown3.Value = (decimal)GL.data[2].smeshenie;
                numericUpDown4.Value = (decimal)GL.data[3].smeshenie;
                numericUpDown5.Value = (decimal)GL.data[4].smeshenie;
                numericUpDown6.Value = (decimal)GL.data[5].smeshenie;
                numericUpDown7.Value = (decimal)GL.data[6].smeshenie;
                numericUpDown8.Value = (decimal)GL.data[7].smeshenie;
                radioButton1.Checked = GL.otnositelnoe_otobragenie;
                radioButton2.Checked = !GL.otnositelnoe_otobragenie;
                porogi_enable(GL.otnositelnoe_otobragenie);
            }
            catch(Exception Er){               
            }
        }
        int send_data()
        {
            const int pr = 10;
            try
            {
                byte[] bdata = new byte[70];
                byte[] ip = new byte[4];
                int flag;
                int i;
                bdata[0] = Convert.ToByte('G');//Идификатор транзакции
                bdata[1] = Convert.ToByte('B');//Идификатор транзакции
                bdata[2] = Convert.ToByte(0x33);//Идификатор протокола
                bdata[3] = Convert.ToByte(0x33);//Идификатор протокола
                bdata[4] = Convert.ToByte(0);//Длина
                bdata[5] = Convert.ToByte(0x3D);//Длина
                bdata[6] = Convert.ToByte('A');//Адрес
                bdata[7] = Convert.ToByte(0x10);//Функциональный код
                bdata[8] = Convert.ToByte(0);//Адрес первого регистра
                bdata[9] = Convert.ToByte(50);//Адрес первого регистра
                bdata[10] = Convert.ToByte(0);                 
                #region byte 1 - alarm
                bdata[1 + pr] = 0;
                if (GL.otnositelnoe_otobragenie == true) { bdata[1 + pr] = 1; } else { bdata[1 + pr] = 0; }
                if (GL.inversion_data == true) { bdata[1 + pr] += 0x02; }
                if (GL.inversion_rele == true) { bdata[1 + pr] += 0x04; }
                if (GL.avariya[0].avariya1_predupregdenie0 == 1) { bdata[1 + pr] += 0x10; }//avariya1_predupregdenie0 == 1, когда выбран тип "Авария"
                if (GL.avariya[1].avariya1_predupregdenie0 == 1) { bdata[1 + pr] += 0x20; }
                if (GL.avariya[2].avariya1_predupregdenie0 == 1) { bdata[1 + pr] += 0x40; }
                if (GL.avariya[3].avariya1_predupregdenie0 == 1) { bdata[1 + pr] += 0x80; }
                #endregion
                #region byte 2 - timeout_alarm
                UInt16 time = (UInt16)(GL.timeout_alarm);
                bdata[3 + pr] = Convert.ToByte(time); //time = Convert.ToUInt16(time >> 8);
                bdata[2 + pr] = 0;
                #endregion
                #region Ethernet setting
                // IP device
                flag = string_to_ip(text_ip_new.Text, ref ip, 1);
                if (flag != 0) { return 1; }
                for (i = 0; i < 4; i++) { bdata[i + 4 + pr] = ip[i]; }
                // maska
                flag = string_to_ip(text_maska_new.Text, ref ip, 2);
                if (flag != 0) { return 1; }
                for (i = 0; i < 4; i++) { bdata[i + 8 + pr] = ip[i]; }
                // IP1
                //flag = string_to_ip(text_ip1.Text, ref ip, 0);
                //if (flag != 0) { return 1; }
                //for (i = 0; i < 4; i++) { bdata[i + 12 + pr] = ip[i]; }
                //int sp = Convert.ToInt32(text_sport_new.Text);
                int dp = Convert.ToInt32(text_dport_new.Text);
                //bdata[17 + pr] = (byte)(sp & 0x00FF);
                //bdata[16 + pr] = (byte)((sp >> 8) & 0x00FF);
                bdata[19 + pr] = (byte)(dp & 0x00FF);
                bdata[18 + pr] = (byte)((dp >> 8) & 0x00FF);

                #endregion
                #region smehenie
                int smechenie = 20;
                for (i = 0; i < 8; i++)
                {
                    double dd = GL.data[i].smeshenie * 2.0;
                    int idd = Convert.ToInt16(dd);
                    bdata[smechenie + pr] = Convert.ToByte((idd >> 8) & 0x00FF); smechenie++;
                    bdata[smechenie + pr] = Convert.ToByte(idd & 0x00FF); smechenie++;
                }
                #endregion
                #region display
                int ipr = GL.porog_max;
                bdata[37 + pr] = Convert.ToByte(ipr & 0x00FF); ipr = ipr >> 8;
                bdata[36 + pr] = Convert.ToByte(ipr & 0x00FF);

                ipr = GL.porog_min;
                bdata[39 + pr] = Convert.ToByte(ipr & 0x00FF); ipr = ipr >> 8;
                bdata[38 + pr] = Convert.ToByte(ipr & 0x00FF);
                #endregion

                #region alarm-porogi

                bdata[40 + pr] = (byte)(((GL.avariya[0].kolvo_avariynih_datchikov << 4) & 0xF0) | (GL.avariya[1].kolvo_avariynih_datchikov & 0x0F));
                bdata[41 + pr] = (byte)(((GL.avariya[2].kolvo_avariynih_datchikov << 4) & 0xF0) | (GL.avariya[3].kolvo_avariynih_datchikov & 0x0F));

                smechenie = 42;
                for (i = 0; i < 4; i++)
                {
                    int idd = GL.avariya[i].porog_max;
                    bdata[smechenie + pr] = Convert.ToByte((idd >> 8) & 0x00FF); smechenie++;
                    bdata[smechenie + pr] = Convert.ToByte(idd & 0x00FF); smechenie++;
                    idd = GL.avariya[i].porog_min;
                    bdata[smechenie + pr] = Convert.ToByte((idd >> 8) & 0x00FF); smechenie++;
                    bdata[smechenie + pr] = Convert.ToByte(idd & 0x00FF); smechenie++;
                }
                #endregion
                byte[] crc = new byte[58 + pr];
                for (i = 0; i < 58 + pr; i++)
                {
                    crc[i] = bdata[i];
                }
                crc32 crc_f = new crc32();
                GL.version_proshivki = crc_f.Crc16(crc, 58 + pr);
                ipr = GL.version_proshivki;
                bdata[59 + pr] = Convert.ToByte(ipr & 0x00FF); ipr = ipr >> 8;
                bdata[58 + pr] = Convert.ToByte(ipr & 0x00FF);
                if (TCP.tcp_start(GL.IP, GL.DPORT) < 0) { return 2; }
                TCP.tcp_send(bdata, 67, text_ip_old.Text, GL.DPORT);
                for (int ff = 0; ff < 5; ff++)
                {
                    byte[] rez = TCP.recive();//text_ip_old.Text, 60, GL.DPORT
                    if (rez == null) { continue; }
                    if (rez[1] == Convert.ToByte('O') && rez[2] == Convert.ToByte('K') && rez[3] == Convert.ToByte('!'))
                    {
                        MessageBox.Show("Параметры успешно установлены!");
                        TCP.tcp_stop(); return 0;
                    }
                }
                TCP.tcp_stop();
                return 2;
            }
            catch (FormatException ee) { return 1; }
            catch { return 2; }
        }
        int string_to_ip(string str_ip, ref byte[] ip, int nr_ip)
        {
            if (str_ip==null) { ip[0] = 0; ip[1] = 0; ip[2] = 0; ip[3] = 0; return 0; }// слишком короткая строка для адреса
            if (str_ip.Length < 7) { ip[0] = 0; ip[1] = 0; ip[2] = 0; ip[3] = 0; return 0; }// слишком короткая строка для адреса
            int p1 = str_ip.IndexOf('.');
            int p2 = str_ip.IndexOf('.', p1 + 1);
            int p3 = str_ip.IndexOf('.', p2 + 1);
            if (p2 == 0 || p3 == 0 || p1 == 0) { ip[0] = 0; ip[1] = 0; ip[2] = 0; ip[3] = 0; return 0; }// нет ip адреса

            try
            {
                int[] iip = new int[4];
                iip[0] = Convert.ToInt16(str_ip.Substring(0, p1));
                iip[1] = Convert.ToInt16(str_ip.Substring(p1 + 1, p2 - p1 - 1));
                iip[2] = Convert.ToInt16(str_ip.Substring(p2 + 1, p3 - p2 - 1));
                iip[3] = Convert.ToInt16(str_ip.Substring(p3 + 1, str_ip.Length - 1 - p3));

                for (int i = 0; i < 4; i++) { if (iip[i] > 255) { throw new FormatException(); } }

                for (int i = 0; i < 4; i++) { ip[i] = Convert.ToByte(iip[i]); }

                return 0;
            }
            catch
            {
                string str = "Неверно введен";
                if (nr_ip == 0) { str = str + " IP пользователя 1"; }
                if (nr_ip == 1) { str = str + " IP-адрес"; }
                if (nr_ip == 2) { str = str + "а маска подсети"; }
                if (nr_ip == 3) { str = str + " IP пользователя 2"; }
                MessageBox.Show(str);
                return 1;
            }
        }  
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            porogi_enable(!radioButton2.Checked);
        }
        void porogi_enable(bool enable)
        {
            numericUpDown1.Enabled = enable;
            numericUpDown2.Enabled = enable;
            numericUpDown3.Enabled = enable;
            numericUpDown4.Enabled = enable;
            numericUpDown5.Enabled = enable;
            numericUpDown6.Enabled = enable;
            numericUpDown7.Enabled = enable;
            numericUpDown8.Enabled = enable;
        }
        private void r1_alarm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (r1_alarm.SelectedIndex == 2) { n_kolvo_datchikov.Value = 1; }
        }
        private void r2_alarm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (r2_alarm.SelectedIndex == 2) { n_kolvo_datchikov2.Value = 1; }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // parametry
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "parametry";
            this.ResumeLayout(false);

        }                    
    }
}
