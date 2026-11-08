namespace StringApp.DAL
{
    public enum DirectionEntity
    {
        Forward,
        Backward
    }

    public class CustomStringEntity
    {
        public string Value { get; set; } = string.Empty;
        public int ShiftKey { get; set; }
        public DirectionEntity ShiftDirection { get; set; }
    }
}