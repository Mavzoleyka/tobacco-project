using Data.CigaretteTables;
using Domain.CigaretteDomain.CigaretteCategorie.Commands.Object;
using Domain.CigaretteDomain.CigarettePhoto.Commands.Object;
using Domain.CigaretteDomain.CigaretteProduct;
using Domain.CigaretteDomain.CigaretteProduct.Commands.Object;
using Domain.CigaretteDomain.CigaretteProduct.Querys.Object;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.CigaretteRepositories
{
    public class CigaretteProductRepository : ICigaretteProductRepository
    {
        private readonly Connection _connection;
        public CigaretteProductRepository(Connection connection)
        {
            ArgumentNullException.ThrowIfNull(connection, nameof(connection));
            _connection = connection;
        }
        public async Task<bool> AddAsync(AddCigaretteProductDTO entity)
        {
            var DTOlist =  entity.CigarettesPhotos.ToList();
            try
            {
                 Product product = new Product();
                 product.Name = entity.Name;
                 product.Description = entity.Description;
                 product.Stock = entity.Stock;
                 product.Brand = entity.Brand;
                 product.Price = entity.Price;
                 product.CategodyId = entity.CategoryId;
                 product.ManufacturerId = entity.ManufacturerId;
                 foreach (var item in DTOlist)
                 {
                    ProductPhoto photo = new ProductPhoto();
                    photo.Id = item.Id;
                    photo.IsMain = item.IsMain;
                    photo.Caption = item.Caption;
                    photo.ImageURL = item.ImageURL;
                    product.CigarettesPhotos.Add(photo);
                 }
                 await _connection.AddAsync(product);
                 await _connection.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }



        public async Task<CigaretteProductDTO?> GetByIdAsync(GetByIdDTO obj)
        {
            return await _connection.СigarettesProducts.Include(p => p.CigarettesPhotos)
                                                       .AsNoTracking()
                                                       .FirstOrDefaultAsync(row => row.Id == obj.Id)
                                                       .ParseAsync();
        }



        public async Task<List<CigaretteProductDTO>> GetAllAsync()
        {
            var list = await _connection.СigarettesProducts
                                         .Include(p => p.CigarettesPhotos) 
                                         .AsNoTracking()
                                         .ToListAsync();

            var products = list.Select(row => new CigaretteProductDTO
            {
                Id = row.Id,
                Brand = row.Brand,
                Description = row.Description,
                Name = row.Name,
                Stock = row.Stock,
                Price = row.Price,
                CategoryId = row.CategodyId,
                ManufacturerId = row.ManufacturerId,
                CigarettesPhotos = row.CigarettesPhotos
                    .Where(p => !p.IsDelete)
                    .Select(obj => new CigarettePhotoDTO
                    {
                        Id = obj.Id,
                        Caption = obj.Caption,
                        ImageURL = obj.ImageURL,
                        IsMain = obj.IsMain,
                        ProductId = obj.ProductId,
                    }).ToList()
            }).ToList(); 

            return products;
        }

        

        public async Task<bool> DeleteAsync(DeleteCigaretteProductDTO obj)
        {
            if (obj == null) return false;

            var product = await _connection.СigarettesProducts
                .FirstOrDefaultAsync(p => p.Id == obj.Id && !p.IsDelete);

            if (product == null) return false;

            product.IsDelete = true;
            await _connection.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(UpdateCigaretteProductDTO obj)
        {
            if (obj == null) return false;

            var product = await _connection.СigarettesProducts
                .FirstOrDefaultAsync(p => p.Id == obj.Id && !p.IsDelete);

            if (product == null) return false;

            product.Name = obj.Name;
            product.Description = obj.Description;
            product.Stock = obj.Stock;
            product.Brand = obj.Brand;
            product.Price = obj.Price;
            product.CategodyId = obj.CategoryId;
            product.ManufacturerId = obj.ManufacturerId;

            await _connection.SaveChangesAsync();
            return true;
        }

        public async Task<List<CigaretteProductDTO>> GetProductsByCategoryAsync(GetByIdDTOforQuery obj)
        {
            var list = await _connection.СigarettesProducts.Include(row=>row.CigarettesPhotos)
                                                           .Include(p=>p.Categorie)
                                                           .Where(p=>p.CategodyId==obj.CategoryId)
                                                           .ToListAsync();
            var products = list.Select(row => new CigaretteProductDTO
            {
                Id = row.Id,
                Brand = row.Brand,
                Description = row.Description,
                Name = row.Name,
                Stock = row.Stock,
                Price = row.Price,
                CategoryId = row.CategodyId,
                ManufacturerId = row.ManufacturerId,
                CategoryName = row.Categorie.Name,
                CigarettesPhotos = row.CigarettesPhotos
                    .Where(p => !p.IsDelete)
                    .Select(obj => new CigarettePhotoDTO
                    {
                        Id = obj.Id,
                        Caption = obj.Caption,
                        ImageURL = obj.ImageURL,
                        IsMain = obj.IsMain,
                        ProductId = obj.ProductId,
                    }).ToList()
            }).ToList(); 

            return products;
        }

        public async Task<NameOfProductCategoryDTO> GetNameOfProductCategoryAsync(GetByIdDTOforQuery obj)
        {
            var name = await _connection.СigarettesProducts
                                        .Include(row => row.Categorie)
                                        .Where(row => row.Id == obj.CategoryId)
                                        .Select(p => p.Categorie.Name)
                                        .FirstOrDefaultAsync();

            var result = new NameOfProductCategoryDTO() { CategoryName = name };
            return result;

        }

        public async Task<NameOfProductManufacturerDTO> GetNameOfProductManufacturerAsync(GetByIdDTOforQuery obj)
        {
            var name = await _connection.СigarettesProducts
                                        .Include (row => row.Manufacturer)
                                        .Where(row=>row.Id==obj.CategoryId)
                                        .Select (p => p.Manufacturer.Name)
                                        .FirstOrDefaultAsync();

            var result = new NameOfProductManufacturerDTO() { ManufacturerName = name };
            return result;
        }
    }
}
