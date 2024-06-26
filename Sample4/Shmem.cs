using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample4
{
    public class SharedMemoryHelper : IDisposable
    {
        private MemoryMappedFile m_hMemoryMapped = null;
        private MemoryMappedViewAccessor m_hMemoryAccessor = null;
        private long m_nCapacity;

        ~SharedMemoryHelper() { Dispose(); }

        public void CreateSharedMemory(string mapName, long capacity = 2048)
        {
            m_nCapacity = capacity;
            m_hMemoryMapped = MemoryMappedFile.CreateNew(mapName, capacity);
            m_hMemoryAccessor = m_hMemoryMapped.CreateViewAccessor();
        }

        public void OpenSharedMemory(string mapName, long capacity = 2048)
        {
            m_nCapacity = capacity;
            m_hMemoryMapped = MemoryMappedFile.OpenExisting(mapName);
            m_hMemoryAccessor = m_hMemoryMapped.CreateViewAccessor();
        }

        public byte[] ReadMemory()
        {
            byte[] bytes = new byte[m_nCapacity];
            m_hMemoryAccessor.ReadArray<byte>(0, bytes, 0, bytes.Length);
            return bytes;
        }

        public void WriteMemory(byte[] bytes)
        {
            m_hMemoryAccessor.WriteArray<byte>(0, bytes, 0, bytes.Length);
        }

        public void Dispose()
        {
            if (m_hMemoryAccessor != null)
            {
                m_hMemoryAccessor.Dispose();
                m_hMemoryAccessor = null;
            }
            if (m_hMemoryMapped != null)
            {
                m_hMemoryMapped.Dispose();
                m_hMemoryMapped = null;
            }
        }
    }
}
