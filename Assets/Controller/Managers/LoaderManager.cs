using Generics;
using Model.Characters.XML;
using Model.Classes.XML;
using Model.Equipment.XML;
using Model.Injuries;
using Model.Mounts.XML;
using Model.Parties.XML;
using Models.Equipment.XML;
using System.Collections.Generic;
using View.Barks;

namespace Assets.Controller.Managers
{
    public class LoaderManager : AbstractSingleton<LoaderManager>
    {
        private List<GenericXMLReader> _readers = new List<GenericXMLReader>();

        public LoaderManager()
        {
            this._readers.Add(ActiveAbilityReader.Instance);
            this._readers.Add(ArmorReader.Instance);
            this._readers.Add(BarkReader.Instance);
            this._readers.Add(ClassReader.Instance);
            this._readers.Add(InjuryReader.Instance);
            this._readers.Add(PartiesReader.Instance);
            this._readers.Add(PredefinedCharacterReader.Instance);
            this._readers.Add(PredefinedCritterReader.Instance);
            this._readers.Add(MountReader.Instance);
            this._readers.Add(RaceDefaultStatsReader.Instance);
            this._readers.Add(SubPartiesReader.Instance);
            this._readers.Add(WeaponAbilityReader.Instance);
            this._readers.Add(WeaponReader.Instance);

            foreach (var reader in this._readers)
                reader.ReadFromFile();
        }
    }
}
