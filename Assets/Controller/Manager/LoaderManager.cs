using Assets.Model.Character.XML;
using Assets.Model.Class.XML;
using Assets.Model.Effect;
using Assets.Model.Equipment.XML;
using Assets.Model.Injury;
using Assets.Model.Mount.XML;
using Assets.Model.Party.XML;
using Assets.Model.Perk.XML;
using Assets.Models.Equipment.XML;
using Assets.View.Barks;
using Generics;
using System.Collections.Generic;

namespace Assets.Controller.Manager
{
    public class LoaderManager : AbstractSingleton<LoaderManager>
    {
        private List<GenericXMLReader> _readers = new List<GenericXMLReader>();

        public LoaderManager()
        {
            this._readers.Add(AbiltiyReader.Instance);
            this._readers.Add(ArmorReader.Instance);
            this._readers.Add(BarkReader.Instance);
            this._readers.Add(ClassReader.Instance);
            this._readers.Add(EffectReader.Instance);
            this._readers.Add(InjuryReader.Instance);
            this._readers.Add(PartiesReader.Instance);
            this._readers.Add(PredefinedCharReader.Instance);
            this._readers.Add(PredefinedCritterReader.Instance);
            this._readers.Add(MountReader.Instance);
            this._readers.Add(PerkReader.Instance);
            this._readers.Add(RaceParamsReader.Instance);
            this._readers.Add(SubPartiesReader.Instance);
            this._readers.Add(WeaponReader.Instance);

            foreach (var reader in this._readers)
                reader.ReadFromFile();
        }
    }
}
