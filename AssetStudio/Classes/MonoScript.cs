﻿namespace AssetStudio
{
    public sealed class MonoScript : NamedObject
    {
        public string m_ClassName;
        public string m_Namespace;
        public string m_AssemblyName;

        public override string Name => string.IsNullOrEmpty(m_Name) ? m_ClassName : m_Name;

        public MonoScript(ObjectReader reader) : base(reader)
        {
            if (version >= "3.4") //3.4 and up
            {
                var m_ExecutionOrder = reader.ReadInt32();
            }
            if (version.Major < 5) //5.0 down
            {
                var m_PropertiesHash = reader.ReadUInt32();
            }
            else
            {
                var m_PropertiesHash = reader.ReadBytes(16);
            }
            if (version.Major < 3) //3.0 down
            {
                var m_PathName = reader.ReadAlignedString();
            }
            m_ClassName = reader.ReadAlignedString();
            if (version.Major >= 3) //3.0 and up
            {
                m_Namespace = reader.ReadAlignedString();
            }
            m_AssemblyName = reader.ReadAlignedString();
            if (version < "2018.2") //2018.2 down
            {
                var m_IsEditorScript = reader.ReadBoolean();
            }
        }
    }
}
