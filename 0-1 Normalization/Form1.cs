using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0_1_Normalization
{
    
    public partial class Form1 : Form
    {
        public List<HeightData> heightData = new List<HeightData>();
        public Form1()
        {
            InitializeComponent();
        }
        protected override void OnActivated(EventArgs e)
        {
            //Using FakeData Plugin
            for (int i = 0; i < 50; i++)
            {
                heightData.Add(new HeightData
                {
                    Data =(double)FakeData.NumberData.GetNumber(110, 220)
                });
            }
            //randomly upload 50 data add heightData list
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double maxHeight = heightData.Max(t=>t.Data);
            double minHeight = heightData.Min(s => s.Data);
            foreach (var item in heightData.ToList())
            {
                
                HeightData newData = new HeightData();
                newData.Data = item.Data;
                newData.ZeroOneNormalizationData = double.Parse(string.Format("{0:0.000}", (((newData.Data) - minHeight) / (maxHeight - minHeight))));
                newData.ZeroFiveNormalizationData = newData.ZeroOneNormalizationData
                    * 5 - 0 + 0;

                heightData.Remove(item);//old data remove(missing 01 normalization data)
                heightData.Add(newData);//new data add heightData list
                ListViewItem listView = new ListViewItem();
                listView.Text = newData.Data.ToString();
                listView.SubItems.Add(newData.ZeroOneNormalizationData.ToString("0.000###"));
                listView.SubItems.Add(newData.ZeroFiveNormalizationData.ToString("0.000###"));
                listView1.Items.Add(listView);
            }
        }
    }
    public class HeightData
    {
        public double Data { get; set; }//Height data
        public double ZeroOneNormalizationData { get; set; }//0-1 normalization data
        public double ZeroFiveNormalizationData { get; set; }//0-5 normalization data
    }
}
