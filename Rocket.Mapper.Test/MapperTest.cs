using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rocket.Mapper.Impl;
using Rocket.Mapper.Test.Model;
using System;

namespace Rocket.Mapper.Test
{
    [TestClass]
    public class MapperTest
    {
        [TestMethod]
        public void Successful()
        {
            //create a mapping service
            var mappingService = new MappingService();
            //Add configuration for Person to Employee mapping
            mappingService.Register<Person, Employee>(source => {
                var result = new Employee();
                //map properties
                result.Fullname = $"{source.Name} {source.Lastname}";
                result.Age = DateTime.Now.Year - source.Birthdate.Year;
                
                return result;
            });

            //define a person object
            var person = new Person { Name = "John Lee", Lastname = "Hooker", Birthdate = new DateTime(1927, 8, 22) };
            //then map it to an employee object
            var employee = mappingService.Map<Employee>(person);

            Assert.IsNotNull(employee);
        }

        [TestMethod]
        public void AddingMultipleConfigurationsForSameTypesThrowsException()
        {
            //create a mapping service
            var mappingService = new MappingService();
            //add configuration
            mappingService.Register<object, object>(source => source);
            //and try to add same configuration again then you will get an exception
            Assert.ThrowsException<Exception>(() => mappingService.Register<object, object>(source => source));
        }

        [TestMethod]
        public void MappingUnconfiguredTypesThrowsException()
        {
            //create a mapping service
            var mappingService = new MappingService();
            //Try to map unconfigured types then you will get an exception
            Assert.ThrowsException<Exception>(() => mappingService.Map<object>(new object()));
        }

        [TestMethod]
        public void MappingNullsThrowsException()
        {
            //create a mapping service
            var mappingService = new MappingService();
            //Try to map a null object then you will get an exception
            Assert.ThrowsException<ArgumentException>(() => mappingService.Map<object>(null));
        }
    }
}
