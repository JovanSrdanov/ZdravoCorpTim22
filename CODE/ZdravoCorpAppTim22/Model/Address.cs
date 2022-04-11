public class Address
{
    public string Street { get; set; }
    public string Number { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public int ID { get; set; }

    public Address()
    {

    }

    public Address(string street, string number, string city, string country, int iD)
    {
        Street = street;
        Number = number;
        City = city;
        Country = country;
        ID = iD;
    }
}