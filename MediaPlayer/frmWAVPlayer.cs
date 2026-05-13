using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaPlayer
{
    public partial class frmWAVPlayer : Form
    {
        SoundPlayer player;
        public frmWAVPlayer()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            this.ofdWAVFile.Filter = "WAV Files (*.wav)|*.wav";
            if(this.ofdWAVFile.ShowDialog() == DialogResult.OK)
            {
                this.txtPath.Text = this.ofdWAVFile.FileName;
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists(txtPath.Text))
            {
                MessageBox.Show("找不到音效檔，請確認檔案路徑是否正確！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // 提早結束，不執行後續的播放邏輯
            }
            try
            {
                UpdateStatus("播放一次", Color.Green);

                player = new SoundPlayer();
                player.SoundLocation = txtPath.Text;
                player.Load();
                player.Play();
                //player.PlaySync();

                //MessageBox.Show("音效播放完成!\n" ,"提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex){
                MessageBox.Show("無法播放音效檔，請確認檔案路徑是否正確!","錯誤",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnLoop_Click(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists(txtPath.Text))
            {
                MessageBox.Show("找不到音效檔，請確認檔案路徑是否正確！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // 提早結束，不執行後續的播放邏輯
            }
            UpdateStatus("循環播放",Color.Pink);
            player = new SoundPlayer(txtPath.Text);
            player.PlayLooping();
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists(txtPath.Text))
            {
                MessageBox.Show("找不到音效檔，請確認檔案路徑是否正確！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // 提早結束，不執行後續的播放邏輯
            }
            UpdateStatus("暫停中", Color.Red);

            player.Stop();
        }
        private void btnEnd_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmWAVPlayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("確定要關閉應用程式嗎？", "關閉確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                e.Cancel = true; // 取消關閉
            }
        }
        private void UpdateStatus(string status, Color color)
        {
            lblStatus.Text = $"目前狀態: {status}";
            lblStatus.BackColor = color;
        }
    }
}
