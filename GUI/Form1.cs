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
using ChartLib;
using Sanford.Multimedia.Midi;


namespace GUI
{
    public partial class Form1 : Form
    {
        Sequence sequence;
        Chart chart;
        List<Exception> errors;

        public Form1()
        {
            sequence = new Sequence();
            errors = new List<Exception>();
            InitializeComponent();
        }

        private void chartToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var openRes = folderBrowserDialog1.ShowDialog();
            var saveRes = folderBrowserDialog2.ShowDialog();

            if (openRes == DialogResult.OK && saveRes == DialogResult.OK)
            {
                var midis = GetMidiPaths(folderBrowserDialog1.SelectedPath);

                errors.Clear();

                genratingProcressBar.Value = 0;
                genratingProcressBar.Maximum = midis.Count * 2;
                midisfound.Text = string.Format("Midis found: {0}", midis.Count);
                midisfound.Visible = true;
                generatingmidi.Visible = true;
                genratingProcressBar.Visible = true;
                midisfound.Update();
                genratingProcressBar.Update();

                foreach (var path in midis)
                {
                    try
                    {
                        generatingmidi.Text = string.Format("Reading midi: {0}", path);
                        generatingmidi.Update();
                        sequence.Load(path);
                        genratingProcressBar.PerformStep();

                        generatingmidi.Text = string.Format("Generating chart: {0}", path);
                        generatingmidi.Update();
                        chart = new Chart(sequence, Path.GetFileNameWithoutExtension(path));
                        chart.WriteChart(folderBrowserDialog2.SelectedPath);
                        genratingProcressBar.PerformStep();
                    }
                        catch (Exception ex)
                    {
                        errors.Add(ex);
                        error.Visible = true;
                        error.Text = string.Format("Errors: {0}", errors.Count);
                        error.Update();
                    }
            }

                midisfound.Visible = false;
                generatingmidi.Visible = false;
                genratingProcressBar.Visible = false;
                error.Visible = false;
            }

        }

        private List<string> GetMidiPaths(string path)
        {
            var dirs = new List<string>(Directory.EnumerateDirectories(path));
            var midis = new List<string>(Directory.EnumerateFiles(path, "*.mid"));

            foreach (var dir in dirs)
            {
                midis.AddRange(GetMidiPaths(dir));
            }

            return midis;
        }
    }
}
