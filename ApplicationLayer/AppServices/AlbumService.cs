using Domain.Models;
using Domain.Repositories;
using MongoDB.Bson;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.AppServices
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;

        public AlbumService(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public async Task<IEnumerable<Album>> GetAllAlbums()
        {
            return await _albumRepository.GetAll();
        }

        public async Task<Album> GetAlbumByIdAsync(ObjectId id)
        {
            try
            {
                var album = await _albumRepository.GetById(id);

                if (album == null)
                {
                    throw new Exception($"Album with id {id} not found.");
                }

                return album;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving photo with id {id}: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Album>> GetAlbumsByUserIdAsync(ObjectId userId)
        {
            try
            {
                var album = await _albumRepository.GetAlbumsByUserIdAsync(userId);

                if (album == null)
                {
                    throw new Exception($"Albums with user id {userId} not found.");
                }

                return album;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving albums with user id {userId}: {ex.Message}");
                throw;
            }
        }

        public async Task CreateAlbumAsync(Album album)
        {
            try
            {
                var existingAlbum = await _albumRepository.GetById(album.Id);
                    
                if (existingAlbum != null)
                {
                    throw new Exception($"Album with id {album.Id} already exists.");
                }

                await _albumRepository.Create(album);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while creating the album: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateAlbumAsync(ObjectId id, Album album)
        {
            try
            {
                var existingAlbum = await _albumRepository.GetById(album.Id);

                if (existingAlbum == null)
                {
                    throw new Exception($"Album with id {album.Id} doesn't exists.");
                }

                await _albumRepository.Update(id,album);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating the album: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteAlbumAsync(ObjectId id)
        {
            try
            {
                var existingAlbum = await _albumRepository.GetById(id);

                if (existingAlbum == null)
                {
                    throw new Exception($"Album with id {id} doesn't exists.");
                }

                await _albumRepository.Delete(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting the album: {ex.Message}");
                throw;
            }
        }
    }
}
