using System;

namespace OffSync.Mapping.Mappert.Examples.HelloWorld
{
    static class Program
    {
        static void Main(
            string[] args)
        {
            // 1. create a mapper for the source and target models
            var mapper = new Mapper<SourceModel, TargetModel>(
                b =>
                {
                    // 2. provide mappings for properties that don't have the same name
                    b.Map(s => s.Name).To(t => t.Description);
                });

            // 3. create the source model
            var source = new SourceModel()
            {
                Id = 1,
                Name = "World",
            };

            // 4. map the target model from the source model
            var target = mapper.Map(source);

            Console.WriteLine($"Hello {target}");
        }
    }
}
