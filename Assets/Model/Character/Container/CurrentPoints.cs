namespace Assets.Model.Character.Container
{
    public class CurrentPoints<T>
    {
        private AChar<T> _parent;
 
        public int CurrentAP { get; set; }
        public int CurrentHP { get; set; }
        public int CurrentMorale { get; set; }
        public int CurrentStamina { get; set; }

        public CurrentPoints(AChar<T> parent)
        {
            this._parent = parent;
        }
    }
}
