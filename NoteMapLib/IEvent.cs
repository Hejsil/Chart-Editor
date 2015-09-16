using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteMapLib
{
	public interface IEvent
	{
		int Posision { get; set; }
		int Length { get; set; }
		object Data { get; }
	}
}
