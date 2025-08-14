using System;
using System.Security.Cryptography;

namespace AssetStudio;

public interface IUnityCN
{
    public class Entry
    {
        public string Name { get; private set; }
        public string Key { get; private set; }

        public Entry(string name, string key)
        {
            Name = name;
            Key = key;
        }

        public bool Validate()
        {
            var bytes = Convert.FromHexString(Key);
            if (bytes.Length != 0x10)
            {
                Logger.Warning($"[UnityCN] {this} has invalid key, size should be 16 bytes, skipping...");
                return false;
            }

            return true;
        }

        public override string ToString() => $"{Name} ({Key})";
    }

    public static Entry entry;
    protected static ICryptoTransform? _encryptor;
    
    public static bool SetKey(Entry entry)
    {
        Logger.Verbose($"Initializing decryptor with key {entry.Key}");
        if (entry.Key.Length != 32 && entry.Key.Length != 16)
            throw new ArgumentException("key must be 32 or 16 characters long");
        try
        {
            using var aes = Aes.Create();
            aes.Mode = CipherMode.ECB;
            aes.Key = Convert.FromHexString(entry.Key);

            _encryptor = aes.CreateEncryptor();
            Logger.Verbose($"Decryptor initialized !!");
        }
        catch (Exception e)
        {
            Logger.Error($"[UnityCN] Invalid key !!\n{e.Message}");
            return false;
        }
        return true;
    }
    
    public void DecryptBlock(Span<byte> bytes, int size, int index);
}