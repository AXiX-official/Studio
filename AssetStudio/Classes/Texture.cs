﻿namespace AssetStudio
{
    public abstract class Texture : NamedObject
    {
        protected Texture(ObjectReader reader) : base(reader)
        {
            if (version >= "2017.3" && version.Major < 6000) //2017.3 and up
            {
                var m_ForcedFallbackFormat = reader.ReadInt32();
                var m_DownscaleFallback = reader.ReadBoolean();
            }
            if (version >= "2020.2") //2020.2 and up
            {
                var m_IsAlphaChannelOptional = reader.ReadBoolean();
            }
            reader.AlignStream();
        }
    }
}
