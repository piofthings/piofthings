using System;
using PiOfThings.GpioCore;
using PiOfThings.GpioUtils;

namespace Piofthings.Test
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			try
			{
				GpioManager gpioManager = new GpioManager();
				gpioManager.SelectPin(GpioId.GPIO17);
				gpioManager.WriteToPin(GpioPinState.Low);

				GpioPinState state = gpioManager.ReadFromPin(gpioManager.CurrentPin);
				Console.WriteLine("Current Pin 17: " + state);

				gpioManager.SelectPin(GpioId.GPIO22);
				gpioManager.WriteToPin(GpioPinState.Low);

				GpioPinState state22 = gpioManager.ReadFromPin(gpioManager.CurrentPin);
				Console.WriteLine("Current Pin 22: " + state22);

				Console.ReadLine();

				GpioDeviceState data = new GpioDeviceState ();
				for (int i = 1; i <= 40; i++) 
				{
					GpioId currentPinId = GpioPinMapping.GetGpioId (i);
					if (currentPinId != GpioId.GPIOUnknown) 
					{
						//_manager.SelectPin (GpioPinMapping.GetGpioId (i));
						GpioPinState pinstate = gpioManager.ReadFromPin (currentPinId);
						data.GpioPinStates.Add (currentPinId, pinstate);
						Console.WriteLine(string.Format("\n\nRead from Pin {0}: {1}\n\n", currentPinId.ToString("D"), pinstate.ToString()));
						//_manager.ReleasePin (currentPinId);
					}
				}
				data.TimeStamp = DateTime.UtcNow.Ticks;

				Console.WriteLine("Press enter to close!");
				Console.ReadLine();
				gpioManager.ReleaseAll();

				gpioManager.SelectPin(GpioId.GPIO17);
				gpioManager.WriteToPin(GpioPinState.High);
				GpioPinState pinstat = gpioManager.ReadFromPin (GpioId.GPIO17);
				Console.WriteLine(string.Format("\n\nRead from Pin {0}: {1}\n\n", GpioId.GPIO17.ToString("D"), pinstat.ToString()));
				gpioManager.SelectPin(GpioId.GPIO22);
				gpioManager.WriteToPin(GpioPinState.High);
				pinstat = gpioManager.ReadFromPin (GpioId.GPIO22);
				Console.WriteLine(string.Format("\n\nRead from Pin {0}: {1}\n\n", GpioId.GPIO22.ToString("D"), pinstat.ToString()));
				gpioManager.ReleaseAll();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}
	}
}
