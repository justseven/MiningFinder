using MiningFinder.Model;
using MiningFinder.Untils;
using SpiderService.Until;
using System.Data;
using static SpiderService.Until.List_DataTable_Helper;

namespace MiningFinder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 导入服务器信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (DialogResult.OK == ofd.ShowDialog())
            {
                BindingListBoxData(ofd.FileName);

            }
        }

        private void BindingListBoxData(string path)
        {
            DataTable dt = NPOIHelper.RenderDataTableFromExcel(path);
            var list = DataTableToListModel<Hosts>.ConvertToModel(dt);
            var checkSource = new List<ShowItem>();
            foreach (var item in list)
            {
                ShowItem showItem = new ShowItem();
                showItem.IP = item.IP;
                showItem.Description = string.IsNullOrEmpty(item.Description) ? item.IP : item.Description;
                showItem.HostsItem = item;
                checkSource.Add(showItem);
            }

            this.checkedListBox1.DataSource = checkSource;
            this.checkedListBox1.ValueMember = "HostsItem";
            this.checkedListBox1.DisplayMember = "Description";
        }

        private void chbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                this.checkedListBox1.SetItemCheckState(i, this.chbSelectAll.CheckState);
            }

        }  

        private void btnCheck_Click(object sender, EventArgs e)
        {
            var checkedItems = this.checkedListBox1.CheckedItems;
            this.progressBar1.Maximum = checkedItems.Count;
            this.progressBar1.Step = 1;
            foreach (ShowItem item in checkedItems)
            {
                ThreadPool.QueueUserWorkItem(delegate
                {
                    Hosts host = item.HostsItem;
                    SSHClass.RunSSHCommands(host.IP, host.UserName, host.Password, "ps -aux");
                    this.progressBar1.Invoke(new Action(() =>
                    {
                        this.progressBar1.Value++;
                    }));
                });
            }
        }

        private void ShowCmdResult(string log)
        {
            string[] array = log.Split('\n');
            
            this.listBox1.Invoke(new Action(() =>
            {
                this.listBox1.Items.AddRange(array);
            }));
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SSHClass.getSSHLog += ShowCmdResult;
            BindingListBoxData(Application.StartupPath + "\\服务器.xls");
        }
    }
}
