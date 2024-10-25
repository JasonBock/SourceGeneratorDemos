namespace SourceGeneratorDemos; 

public interface ICustomerRepository
{
	Customer Retrieve(int id);
	void Delete(int id);
}
