using Assets.Model.Class.Enum;
using Assets.Model.Class.Table;
using Generics;
using System;
using System.Collections.Generic;

namespace Assets.Model.Class.Builder
{
    public class ClassBuilder : AbstractBuilder<EClass, MClass>
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
