using NoteMapLib.Generators;
using NoteMapLib.Model;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIWPF.ViewModels
{
    class ViewAViewModel : BindableBase
    {
        private NoteMap _MyProperty;
        public ObservableCollection<Track> MyProperty
        {
            get { return new ObservableCollection<Track>(_MyProperty); }
        }

        public ViewAViewModel()
        {
            _MyProperty = new FromMidiNoteMapGenerator().Generate(@"D:\Hejsil\Downloads\FZC_Mute_City_Arr-KM.mid");
        }
    }
}
