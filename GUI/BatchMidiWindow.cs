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

namespace GUI
{
	public partial class BatchMidiWindow : Form
	{
		public BatchMidiWindow()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			contextMenuStrip1.Show(button1, 0, 0);
		}

		private void addSingleMidiToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (singleFileOpen.ShowDialog() == DialogResult.OK)
			{
				listBox1.Items.Add(Path.GetFileNameWithoutExtension(singleFileOpen.FileName));
            }
		}

		private void addFolderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (folderFileDialog.ShowDialog() == DialogResult.OK)
			{
				var midis = from file in Directory.GetFiles(folderFileDialog.SelectedPath)
							where Path.GetExtension(file) == ".mid"
							select file;
				
				foreach (var midi in midis)
				{
					listBox1.Items.Add(Path.GetFileNameWithoutExtension(midi));
				}
			}
		}
	}
}
