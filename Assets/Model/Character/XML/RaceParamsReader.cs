using Assets.Model.Character.Enum;
using Assets.Model.Character.Param.Race;
using Assets.Model.Character.Table;
using System.Xml.Linq;
using Template.Utility;
using Template.XML;

namespace Assets.Model.Character.XML
{
    public class RaceParamsReader : XMLReader
    {
        private static RaceParamsReader _instance;
        public static RaceParamsReader Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new RaceParamsReader();
                return _instance;
            }
        }

        public RaceParamsReader()
        {
            this._path = "Assets/Model/Characters/XML/RaceParams.xml";
        }

        public override void ReadFromFile()
        {
            var table = RaceParamsTable.Instance as RaceParamsTable;
            var doc = XDocument.Load(this._path);
            ERace race = ERace.None;

            foreach (var el in doc.Root.Elements())
            {
                foreach (var att in el.Attributes())
                {
                    EnumUtil<ERace>.TryGetEnumValue(att.Value, ref race);
                    if (!table.Table.ContainsKey(race))
                        table.Table.Add(race, new RaceParams());

                    foreach(var ele in el.Elements())
                    {
                        switch (ele.Name.ToString())
                        {
                            case ("PrimaryStats"): { PrimaryStatsParser.ParseXElementForStats(ele, table.Table[race].PrimaryStats); } break;
                            case ("Sprites"): { this.HandleSprites(ele, table.Table[race].Sprites); } break;
                        }
                    }
                        
                }
            }
        }

        private void HandleSprites(XElement el, RaceSprites sprites)
        {
            foreach (var ele in el.Elements())
            {
                var csv = ele.Value.ToString().Split(',');
                foreach(var v in csv)
                {
                    if (!v.Equals(""))
                    {
                        switch (ele.Name.ToString())
                        {
                            case ("Dead"): { sprites.Dead.Add(int.Parse(v)); } break;
                            case ("Face"): { sprites.Face.Add(int.Parse(v)); } break;
                            case ("Flinch"): { sprites.Flinch.Add(int.Parse(v)); } break;
                            case ("CharHead"): { sprites.Head.Add(int.Parse(v)); } break;
                            case ("CharHeadDeco1"):  { sprites.HeadDeco1.Add(int.Parse(v)); } break;
                            case ("CharHeadDeco2"):  { sprites.HeadDeco2.Add(int.Parse(v)); } break;
                            case ("CharTorsoDeco1"):  { sprites.TorsoDeco1.Add(int.Parse(v)); } break;
                            case ("CharTorsoDeco2"):  { sprites.TorsoDeco2.Add(int.Parse(v)); } break;
                            case ("Torso"):  { sprites.Torso.Add(int.Parse(v)); } break;
                        }
                    }
                }
            }
        }
    }
}