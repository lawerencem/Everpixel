using Assets.Controller.Character;
using Assets.Model.Action;
using Assets.Model.Character.Enum;
using Assets.Model.Combat.Hit;
using Assets.Template.CB;
using Assets.Template.Util;
using Assets.View.Fatality;
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
            var fatality = FatalityFactory.Instance.GetFatality(a);
            if (fatality != null)
            {
                fatality.Init();
                return true;
            }
            return false;
        }

        public bool IsFatality(MAction a)
        {
            bool sucess = false;
            foreach (var hit in a.Data.Hits)
            {
                if (hit.Data.Target.Current != null && hit.Data.Target.Current.GetType().Equals(typeof(CharController)))
                {
                    if (this.IsHitFatal(hit))
                    {
                        var roll = RNG.Instance.NextDouble();
                        if (roll < CombatGUIParams.FATALITY_CHANCE)
                        {
                            sucess = true;
                            hit.Data.IsFatality = true;
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

        private bool IsHitFatal(MHit hit)
        {
            var target = hit.Data.Target.Current as CharController;
            if (target.Proxy.GetPoints(ESecondaryStat.HP) - hit.Data.Dmg <= 0)
            {
                if (!FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Dodge) &&
                    !FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Parry))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
