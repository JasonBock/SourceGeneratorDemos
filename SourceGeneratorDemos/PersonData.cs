using InlineMapping;
using System;

namespace SourceGeneratorDemos;

[MapTo(typeof(Person))]
public class PersonData
{
	public Guid Id { get; set; }
	public string? Name { get; set; }
	public DateTime When { get; set; }
}
