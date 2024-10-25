using System;

namespace SourceGeneratorDemos; 

public class Person
{
	public Guid Id { get; set; }
	public string? Name { get; set; }
	public DateTime When { get; set; }
	public string? Reason { get; set; }
}
