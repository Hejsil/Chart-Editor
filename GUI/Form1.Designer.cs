namespace GUI
{
    partial class Form1
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.button1 = new System.Windows.Forms.Button();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			this.generateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.chartToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.label1 = new System.Windows.Forms.Label();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.folderBrowserDialog2 = new System.Windows.Forms.FolderBrowserDialog();
			this.genratingProcressBar = new System.Windows.Forms.ProgressBar();
			this.generatingmidi = new System.Windows.Forms.Label();
			this.midisfound = new System.Windows.Forms.Label();
			this.error = new System.Windows.Forms.Label();
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
            this.toolStripDropDownButton1});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(384, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripDropDownButton1
			// 
			this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateToolStripMenuItem});
			this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
			this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			this.toolStripDropDownButton1.Size = new System.Drawing.Size(38, 22);
			this.toolStripDropDownButton1.Text = "File";
			// 
			// generateToolStripMenuItem
			// 
			this.generateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chartToolStripMenuItem1});
			this.generateToolStripMenuItem.Name = "generateToolStripMenuItem";
			this.generateToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
			this.generateToolStripMenuItem.Text = "Generate";
			// 
			// chartToolStripMenuItem1
			// 
			this.chartToolStripMenuItem1.Name = "chartToolStripMenuItem1";
			this.chartToolStripMenuItem1.Size = new System.Drawing.Size(103, 22);
			this.chartToolStripMenuItem1.Text = "Chart";
			this.chartToolStripMenuItem1.Click += new System.EventHandler(this.chartToolStripMenuItem1_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 122);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(0, 13);
			this.label1.TabIndex = 3;
			// 
			// genratingProcressBar
			// 
			this.genratingProcressBar.Location = new System.Drawing.Point(12, 112);
			this.genratingProcressBar.Name = "genratingProcressBar";
			this.genratingProcressBar.Size = new System.Drawing.Size(360, 23);
			this.genratingProcressBar.TabIndex = 4;
			this.genratingProcressBar.Visible = false;
			// 
			// generatingmidi
			// 
			this.generatingmidi.AutoSize = true;
			this.generatingmidi.Location = new System.Drawing.Point(12, 93);
			this.generatingmidi.Name = "generatingmidi";
			this.generatingmidi.Size = new System.Drawing.Size(23, 13);
			this.generatingmidi.TabIndex = 5;
			this.generatingmidi.Text = "File";
			this.generatingmidi.Visible = false;
			// 
			// midisfound
			// 
			this.midisfound.AutoSize = true;
			this.midisfound.Location = new System.Drawing.Point(12, 77);
			this.midisfound.Name = "midisfound";
			this.midisfound.Size = new System.Drawing.Size(67, 13);
			this.midisfound.TabIndex = 6;
			this.midisfound.Text = "Midis found: ";
			this.midisfound.Visible = false;
			// 
			// error
			// 
			this.error.AutoSize = true;
			this.error.Location = new System.Drawing.Point(12, 142);
			this.error.Name = "error";
			this.error.Size = new System.Drawing.Size(40, 13);
			this.error.TabIndex = 7;
			this.error.Text = "Errors: ";
			this.error.Visible = false;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(384, 262);
			this.Controls.Add(this.error);
			this.Controls.Add(this.midisfound);
			this.Controls.Add(this.generatingmidi);
			this.Controls.Add(this.genratingProcressBar);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.button1);
			this.Name = "Form1";
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
        private System.Windows.Forms.ToolStripMenuItem generateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chartToolStripMenuItem1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog2;
        private System.Windows.Forms.ProgressBar genratingProcressBar;
        private System.Windows.Forms.Label generatingmidi;
        private System.Windows.Forms.Label midisfound;
        private System.Windows.Forms.Label error;
    }
}

