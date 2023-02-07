using IdentifyBase.Application.Abstractions.Database;
using IdentifyBase.Application.Abstractions.Services;
using IdentifyBase.Domain.Entities;
using IdentifyBase.Domain.Features.Commands.User;
using IdentifyBase.Domain.Features.Responses;
using IdentifyBase.Domain.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentifyBase.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IMainContextRepository<Product> _productRepository;

        public ProductService(IMainContextRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<RepositoryResponse<IEnumerable<Product>>> GetProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }
    }
}