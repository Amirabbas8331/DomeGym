
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Admins;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Admins.Persistence;

public class AdminRepository : IAdminRepository
{
    private readonly GymManagementDbContext _context;
    public AdminRepository(GymManagementDbContext context)
    {
        _context = context;

    }
    public async Task<Admin?> GetByIdAsync(Guid adminId)
    {
      return  await _context.Admins.FirstOrDefaultAsync(x => x.Id == adminId);
    }

    public Task UpdateAsync(Admin admin)
    {
        _context.Admins.Update(admin);
        return Task.CompletedTask;
    }
}
