using Data.CigaretteTables;
using Domain.CigaretteDomain.CigaretteCategorie.Commands.Object;
using Domain.CigaretteDomain.CigarettePhoto.Commands.Object;
using Domain.CigaretteDomain.CigaretteProduct.Commands.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Data.CigaretteRepositories
{
    public static class ConverterCigarette 
    {
        public static async Task<CigaretteManufacturerDTO?> ParseAsync(this Task<CigarettesManufacturer?> element)
        {
            var manufacturer = await element;
            if (manufacturer == null) return null;
            CigaretteManufacturerDTO item = new CigaretteManufacturerDTO();
            item.Id = manufacturer.Id;
            item.Name = manufacturer.Name;
            item.Country = manufacturer.Country;
            return item;
        }

        public static async Task<CigaretteCategoryDTO?> ParseAsync(this Task<CigarettesCategorie?> element)
        {
            var category = await element;
            if (category == null) return null;
            CigaretteCategoryDTO item = new CigaretteCategoryDTO();
            item.Id = category.Id;
            item.Name = category.Name;
            return item;
        }

        public static async Task<CigaretteProductDTO?> ParseAsync(this Task<СigarettesProduct?> element)
        {
            var entity = await element;
            if (entity == null) return null;


            var DTOlist =  entity.CigarettesPhotos.ToList();

            CigaretteProductDTO product = new CigaretteProductDTO();
            product.Name = entity.Name;
            product.Description = entity.Description;
            product.Stock = entity.Stock;
            product.Brand = entity.Brand;
            product.CategoryId = entity.CategodyId;
            product.ManufacturerId = entity.ManufacturerId;
            foreach (var item in DTOlist)
            {
                CigarettePhotoDTO photo = new CigarettePhotoDTO();
                photo.Id = item.Id;
                photo.IsMain = item.IsMain;
                photo.Caption = item.Caption;
                photo.ImageURL = item.ImageURL;
                product.CigarettesPhotos.Add(photo);
            }
            return product;
        }

        public static async Task<CigarettePhotoDTO?> ParseAsync(this Task<CigarettesPhoto?> element)
        {
            var entity = await element;
            if (entity == null) return null;
            CigarettePhotoDTO photoDTO  = new CigarettePhotoDTO();
            photoDTO.Id = entity.Id;
            photoDTO.IsMain = entity.IsMain;
            photoDTO.Caption = entity.Caption;
            photoDTO.ImageURL = entity.ImageURL;
            return photoDTO;
        }
    }
}
