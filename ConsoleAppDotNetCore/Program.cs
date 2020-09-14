using System;
using System.Linq;

using Microsoft.Management.Infrastructure;

namespace ConsoleAppDotNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            PrintMonitorInfos();
        }

        private static void PrintMonitorInfos()
        {
            var mySession = CimSession.Create(null);

            var queryInstances =
                mySession.QueryInstances(@"root\wmi",
                    "WQL",
                    @"select * from WmiMonitorID");

            foreach (var queryInstance in queryInstances)
            {
                foreach (var property in queryInstance.CimInstanceProperties)
                {
                    var value = property.CimType == CimType.UInt16Array
                        ? ConvertUInt16ArrayToString(property)
                        : Convert.ToString(property.Value);

                    Console.WriteLine(property.Name + " " + value);
                }
            }
        }

        private static string ConvertUInt16ArrayToString(CimProperty cimInstanceProperty)
        {
            var value = cimInstanceProperty.Value;
            if (value == null)
            {
                return String.Empty;
            }
            var ushortValues = (ushort[])value;

            var listOfChars = ushortValues.Select(x => Char.ConvertFromUtf32(Convert.ToInt32(x)));

            return string.Join("", listOfChars);
        }
    }
}
