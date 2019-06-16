using System;
using System.Diagnostics;

using OffSync.Mapping.Mappert.DynamicMethods;
using OffSync.Mapping.Mappert.Practises;
using OffSync.Mapping.Practises;

namespace OffSync.Mapping.Mappert.Examples.Performance
{
    static class Program
    {
        const int IterationCount = 2_000_000;

        static void Main(
            string[] args)
        {
            // 1. create a mapper for the source and target models
            var mapper = new Mapper<SourceModel, TargetModel>(
                b =>
                {
                    b.Map(s => s.Name).To(t => t.Description);
                });

            // 2. measure performance with out-of-the-box Reflection implementation (.NET Standard 2.0 compatible)
            MeasurePerformance(mapper);

            // 3. update default mapping delegate builder to Dynamic Method implementation (requires .NET Core / .NET Framework)
            DynamicMethodMappingDelegateBuilder.SetAsDefault();

            // 4. re-create a mapper to allow the usage of the updated default mapping delegate builder
            mapper = new Mapper<SourceModel, TargetModel>(
                b =>
                {
                    b.Map(s => s.Name).To(t => t.Description);
                });

            // 5. measure performance with Dynamic Method mapping
            MeasurePerformance(mapper);
        }

        static void MeasurePerformance(
            IMapper<SourceModel, TargetModel> mapper)
        {
            Console.WriteLine($"Using: {MappingDelegateBuilderRegistry.Default?.GetType().Name ?? "(default)"}");

            var source = new SourceModel()
            {
                Id = 1,
                Name = "World",
            };

            var sw = new Stopwatch();
            sw.Start();

            for (int i = 0; i < IterationCount; i++)
            {
                mapper.Map(source);
            }

            sw.Stop();

            Console.WriteLine($"\tmapper performed {IterationCount:#,##0} mappings in {sw.Elapsed.TotalSeconds:0.000} [s] ({(double)IterationCount / sw.Elapsed.TotalSeconds:#,##0.} [1/s])");
        }
    }
}
