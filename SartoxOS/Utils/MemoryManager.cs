using Cosmos.Core;

namespace SartoxOS.Utils
{
    public static class MemoryManager
    {
        public static uint UsedMemory()
        {
            return (CPU.GetEndOfKernel() + 1024) / 1048576;
        }
        public static uint TotalMemory()
        {
            return CPU.GetAmountOfRAM();
        }
    }
}
