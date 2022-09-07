using CustomerManagement.Entities;
using CustomerManagement.Interfaces;
using CustomerManagement.Repositories;

namespace CustomerManagement.Services;

public class CustomerService : IService<Customer>
{
    private readonly IRepository<Customer> _customerRepository = new CustomerRepository();
    private readonly IRepository<Address> _addressRepository = new AddressRepository();
    private readonly IRepository<Note> _noteRepository = new NoteRepository();

    public CustomerService() { }

    public CustomerService(IRepository<Customer> customerRepository, IRepository<Address>? addressRepository = null, IRepository<Note>? noteRepository = null)
    {
        _customerRepository = customerRepository;
        _addressRepository = addressRepository ?? new AddressRepository();
        _noteRepository = noteRepository ?? new NoteRepository();
    }

    public Customer? Get(int entityId)
    {
        if (entityId < 0)
        {
            throw new ArgumentOutOfRangeException($"Entity Id can not be < 0, actual: {entityId}");
        }

        var customer = _customerRepository.Read(entityId);

        if (customer == null)
        {
            return null;
        }

        customer.Addresses = _addressRepository.ReadAll(entityId);
        customer.Notes = _noteRepository.ReadAll(entityId);
        return customer;
    }

    public Customer? Create(Customer entity)
    {
        var createdCustomer = _customerRepository.Create(entity);
        if (createdCustomer != null)
        {
            foreach (var address in entity.Addresses)
            {
                var createdAddress = _addressRepository.Create(address);
                if (createdAddress != null)
                    createdCustomer.Addresses.Add(createdAddress);
            }

            foreach (var note in entity.Notes)
            {
                var createdNote = _noteRepository.Create(note);
                if (createdNote != null)
                    createdCustomer.Notes.Add(createdNote);
            }
        }

        return createdCustomer;
    }

    public bool Update(Customer customer)
    {
        var customerToUpdate = Get(customer.Id);

        if (customerToUpdate == null)
        {
            throw new InvalidOperationException($"Can't find customer with id {customer.Id} in db to update");
        }

        foreach (var customerAddress in customer.Addresses)
        {
            if (!_addressRepository.Update(customerAddress))
            {
                throw new InvalidOperationException($"Can't update address {customerAddress.AddressLine}");
            }
        }

        foreach (var customerNote in customer.Notes)
        {
            if (!_noteRepository.Update(customerNote))
            {
                throw new InvalidOperationException($"Can't update note {customerNote.Text}");
            }
        }

        return _customerRepository.Update(customer);
    }

    public bool Delete(int entityId)
    {
        var customerToDelete = Get(entityId);

        if (customerToDelete == null)
        {
            throw new InvalidOperationException($"Can't find customer with id {entityId}");
        }

        foreach (var note in customerToDelete.Notes)
        {
            _noteRepository.Delete(note.Id);
        }

        foreach (var address in customerToDelete.Addresses)
        {
            _addressRepository.Delete(address.AddressId);
        }

        return _customerRepository.Delete(customerToDelete.Id);
    }

    public List<Customer> GetAll()
    {
        var allCustomers = _customerRepository.ReadAll();

        foreach (var customer in allCustomers)
        {
            customer.Addresses = _addressRepository.ReadAll(customer.Id);
            customer.Notes = _noteRepository.ReadAll(customer.Id);
        }

        return allCustomers;
    }
}