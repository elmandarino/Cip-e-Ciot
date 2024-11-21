namespace NetCoreClient.ValueObjects
{
    internal class WaterPression
    {
        public int Value { get; private set; }

        public WaterPression(int value)
        {
            this.Value = value;
        }

    }
}
