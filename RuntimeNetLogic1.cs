#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.UI;
using FTOptix.NativeUI;
using FTOptix.HMIProject;
using FTOptix.RAEtherNetIP;
using FTOptix.Retentivity;
using FTOptix.CoreBase;
using FTOptix.CommunicationDriver;
using FTOptix.Core;
using FTOptix.NetLogic;
#endregion

public class RuntimeNetLogic1 : BaseNetLogic
{
    private LogicCommand startCommand;
    private LogicCommand jogCommand;
    private LogicCommand stopCommand;

        public override void Start()
        {
        // Insert code to be executed when the user-defined logic is started
        // Get references to the Logic Commands
        startCommand = Project.Current.Get<LogicCommand>("/Objects/drive_tmrwlab_screen_april17/CommDrivers/RAEtherNet_IPDriver2/RAEtherNet_IPStation2/Tags/Controller Tags/Drive_1&:O/LogicCommand_Start"); // Replace YourPageName
        jogCommand = Project.Current.Get<LogicCommand>("/Objects/drive_tmrwlab_screen_april17/CommDrivers/RAEtherNet_IPDriver2/RAEtherNet_IPStation2/Tags/Controller Tags/Drive_1&:O/LogicCommand_Jog1");   // Replace YourPageName and YourJogLogicCommandName
        stopCommand = Project.Current.Get<LogicCommand>("/Objects/drive_tmrwlab_screen_april17/CommDrivers/RAEtherNet_IPDriver2/RAEtherNet_IPStation2/Tags/Controller Tags/Drive_1&:O/LogicCommand_Stop"); // Replace YourPageName and YourStopLogicCommandName
            if (startCommand == null || jogCommand == null || stopCommand == null)
            {
            Log.Error("RuntimeNetLogic1: One or more Logic Commands not found!");
            }
        
        }

        public override void Stop()
        {
        // Insert code to be executed when the user-defined logic is stopped
        }

        //  Start button's associated NetLogic is executed
            [ExportMethod]
            public void ExecuteStart()
            {
                if (startCommand != null)
                {
                startCommand.Execute();
                Log.Info("RuntimeNetLogic1: Start command executed from C#.");
                }
            }

            // Jog button's associated NetLogic is executed
            [ExportMethod]
            public void ExecuteJog()
            {
                if (jogCommand != null)
                {
                jogCommand.Execute();
                Log.Info("RuntimeNetLogic1: Jog command executed from C#.");
                }
            }

            // Stop button's associated NetLogic is executed
            [ExportMethod]
            public void ExecuteStop()
            {
                if (stopCommand != null)
                {
                stopCommand.Execute();
                Log.Info("RuntimeNetLogic1: Stop command executed from C#.");
                }
            }
}
    

