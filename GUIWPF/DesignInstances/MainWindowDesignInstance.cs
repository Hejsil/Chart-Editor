using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteMapLib.Models;
using GUIWPF.ViewModels;
using NoteMapLib.Models.Events;
using NoteMapLib.Models.Enums;

namespace GUIWPF.DesignInstances
{
    class MainWindowDesignInstance : MainWindowViewModel
    {
        public MainWindowDesignInstance()
        {
            Map = new NoteMap()
            {
                new Track(TrackTypes.Guitar, TrackDifficulties.Expert)
                {
                    new TrackEvent()
                    {
                        Type = TrackEventTypes.Note,
                        Position = 0,
                        Value = 0
                    },
                    new TrackEvent()
                    {
                        Type = TrackEventTypes.Note,
                        Position = 128 + 64 * 2,
                        Value = 5
                    }
                },
                new Track(),
                new Track(),
                new Track(),
                new Track(),
                new Track(),
                new Track(),
                new Track(),
                new Track(),
                new Track(),
                new Track(),
                new Track(),
                new Track(),
            };
        }
    }
}
