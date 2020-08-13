using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Lolipop;
using Lolipop.Engine;
using LolipopTestSimple.Model;

namespace LolipopTestSimple
{
    public partial class Form1 : Form
    {
        TestModel t1 = new TestModel
        {
            Name = "卢凌峰",
            Age = 20
        };
        TestModel t2 = new TestModel
        {
            Name = "晓芳",
            Age = 18
        };
        TestModel t3 = new TestModel
        {
            Name = "建阳",
            Age = 22
        };

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 插入操作
            t1.Save();
            t2.Save();
            t3.Save();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            t1.Name = "陈思涵";
            t1.Age = 16;
            t1.Update();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            t1.Delete();
            t2.Delete();
            t3.Delete();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TestModel t = t1.Find(new Guid("39fe4ba4-b339-4486-88b8-1544b90f883e"));
            MessageBox.Show(t.Name);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<TestModel> list = new TestModel().ToList();

            foreach(TestModel test in list)
            {
                MessageBox.Show(test._id.ToString());
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> condition = new Dictionary<string, string>();
            condition.Add("Name", "李晓芳");
            condition.Add("Age", "22");
            TestModel t = t1.Find(condition, Lolipop.Entity.Enum.QueryCondition.Or);
            MessageBox.Show(t._id.ToString());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> condition = new Dictionary<string, string>();
            condition.Add("Name", "张晓芳");
            condition.Add("Age", "26");
            List<TestModel> list = new TestModel().ToList(condition);

            foreach (TestModel test in list)
            {
                MessageBox.Show(test._id.ToString());
            }
        }
    }
}
