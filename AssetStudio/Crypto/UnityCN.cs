using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace AssetStudio
{
    public sealed unsafe class UnityCN: IUnityCN
    {
        private const string Signature = "#$unity3dchina!@";
        
        public UInt32 Value;
        public readonly byte[] InfoBytes;
        public readonly byte[] InfoKey;
        public readonly byte[] SignatureBytes;
        public readonly byte[] SignatureKey;

        public readonly byte[] Index = new byte[0x10];
        public readonly byte[] Sub = new byte[0x10];
    
        private GCHandle _subHandle;
        private GCHandle _indexHandle;
        private readonly byte* _subPtr;
        private readonly byte* _indexPtr;

        private readonly bool _isIndexSpecial = true;
        
        public UnityCN() {}
        
        public UnityCN(EndianBinaryReader reader)
        {
            Value = reader.ReadUInt32();
            InfoBytes = reader.ReadBytes(0x10);
            InfoKey = reader.ReadBytes(0x10);
            reader.Position += 1;

            SignatureBytes = reader.ReadBytes(0x10);
            SignatureKey = reader.ReadBytes(0x10);
            reader.Position += 1;

            Reset();
            
            for (int i = 0; i < Index.Length; i++)
            {
                if (Index[i] != i)
                {
                    _isIndexSpecial = false;
                    break;
                }
            }
            
            _subHandle = GCHandle.Alloc(Sub, GCHandleType.Pinned);
            _indexHandle = GCHandle.Alloc(Index, GCHandleType.Pinned);

            _subPtr = (byte*)_subHandle.AddrOfPinnedObject();
            _indexPtr = (byte*)_indexHandle.AddrOfPinnedObject();
        }
        
        ~UnityCN()
        {
            if (_subHandle.IsAllocated) _subHandle.Free();
            if (_indexHandle.IsAllocated) _indexHandle.Free();
        }
        
        public void Reset()
        {
            var infoBytes = (byte[])InfoBytes.Clone();
            var infoKey = (byte[])InfoKey.Clone();
            var signatureBytes = (byte[])SignatureBytes.Clone();
            var signatureKey = (byte[])SignatureKey.Clone();
        
            XorWithKey(signatureKey, signatureBytes);

            var str = Encoding.UTF8.GetString(signatureBytes);
            if (str != Signature)
            {
                throw new Exception($"Invalid Signature, Expected {Signature} but found {str} instead");
            }
        
            XorWithKey(infoKey, infoBytes);
            var buffer = new byte[infoBytes.Length * 2];
            for (var i = 0; i < infoBytes.Length; i++)
            {
                var idx = i * 2;
                buffer[idx] = (byte)(infoBytes[i] >> 4);
                buffer[idx + 1] = (byte)(infoBytes[i] & 0xF);
            }
            buffer.AsSpan(0, 0x10).CopyTo(Index);
            var subBytes = buffer.AsSpan(0x10, 0x10);
            for (var i = 0; i < subBytes.Length; i++)
            {
                var idx = (i % 4 * 4) + (i / 4);
                Sub[idx] = subBytes[i];
            }
        }
        
        private void XorWithKey(byte[] key, byte[] data)
        {
            key = IUnityCN._encryptor.TransformFinalBlock(key, 0, key.Length);
            for (int i = 0; i < 0x10; i++)
                data[i] ^= key[i];
        }

        public void DecryptBlock(Span<byte> bytes, int size, int index)
        {
            var offset = 0;
            while (offset < size)
            {
                offset += Decrypt(bytes[offset..], index++, size - offset);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private byte DecryptByteUnsafe(byte* p, int index)
        {
            var b = *p;
            var mb = _subPtr[index & 3]
                     + _subPtr[((index >> 2) & 3) + 4]
                     + _subPtr[((index >> 4) & 3) + 8]
                     + _subPtr[((byte)index >> 6) + 12];
            *p = _isIndexSpecial ?
                (byte)(((b & 0xF) - mb) & 0xF | ((b >> 4) - mb) << 4) :
                (byte)((_indexPtr[b & 0xF] - mb) & 0xF | (_indexPtr[b >> 4] - mb) << 4);
            return *p;
        }

        private int Decrypt(Span<byte> bytes, int index, int remaining)
        {
            fixed (byte* ptr = &bytes.GetPinnableReference())
            {
                byte* p = ptr;
                while (p - ptr < remaining)
                {
                    var innerIndex = index;
                    var token = DecryptByteUnsafe(p++, innerIndex++);
                    var literalLength = token >> 4;
                    var matchLength = token & 0xF;

                    if (literalLength == 0xF)
                    {
                        int b;
                        do
                        {
                            b = DecryptByteUnsafe(p++, innerIndex++);
                            literalLength += b;
                        } while (b == 0xFF);
                    }

                    p += literalLength;
                    if (p - ptr == remaining && matchLength == 0) break;
                    if (p - ptr >= remaining) throw new Exception("Invalid compressed data");
                    
                    DecryptByteUnsafe(p++, innerIndex++);
                    DecryptByteUnsafe(p++, innerIndex++);
                    if (matchLength == 0xF)
                    {
                        int b;
                        do
                        {
                            b = DecryptByteUnsafe(p++, innerIndex++);
                        } while (b == 0xFF);
                    }
                    index++;
                }

                return  (int)(p - ptr);
            }
        }
    }
}