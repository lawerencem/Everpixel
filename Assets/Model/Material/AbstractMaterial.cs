namespace Model.Material
{
    abstract public class AbstractMaterial
    {
        protected MaterialModifierEnum _mod;
        protected MaterialEnum _type;

        public MaterialModifierEnum Mod { get { return this._mod; } }
        public MaterialEnum Type { get { return this._type; } }
    }
}
