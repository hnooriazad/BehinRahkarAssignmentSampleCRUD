using Dapper;
using Microsoft.EntityFrameworkCore;
using SampleCRUD.Data.Context;
using SampleCRUD.Models;
using System.Data;

namespace System.Data.Repository
{

    //public class DeviceRepository
    //{
    //    private readonly IDbConnection _connection;

    //    public DeviceRepository(IDbConnection connection)
    //    {
    //        _connection = connection;
    //    }

    //    public async Task<IEnumerable<Device>> GetAllDevicesAsync()
    //    {
    //        var sql = "SELECT * FROM Device";
    //        return await _connection.QueryAsync<Device>(sql);
    //    }

    //    public async Task<Device> GetDeviceByIdAsync(Guid deviceId)
    //    {
    //        var sql = "SELECT * FROM Device WHERE DeviceId = @DeviceId";
    //        return await _connection.QueryFirstOrDefaultAsync<Device>(sql, new { DeviceId = deviceId });
    //    }

    //    public async Task CreateDeviceAsync(Device device)
    //    {
    //        var sql = "INSERT INTO Device (DeviceId, DeviceName, DeviceCode) " +
    //                  "VALUES (@DeviceId, @DeviceName, @DeviceCode)";
    //        await _connection.ExecuteAsync(sql, device);
    //    }

    //    public async Task UpdateDeviceAsync(Device device)
    //    {
    //        var sql = "UPDATE Device " +
    //                  "SET DeviceName = @DeviceName, DeviceCode = @DeviceCode " +
    //                  "WHERE DeviceId = @DeviceId";
    //        await _connection.ExecuteAsync(sql, device);
    //    }

    //    public async Task DeleteDeviceAsync(Guid deviceId)
    //    {
    //        var sql = "DELETE FROM Device WHERE DeviceId = @DeviceId";
    //        await _connection.ExecuteAsync(sql, new { DeviceId = deviceId });
    //    }
    //}

    public class DeviceRepository
    {
        private readonly SimpleCRUDDbContext _context;
        private readonly IDbConnection _connection;

        public DeviceRepository(SimpleCRUDDbContext context, IDbConnection connection)
        {
            _context = context;
            _connection = connection;
        }

        // Create
        public async Task CreateDeviceAsync(Device device)
        {
            _context.Device.Add(device);
            await _context.SaveChangesAsync();
        }

        // Read
        public async Task<IEnumerable<Device>> GetAllDevicesAsync()
        {
            return await _context.Device.ToListAsync();
        }

        public async Task<Device> GetDeviceByIdAsync(Guid deviceId)
        {
            return await _context.Device.FirstOrDefaultAsync(d => d.DeviceId == deviceId);
        }

        public async Task<IEnumerable<Device>> GetDevicesByRawQueryAsync(string sql)
        {
            return await _connection.QueryAsync<Device>(sql);
        }

        // Update
        public async Task UpdateDeviceAsync(Device device)
        {
            _context.Entry(device).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // Delete
        public async Task DeleteDeviceAsync(Guid deviceId)
        {
            var device = await _context.Device.FirstOrDefaultAsync(d => d.DeviceId == deviceId);
            if (device != null)
            {
                _context.Device.Remove(device);
                await _context.SaveChangesAsync();
            }
        }
    }

}