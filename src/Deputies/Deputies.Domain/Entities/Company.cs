using System;
using Deputies.Domain.AbstractEntities;
using Deputies.Domain.ValueObjects;

namespace Deputies.Domain.Entities;

public sealed class Company : Participant
{
    private Company(Cnpj cnpj, string companyName)
    {
        Cnpj = cnpj;
        CompanyName = companyName;
    }

    public Cnpj Cnpj { get; }
    public string CompanyName { get; }

    public static Company Create(Cnpj cnpj, string companyName)
    {
        if (cnpj == null)
            throw new ArgumentNullException(nameof(cnpj), "CNPJ cannot be null.");
        if (string.IsNullOrWhiteSpace(companyName))
            throw new ArgumentException("CompanyName cannot be null or empty.", nameof(companyName));

        return new Company(cnpj, companyName);
    }

    public override string DisplayName => CompanyName;

    public override bool Equals(object obj)
    {
        if (obj is Company other)
        {
            // For Company, equality is based on the unique CNPJ
            return Cnpj.Equals(other.Cnpj);
        }
        return false;
    }

    public override int GetHashCode() => Cnpj.GetHashCode();
}