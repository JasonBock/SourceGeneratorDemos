using InlineMapping;
using InlineMappingDemo;
using PartiallyAppliedDemo;
using Rocks;
using RocksDemo;
using System;

DemoInlineMapping();

static void DemoInlineMapping() 
{
	var source = new PersonData
	{
		Id = Guid.NewGuid(),
		Name = "Jason",
		When = DateTime.Now
	};

	Console.Out.WriteLine(
		$"{source.Id}, {source.Name}, {source.When}");

	var destination = source.MapToPerson();

	Console.Out.WriteLine(
		$"{destination.Id}, {destination.Name}, {destination.When}, {destination.Reason}");
}

//DemoPartiallyApplied();

static void DemoPartiallyApplied() 
{
	var incrementBy3 = Partially.Apply(Maths.Add, 3);
	Console.Out.WriteLine($"incrementBy3(4) is {incrementBy3(4)}");

	var functions = new Functions();
	var triple = Partially.Apply(functions.Multiply, 3);
	Console.Out.WriteLine($"triple(4) is {triple(4)}");

	var addWith3 = Partially.ApplyWithOptionals(Maths.AddOptionals, 3);
	Console.Out.WriteLine($"addWith3() is {addWith3()}");
	Console.Out.WriteLine($"addWith3(10) is {addWith3(10)}");
	Console.Out.WriteLine($"addWith3(10, 20) is {addWith3(10, 20)}");
}

//DemoRocks();

static void DemoRocks() 
{
	var id = 3;
	var customer = new Customer(id, "Jason", 29);

	var expectations = Rock.Create<ICustomerRepository>();
	//expectations.Properties().Getters().Id().Returns(id);
	expectations.Methods().Retrieve(id).Returns(customer);
	var mock = expectations.Instance();

	var retriever = new CustomerRetriever(mock);
	var retrievedCustomer = retriever.Get(3);

	Console.Out.WriteLine(retrievedCustomer);

	expectations.Verify();
}

namespace InlineMappingDemo
{
	[MapTo(typeof(Person))]
	public class PersonData
	{
		public Guid Id { get; set; }
		public string? Name { get; set; }
		public DateTime When { get; set; }
	}

	public class Person
	{
		public Guid Id { get; set; }
		public string? Name { get; set; }
		public DateTime When { get; set; }
		public string? Reason { get; set; }
	}
}

namespace PartiallyAppliedDemo
{
	public static class Maths
	{
		public static int Add(int a, int b) => a + b;
		public static int AddOptionals(int a = 3, int b = 4, int c = 5) => a + b + c;
	}

	public class Functions
	{
		public int Multiply(int a, int b) => a * b;
	}
}

namespace RocksDemo
{
	public record Customer(int Id, string Name, uint Age);

	public interface ICustomerRepository
	{
		int Id { get; }
		Customer Retrieve(int id);
		void Delete(int id);
	}

	public sealed class CustomerRetriever
	{
		private readonly ICustomerRepository repository;

		public CustomerRetriever(ICustomerRepository repository) =>
			this.repository = repository ?? throw new ArgumentNullException(nameof(repository));

		public Customer Get(int id) =>
			this.repository.Retrieve(id);
	}
}