# Rocket.Mapper
this library allows you configure object mappings in a seperate layer


### sample code :

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
            
            //Expected result is; John Lee Hooker,92,0 (age is 92 on 2019 and its value depends on the current year)  
            Console.Write($"{employee.Fullname},{employee.Age},{employee.Salary}");
