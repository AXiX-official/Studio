using System.Collections.Generic;

namespace AssetStudio
{
    public sealed class SkinnedMeshRenderer : Renderer
    {
        public PPtr<Mesh> m_Mesh;
        public List<PPtr<Transform>> m_Bones;
        public float[] m_BlendShapeWeights;
        public PPtr<Transform> m_RootBone;
        public AABB m_AABB;
        public bool m_DirtyAABB;

        public SkinnedMeshRenderer(ObjectReader reader) : base(reader)
        {
            int m_Quality = reader.ReadInt32();
            var m_UpdateWhenOffscreen = reader.ReadBoolean();
            var m_SkinNormals = reader.ReadBoolean(); //3.1.0 and below
            reader.AlignStream();

            if (version.Major == 2 && version.Minor < 6) //2.6 down
            {
                var m_DisableAnimationWhenOffscreen = new PPtr<Animation>(reader);
            }

            m_Mesh = new PPtr<Mesh>(reader);

            var numBones = reader.ReadInt32();
            m_Bones = new List<PPtr<Transform>>();
            for (int b = 0; b < numBones; b++)
            {
                m_Bones.Add(new PPtr<Transform>(reader));
            }
            if (reader.Game.Type.IsZZZ())
            {
                var m_SortingFudge = reader.ReadSingle();
            }
            if (version >= "4.3") //4.3 and up
            {
                m_BlendShapeWeights = reader.ReadSingleArray();
            }

            if (reader.Game.Type.IsGIGroup() || reader.Game.Type.IsZZZ())
            {
                m_RootBone = new PPtr<Transform>(reader);
                m_AABB = new AABB(reader);
                m_DirtyAABB = reader.ReadBoolean();
                reader.AlignStream();
            }
        }
    }
}
