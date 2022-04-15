using System.Text.Json.Serialization;
using ZdravoCorpAppTim22.Model.Generic;

public class Address : IHasID
{
    public string Street { get; set; }
    public string Number { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public int Id { get; set; }

    [JsonConstructor]
    public Address()
    {
        Street = "";
        Number = "";
        City = "";
        Country = "";
    }
    public Address(string street = "", string number = "", string city = "", string country = "")
    {
        Street = street;
        Number = number;
        City = city;
        Country = country;
    }

    public Address(string street, string number, string city, string country, int id)
    {
        Street = street;
        Number = number;
        City = city;
        Country = country;
        Id = id;
    }
    public override string ToString()
    {
        return Street + "/" + Number + "/" + City + "/" + Country;

    }
}