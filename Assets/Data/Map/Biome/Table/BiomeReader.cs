using Assets.Data.Map.Deco.Table;
using Assets.Model.Biome;
using Assets.Model.Map.Deco;
using Assets.Model.Map.Landmark;
using Assets.Model.Map.Tile;
using Assets.Template.Util;
using Assets.Template.XML;
using System.Xml.Linq;

namespace Assets.Data.Map.Deco.XML
{
    public class BiomeReader : XMLReader
    {
        private BiomeTable table = BiomeTable.Instance;

        private static BiomeReader _instance;
        public static BiomeReader Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new BiomeReader();
                return _instance;
            }
        }

        public BiomeReader() : base()
        {
            this._paths.Add("Assets/Data/Map/Biome/XML/Grassland.xml");
            this._paths.Add("Assets/Data/Map/Biome/XML/Snowland.xml");
        }

        public override void ReadFromFile()
        {
            foreach (var path in this._paths)
            {
                var doc = XDocument.Load(path);
                var type = EBiome.None;

                foreach(var att in doc.Root.Attributes())
                {
                    if (EnumUtil<EBiome>.TryGetEnumValue(att.Value, ref type))
                    {
                        if (!this.table.Table.ContainsKey(type))
                            this.table.Table.Add(type, new BiomeParam(type));

                        foreach (var el in doc.Elements())
                        {
                            foreach(var ele in el.Elements())
                            {
                                switch(ele.Name.ToString())
                                {
                                    case ("Decos"): { this.HandleDecos(ele, type); } break;
                                    case ("Landmarks"): { this.HandleLandmarks(ele, type); } break;
                                    case ("Tiles"): { this.HandleTiles(ele, type); } break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void HandleDecos(XElement el, EBiome type)
        {
            foreach(var ele in el.Elements())
            {
                foreach(var name in ele.Attributes())
                {
                    var deco = EDeco.None;
                    if (EnumUtil<EDeco>.TryGetEnumValue(name.Value, ref deco))
                    {
                        foreach(var elem in ele.Elements())
                        {
                            if (elem.Name == "Chance")
                            {
                                double chance = 0;
                                if (double.TryParse(elem.Value, out chance))
                                {
                                    this.table.Table[type].DecoDict.Add(deco, chance);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void HandleLandmarks(XElement el, EBiome type)
        {
            foreach (var ele in el.Elements())
            {
                foreach (var name in ele.Attributes())
                {
                    var landmark = ELandmark.None;
                    if (EnumUtil<ELandmark>.TryGetEnumValue(name.Value, ref landmark))
                    {
                        foreach (var elem in ele.Elements())
                        {
                            if (elem.Name == "Chance")
                            {
                                double chance = 0;
                                if (double.TryParse(elem.Value, out chance))
                                {
                                    this.table.Table[type].LandmarkDict.Add(landmark, chance);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void HandleTiles(XElement el, EBiome type)
        {
            foreach (var ele in el.Elements())
            {
                foreach (var name in ele.Attributes())
                {
                    var tileType = ETile.None;
                    if (EnumUtil<ETile>.TryGetEnumValue(name.Value, ref tileType))
                    {
                        foreach (var elem in ele.Elements())
                        {
                            if (elem.Name == "Chance")
                            {
                                double chance = 0;
                                if (double.TryParse(elem.Value, out chance))
                                {
                                    this.table.Table[type].TileDict.Add(tileType, chance);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
