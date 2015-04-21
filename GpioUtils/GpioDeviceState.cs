using System;
using System.Collections.Generic;

namespace PiOfThings.GpioUtils
{
	public class GpioDeviceState
	{
		public GpioDeviceState ()
		{
			GpioPinStates = new Dictionary<GpioId, GpioPinState> ();
		}

		public Dictionary<GpioId, GpioPinState> GpioPinStates 
		{
			get;
			set;
		}

		public long TimeStamp 
		{
			get;
			set;
		}
	}
}