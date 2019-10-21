using System;
using System.Collections.Generic;
using System.Linq;

public class Car : ICar
{
	private const int DefaultFuelLevel = 20;
	private const int DefaultAcceleration = 10;
	private const int MinimumAcceleration = 5;
	private const int MaximumAcceleration = 20;
	private const int MaxBrakeSpeed = 10;

	private IDrivingProcessor drivingProcessor; // car #2
	private IEngine engine;
	private IFuelTank fuelTank;
	private CarDrivingState state;
	private readonly int acceleration;

	public IDrivingInformationDisplay drivingInformationDisplay; // car #2
	public IFuelTankDisplay fuelTankDisplay;

	public bool EngineIsRunning => engine.IsRunning;

	public Car() : this(DefaultFuelLevel)
	{
	}

	public Car(double fuelLevel) : this(fuelLevel, DefaultAcceleration)
	{
	}

	public Car(double fuelLevel, int maxAcceleration) // car #2
	{
		fuelTank = new FuelTank(fuelLevel);
		engine = new Engine(fuelTank);
		fuelTankDisplay = new FuelTankDisplay(fuelTank);
		acceleration = GetRightAcceleration(maxAcceleration);
		drivingProcessor = new DrivingProcessor(acceleration, MaxBrakeSpeed);
		drivingInformationDisplay  = new DrivingInformationDisplay(drivingProcessor);
		state = new IdleCarState();
	}

	public void BrakeBy(int speed)
	{
		state = new BrakeState(speed);
		Tick();
	}

	public void Accelerate(int speed)
	{
		state = new AccelerateState(speed);
		Tick();
	}

	public void FreeWheel()
	{
		state = new FreeWheelState();
		Tick();
	}

	public void EngineStart() => engine.Start();

	public void EngineStop() => engine.Stop();

	public void Refuel(double liters)
	{
		fuelTank.Refuel(liters);
	}

	public void RunningIdle()
	{
		state = new IdleCarState();
		Tick();
	}

	private void Tick()
	{
		if (EngineIsRunning)
		{
			state.Tick(this);
		}
	}

	private static int GetRightAcceleration(int acceleration)
	{
		if (acceleration < MinimumAcceleration)
		{
			return MinimumAcceleration;
		}

		if (acceleration > MaximumAcceleration)
		{
			return MaximumAcceleration;
		}

		return acceleration;
	}

	private abstract class CarDrivingState
	{
		public abstract void Tick(Car car);
	}

	private class IdleCarState : CarDrivingState
	{
		private const double IdleConsume = 0.0003;

		public override void Tick(Car car)
		{
			car.engine.Consume(IdleConsume);
		}
	}

	private class AccelerateState : FreeWheelState
	{
		private static readonly Dictionary<InclusiveRange, double> FuelConsumptionMap = new Dictionary<InclusiveRange, double>
		{
			[new InclusiveRange(1, 60)] = 0.002,
			[new InclusiveRange(61, 100)] = 0.0014,
			[new InclusiveRange(101, 140)] = 0.002,
			[new InclusiveRange(141, 200)] = 0.0025,
			[new InclusiveRange(201, 250)] = 0.003
		};

		private readonly int speed;

		public AccelerateState(int speed)
		{
			this.speed = speed;
		}

		public override void Tick(Car car)
		{
			if (speed >= car.drivingInformationDisplay.ActualSpeed)
			{
				car.drivingProcessor.IncreaseSpeedTo(speed);
				var consumption = FuelConsumptionMap
					.First(x => x.Key.IsInRange(car.drivingInformationDisplay.ActualSpeed)).Value;
				car.engine.Consume(consumption);
			}
			else
			{
				base.Tick(car);
			}
		}
	}

	private class BrakeState : CarDrivingState
	{
		private readonly int breakBy;

		public BrakeState(int breakBy)
		{
			this.breakBy = breakBy;
		}

		public override void Tick(Car car)
		{
			car.drivingProcessor.ReduceSpeed(breakBy);
		}
	}

	private class FreeWheelState : IdleCarState
	{
		private const int slowingSpeed = 1;

		public override void Tick(Car car)
		{
			car.drivingProcessor.ReduceSpeed(slowingSpeed);
			if (car.drivingInformationDisplay.ActualSpeed == 0)
			{
				base.Tick(car);
			}
		}
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
			if (IsEmptyFuelTank())
			{
				Stop();
			}
		}
	}

	public void Start()
	{
		if (!IsEmptyFuelTank())
		{
			IsRunning = true;
		}
	}

	public void Stop()
	{
		IsRunning = false;
	}

	private bool IsEmptyFuelTank() => Math.Abs(fuelTank.FillLevel) < 0.000001;
}

public class FuelTank : IFuelTank
{
	private const double MaxFuelLevel = 60;
	private const double ReserveFuelLevel = 5;

	private double fillLevel;

	public double FillLevel
	{
		get => fillLevel;
		set
		{
			fillLevel = value;
			if (fillLevel > MaxFuelLevel)
			{
				fillLevel = MaxFuelLevel;
			}

			if (fillLevel < 0)
			{
				fillLevel = 0;
			}
		}
	}

	public bool IsOnReserve => FillLevel < ReserveFuelLevel;

	public bool IsComplete => FillLevel >= MaxFuelLevel;

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

public class DrivingInformationDisplay : IDrivingInformationDisplay // car #2
{
	private readonly IDrivingProcessor drivingProcessor;

	public int ActualSpeed => drivingProcessor.ActualSpeed;

	public DrivingInformationDisplay(IDrivingProcessor drivingProcessor)
	{
		this.drivingProcessor = drivingProcessor;
	}
}

public class DrivingProcessor : IDrivingProcessor // car #2
{
	private const int MaxSpeed = 250;

	private readonly int acceleration;
	private readonly int breakSpeed;
	private int actualSpeed;

	public int ActualSpeed
	{
		get => actualSpeed;
		private set
		{
			if (value > MaxSpeed)
			{
				actualSpeed = MaxSpeed;
			}
			else if (value < 0)
			{
				actualSpeed = 0;
			}
			else
			{
				actualSpeed = value;
			}
		}
	}

	public DrivingProcessor(int acceleration, int breakSpeed)
	{
		this.acceleration = acceleration;
		this.breakSpeed = breakSpeed;
	}

	public void IncreaseSpeedTo(int speed)
	{
		ActualSpeed = Math.Min(Math.Max(speed, ActualSpeed), ActualSpeed + acceleration);
	}

	public void ReduceSpeed(int speed) => 
		ActualSpeed -= Math.Min(speed, breakSpeed);
}

public class InclusiveRange
{
	public int Start { get; }

	public int End { get; }

	public InclusiveRange(int start, int end)
	{
		Start = start;
		End = end;
	}

	public bool IsInRange(int value) => value >= Start && value <= End;
}