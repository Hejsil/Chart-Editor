namespace GUI
{
	partial class BatchMidiWindow
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.button1 = new System.Windows.Forms.Button();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.addSingleMidiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addFolderAndSubFoldersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.singleFileOpen = new System.Windows.Forms.OpenFileDialog();
			this.folderFileDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.button2 = new System.Windows.Forms.Button();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// listBox1
			// 
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Location = new System.Drawing.Point(12, 12);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(138, 95);
			this.listBox1.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(12, 114);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(138, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "Add midis";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addSingleMidiToolStripMenuItem,
            this.addFolderToolStripMenuItem,
            this.addFolderAndSubFoldersToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(221, 70);
			// 
			// addSingleMidiToolStripMenuItem
			// 
			this.addSingleMidiToolStripMenuItem.Name = "addSingleMidiToolStripMenuItem";
			this.addSingleMidiToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
			this.addSingleMidiToolStripMenuItem.Text = "Add single midi...";
			this.addSingleMidiToolStripMenuItem.Click += new System.EventHandler(this.addSingleMidiToolStripMenuItem_Click);
			// 
			// addFolderToolStripMenuItem
			// 
			this.addFolderToolStripMenuItem.Name = "addFolderToolStripMenuItem";
			this.addFolderToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
			this.addFolderToolStripMenuItem.Text = "Add folder...";
			this.addFolderToolStripMenuItem.Click += new System.EventHandler(this.addFolderToolStripMenuItem_Click);
			// 
			// addFolderAndSubFoldersToolStripMenuItem
			// 
			this.addFolderAndSubFoldersToolStripMenuItem.Name = "addFolderAndSubFoldersToolStripMenuItem";
			this.addFolderAndSubFoldersToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
			this.addFolderAndSubFoldersToolStripMenuItem.Text = "Add folder and subfolders...";
			// 
			// singleFileOpen
			// 
			this.singleFileOpen.Filter = "Midi files|*.mid";
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(12, 143);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(138, 23);
			this.button2.TabIndex = 2;
			this.button2.Text = "Export as...";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// BatchMidiWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(162, 174);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.listBox1);
			this.Name = "BatchMidiWindow";
			this.Text = "BatchMidiWindow";
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem addSingleMidiToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addFolderToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addFolderAndSubFoldersToolStripMenuItem;
		private System.Windows.Forms.OpenFileDialog singleFileOpen;
		private System.Windows.Forms.FolderBrowserDialog folderFileDialog;
		private System.Windows.Forms.Button button2;
	}
}