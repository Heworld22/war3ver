using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace War3Ver
{
    public partial class Form1 : Form
    {
        public string iniFilePath = null;

        public string WAR = "war3";
      
        public Form1()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        //自动创建按钮
        public void CreateVerButton()
        {
            List<string> keys = new List<string>();

            if (File.Exists(this.iniFilePath))
            {
                keys = IniHelper.GetKeys(WAR, this.iniFilePath);
            }

            int temp = 0;

            for (int i = 0; i < keys.Count; i++)
            {
                int n = i % 4;
                if (n == 0 && i != 0)
                {
                    temp++;    //每4个按钮换一行
                }
                int x = n * 120, y = 70 * temp;

                //创建按钮
                Button btn = new Button();

                // 加上 18 像素左边距，让 4 列按钮在 500 宽的框内居中对齐
                btn.Location = new System.Drawing.Point(x + 18, y + 30);

                btn.Name = keys[i].ToString();

                btn.Size = new System.Drawing.Size(102, 48);

                btn.TabIndex = 18 + i;

                btn.Text = keys[i].ToString();

                btn.UseVisualStyleBackColor = true;

                btn.Click += new EventHandler(btn_Click);//button的单击事件

                this.groupBox1.Controls.Add(btn);
            }

            this.label1.Text = "war3....加载配置成功！本次共加载" + keys.Count + "版本!";
        }

        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string nameid = btn.Name;

            FileZipOpr zip = new FileZipOpr();

            var dir = System.AppDomain.CurrentDomain.BaseDirectory;

            if (File.Exists(this.iniFilePath))
            {
                var value = IniHelper.GetValue(WAR, nameid, this.iniFilePath);
                
                var p = Path.Combine(dir, "ver", value);
                try
                {
                    zip.UnZipFile(p, dir, true);
                    this.label1.Text = "版本成功更换为：" + nameid + "，请进游戏体验！";
                    MessageBox.Show("版本成功更换为：" + nameid);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("文件加载失败，请确认是否存在此文件：" + this.iniFilePath);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string directory = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "ver");
            this.iniFilePath = Path.Combine(directory, "war3version.ini");
            CreateVerButton();
        }

        public void ReadIniKeys()
        {
            if (File.Exists(this.iniFilePath))
            {
                MessageBox.Show("读取文档成功");
            }
            else
            {
                MessageBox.Show("文件加载失败，请确认是否存在此文件：" + this.iniFilePath);
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            var dir = System.AppDomain.CurrentDomain.BaseDirectory;
            var path = Path.Combine(dir, "War3.exe");

            try
            {
                System.Diagnostics.Process.Start(path);
            }
            catch (Exception)
            {
                MessageBox.Show("未找到WAR3.exe");
            }
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.Owner = this;
            about.Show();
        }
    }
}
