using System;
using Fractions;
using NoteMapLib.Models.Enums;

namespace NoteMapLib.Models.Events
{
	public class MetaEvent : Event
	{
        #region Properties
        public MetaEventTypes Type { get; set; }
        public object Data { get; set; }
        #endregion
    }
}