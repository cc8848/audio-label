﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Runtime.InteropServices;
using System.IO;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        //static List<string> list = new List<string>();//定义list变量，存放获取到的路径
        List<RadioButton> labelBoxs = new List<RadioButton>();
        OpenFileDialog openFile = new OpenFileDialog();
        Stack<Double> stopPoint = new Stack<Double>();
        Double myDtime;
        int linesNum = -1;
        //private int cur = 0;

        public Form1()
        {
            InitializeComponent();

            //添加8个radioButton
            labelBoxs.Add(this.radioButton1);
            labelBoxs.Add(this.radioButton2);
            labelBoxs.Add(this.radioButton3);
            labelBoxs.Add(this.radioButton4);
            labelBoxs.Add(this.radioButton5);
            labelBoxs.Add(this.radioButton6);
            labelBoxs.Add(this.radioButton7);
            labelBoxs.Add(this.radioButton8);

            //DirectoryInfo dir = new DirectoryInfo("G:/后摄像头可爱的西沙群岛  下乡明德小学赵丹羽公开课");
            //FileInfo[] fil = dir.GetFiles();
            //foreach (FileInfo f in fil)
            //{
            //    list.Add(f.FullName);//添加文件的路径到列表
            //}   
        }
        
        private void button6_Click(object sender, EventArgs e)
        {
           
            //打开播放文件
            openFile.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //播放
            axWindowsMediaPlayer1.URL = openFile.FileName;
            String fullName = openFile.FileName;
            stopPoint.Push(0.0000000);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //快进
            if (axWindowsMediaPlayer1.Ctlcontrols.currentPosition < axWindowsMediaPlayer1.currentMedia.duration - 5)
            {
                axWindowsMediaPlayer1.Ctlcontrols.currentPosition += 5;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //快退
            if (axWindowsMediaPlayer1.Ctlcontrols.currentPosition > 5 && axWindowsMediaPlayer1.Ctlcontrols.currentPosition > stopPoint.Peek() + 5)
            {
                axWindowsMediaPlayer1.Ctlcontrols.currentPosition -= 5;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //1.设断点
            this.axWindowsMediaPlayer1.Ctlcontrols.pause();            
            myDtime = this.axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
            Double beforeStop = stopPoint.Peek();
            linesNum++;
            textBox2.AppendText((linesNum+1) +" "+ beforeStop + " " + myDtime + " ");
            stopPoint.Push(myDtime);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //2.标记
            //String filename = list[this.cur];
            //filename = filename.Substring(0, filename.Length - 4);
            String myLabel = "";

            foreach (RadioButton cb in this.labelBoxs)
            {
                if (cb.Checked)
                {
                    myLabel = myLabel + cb.Text + "-";
                }
            }
            myLabel = myLabel.Substring(0, myLabel.Length - 1);

            //clsPlaySound.ChangeFileName(list[this.cur], filename + "-" + myLabel + ".wav");
            textBox2.AppendText("-" + myLabel + " \r\n");
            //this.cur++;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //3.从断点播放
            this.axWindowsMediaPlayer1.Ctlcontrols.currentPosition = stopPoint.Peek();
            this.axWindowsMediaPlayer1.Ctlcontrols.play();
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //撤回一行
            stopPoint.Pop();
            int start = textBox2.GetFirstCharIndexFromLine(linesNum);//最后一行第一个字符的索引
            int end = textBox2.Text.Length;//最后一行最后一个字符的索引
            textBox2.Select(start, end);//选中一行
            textBox2.SelectedText = "";//设置一行的内容为空
            linesNum--;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //撤回标记 
            int start = textBox2.Text.Length - 9;
            int end = textBox2.Text.Length;
            textBox2.Select(start, end);//选中
            textBox2.SelectedText = "";//设置的内容为空

        }

        private void button7_Click(object sender, EventArgs e)
        {
            //导出txt文件
            // "保存为"对话框
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "文本文件|*.txt";
            // 显示对话框
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // 文件名
                string fileName = dialog.FileName;
                // 创建文件，准备写入
                FileStream fs = File.Open(fileName,
                        FileMode.Create,
                        FileAccess.Write);
                StreamWriter wr = new StreamWriter(fs);

                // 逐行将textBox1的内容写入到文件中
                foreach (string line in textBox2.Lines)
                {
                    wr.WriteLine(line);
                }

                // 关闭文件
                wr.Flush();
                wr.Close();
                fs.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }


    }

    //private void button1_Click(object sender, EventArgs e)
    //{
    //    //连续播放时
    //    if (this.cur < list.Count())
    //    {
    //        // play list[cur]
    //        String fname = list[cur];
    //        fname = fname.Substring(fname.Length - 10);
    //        textBox1.Text = fname;
    //        clsPlaySound.PlaySoundFile(list[cur], true);
    //    }
    //}

    //public class clsPlaySound
    //{
    //    protected const int SND_SYNC = 0x0;
    //    protected const int SND_ASYNC = 0x1;
    //    protected const int SND_NODEFAULT = 0x2;
    //    protected const int SND_MEMORY = 0x4;
    //    protected const int SND_LOOP = 0x8;
    //    protected const int SND_NOSTOP = 0x10;
    //    protected const int SND_NOWAIT = 0x2000;
    //    protected const int SND_ALIAS = 0x10000;
    //    protected const int SND_ALIAS_ID = 0x110000;
    //    protected const int SND_FILENAME = 0x20000;
    //    protected const int SND_RESOURCE = 0x40004;
    //    protected const int SND_PURGE = 0x40;
    //    protected const int SND_APPLICATION = 0x80;

    //    [DllImport("Winmm.dll", CharSet = CharSet.Auto)]
    //    protected extern static bool PlaySound(string strFile, IntPtr hMod, int flag);

    //    //播放声音函数
    //    //strSoundFile   --- 声音文件
    //    //bSynch         --- 是否同步，如果为True，则播放声音完毕再执行后面的操作，为False，则播放声音的同时继续执行后面的操作
    //    public static bool PlaySoundFile(string strSoundFile, bool bSynch)
    //    {
    //        if (!System.IO.File.Exists(strSoundFile))
    //            return false;
    //        int Flags;
    //        if (bSynch)
    //            Flags = SND_FILENAME | SND_SYNC;
    //        else
    //            Flags = SND_FILENAME | SND_ASYNC;

    //        return PlaySound(strSoundFile, IntPtr.Zero, Flags);
    //    }

    //    public static bool ChangeFileName(string srcFileName, string destFileName)
    //    {
    //        if (System.IO.File.Exists(srcFileName))
    //        {
    //            System.IO.File.Move(srcFileName, destFileName);
    //            return true;
    //        }
    //        else
    //            return false;
    //    }

    //}

}