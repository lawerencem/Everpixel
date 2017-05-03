namespace Model.Characters
{
    abstract public class AbstractCharacter<T>
    {
        public PrimaryStats PrimaryStats { get; set; }
        public SecondaryStats SecondaryStats { get; set; }
        public SlotCollection Slots { get; set; }
        public T Type { get; set; }
    }
}