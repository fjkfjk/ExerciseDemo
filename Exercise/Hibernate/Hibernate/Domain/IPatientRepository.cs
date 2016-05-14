using System;
using System.Collections.Generic;

namespace Hibernate.Domain
{
    public interface IPatientRepository
    {
        void Add(Patient patient);
        void Update(Patient patient);
        void Remove(Patient patient);
        Patient GetById(int patientId);
        Patient GetByName(string name);
        ICollection<Patient> GetByAge(int age);
    }
}
