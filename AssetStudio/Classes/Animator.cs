﻿namespace AssetStudio
{
    public sealed class Animator : Behaviour
    {
        public PPtr<Avatar> m_Avatar;
        public PPtr<RuntimeAnimatorController> m_Controller;
        public bool m_HasTransformHierarchy = true;

        public override string Name => m_GameObject.Name;

        public Animator(ObjectReader reader) : base(reader)
        {
            m_Avatar = new PPtr<Avatar>(reader);
            m_Controller = new PPtr<RuntimeAnimatorController>(reader);
            if (reader.Game.Type.IsGISubGroup())
            {
                var m_FBIKAvatar = new PPtr<Object>(reader); //FBIKAvatar placeholder
            }
            var m_CullingMode = reader.ReadInt32();

            if (version >= "4.5") //4.5 and up
            {
                var m_UpdateMode = reader.ReadInt32();
            }
            
            if (reader.Game.Type.IsSR())
            {
                var m_MotionSkeletonMode = reader.ReadInt32();
            }

            var m_ApplyRootMotion = reader.ReadBoolean();
            if (version.Major == 4 && version.Minor >= 5) //4.5 and up - 5.0 down
            {
                reader.AlignStream();
            }

            if (version.Major >= 5) //5.0 and up
            {
                var m_LinearVelocityBlending = reader.ReadBoolean();
                if (version >= "2021.2") //2021.2 and up
                {
                    var m_StabilizeFeet = reader.ReadBoolean();
                }
                reader.AlignStream();
            }

            if (version < "4.5") //4.5 down
            {
                var m_AnimatePhysics = reader.ReadBoolean();
            }

            if (version >= "4.3") //4.3 and up
            {
                m_HasTransformHierarchy = reader.ReadBoolean();
            }

            if (version >= "4.5") //4.5 and up
            {
                var m_AllowConstantClipSamplingOptimization = reader.ReadBoolean();
            }
            if (version.Major >= 5 && version.Major < 2018) //5.0 and up - 2018 down
            {
                reader.AlignStream();
            }

            if (version.Major >= 2018) //2018 and up
            {
                var m_KeepAnimatorControllerStateOnDisable = reader.ReadBoolean();
                reader.AlignStream();
            }
        }
    }
}
