using Assets.Data.Class.Table;
using Assets.Model.Class.Enum;
using Assets.Template.Builder;
using System;
using System.Collections.Generic;

namespace Assets.Model.Class.Builder
{
    public class ClassBuilder : ABuilder<EClass, MClass>
    {
        public override MClass Build()
        {
            throw new NotImplementedException();
        }

        public override MClass Build(List<EClass> args)
        {
            throw new NotImplementedException();
        }

        public override MClass Build(EClass arg)
        {
            var cParams = ClassParamTable.Instance.Table[arg];
            return new MClass(cParams, arg);
        }
    }
}
