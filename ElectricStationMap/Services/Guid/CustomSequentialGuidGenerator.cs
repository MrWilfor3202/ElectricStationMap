using System.Runtime.InteropServices;

namespace ElectricStationMap.Services.Guid
{
    public class CustomSequentialGuidGenerator : ISequentialGuidGenerator
    {
        public System.Guid CreateGuid()
        {
            System.Guid guid;

            int result = UuidCreateSequential(out guid);

            if (result == 0)
            {
                var bytes = guid.ToByteArray();
                var indexes = new int[] { 3, 2, 1, 0, 5, 4, 7,
                    6, 8, 9, 10, 11, 12, 13, 14, 15 };

                return new System.Guid(indexes.Select(i => bytes[i]).ToArray());
            }
            else
                throw new Exception("Error generating sequential GUID");

        }

        [DllImport("rpcrt4.dll", SetLastError = true)]
        private static extern int UuidCreateSequential(out System.Guid guid); 
    }
}
