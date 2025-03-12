using AdvancedSharpAdbClient;
using AdvancedSharpAdbClient.Models;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Runtime.InteropServices;

namespace MistyDroid.Server.Services;

public class AdbSession
{
    AdbClient adbClient;
    DeviceData device;

    public AdbSession()
    {
        adbClient = new AdbClient();
        adbClient.Connect("127.0.0.1:62001");

        device = adbClient.GetDevices().FirstOrDefault(_device => _device.Serial == "localhost:5555");
    }


    public async Task<byte[]> Screenshot()
    {
        var framebuffer = await adbClient.GetFrameBufferAsync(device);

        using var pinnedFrame = new AutoPinner(framebuffer.Data!);

        var screenshotFilename = "screenshot.png";

        var img = new Image<Rgba, Byte>((int)framebuffer.Header.Width, (int)framebuffer.Header.Height, (int)framebuffer.Header.Width * 4, pinnedFrame);
        img.Save(screenshotFilename);

        return await File.ReadAllBytesAsync(screenshotFilename);

    }
}


class AutoPinner : IDisposable
{
    GCHandle _pinnedArray;
    public AutoPinner(Object obj)
    {
        _pinnedArray = GCHandle.Alloc(obj, GCHandleType.Pinned);
    }
    public static implicit operator IntPtr(AutoPinner ap)
    {
        return ap._pinnedArray.AddrOfPinnedObject();
    }
    public void Dispose()
    {
        _pinnedArray.Free();
    }
}