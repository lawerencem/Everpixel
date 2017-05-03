using Generics;
using Model.Characters.XML;
using Model.Classes;
using Model.Equipment.XML;
using Model.Mounts.XML;
using Model.Parties.XML;
using System.Collections.Generic;
using System.Threading;

namespace Assets.Controller.Managers
{
    public class LoaderManager : AbstractSingleton<LoaderManager>
    {
        private List<GenericXMLReader> _readers = new List<GenericXMLReader>();

        public LoaderManager()
        {
            this._readers.Add(ArmorReader.Instance);
            this._readers.Add(ClassReader.Instance);
            this._readers.Add(PartiesReader.Instance);
            this._readers.Add(PredefinedCharacterReader.Instance);
            this._readers.Add(MountReader.Instance);
            this._readers.Add(RaceDefaultStatsReader.Instance);
            this._readers.Add(SubPartiesReader.Instance);
            this._readers.Add(WeaponReader.Instance);

            foreach (var reader in this._readers)
                reader.ReadFromFile();
        }
    }
}
