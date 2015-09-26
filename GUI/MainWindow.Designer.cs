namespace GUI
{
    partial class MainWindow
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
			this.button1 = new System.Windows.Forms.Button();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			this.exportAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.guitarHero3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.phaseShiftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.foFixToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
			this.toolStripDropDownButton3 = new System.Windows.Forms.ToolStripDropDownButton();
			this.toolStripDropDownButton4 = new System.Windows.Forms.ToolStripDropDownButton();
			this.toolStripDropDownButton5 = new System.Windows.Forms.ToolStripDropDownButton();
			this.toolStripDropDownButton6 = new System.Windows.Forms.ToolStripDropDownButton();
			this.generateFromToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.midiFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.label1 = new System.Windows.Forms.Label();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.folderBrowserDialog2 = new System.Windows.Forms.FolderBrowserDialog();
			this.midisfound = new System.Windows.Forms.Label();
			this.generateMidiFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.exportGuitarHero3FileDialog = new System.Windows.Forms.SaveFileDialog();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(0, 0);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 2;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripDropDownButton2,
            this.toolStripDropDownButton3,
            this.toolStripDropDownButton4,
            this.toolStripDropDownButton5,
            this.toolStripDropDownButton6});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(387, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripDropDownButton1
			// 
			this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportAsToolStripMenuItem});
			this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
			this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			this.toolStripDropDownButton1.Size = new System.Drawing.Size(38, 22);
			this.toolStripDropDownButton1.Text = "File";
			// 
			// exportAsToolStripMenuItem
			// 
			this.exportAsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.guitarHero3ToolStripMenuItem,
            this.phaseShiftToolStripMenuItem,
            this.foFixToolStripMenuItem});
			this.exportAsToolStripMenuItem.Name = "exportAsToolStripMenuItem";
			this.exportAsToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
			this.exportAsToolStripMenuItem.Text = "Export To";
			// 
			// guitarHero3ToolStripMenuItem
			// 
			this.guitarHero3ToolStripMenuItem.Name = "guitarHero3ToolStripMenuItem";
			this.guitarHero3ToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.guitarHero3ToolStripMenuItem.Text = "Guitar Hero 3";
			this.guitarHero3ToolStripMenuItem.Click += new System.EventHandler(this.guitarHero3ToolStripMenuItem_Click);
			// 
			// phaseShiftToolStripMenuItem
			// 
			this.phaseShiftToolStripMenuItem.Name = "phaseShiftToolStripMenuItem";
			this.phaseShiftToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.phaseShiftToolStripMenuItem.Text = "Phase Shift";
			// 
			// foFixToolStripMenuItem
			// 
			this.foFixToolStripMenuItem.Name = "foFixToolStripMenuItem";
			this.foFixToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.foFixToolStripMenuItem.Text = "FoFix";
			// 
			// toolStripDropDownButton2
			// 
			this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
			this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
			this.toolStripDropDownButton2.Size = new System.Drawing.Size(40, 22);
			this.toolStripDropDownButton2.Text = "Edit";
			// 
			// toolStripDropDownButton3
			// 
			this.toolStripDropDownButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripDropDownButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton3.Image")));
			this.toolStripDropDownButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButton3.Name = "toolStripDropDownButton3";
			this.toolStripDropDownButton3.Size = new System.Drawing.Size(51, 22);
			this.toolStripDropDownButton3.Text = "Select";
			// 
			// toolStripDropDownButton4
			// 
			this.toolStripDropDownButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripDropDownButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton4.Image")));
			this.toolStripDropDownButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButton4.Name = "toolStripDropDownButton4";
			this.toolStripDropDownButton4.Size = new System.Drawing.Size(45, 22);
			this.toolStripDropDownButton4.Text = "View";
			// 
			// toolStripDropDownButton5
			// 
			this.toolStripDropDownButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripDropDownButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton5.Image")));
			this.toolStripDropDownButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButton5.Name = "toolStripDropDownButton5";
			this.toolStripDropDownButton5.Size = new System.Drawing.Size(48, 22);
			this.toolStripDropDownButton5.Text = "Track";
			// 
			// toolStripDropDownButton6
			// 
			this.toolStripDropDownButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripDropDownButton6.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateFromToolStripMenuItem});
			this.toolStripDropDownButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton6.Image")));
			this.toolStripDropDownButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButton6.Name = "toolStripDropDownButton6";
			this.toolStripDropDownButton6.Size = new System.Drawing.Size(48, 22);
			this.toolStripDropDownButton6.Text = "Tools";
			// 
			// generateFromToolStripMenuItem
			// 
			this.generateFromToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.midiFileToolStripMenuItem});
			this.generateFromToolStripMenuItem.Name = "generateFromToolStripMenuItem";
			this.generateFromToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.generateFromToolStripMenuItem.Text = "Generate From";
			// 
			// midiFileToolStripMenuItem
			// 
			this.midiFileToolStripMenuItem.Name = "midiFileToolStripMenuItem";
			this.midiFileToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
			this.midiFileToolStripMenuItem.Text = "Midi file...";
			this.midiFileToolStripMenuItem.Click += new System.EventHandler(this.midiFileToolStripMenuItem_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 122);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(0, 13);
			this.label1.TabIndex = 3;
			// 
			// midisfound
			// 
			this.midisfound.AutoSize = true;
			this.midisfound.Location = new System.Drawing.Point(12, 26);
			this.midisfound.Name = "midisfound";
			this.midisfound.Size = new System.Drawing.Size(67, 13);
			this.midisfound.TabIndex = 6;
			this.midisfound.Text = "Midis found: ";
			this.midisfound.Visible = false;
			// 
			// generateMidiFileDialog
			// 
			this.generateMidiFileDialog.FileName = "openFileDialog1";
			this.generateMidiFileDialog.Filter = "Midi files|*.mid";
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(387, 262);
			this.Controls.Add(this.midisfound);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.button1);
			this.Name = "MainWindow";
			this.Text = "Form1";
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog2;
        private System.Windows.Forms.Label midisfound;
		private System.Windows.Forms.OpenFileDialog generateMidiFileDialog;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton3;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton4;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton5;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton6;
		private System.Windows.Forms.ToolStripMenuItem generateFromToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem midiFileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exportAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem guitarHero3ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem phaseShiftToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem foFixToolStripMenuItem;
		private System.Windows.Forms.SaveFileDialog exportGuitarHero3FileDialog;
	}
}

