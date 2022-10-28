using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CDS.Core;
using CDS.InstrumentModel;
using Communicator;
using Microsoft.UI.Xaml.Controls;
using Chromass.ChroZenPump;
using Chromass.ChroZenPump.APIs;

namespace CDS.Chromass.ChroZenPump;

[Refer("Monitor", typeof(CDS.Chromass.ChroZenPump.Views.MonitorView))]
[Refer("Controller", typeof(CDS.Chromass.ChroZenPump.Views.ControllerView))]
public class ChroZenPumpDevice : Device
{
    public API API
    {
        get; private set;
    }

    public ChroZenPumpDevice(ModelBase? parent, string? name) : base(parent, name)
    {
        API = new API(new Tcp());
    }

    public override TimeSpan RunTime
        => TimeSpan.FromMinutes(
                Math.Max(API.Setup.Gradients.LastOrDefault()?.Time ?? 0.0,
                            API.Setup.Events.LastOrDefault()?.Time ?? 0.0)
            );

    public async override Task<bool> ConnectAsync(CancellationToken token)
    {
        if (Uri == null)
            throw new InvalidOperationException("set an URI before connection");

        try
        {
            await API.ConnectAsync(Uri.Host, Uri.Port, token);
        }
        catch(Exception ex)
        {
            Debug.WriteLine(ex);
        }
        return API.IsConnected;
    }

    public override void Disconnect() => API.Close();

    public override void GetMethod(IMethod? method)
    {
        method?.SetMethod(this,
            new Method(new Information(API.Information),
                        new Configuration(API.Configuration),
                        new Setup(API.Setup))
            );
    }

    protected override bool SendMethod(IMethod? method)
    {
        if (method?.GetMethod(this) is Method m)
        {
            API.Setup.Assign(m.Setup);
            return true;
        }
        return false;
    }

    protected async override Task<bool> LoadMethodAsync(IMethod? method)
    {
        if (await API.LoadSetupAsync())
        {
            GetMethod(method);
            return true;
        }
        return false;
    }

    protected override bool CheckReadyStatus() => API.State.Flow > 0.0 && API.Setup.Flow == API.State.Flow;
    
    protected override void Halt() => API.Halt();
    protected override bool Ready()
    {
        API.Ready();
        Status = DeviceStatus.NotReady;
        return true;
    }

    protected override bool PreRun() => false;
    
    protected override bool Run()
    {
        if (RunTime > TimeSpan.Zero)
        {
            API.Run();
            Status = DeviceStatus.Run;
        }
        return false;
    }

    protected override bool PostRun() => false;
    protected override bool PostWork() => false;
    protected override void Reset()
    {
        if (API.State.Status == Statuses.Error)
            API.ResetError();
    }

    protected override void Stop() => API.Stop();
}
