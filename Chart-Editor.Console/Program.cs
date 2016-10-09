using System;
using NoteMapLib;

namespace Chart_Editor.ConsoleApp
{
    public class Program
    {
        /*
         * Plans for how to use Chart-Editor in console
         * chart-editor <path to file or folder> [options]
         * Options:
         *  -o --o <path to file or folder>                         The path to the file of folder where the program should save results   
         * 
         *  -it --input-type <midi-ps/midi-song/chart-gh/notemap>   What type of the file/files the program will be taking as input
         *                                                              midi-ps: Phase Shift midi-files
         *                                                              midi-song: Standart midi-files
         *                                                              chart-gh: Guitar Hero 3 chart-files
         *                                                              notemap: Chart-Editors own file format
         *  
         *  -ot --output-type <midi-ps/chart-gh/notemap>            What type of file/files the program the program should output
         */

        public static void Main(string[] args)
        {
            NoteMap.GenerateFromMidi(@"D:\Users\Jimmi\Downloads\33557_Dragonforce--Through-Fire-and-Flames.mid")
                .WriteGuitarHeroChart(@"D:\Users\Jimmi\Downloads\test.chart");
        }
    }
}