using Process;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace FancyFormsApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.backgroundWorker1.WorkerReportsProgress = true;  //设置能报告进度更新
            this.backgroundWorker1.WorkerSupportsCancellation = true;  //设置支持异步取消
        }
        private static List<string> allFile = new List<string>();
        private static bool isOk = true;
      
        #region init
        private void initData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("序号");
            dt.Columns.Add("文件路径");
            for (int i = 0; i < allFile.Count; i++)
            {
                DataRow row = dt.NewRow();
                row[0] = i;
                row[1] = allFile[i];
                dt.Rows.Add(row);
            }
            this.dataGridView1.DataSource = dt;
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.Columns[0].Width = 40;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            initData();
            this.textBox2.Text = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
            //this.textBox2.Text = @"C:\Users\Administrator\Desktop";         
        }
        private string getNewPath(string fileName)
        {
            if (string.IsNullOrEmpty(this.textBox2.Text.Trim()))
            {
                this.textBox2.Text = @"D:\fancyOutput";
            }
            if (!Directory.Exists(this.textBox2.Text.Trim()))
            {
                Directory.CreateDirectory(this.textBox2.Text.Trim());
            }
            return Path.Combine(this.textBox2.Text.Trim(), fileName);
        }

        private void AddLog(String LogContext)
        {
            textBox1.AppendText(LogContext + "\r\n\r\n");
            textBox1.ScrollToCaret();
            //ListViewItem lvi = new ListViewItem();
            //lvi.Text = LogContext;
            //this.listView1.Items.Add(lvi);
        }

        void process_BackgroundWorkerCompleted(object sender, BackgroundWorkerEventArgs e)
        {
            if (e.BackGroundException == null)
            {
                //MessageBox.Show("执行完毕");
            }
            else
            {
                MessageBox.Show("异常:" + e.BackGroundException.Message);
            }
        }
        #endregion
        
        //增加
        private void button4_Click(object sender, EventArgs e)
        {           
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = true;
            dlg.DefaultExt = ".txt";
            //fileDialog.Filter = "图片文件|*.jpg;*.jpg;*.jpeg;*.gif;*.png|WORD文件|*.doc;*.docx|EXCEL文件|*.xls;*.xlsx|PDF文件|*.pdf";
            dlg.Filter = "所有文件|*.*";

            //判断用户是否正确的选择了文件
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in dlg.FileNames)
                {
                    allFile.Add(file);
                }

                initData();
            }
        }

        //删除
        private void button5_Click(object sender, EventArgs e)
        {          
            allFile.Clear();
            initData();
        }

        //word转pdf
        private void button1_Click(object sender, EventArgs e)
        {
            //进度条不带百分比
            ProcessOperator process = new ProcessOperator();            
            process.MessageInfo = "正在执行中。。。。。。";
            process.BackgroundWork = this.button1_ClickDone;
            process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(process_BackgroundWorkerCompleted);
            process.Start();
        }

        //excel转pdf
        private void button2_Click(object sender, EventArgs e)
        {
            //进度条带百分比
            PercentProcessOperator process = new PercentProcessOperator();
            process.BackgroundWork = this.button2_ClickDone;
            process.MessageInfo = "正在执行中";
            process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(process_BackgroundWorkerCompleted);
            process.Start();
        }
        //图片转pdf
        private void button3_Click(object sender, EventArgs e)
        {
            ProcessOperator process = new ProcessOperator();
            process.BackgroundWork = this.button3_ClickDone;
            process.MessageInfo = "正在执行中";
            process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(process_BackgroundWorkerCompleted);
            process.Start();
        }
        //图片转pdf
        private void button10_Click(object sender, EventArgs e)
        {
            ProcessOperator process = new ProcessOperator();
            process.BackgroundWork = this.button10_ClickDone;
            process.MessageInfo = "正在执行中";
            process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(process_BackgroundWorkerCompleted);
            process.Start();
        }
        //pdf 转word
        private void button6_Click(object sender, EventArgs e)
        {
            ProcessOperator process = new ProcessOperator();
            process.BackgroundWork = this.button6_ClickDone;
            process.MessageInfo = "正在执行中";
            process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(process_BackgroundWorkerCompleted);
            process.Start();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            ProcessOperator process = new ProcessOperator();
            process.BackgroundWork = this.button7_ClickDone;
            process.MessageInfo = "正在执行中";
            process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(process_BackgroundWorkerCompleted);
            process.Start();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            ProcessOperator process = new ProcessOperator();
            process.BackgroundWork = this.button8_ClickDone;
            process.MessageInfo = "正在执行中";
            process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(process_BackgroundWorkerCompleted);
            process.Start();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            ProcessOperator process = new ProcessOperator();
            process.BackgroundWork = this.button9_ClickDone;
            process.MessageInfo = "正在执行中";
            process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(process_BackgroundWorkerCompleted);
            process.Start();

        }
        #region
        private void showProcess(Action<int> percent)
        {
            for (int i = 0; i <= 100; i++)
            {
                Thread.Sleep(50);
                percent(i);
            }
        }
        private void button1_ClickDone()
        {
            //word转pdf
            if (!valide(new string[] { ".doc", ".docx" }))
            {
                return;
            }
            for (int i = 0; i < allFile.Count; i++)
            {
                string extension = Path.GetExtension(allFile[i]);
                string[] str = new string[] { ".doc", ".docx" };
                if (!((IList)str).Contains(extension))
                {
                    MessageBox.Show(
                        "仅支持word转换，请重新添加. \r\nerror：" + Path.GetFileName(allFile[i]),
                        "",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.None,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.ServiceNotification,
                        false
                        );
                    return;
                }
            }
            AddLog("------ 开始进行word转pdf....------ ");
            for (int i = 0; i < allFile.Count; i++)
            {
                AddLog("正在转换 " + Path.GetFileName(allFile[i]) + " .......");
                string newName =  Path.GetFileNameWithoutExtension(allFile[i]) + ".pdf";
                string newPath = AsposeUtils.WordToPdf(allFile[i], getNewPath(newName));
                AddLog(newPath);
            }
            AddLog("------ 结束进行word转pdf....------ ");
        }
        private void button2_ClickDone(Action<int> percent)
        {
            //excel转pdf
            if (!valide(new string[] { ".xls", ".xlsx" }))
            {
                return;
            }            
            AddLog("------ 开始进行excel转pdf....------ ");            
            for (int i = 0; i < allFile.Count; i++)
            {
                percent(i+1);
                AddLog("正在转换 " + Path.GetFileName(allFile[i]) + " .......");
                string newName =  Path.GetFileNameWithoutExtension(allFile[i]) + ".pdf";
                string newPath = AsposeUtils.ExcelToPdf(allFile[i], getNewPath(newName));
                AddLog(newPath);
            }
            AddLog("------ 结束进行excel转pdf....------ ");
            isOk = false;
        }
        private void button3_ClickDone()
        {//图片转pdf
            if (!valide(new string[] { ".jpg",".jpeg",".gif",".png" }))
            {
                return;
            }
            AddLog("------ 开始进行图片转pdf....------ ");
            for (int i = 0; i < allFile.Count; i++)
            {
                AddLog("正在转换 " + Path.GetFileName(allFile[i]) + " .......");
                string newName =  Path.GetFileNameWithoutExtension(allFile[i]) + ".pdf";
                string newPath = AsposeUtils.ImgToPdf(allFile[i], getNewPath(newName));
                AddLog(newPath);
            }
            AddLog("------ 结束进行图片转pdf....------ ");            
        }
        private void button10_ClickDone()
        {
            //图片合并成pdf
            if (!valide(new string[] { ".jpg", ".jpeg", ".gif", ".png" }))
            {
                return;
            }
            AddLog("------ 开始进行图片合并成pdf....------ ");
            AddLog("正在转换 " + Path.GetFileName(allFile[0]) + " .......");
            string newName =  Path.GetFileNameWithoutExtension(allFile[0]) + ".pdf";
            string newPath = AsposeUtils.ImgToPdf(allFile, getNewPath(newName));
            AddLog(newPath);
            AddLog("------ 结束进行图片合并成pdf....------ ");
        }
        private void button6_ClickDone()
        {
            if (!valide(new string[] { ".pdf" }))
            {
                return;
            }
            AddLog("------ 开始进行pdf转word....------ ");
            for (int i = 0; i < allFile.Count; i++)
            {
                AddLog("正在转换 " + Path.GetFileName(allFile[i]) + " .......");
                string newName = Path.GetFileNameWithoutExtension(allFile[i]) + ".docx";
                string newPath = AsposeUtils.PdfToWord(allFile[i], getNewPath(newName));
                AddLog(newPath);
            }
            AddLog("------ 结束进行pdf转word....------ ");
        }
        private void button7_ClickDone()
        {
            if (!valide(new string[] { ".pdf" }))
            {
                return;
            }
            AddLog("------ 开始进行pdf转图片....------ ");
            for (int i = 0; i < allFile.Count; i++)
            {
                AddLog("正在转换 " + Path.GetFileName(allFile[i]) + " .......");                
                string newPath = AsposeUtils.PdfToImg(allFile[i], getNewPath(""));
                AddLog(newPath);
            }
            AddLog("------ 结束进行pdf转图片....------ ");
        }
        private void button8_ClickDone()
        {
            if (!valide(new string[] { ".pdf" }))
            {
                return;
            }
            AddLog("------ 开始进行pdf合并....------ ");
            AddLog("正在转换  .......");
            string newName = Path.GetFileNameWithoutExtension(allFile[0]) + ".pdf";
            string newPath = AsposeUtils.PdfMergn(allFile, getNewPath(newName));            
            AddLog(newPath);
            AddLog("------ 结束进行pdf合并....------ ");
        }
        private void button9_ClickDone()
        {
            if (!valide(new string[] { ".pdf" }))
            {
                return;
            }
            AddLog("------ 开始进行添加水印....------ ");
            AddLog("正在转换  .......");            
            string newPath = AsposeUtils.PdfWatermark(allFile, getNewPath(""),this.txtWatermark.Text);
            AddLog(newPath);
            AddLog("------ 结束进行添加水印....------ ");
        }
        private bool valide(string[] str)
        {
            if (allFile.Count == 0)
            {
                MessageBox.Show(
                   "亲，请选择文件在操作！",
                    "",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.None,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.ServiceNotification,
                    false
                    );
                //MessageBox.Show("亲，请选择文件在操作！");
                return false;
            }
            for (int i = 0; i < allFile.Count; i++)
            {
                string extension = Path.GetExtension(allFile[i]);
                //string[] str = new string[] { ".doc", ".docx" };
                if (!((IList)str).Contains(extension))
                {
                    MessageBox.Show(
                   "仅支持" + string.Join(",", str) + "转换，请重新添加. \r\nerror：" + Path.GetFileName(allFile[i]),
                    "",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.None,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.ServiceNotification,
                    false
                    );
                    //MessageBox.Show("仅支持" + string.Join(",", str) + "转换，请重新添加. \r\nerror：" + Path.GetFileName(allFile[i]));
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region
        //不带百分比
        void Do()
        {
            for (int i = 0; i <= 100; i++)
            {
                Thread.Sleep(20);
            }
        }
        //带百分比
        void DoWithProcess(Action<int> percent)
        {
            for (int i = 0; i <= 100; i++)
            {
                Thread.Sleep(50);
                percent(i);
            }
        }











        #endregion

       
    }
}
