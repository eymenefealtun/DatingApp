using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            // return await _context.Users          //Mapping without AutoMapper
            // .Where(x => x.UserName == username)
            // .Select(user => new MemberDto
            // {
            //     Id = user.Id,
            //     UserName = user.UserName,
            //     KnownAs = user.KnownAs,

            // }).SingleOrDefaultAsync();


            return await _context.Users
                  .Where(x => x.UserName == username)
                  .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();

        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            return await _context.Users
             .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
             .ToListAsync();
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
            .Include(p => p.Photos)
            .SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users
            .Include(p => p.Photos) //it is going to add the related entity
            .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0; // If number of changes is 0  it is going to return false. If it is greater than zero return true  
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified; // tells ef tracker that something has changed with the given entity //we are not saving here!!
        }

    }
}