using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProjectApp.Models;
using MiniProjectApp.Models.DTO;
using MiniProjectApp.Repositories.Interface;

namespace MiniProjectApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository<int,User> _userRepository;
        private readonly ICompositeKeyRepository<int, SuperCart> _superCartRepository;
        public UserController(IRepository<int,User> userRepository, ICompositeKeyRepository<int, SuperCart> superCartRepository)
        {
            _userRepository = userRepository;
            _superCartRepository = superCartRepository;
        }

        [HttpGet]
        public async Task<ActionResult<DummyDTO>> Login(int id)
        {
            User user = await _userRepository.GetByKey(id);

            var cartItems = user.SuperCartItems.ToList();

            foreach (var cartItem in cartItems)
            {
                Console.WriteLine("Book Id" + cartItem.BookId);
            }

           DummyDTO dto = new DummyDTO();

            dto.UserId = user.Id;
            dto.UserName = user.Name;

            return dto;


        }


        [HttpGet]
        [Route("/DeleteCartItem")]
        public async Task<ActionResult<SuperCart>> DeleteCartItem(int userId, int BookId)
        {
            SuperCart Item = await _superCartRepository.DeleteByKey(userId, BookId);

            

            return Item;


        }



    }
}
