namespace NetCoreClient.ValueObjects
{
    internal class WaterLevel
    {
        public int Value { get; private set; }

        public WaterLevel(int value)
        {
            this.Value = value;
        }

    }
}
