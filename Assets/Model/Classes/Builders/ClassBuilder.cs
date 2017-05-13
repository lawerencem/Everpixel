using System;
using System.Collections.Generic;
using Generics;

namespace Model.Classes
{
    public class ClassBuilder : AbstractBuilder<ClassEnum, GenericClass>
    {
        public override GenericClass Build()
        {
            throw new NotImplementedException();
        }

        public override GenericClass Build(List<ClassEnum> args)
        {
            throw new NotImplementedException();
        }

        public override GenericClass Build(ClassEnum arg)
        {
            var cParams = ClassParamTable.Instance.Table[arg];
            return new GenericClass(cParams, arg);
        }
    }
}
