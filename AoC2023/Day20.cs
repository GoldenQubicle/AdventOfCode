namespace AoC2023;

public class Day20 : Solution
{
	private readonly Dictionary<string, Module> modules;

	public Day20(string file) : base(file)
	{
		modules = Input.Select(line =>
		{
			var parts = line.Split(" -> ");
			var destinations = parts[1].Split(',').Select(s => s.Trim( )).ToList( );
			var name = parts[0][1..];

			return parts[0][0] switch
			{
				'%' => new FlipFlop(name, destinations),
				'&' => new Conjunction(name, destinations),
				_ => new BroadCaster(nameof(BroadCaster), destinations) as Module
			};
		}).ToDictionary(m => m.Name, m => m);

		modules.Values
				.Where(m => m is Conjunction)
				.ForEach(c =>
				{
					(c as Conjunction)!.Inputs = modules.Values
						.Where(m => m.Destinations.Contains(c.Name))
						.ToDictionary(m => m.Name, _ => Pulse.Low);
				});
	}


	public override async Task<string> SolvePart1()
	{
		var pulsesSend = new Dictionary<dynamic, dynamic>
		{
			{ Pulse.Low, 0L },
			{ Pulse.High, 0L }
		};

		var result = await RunModules(pulsesSend, 1000);

		return (result[Pulse.Low] * result[Pulse.High]).ToString( );

	}

	public override async Task<string> SolvePart2()
	{
		//ns is the only module to send to rx
		//ns will only send a low pulse when all its inputs are high
		//so determine when each input sends a high pulse to ns and calculate the LCM 
		
		var counter = modules.Values
			.Where(m => m.Destinations.Contains("ns"))
			.ToDictionary(dynamic (m) => m.Name, dynamic (_) => new List<long>( ));

		var result = await RunModules(counter, 10_000, isPart2: true);

		var cycles = result.Values
			.Select(long (l) => l[1] - l[0]);

		return Maths.LeastCommonMultiple(cycles).ToString( );
	}

	private async Task<Dictionary<dynamic, dynamic>> RunModules(Dictionary<dynamic, dynamic> result, int buttonPresses, bool isPart2 = false)
	{
		for (var i = 0 ;i < buttonPresses ;i++)
		{
			var packets = new Queue<Packet>( );
			packets.Enqueue(new (nameof(BroadCaster), "", Pulse.Low));

			while (packets.TryDequeue(out var packet))
			{
				if (isPart2)
				{
					if (packet is { Pulse: Pulse.High, Receiver: "ns" })
						result[packet.Sender].Add(i);

					if (result.Values.All(c => c.Count == 2))
						return result;
				}

				if (!isPart2)
					result[packet.Pulse]++;

				if (!modules.TryGetValue(packet.Receiver, out var module))
					continue;

				var next = module.Handle(packet);

				if (next.Count == 0)
					continue;

				packets.EnqueueAll(next);

			}
		}

		return result;
	}

	internal enum Pulse { Low, High }

	internal record Packet(string Receiver, string Sender, Pulse Pulse);

	internal abstract class Module(string name, List<string> destinations)
	{
		internal string Name { get; } = name;
		internal List<string> Destinations { get; } = destinations;
		internal abstract List<Packet> Handle(Packet packet);
	}

	internal class FlipFlop(string name, List<string> destinations) : Module(name, destinations)
	{
		private bool isOn;
		internal override List<Packet> Handle(Packet packet)
		{
			if (packet.Pulse == Pulse.High)
				return [ ];

			isOn = !isOn;

			return Destinations.Select(d => new Packet(d, Name, isOn ? Pulse.High : Pulse.Low)).ToList( );

		}
	}

	internal class Conjunction(string name, List<string> destinations) : Module(name, destinations)
	{
		public Dictionary<string, Pulse> Inputs { get; set; } = new( );
		internal override List<Packet> Handle(Packet packet)
		{
			Inputs[packet.Sender] = packet.Pulse;

			var allHigh = Inputs.Values.All(p => p == Pulse.High);

			return Destinations.Select(d => new Packet(d, Name, allHigh ? Pulse.Low : Pulse.High)).ToList( );
		}
	}

	internal class BroadCaster(string name, List<string> destinations) : Module(name, destinations)
	{
		internal override List<Packet> Handle(Packet packet) =>
			Destinations.Select(d => new Packet(d, Name, packet.Pulse)).ToList( );

	}
}
