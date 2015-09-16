using System;
using Fractions;

namespace NoteMapLib
{
	public enum MetaEventType
	{
		Tempo,
		TimeSignature,
		SectionName,
	}

	public class MetaEvent : IComparable<MetaEvent>, IEvent
	{
		#region Fields
		public int Posision { get; set; }
		public int Length { get; set; }
		public MetaEventType Type { get; private set; }
		
		public object Data
		{
			get
			{
				return data;
			}
			private set
			{
				switch (Type)
				{
					case MetaEventType.Tempo:
						if (!(value is int))
							throw new ArgumentException("Expected int, but was " + value.GetType());
						break;
					case MetaEventType.TimeSignature:
						if (!(value is Fraction))
							throw new ArgumentException("Expected Fraction, but was " + value.GetType());
						break;
					case MetaEventType.SectionName:
						if (!(value is string))
							throw new ArgumentException("Expected string, but was " + value.GetType());
						break;
					default:
						break;
				}

				data = value;
			}
		}
		object data;
		#endregion

		#region Constructors
		public MetaEvent(int pos, MetaEventType type, object data, int length = 0)
		{
			Posision = pos;
			Type = type;
			Data = data;
			Length = length;
		}
		#endregion

		#region Public Methods

		public int CompareTo(MetaEvent other)
		{
			return Posision - other.Posision;
		}
		#endregion

		#region Private Methods

		#endregion
	}
}