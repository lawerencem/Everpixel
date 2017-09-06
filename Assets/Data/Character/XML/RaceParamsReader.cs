using Assets.Data.Character.Table;
using Assets.Model.Character.Enum;
using Assets.Model.Character.Param.Race;
using Assets.Template.Util;
using Assets.Template.XML;
using System.Xml.Linq;

namespace Assets.Data.Character.XML
{
    public class RaceReader : XMLReader
    {
        private static RaceReader _instance;
        public static RaceReader Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new RaceReader();
                return _instance;
            }
        }

        public RaceReader() : base()
        {
            this._paths.Add("Assets/Data/Character/XML/RaceParams.xml");
        }

        public override void ReadFromFile()
        {
            foreach(var path in this._paths)
            {
                var table = RaceParamsTable.Instance as RaceParamsTable;
                var doc = XDocument.Load(path);
                ERace race = ERace.None;

                foreach (var el in doc.Root.Elements())
                {
                    foreach (var att in el.Attributes())
                    {
                        EnumUtil<ERace>.TryGetEnumValue(att.Value, ref race);
                        if (!table.Table.ContainsKey(race))
                            table.Table.Add(race, new RaceParams());

                        foreach (var ele in el.Elements())
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