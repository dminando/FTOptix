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

public class WheelRotation_P1 : BaseNetLogic
{
    private IImage wheelImage;
    private ITag liveFeedbackFreqTag;
    private double currentRotationAngle = 0;
    private double rotationSpeedScale = 10.0; // Adjust for desired base speed
    private Timer rotationTimer;
    private DateTime lastUpdateTime;

    public override void Start()
    {
        wheelImage = (IImage)Owner.Get("wheel");                     // Get a reference to the "wheel" image
        liveFeedbackFreqTag = Owner.GetVariable("LiveFeedbackFreq"); // Get a reference to the "LiveFeedbackFreq" tag
            if (wheelImage == null || liveFeedbackFreqTag == null)
            {
                Log.Error("WheelRotationScript_Phase1: Could not find 'wheel' image or 'LiveFeedbackFreq' tag on the same container.");
                return;
            }
            // Initialize the rotation timer
            rotationTimer = new Timer(30); // Update every 30 milliseconds
            rotationTimer.Elapsed += OnRotationTimerElapsed;
            rotationTimer.AutoReset = true;
            rotationTimer.Start();

            lastUpdateTime = DateTime.Now;
    

    }

    public override void Stop()
    {
        // Stop and dispose of the timer
        if (rotationTimer != null)
        {
            rotationTimer.Stop();
            rotationTimer.Dispose();
            rotationTimer = null;
        }
    }

    private void OnRotationTimerElapsed(object sender, ElapsedEventArgs e)
    {
        if (wheelImage == null || liveFeedbackFreqTag == null)
            return;

        DateTime currentTime = DateTime.Now;
        double deltaTime = (currentTime - lastUpdateTime).TotalSeconds;
        lastUpdateTime = currentTime;

        // Read the current frequency value
        double feedbackValue = Convert.ToDouble(liveFeedbackFreqTag.Value);

        // Determine rotation direction and speed
        double rotationSpeed = feedbackValue * rotationSpeedScale;

        // Update the rotation angle
        currentRotationAngle += rotationSpeed * deltaTime;

        // Apply the new rotation angle to the image
        wheelImage.Rotation = currentRotationAngle;
    }

}
