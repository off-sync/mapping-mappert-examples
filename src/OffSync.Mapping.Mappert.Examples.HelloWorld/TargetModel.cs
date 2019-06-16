namespace OffSync.Mapping.Mappert.Examples.HelloWorld
{
    public class TargetModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public override string ToString() => $"{Description} (#{Id})";
    }
}
