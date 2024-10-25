using Rocks;
using SourceGeneratorDemos;
using System;

[assembly: Rock(typeof(ICustomerRepository), BuildType.Create)]

DemoRegularExpressions();

static void DemoRegularExpressions()
{
	var id = Guid.NewGuid();
	Console.WriteLine(GuidRegex.GetRegex().IsMatch(id.ToString()));
	Console.WriteLine(GuidRegex.GetRegex().IsMatch(id.ToString("N")));
}

//DemoInlineMapping();

static void DemoInlineMapping()
{
	var source = new PersonData
	{
		Id = Guid.NewGuid(),
		Name = "Jason",
		When = DateTime.UtcNow
	};

	Console.Out.WriteLine(
		$"{source.Id}, {source.Name}, {source.When}");

	var destination = source.MapToPerson();

	Console.Out.WriteLine(
		$"{destination.Id}, {destination.Name}, {destination.When}, {destination.Reason}");
}

//DemoAutoDeconstruct();

static void DemoAutoDeconstruct()
{
	var destination = new Person() { Id = Guid.NewGuid(), Name = "Jason", When = DateTime.UtcNow };
	Console.WriteLine(destination.Id);

	(var id, var name, var when, var reason) = destination;
	Console.WriteLine(id);
}

//DemoRocks();

static void DemoRocks()
{
	var id = 3;
	var customer = new Customer(id, "Jason", 29);

	var expectations = new ICustomerRepositoryCreateExpectations();
	//expectations.Methods.Delete(id);
	expectations.Methods.Retrieve(id).ReturnValue(customer);
	var mock = expectations.Instance();

	var retriever = new CustomerRetriever(mock);
	var retrievedCustomer = retriever.Get(3);

	Console.Out.WriteLine(retrievedCustomer);

	expectations.Verify();
}