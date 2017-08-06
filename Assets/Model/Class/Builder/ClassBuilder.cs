using System;
using System.Collections.Generic;
using Generics;
using Model.Classes;

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
