using Assets.Model.Ability.Enum;
using Assets.Model.AI.Agent;
using Assets.Template.Util;
using Assets.Template.XML;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Assets.Data.AI.Orient.Agent
{
    public class AgentRoleOrientDataReader : XMLReader
    {
        private AgentRoleOrientAbilitiesTable _orientAbilities;

        public AgentRoleOrientDataReader() : base()
        {
            this._paths.Add("Assets/Data/AI/Orient/Agent/AgentRoleOrientData.xml");
            this._orientAbilities = AgentRoleOrientAbilitiesTable.Instance;
        }

        public override void ReadFromFile()
        {
            foreach (var path in this._paths)
            {
                var role = EAgentRole.None;

                var doc = XDocument.Load(path);
                foreach (var el in doc.Root.Elements())
                    HandleRole(el, ref role);
            }
        }

        private void HandleRole(XElement el, ref EAgentRole role)
        {
            if (EnumUtil<EAgentRole>.TryGetEnumValue(el.Name.ToString(), ref role))
            {
                this._orientAbilities.Table.Add(role, new Dictionary<EAbility, double>());
            }
            foreach (var ele in el.Elements())
            {
                if (ele.Name.ToString().Equals("Abilities"))
                    this.HandleAbilities(ele, ref role);
            }
        }

        private void HandleAbilities(XElement el, ref EAgentRole role)
        {
            var ability = EAbility.None;
            foreach (var ele in el.Elements())
            {
                if (EnumUtil<EAbility>.TryGetEnumValue(ele.Name.ToString(), ref ability))
                    this._orientAbilities.Table[role][ability] = double.Parse(ele.Value);
            }
        }
    }
}
