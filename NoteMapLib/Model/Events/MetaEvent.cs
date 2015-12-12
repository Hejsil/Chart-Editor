using System;
using Fractions;
using NoteMapLib.Model.Enums;

namespace NoteMapLib.Model.Events
{
	public class MetaEvent : Event
	{
        #region Properties
        public MetaEventTypes Type { get; set; }
        public object Data { get; set; }
        #endregion
    }
}