namespace ConsoleApp2.ConstructingCar1
{
	using System;

	public class Car : ICar
	{
		public IFuelTankDisplay fuelTankDisplay;

		private IEngine engine;

		private IFuelTank fuelTank;

		public bool EngineIsRunning => engine.IsRunning;

		public Car() : this(20)
		{
		}

		public Car(double fuelLevel)
		{
			fuelTank = new FuelTank(fuelLevel);
			engine = new Engine(fuelTank);
			fuelTankDisplay = new FuelTankDisplay(fuelTank);
		}

		public void EngineStart() => engine.Start();

		public void EngineStop() => engine.Stop();

		public void Refuel(double liters)
		{
			fuelTank.Refuel(liters);
		}

		public void RunningIdle()
		{
			engine.Consume(0.0003);
		}
	}

	public class Engine : IEngine
	{
		private readonly IFuelTank fuelTank;

		public Engine(IFuelTank fuelTank)
		{
			this.fuelTank = fuelTank;
		}

		public bool IsRunning { get; private set; }

		public void Consume(double liters)
		{
			if (IsRunning)
			{
				fuelTank.Consume(liters);
				if (Math.Abs(fuelTank.FillLevel) < 0.000001)
				{
					Stop();
				}
			}
		}

		public void Start()
		{
			if (fuelTank.FillLevel > 0)
			{
				IsRunning = true;
			}
		}

		public void Stop()
		{
			IsRunning = false;
		}
	}

	public class FuelTank : IFuelTank
	{
		private double fillLevel;

		public double FillLevel
		{
			get => fillLevel;
			set
			{
				fillLevel = value;
				if (fillLevel > 60)
				{
					fillLevel = 60;
				}

				if (fillLevel < 0)
				{
					fillLevel = 0;
				}
			}
		}

		public bool IsOnReserve => FillLevel < 5;

		public bool IsComplete => FillLevel >= 60;

		public FuelTank(double fillLevel)
		{
			FillLevel = fillLevel;
		}

		public void Consume(double liters)
		{
			FillLevel -= liters;
		}

		public void Refuel(double liters)
		{
			FillLevel += liters;
		}
	}

	public class FuelTankDisplay : IFuelTankDisplay
	{
		private readonly IFuelTank fuelTank;

		public FuelTankDisplay(IFuelTank fuelTank)
		{
			this.fuelTank = fuelTank;
		}

		public double FillLevel => Math.Round(fuelTank.FillLevel, 2);

		public bool IsOnReserve => fuelTank.IsOnReserve;

		public bool IsComplete => fuelTank.IsComplete;
	}
}
