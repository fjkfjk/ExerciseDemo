using Hibernate.Domain;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;

namespace Hibernate.Repositories
{
    public class PatientRepository:IPatientRepository
    {
        public void Setup()
        {
            NHibernateHelper.SetupContext();
        }
        public void Add(Patient patient)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(patient);
                    transaction.Commit();
                }
            }
        }
        public void BatchAdd(int times)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    for (int i = 0; i <= times; i++)
                    {
                        Patient patient = new Patient();
                        patient.Id = i;
                        patient.Name = i.ToString();
                        patient.Age = i;
                        session.Save(patient);
                    }
                    transaction.Commit();
                }
            }
        }

        public void Update(Patient patient)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(patient);
                    transaction.Commit();
                }
            }
        }

        public void Remove(Patient patient)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(patient);
                    transaction.Commit();
                }
            }
        }

        public Patient GetById(int patientId)
        {
            Patient patient = new Patient();
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    patient = session.Get<Patient>(patientId);
                    transaction.Commit();
                }
            }
            return patient;
        }

        public Patient GetByName(string name)
        {
            Patient patient = new Patient();
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    patient = session.Get<Patient>(name);
                    transaction.Commit();
                }
            }
            return patient;
        }

        public ICollection<Patient> GetByAge(int age)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var patients = session.CreateCriteria(typeof(Patient)).Add(Restrictions.Eq("Age", age)).List<Patient>();

                    return patients;
                }
            }
        }
    }
}
