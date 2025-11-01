using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MoodPicker
{
    public partial class Mode2_AddList: UserControl
    {
        public Mode2_AddList()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Mode2_AddList_Load(object sender, EventArgs e)
        {
            string filePath = "list.txt";
            LoadTreeViewFromTextFile(filePath);

            // AfterSelect 事件綁定 (+=)
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void LoadTreeViewFromTextFile(string filePath)
        {
            treeView1.Nodes.Clear();

            try
            {
                // 一次性讀取所有行
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string trimmedLine = line.Trim();

                    // 忽略空白行
                    if (string.IsNullOrWhiteSpace(trimmedLine))
                    {
                        continue;
                    }

                    char[] delimiters = new char[] { ' ' };

                    string[] parts = trimmedLine.Split(
                        delimiters,
                        StringSplitOptions.RemoveEmptyEntries // 忽略因多個連續空白產生的空字串
                    );

                    // 確保至少有一個項目 (種類)
                    if (parts.Length < 1)
                    {
                        continue;
                    }

                    // 第0項是種類
                    string parentText = parts[0];
                    TreeNode parentNode = new TreeNode(parentText);
                    treeView1.Nodes.Add(parentNode);

                    // 處理剩餘的項目 (選項1, 選項2, ...)
                    for (int i = 1; i < parts.Length; i++)
                    {
                        string childText = parts[i];
                        parentNode.Nodes.Add(new TreeNode(childText));
                    }
                }

                // 展開所有節點
                treeView1.ExpandAll();
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show($"錯誤：找不到檔案 {filePath}", "檔案錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"讀取檔案時發生錯誤: {ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
