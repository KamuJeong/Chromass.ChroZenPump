using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CDS.Core;
using CDS.InstrumentModel;
using Communicator;

namespace Chromass.ChroZenPump;
public class ChroZenPump : Device
{
    public API API
    {
        get; private set;
    }

    public ChroZenPump(ModelBase? parent, string? name) : base(parent, name)
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
            new Method(new APIs.Information(API.Information),
                        new APIs.Configuration(API.Configuration),
                        new APIs.Setup(API.Setup))
            );
    }

    public override bool SetMethod(IMethod? method)
    {
        if (method?.GetMethod(this) is Method m)
        {
            API.Setup.Assign(m.Setup);
            return true;
        }
        return false;
    }

    protected override bool SendMethod()
    {
        API.Setup.CallAction();
        return true;
    }

    protected async override Task<bool> LoadMethodAsync()
    {
        return await API.Controller.AskSetupAsync(1000);
    }

    protected override void CheckReadyStatus()
    {
        if (new[] { DeviceStatus.NotReady, DeviceStatus.Ready }.Contains(Status))
        {
            Status = (API.State.Flow > 0.0 && API.Setup.Flow == API.State.Flow) ?
                DeviceStatus.Ready : DeviceStatus.NotReady;
        }
    }
    
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
