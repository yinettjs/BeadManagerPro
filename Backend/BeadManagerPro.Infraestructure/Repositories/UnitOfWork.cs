using Microsoft.EntityFrameworkCore;
using BeadManagerPro.Domain.Entities.Encargos;
using BeadManagerPro.Domain.Entities.Usuarios;

namespace BeadManagerPro.Infrastructure.Repositories;

// 1. INTERFACES ADAPTADAS
public interface IPedidoRepository : IGenericRepository<Pedido>
{
    Task<IEnumerable<Pedido>> GetAllWithDetailsAsync();
    Task<Pedido?> GetWithDetailsByIdAsync(int id);
    Task UpdateAsync(Pedido pedido);
}

public interface IArtesanaRepository : IGenericRepository<Artesana>
{
    Task<IEnumerable<Artesana>> GetAllWithPedidosAsync();
}

public interface IUnitOfWork : IDisposable
{
    IPedidoRepository Pedidos { get; }
    IArtesanaRepository Artesanas { get; }
    IGenericRepository<Cliente> Clientes { get; }
    IGenericRepository<Pieza> Piezas { get; }
    IGenericRepository<Material> Materiales { get; }
    Task<int> SaveChangesAsync();
}

// 2. IMPLEMENTACIONES
public class PedidoRepository : GenericRepository<Pedido>, IPedidoRepository
{
    public PedidoRepository(DbContext ctx) : base(ctx) { }

    public async Task<IEnumerable<Pedido>> GetAllWithDetailsAsync() =>
        await _dbSet
            .Include(p => p.Cliente)
            .Include(p => p.Artesana)
            .Where(p => !p.IsDeleted)
            .ToListAsync();

    public async Task<Pedido?> GetWithDetailsByIdAsync(int id) =>
        await _dbSet
            .Include(p => p.Cliente)
            .Include(p => p.Artesana)
            .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

    public Task UpdateAsync(Pedido pedido)
    {
        throw new NotImplementedException();
    }
}

public class ArtesanaRepository : GenericRepository<Artesana>, IArtesanaRepository
{
    public ArtesanaRepository(DbContext ctx) : base(ctx) { }

    public async Task<IEnumerable<Artesana>> GetAllWithPedidosAsync() =>
        await _dbSet
            .Include(a => a.Pedidos)
            .Where(a => !a.IsDeleted)
            .ToListAsync();
}

// 3. LA UNIDAD DE TRABAJO (UnitOfWork)
public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;

    public IPedidoRepository Pedidos { get; }
    public IArtesanaRepository Artesanas { get; }
    public IGenericRepository<Cliente> Clientes { get; }
    public IGenericRepository<Pieza> Piezas { get; }
    public IGenericRepository<Material> Materiales { get; }

    public UnitOfWork(DbContext context)
    {
        _context = context;
        Pedidos = new PedidoRepository(context);
        Artesanas = new ArtesanaRepository(context);
        Clientes = new GenericRepository<Cliente>(context);
        Piezas = new GenericRepository<Pieza>(context);
        Materiales = new GenericRepository<Material>(context);
    }

    public async Task<int> SaveChangesAsync() =>
        await _context.SaveChangesAsync();

    public void Dispose() =>
        _context.Dispose();
}

