// A lightweight wrapper around the
// GPIO ports of a Raspberry Pi 2 and Raspberry Pi 1 B+
// It uses the Filesystem to read from and write to 
// the pins

// Inspired by the RaspberryGPIOManager  

using PiOfThings.GpioUtils;
using System;
using System.Collections.Generic;
using System.IO;

namespace PiOfThings.GpioCore
{
	public class GpioManager
	{
		private readonly string GPIO_ROOT_DIR;

		readonly Dictionary<GpioId, bool> _busyPins;

		public GpioId CurrentPin
		{
			get;
			private set;
		}

		public GpioManager (string rootDirectory = "/sys/class/gpio/")
		{
			GPIO_ROOT_DIR = rootDirectory;
			_busyPins = new Dictionary<GpioId, bool> ();
		}

		public void SelectPin (GpioId pin)
		{
			if (_busyPins.ContainsKey (pin))
			{
				if (!this._busyPins [pin])
				{
					if (ReservePin (pin))
					{
						this._busyPins [pin] = true;
						this.CurrentPin = pin;
					}
				}
				else
				{
					throw new ArgumentException ("Pin in Use", string.Format ("The GPIO pin {0} is already in use", pin));
				}
			}
			else
			{
				if (ReservePin (pin))
				{
					this._busyPins.Add (pin, true);
					this.CurrentPin = pin;
				}
			}
		}

		public bool WriteToPin (GpioPinState state)
		{
			try
			{
				if (CurrentPin != GpioId.GPIOUnknown)
				{
					File.WriteAllText (String.Format ("{0}gpio{1}/direction", GPIO_ROOT_DIR, CurrentPin.ToString ("D")), GPIOPinDirection.Out);
					File.WriteAllText (String.Format ("{0}gpio{1}/value", GPIO_ROOT_DIR, CurrentPin.ToString ("D")), state.ToString ("D"));
					return true;
				}
				else
				{
					Console.WriteLine ("Failed to WriteToPin: " + CurrentPin.ToString ("D") + " Check if Pin selection succeeded earlier.");
					return false;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine ("Failed to WriteToPin: " + CurrentPin.ToString ("D") + " " + ex.Message + "\n" + ex.StackTrace);
			}
			return false;
		}

		public GpioPinState ReadFromPin (GpioId pin)
		{
			GpioPinState currentState = GpioPinState.Unknown;
			try
			{
				string state = File.ReadAllText (String.Format ("{0}gpio{1}/value", GPIO_ROOT_DIR, pin.ToString ("D")));
				currentState = (state == "1" ? GpioPinState.High : GpioPinState.Low);			
			}
			catch (Exception ex)
			{
				Console.WriteLine ("Failed to ReadFromPin: " + pin.ToString ("D") + " " + ex.Message + "\n" + ex.StackTrace);
			}
			return currentState;
		}


		public bool ReleasePin (GpioId pin)
		{
			try
			{
				File.WriteAllText (GPIO_ROOT_DIR + "unexport", pin.ToString ("D"));
				CurrentPin = GpioId.GPIOUnknown;
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine ("Failed to ReleasePin: " + pin.ToString ("D") + " " + ex.Message + "\n" + ex.StackTrace);
			}
			return false;
		}

		public bool ReleasePin ()
		{
			return ReleasePin (CurrentPin);
		}

		public void ReleaseAll ()
		{
			foreach (var busyPin in _busyPins)
			{
				if (busyPin.Value)
				{
					ReleasePin (busyPin.Key);
				}
			}
			_busyPins.Clear ();
		}

		private bool ReservePin (GpioId pin)
		{
			try
			{
				File.WriteAllText (GPIO_ROOT_DIR + "export", pin.ToString ("D"));
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine ("Failed to ReservePin: " + pin.ToString ("D") + " " + ex.Message + "\n" + ex.StackTrace);
				Console.WriteLine ("Attempting to release pin");
                if (ReleasePin(pin))
                {
                    Console.WriteLine("Release pin successfully, please retry Select.");
                }
			}
			return false;
		}
	}
}