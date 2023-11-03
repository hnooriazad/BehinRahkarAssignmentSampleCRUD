using Microsoft.AspNetCore.Mvc;
using SampleCRUD.Models;
using System;
using System.Data.Repository;
using System.Threading.Tasks;

[Route("api/devices")]
[ApiController]
public class DeviceController : ControllerBase
{
    //private readonly DeviceRepository _deviceRepository;

    //public DeviceController(DeviceRepository deviceRepository)
    //{
    //    _deviceRepository = deviceRepository;
    //}

    //[HttpGet]
    //public async Task<IActionResult> GetAllDevices()
    //    {
    //    var devices = await _deviceRepository.GetAllDevicesAsync();
    //    return Ok(devices);
    //}

    //[HttpGet("{deviceId}")]
    //public async Task<IActionResult> GetDevice(Guid deviceId)
    //{
    //    var device = await _deviceRepository.GetDeviceByIdAsync(deviceId);
    //    if (device == null)
    //    {
    //        return NotFound();
    //    }
    //    return Ok(device);
    //}

    //[HttpPost]
    //public async Task<IActionResult> CreateDevice(Device device)
    //{
    //    await _deviceRepository.CreateDeviceAsync(device);
    //    return CreatedAtAction(nameof(GetDevice), new { deviceId = device.DeviceId }, device);
    //}

    //[HttpPut("{deviceId}")]
    //public async Task<IActionResult> UpdateDevice(Guid deviceId, Device device)
    //{
    //    if (deviceId != device.DeviceId)
    //    {
    //        return BadRequest();
    //    }

    //    var existingDevice = await _deviceRepository.GetDeviceByIdAsync(deviceId);
    //    if (existingDevice == null)
    //    {
    //        return NotFound();
    //    }

    //    await _deviceRepository.UpdateDeviceAsync(device);
    //    return NoContent();
    //}

    //[HttpDelete("{deviceId}")]
    //public async Task<IActionResult> DeleteDevice(Guid deviceId)
    //{
    //    var device = await _deviceRepository.GetDeviceByIdAsync(deviceId);
    //    if (device == null)
    //    {
    //        return NotFound();
    //    }

    //    await _deviceRepository.DeleteDeviceAsync(deviceId);
    //    return NoContent();
    //}
    private readonly DeviceRepository _deviceRepository;

    public DeviceController(DeviceRepository deviceRepository)
    {
        _deviceRepository = deviceRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDevices()
    {
        var devices = await _deviceRepository.GetAllDevicesAsync();
        return Ok(devices);
    }

    [HttpGet("{deviceId}")]
    public async Task<IActionResult> GetDevice(Guid deviceId)
    {
        var device = await _deviceRepository.GetDeviceByIdAsync(deviceId);
        if (device == null)
        {
            return NotFound();
        }
        return Ok(device);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDevice(Device device)
    {
        await _deviceRepository.CreateDeviceAsync(device);
        return CreatedAtAction(nameof(GetDevice), new { deviceId = device.DeviceId }, device);
    }

    [HttpPut("{deviceId}")]
    public async Task<IActionResult> UpdateDevice(Guid deviceId, Device device)
    {
        if (deviceId != device.DeviceId)
        {
            return BadRequest();
        }

        await _deviceRepository.UpdateDeviceAsync(device);
        return NoContent();
    }

    [HttpDelete("{deviceId}")]
    public async Task<IActionResult> DeleteDevice(Guid deviceId)
    {
        await _deviceRepository.DeleteDeviceAsync(deviceId);
        return NoContent();
    }

    [HttpGet("raw")]
    public async Task<IActionResult> GetDevicesByRawQuery()
    {
        var sql = "SELECT * FROM Device"; // Your raw query
        var devices = await _deviceRepository.GetDevicesByRawQueryAsync(sql);
        return Ok(devices);
    }
}
