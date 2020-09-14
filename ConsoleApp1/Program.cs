using System;
using System.Collections.Generic;
using Microsoft.Management.Infrastructure;
using Microsoft.Management.Infrastructure.Options;

namespace ConsoleApp1
{
    class Program
    {

        /*
         * "function Decode {If ($args[0] -is [System.Array]) {[System.Text.Encoding]::ASCII.GetString($args[0])}
         * Else {\"Not Found\"}}
         *
         *
         * $Monitors = Get-CimInstance WmiMonitorID -Namespace root\\wmi;
         * ForEach ($Monitor in $monitors) {$Serial = Decode $Monitor.SerialNumberID -notmatch 0; if($Serial -ne 0){return $Serial}}");
         */
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string Namespace = @"root\wmi";
            string className = "WmiMonitorID";

            CimInstance myDrive = new CimInstance(className, Namespace);

            //myDrive.CimInstanceProperties.Add(CimProperty.Create("SerialNumberID", null, CimFlags.Key)); //CimProperty.Create("SerialNumberID", "C:", CimFlags.Key));
            
            CimSession mySession = CimSession.Create(null);
            
            CimInstance searchInstance = mySession.GetInstance(Namespace, myDrive);


            IEnumerable<CimInstance> queryInstances =
                mySession.QueryInstances(@"root\wmi",
                    "WQL",
                    @"select * from WmiMonitorID");
        }
    }
}
