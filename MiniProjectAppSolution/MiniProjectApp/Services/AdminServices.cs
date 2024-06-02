using Microsoft.Extensions.Logging;
using MiniProjectApp.BussinessLogics.Interfaces;
using MiniProjectApp.Exceptions;
using MiniProjectApp.Models;
using MiniProjectApp.Models.DTO;
using MiniProjectApp.Repositories;
using MiniProjectApp.Repositories.Interface;
using System.Collections.Generic;

namespace MiniProjectApp.BussinessLogics
{
    public class AdminServices : IAdminServices
    {

        
        
        private readonly ICompositeKeyRepository<int, Cart> _CartRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IRepository<int, Sale> _saleRepository;
        private readonly ICompositeKeyRepository<int, SaleDetail> _saleDetailRepository;
        private readonly IRepository<int, Feedback> _feedbackRepository;
        
       
        private readonly IRepository<int, Rent> _rentRepository;
        private readonly ICompositeKeyRepository<int, RentDetail> _rentDetailRepository;
     

        private readonly IRepository<int, UserCredential> _userCredentialRepository;
        private readonly IRepository<int, Fine> _fineRepository;
       
        public AdminServices( ICompositeKeyRepository<int, Cart> CartRepository, ITransactionRepository transactionRepository, IRepository<int, Sale> saleRepository, ICompositeKeyRepository<int, SaleDetail> saleDetailRepository, IRepository<int, Feedback> feedbackRepository, IRepository<int,UserCredential> userCredentialRepository, IRepository<int,Rent> rentRepository, ICompositeKeyRepository<int,RentDetail> rentDetailRepository, IRepository<int,Fine> fineRepository)
        {

           
            _CartRepository = CartRepository;
           
            _transactionRepository = transactionRepository;
            _saleRepository = saleRepository;
            _saleDetailRepository = saleDetailRepository;
            _feedbackRepository = feedbackRepository;
            
           
            _userCredentialRepository = userCredentialRepository;
            _rentRepository = rentRepository;
            _rentDetailRepository = rentDetailRepository;
            
            _fineRepository = fineRepository;
          

        }

        


       

       

       


      

        

       

      

      

    }
}
