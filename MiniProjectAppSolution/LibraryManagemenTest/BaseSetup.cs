using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MiniProjectApp.BussinessLogics;
using MiniProjectApp.BussinessLogics.Services;
using MiniProjectApp.Context;
using MiniProjectApp.Models;
using MiniProjectApp.Repositories;
using MiniProjectApp.Repositories.Interface;
using MiniProjectApp.Services;
using MiniProjectApp.Services.Interfaces;
using Moq;


namespace LibraryManagemenTest
{
   public class BaseSetup
   {

       public LibraryManagementContext context;

       private IRepository<int, Book> _bookRepository;
       public IRepository<int, Feedback> _feedbackRepository;
       public ICompositeKeyRepository<int, Cart> _cartRepository;
        
       public IRepository<int, Fine> _fineRepository;
       public ICompositeKeyRepository<int, FineDetail> _fineDetailRepository;
       public IRepository<int, Purchase> _purchaseRepository;
       public ICompositeKeyRepository<int, PurchaseDetail> _purchaseDetailRepository;
       public ICompositeKeyRepository<int, RentCart> _rentCartRepository;
       public IRepository<int, Rent> _rentRepository;
       public ICompositeKeyRepository<int, RentDetail> _rentDetailRepository;
       public IRepository<int, RentStock> _rentStockRepository;
       public IRepository<int, Sale> _saleRepository;
       public ICompositeKeyRepository<int, SaleDetail> _saleDetailRepository;
       public IRepository<int, SalesStock> _saleStockRepository;
       public ICompositeKeyRepository<int, SuperRentCart> _superRentCartRepository;
       public ITransactionRepository  _transactionRepository;

       private IRepository<int, User> _userRepository;
       private IRepository<int, UserCredential> _userCredentialRepository;



       public IBookServices _bookServices;
       public ICartServices _cartServices;
       public IRentServices _rentServices;
       public ISaleServices _saleServices;
       public IPurchaseServices _purchaseServices;
       public ITokenService _tokenService;
       public IAuthService _authServices;
       public IUserValidationService _userValidationService;
       public IFineServices _fineServices;


       [SetUp]
       public async Task Setup()
       {

           var options = new DbContextOptionsBuilder<LibraryManagementContext>()
                         .UseSqlite("DataSource=:memory:")
                         .Options;

           context = new LibraryManagementContext(options);

           context.Database.OpenConnection();
           context.Database.EnsureDeleted();
           context.Database.EnsureCreated();


           Mock<IConfigurationSection> configurationJWTSection = new Mock<IConfigurationSection>();
           configurationJWTSection.Setup(x => x.Value).Returns("This is the dummy key which has to be a bit long for the 512. which should be even more longer for the passing");
           Mock<IConfigurationSection> congigTokenSection = new Mock<IConfigurationSection>();
           congigTokenSection.Setup(x => x.GetSection("JWT")).Returns(configurationJWTSection.Object);
           Mock<IConfiguration> mockConfig = new Mock<IConfiguration>();
           mockConfig.Setup(x => x.GetSection("TokenKey")).Returns(congigTokenSection.Object);





           
           _feedbackRepository = new FeedbackRepository(context);
           _bookRepository = new BookRepository(context);

           _cartRepository = new CartRepository(context);
           _fineRepository = new FineRepository(context);
           _fineDetailRepository = new FineDetailRepository(context);
           _purchaseRepository = new PurchaseRepository(context);
           _purchaseDetailRepository = new PurchaseDetailRepository(context);
           _rentCartRepository = new RentCartRepository(context);
           _rentRepository = new RentRepository(context);
           _rentDetailRepository = new RentDetailRepository(context);
           _rentStockRepository = new RentStockRepository(context);
           _saleRepository = new SaleRepository(context);
           _saleDetailRepository = new SaleDetailRepository(context);
           _saleStockRepository = new SaleStockRepository(context);
           _superRentCartRepository = new SuperRentCartRepository(context);
           _transactionRepository = new TransactionRepository(context);



           _saleStockRepository = new SaleStockRepository(context);

           _userRepository = new UserRepository(context);
           _userCredentialRepository = new UserCredentialRepository(context);


           _bookServices = new BookServices(_userRepository, _feedbackRepository, _bookRepository, _saleStockRepository, _rentStockRepository);

           _cartServices = new CartServices(_userRepository,_userCredentialRepository, _cartRepository, _saleStockRepository, _transactionRepository, _saleRepository, _saleDetailRepository);

           _rentServices = new RentServices(_userRepository, _transactionRepository, _userCredentialRepository, _rentRepository, _rentStockRepository, _rentDetailRepository, _rentCartRepository, _superRentCartRepository);

           _saleServices = new SaleServices(_userRepository, _saleRepository, _rentRepository);

           _purchaseServices = new PurchaseServices(_saleStockRepository, _bookRepository, _rentStockRepository, _purchaseRepository, _purchaseDetailRepository, _transactionRepository);

           _tokenService = new TokenBL(mockConfig.Object);

           _authServices = new AuthBL(_userRepository, _userCredentialRepository, _tokenService);

           _userValidationService = new UserValidationService(_rentRepository, _userRepository, _fineDetailRepository, _userCredentialRepository, _fineRepository, _rentDetailRepository, _rentCartRepository, _superRentCartRepository);

           _fineServices = new FineServices(_userCredentialRepository, _fineDetailRepository, _rentRepository, _fineRepository, _rentStockRepository, _rentCartRepository, _superRentCartRepository, _rentDetailRepository);

       }




       [TearDown]
       public void TearDown()
       {
           context.Database.EnsureDeleted();
           context.Dispose();
       }






   }
}
