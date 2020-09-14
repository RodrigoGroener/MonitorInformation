using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Management.Infrastructure;
using CimType = Microsoft.Management.Infrastructure.CimType;

namespace ConsoleApp2
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
            CimSession mySession = CimSession.Create(null);

            IEnumerable<CimInstance> queryInstances =
                mySession.QueryInstances(@"root\wmi",
                    "WQL",
                    @"select * from WmiMonitorID");

            foreach (var queryInstance in queryInstances)
            {
                foreach (var cimInstanceProperty in queryInstance.CimInstanceProperties)
                {
                    var value = cimInstanceProperty.CimType == CimType.UInt16Array
                        ? ConvertUInt16ArrayToString(cimInstanceProperty)
                        : Convert.ToString(cimInstanceProperty.Value);

                    Console.WriteLine(cimInstanceProperty.Name + " " + value);
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
            var ushortValues = (ushort[]) value;
            
            var listOfChars = ushortValues.Select(x => Char.ConvertFromUtf32(Convert.ToInt32(x)));
            
            return string.Join("", listOfChars);
        }
    }
}
