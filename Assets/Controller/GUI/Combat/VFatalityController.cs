using Assets.Controller.Character;
using Assets.Model.Action;
using Assets.Template.CB;
using Assets.Template.Util;
using Assets.View.Fatality;
using Assets.View.Fatality.Magic;
using Assets.View.Fatality.Weapon;
using System.Collections.Generic;

namespace Assets.Controller.GUI.Combat
{
    public class VFatalityController : ICallback
    {
        private List<Callback> _callbacks;

        private static VFatalityController _instance;
        public static VFatalityController Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new VFatalityController();
                return _instance;
            }
        }

        public void AddCallback(Callback callback)
        {
            this._callbacks.Add(callback);
        }

        public void DoCallbacks()
        {
            foreach (var callback in this._callbacks)
                callback(this);
        }

        public bool FatalitySuccessful(MAction a)
        {
            var success = false;

            var data = new FatalityData();
            data.Target = a.Data.Target;
            var fatality = FatalityFactory.Instance.GetFatality(a);
            if (fatality != null)
            {
                switch (fatality.Type)
                {
                    case (EFatality.Crush): { fatality = fatality as CrushFatality; success = true; } break;
                    case (EFatality.Fighting): { fatality = fatality as FightingFatality; success = true; } break;
                    case (EFatality.Slash): { fatality = fatality as SlashFatality; success = true; } break;
                }
            }

            if (success)
                fatality.Init();
            return success;
        }

        public bool IsFatality(MAction a)
        {
            bool sucess = false;
            foreach (var hit in a.Data.Hits)
            {
                if (hit.Data.Target != null && hit.Data.Target.GetType().Equals(typeof(CharController)))
                {
                    var target = hit.Data.Target.Current as CharController;
                    if (target.Model.GetCurrentHP() - hit.Data.Dmg <= 0)
                    {
                        var roll = RNG.Instance.NextDouble();
                        if (roll < CombatGUIParams.FATALITY_CHANCE)
                        {
                            sucess = true;
                            //a.FatalityHits.Add(hit);
                        }
                    }
                }
            }
            return sucess;
        }

        public void SetCallback(Callback callback)
        {
            this._callbacks = new List<Callback>() { callback };
        }
    }
}
