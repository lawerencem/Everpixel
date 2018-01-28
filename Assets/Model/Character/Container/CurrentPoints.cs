using Assets.Model.Character.Enum;

namespace Assets.Model.Character.Container
{
    public class CurrentPoints
    {
        private AChar _parent;

        private int _ap;
        private int _hp;
        private int _morale;
        private int _stamina;

        public CurrentPoints(AChar parent)
        {
            this._parent = parent;
        }

        public void AddValue(ESecondaryStat type, double v)
        {
            switch (type)
            {
                case (ESecondaryStat.AP): { this._ap += (int)v; } break;
                case (ESecondaryStat.HP): { this._hp += (int)v; } break;
                case (ESecondaryStat.Morale): { this._morale += (int)v; } break;
                case (ESecondaryStat.Stamina): { this._stamina += (int)v; } break;
            }
            this.CheckMaxValue(type);
        }

        public void CheckMaxValue(ESecondaryStat type)
        {
            var current = this.GetCurrValue(type);
            var max = this._parent.GetBaseStats().GetStatValue(type);
            if (current > max)
                this.SetValue(type, max);
        }

        public int GetCurrValue(ESecondaryStat type)
        {
            switch (type)
            {
                case (ESecondaryStat.AP): { return this._ap; }
                case (ESecondaryStat.HP): { return this._hp; }
                case (ESecondaryStat.Morale): { return this._morale; }
                case (ESecondaryStat.Stamina): { return this._stamina; }
                default: return 0;
            }
        }

        public void Init(CharStats current)
        {
            this.SetValue(ESecondaryStat.AP, current.GetStatValue(ESecondaryStat.AP));
            this.SetValue(ESecondaryStat.HP, current.GetStatValue(ESecondaryStat.HP));
            this.SetValue(ESecondaryStat.Morale, current.GetStatValue(ESecondaryStat.Morale));
            this.SetValue(ESecondaryStat.Stamina, current.GetStatValue(ESecondaryStat.Stamina));
        }

        public void SetValue(ESecondaryStat type, double v)
        {
            switch (type)
            {
                case (ESecondaryStat.AP): { this._ap = (int)v; } break;
                case (ESecondaryStat.HP): { this._hp = (int)v; } break;
                case (ESecondaryStat.Morale): { this._morale = (int)v; } break;
                case (ESecondaryStat.Stamina): { this._stamina = (int)v; } break;
            }
        }
    }
}
