using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Common.Exceptions;

namespace testIoTAlangulo
{
    class Program
    {
        static RegistryManager registryManager;
        static string connectionString= System.Configuration.ConfigurationManager.AppSettings["connectionString"];
    

        static void Main(string[] args)
        {
            registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            RegisterDeviceAsync().Wait();
            Console.ReadLine();
        }

        private static async Task RegisterDeviceAsync()
        {
            string deviceId = "medicaldevice-patient1";
            Device device;
            try
            {
                device = await registryManager.AddDeviceAsync(new Device(deviceId));
            }

            catch (DeviceAlreadyExistsException)
            {
                device= await registryManager.AddDeviceAsync(new Device(deviceId));
            }

            Console.WriteLine("Device shared access key:" + device.Authentication.SymmetricKey.PrimaryKey);
        }
    }
}
