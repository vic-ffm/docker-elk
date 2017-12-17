using Bogus;

namespace console_app
{
    public class Customer
    {
        public string FirstName{ get; set;}
        public string LastName{ get; set;}
        public string SSNumber {get; set;}

        private static readonly Faker<Customer> faker;

        static Customer()
        {
            var randomizer = new Randomizer();

            faker = new Faker<Customer>()
                .RuleFor(customer => customer.FirstName, faker => faker.Name.FirstName())
                .RuleFor(customer => customer.LastName, faker => faker.Name.LastName())
                .RuleFor(customer => customer.SSNumber, _ => randomizer.Replace("###-##-####"));
        }

        public static Customer Generate()
        {
            return faker.Generate();
        }
    }

}