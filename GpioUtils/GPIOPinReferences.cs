using System;
using System.Collections.Generic;

namespace PiOfThings.GpioUtils
{
	public static class GPIOPinDirection
	{
		public static string Out = "out";
		public static string In = "in";
	}

	public enum GpioPinState
	{
		Low = 0,
		High = 1,
		Unknown = 3
	}

	public enum GpioId
	{
		GPIOUnknown = -1,
		GPIO02 = 2,
		GPIO03 = 3,
		GPIO04 = 4,
		GPIO07 = 7,
		GPIO08 = 8,
		GPIO09 = 9,
		GPIO10 = 10,
		GPIO11 = 11,
		GPIO14 = 14,
		GPIO15 = 15,
		GPIO17 = 17,
		GPIO18 = 18,
		GPIO22 = 22,
		GPIO23 = 23,
		GPIO24 = 24,
		GPIO25 = 25,
		GPIO27 = 27
	}

	public static class GpioPinMapping
	{
		private static Dictionary<GpioId, int> GPIOToPin = new Dictionary<GpioId, int>{
			{ GpioId.GPIO02, 3 },
			{ GpioId.GPIO03, 5 },
			{ GpioId.GPIO04, 4 },
			{ GpioId.GPIO07, 26 },
			{ GpioId.GPIO08, 24 },
			{ GpioId.GPIO09, 21 },
			{ GpioId.GPIO10, 19 },
			{ GpioId.GPIO11, 23 },
			{ GpioId.GPIO14, 8 },
			{ GpioId.GPIO15, 10 },
			{ GpioId.GPIO17, 11 },
			{ GpioId.GPIO18, 12 },
			{ GpioId.GPIO22, 15 },
			{ GpioId.GPIO23, 16 },
			{ GpioId.GPIO24, 18 },
			{ GpioId.GPIO25, 22 },
			{ GpioId.GPIO27, 13 }
		};

		private static readonly Dictionary<int, GpioId> PinToGPIO = new Dictionary<int, GpioId>{
			{ 1, GpioId.GPIOUnknown },
			{ 2, GpioId.GPIOUnknown },
			{ 3, GpioId.GPIO02 },
			{ 4, GpioId.GPIO04 },
			{ 5, GpioId.GPIO03 },
			{ 6, GpioId.GPIOUnknown },
			{ 7, GpioId.GPIOUnknown },
			{ 8, GpioId.GPIO14 },
			{ 9, GpioId.GPIOUnknown },
			{ 10, GpioId.GPIO15 },
			{ 11, GpioId.GPIO17 },
			{ 12, GpioId.GPIO18 },
			{ 13, GpioId.GPIO27 },
			{ 14, GpioId.GPIOUnknown },
			{ 15, GpioId.GPIO22 },
			{ 16, GpioId.GPIO23 },
			{ 17, GpioId.GPIOUnknown },
			{ 18, GpioId.GPIO24 },
			{ 19, GpioId.GPIO10 },
			{ 20, GpioId.GPIOUnknown },
			{ 21, GpioId.GPIO09 },
			{ 22, GpioId.GPIO25 },
			{ 23, GpioId.GPIO11 },
			{ 24, GpioId.GPIO08 },
			{ 25, GpioId.GPIOUnknown },
			{ 26, GpioId.GPIOUnknown },
			{ 27, GpioId.GPIOUnknown },
			{ 28, GpioId.GPIOUnknown },
			{ 29, GpioId.GPIOUnknown },
			{ 30, GpioId.GPIOUnknown },
			{ 31, GpioId.GPIOUnknown },
			{ 32, GpioId.GPIOUnknown },
			{ 33, GpioId.GPIOUnknown },
			{ 34, GpioId.GPIOUnknown },
			{ 35, GpioId.GPIOUnknown },
			{ 36, GpioId.GPIOUnknown },
			{ 37, GpioId.GPIOUnknown },
			{ 38, GpioId.GPIOUnknown },
			{ 39, GpioId.GPIOUnknown },
			{ 40, GpioId.GPIOUnknown },
		};

		public static int GetPinNumber (GpioId gpioNumber)
		{
			return GPIOToPin [gpioNumber];
		}

		public static GpioId GetGPIOId(int pin)
		{
			if (pin > 0 && pin <= 40) 
			{
				return PinToGPIO [pin];
			} 
			else 
			{
				throw new ArgumentOutOfRangeException ("pin", string.Format ("Invalid pin {0}. Please enter value between 1 and 40 (both inclusive).", pin)); 
			}
		}
	}
}