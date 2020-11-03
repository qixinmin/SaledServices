using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SaledServices.additionForm
{
    public partial class Xml2ExcelForm : Form
    {
        public Xml2ExcelForm()
        {
            InitializeComponent();
        }

        private void findFile_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();

            string fileName = openFileDialog.FileName.ToLower();
            if (fileName.EndsWith("xls") || fileName.EndsWith("xlsx") || fileName.EndsWith("xml"))
            {
                filePath.Text = fileName;
                importButton.Enabled = true;
            }
            else
            {
                MessageBox.Show("请输入正确的excel或xml文件!");
            }
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (filePath.Text.EndsWith("xml"))//xml 2 excel file
                {
                    List<xml2excel.xml2excelbody> list = xml2excel.getContentFromXml(filePath.Text);
                    xml2excel.generateExcelFromList(list);
                }
                else
                {
                    List<String> strList = xml2excel.getContentFromExcel(filePath.Text, "test");

                    string path = "D:\\导出文件汇总\\";
                    xml2excel.createxmlFromExcel(strList, path + "test.xml");
                }

                MessageBox.Show("转换成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }  
        }
    }
}
