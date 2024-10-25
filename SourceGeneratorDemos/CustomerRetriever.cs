using System;

namespace SourceGeneratorDemos; 

public sealed class CustomerRetriever
{
	private readonly ICustomerRepository repository;

	public CustomerRetriever(ICustomerRepository repository) =>
		this.repository = repository ?? throw new ArgumentNullException(nameof(repository));

	public Customer Get(int id) =>
		this.repository.Retrieve(id);
}
