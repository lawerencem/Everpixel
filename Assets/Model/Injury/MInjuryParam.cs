﻿using Assets.Model.Character.Enum;
using Assets.Model.Character.Param;
using Assets.Model.OTE;
using Assets.Model.OTE.DoT;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Model.Injury
{
    public class MInjuryParam
    {
        private EInjury _type;
        
        public Pair<EDoT, int> DoT { get; set; }
        public List<Pair<EPrimaryStat, double>> PStatMods {get;set;}
        public List<Pair<ESecondaryStat, double>> SStatMods { get; set; }
        public EInjury Type { get { return this._type; } }

        public MInjuryParam(EInjury type)
        {
            this._type = type;
            this.DoT = new Pair<EDoT, int>();
            this.PStatMods = new List<Pair<EPrimaryStat, double>>();
            this.SStatMods = new List<Pair<ESecondaryStat, double>>();
        }

        public MInjury GetInjury()
        {
            var injury = new MInjury(this._type);
            if (this.DoT.X != EDoT.None)
            {
                var data = new MOTEData();
                data.Dur = this.DoT.Y;
                injury.DoT = new MDoT(this.DoT.X, data);
            }
            foreach (var stat in this.PStatMods)
            {
                var data = this.GetModProto();
                data.Stat = stat.X;
                data.Scalar = stat.Y;
                injury.Mods.Add(new StatMod(data));
            }
            foreach (var stat in this.SStatMods)
            {
                var data = this.GetModProto();
                data.Stat = stat.X;
                data.Scalar = stat.Y;
                injury.Mods.Add(new StatMod(data));
            }
            return injury;
        }

        protected StatModData GetModProto()
        {
            var mod = new StatModData();
            mod.DurationMod = false;
            mod.FlatMod = false;
            return mod;
        }
    }
}
