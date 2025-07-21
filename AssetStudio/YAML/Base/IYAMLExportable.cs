namespace AssetStudio
{
    public interface IYAMLExportable
    {
        YAMLNode ExportYAML(UnityVersion version);
    }
}
