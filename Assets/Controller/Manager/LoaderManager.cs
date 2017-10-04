using Assets.Data.Ability.XML;
using Assets.Data.Character.XML;
using Assets.Data.Class.XML;
using Assets.Data.Equipment.XML;
using Assets.Data.Injury.XML;
using Assets.Data.Mount.XML;
using Assets.Data.Party.XML;
using Assets.Data.Perk.XML;
using Assets.Data.Zone.XML;
using Assets.Template.Other;
using Assets.Template.XML;
using Assets.View.Bark;
using System.Collections.Generic;

namespace Assets.Controller.Manager
{
    public class LoaderManager : ASingleton<LoaderManager>
    {
        private List<XMLReader> _readers = new List<XMLReader>();

        public LoaderManager()
        {
            this._readers.Add(AbilityReader.Instance);
            this._readers.Add(ArmorReader.Instance);
            this._readers.Add(BarkReader.Instance);
            this._readers.Add(ClassReader.Instance);
            this._readers.Add(InjuryReader.Instance);
            this._readers.Add(PartyReader.Instance);
            this._readers.Add(PredefinedCharReader.Instance);
            this._readers.Add(MountReader.Instance);
            this._readers.Add(PerkReader.Instance);
            this._readers.Add(RaceReader.Instance);
            this._readers.Add(SubPartyReader.Instance);
            this._readers.Add(WeaponReader.Instance);
            this._readers.Add(ZoneReader.Instance);

            foreach (var reader in this._readers)
                reader.ReadFromFile();
        }
    }
}
