using Domain.Models;
using Infrastructure.Data.UserRoles__Static_;
using Infrastructure.MongoDB;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

public class MongoDBContext<TUser, TRole, TKey> : IdentityDbContext<TUser, TRole, TKey>
    where TUser : IdentityUser<TKey>
    where TRole : IdentityRole<TKey>
    where TKey : IEquatable<TKey>
{

    private readonly IMongoCollection<Photo> _photos;
    private readonly IMongoCollection<Album> _albums;
    private readonly IMongoCollection<Like> _likes;
    private readonly IMongoCollection<Comment> _comments;

    private readonly IMongoCollection<User> _users;
    private readonly IMongoCollection<UserRole> _roles;


    private readonly UserManager<User> _userManager;
    private readonly RoleManager<UserRole> _roleManager;

    public MongoDBContext(IOptions<MongoDBSettings> settings, UserManager<User> userManager, RoleManager<UserRole> roleManager)
    {
        var client = new MongoClient(settings.Value.ConnectionUri);
        var database = client.GetDatabase(settings.Value.DatabaseName);

        _users = database.GetCollection<User>(settings.Value.UserCollection);
        _roles = database.GetCollection<UserRole>(settings.Value.UserRoleCollection);
        _photos = database.GetCollection<Photo>(settings.Value.PhotoCollection);
        _albums = database.GetCollection<Album>(settings.Value.AlbumCollection);
        _likes = database.GetCollection<Like>(settings.Value.LikeCollection);
        _comments = database.GetCollection<Comment>(settings.Value.CommentCollection);

        _userManager = userManager;
        _roleManager = roleManager;
    }


    public IMongoCollection<Photo> Photos => _photos;
    public IMongoCollection<Album> Albums => _albums;
    public IMongoCollection<Like> Likes => _likes;
    public IMongoCollection<Comment> Comments => _comments;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Customize the Identity model

        // Ignore additional properties in the User class
        builder.Entity<TUser>(entity =>
        {
            entity.Ignore(e => e.EmailConfirmed);
            entity.Ignore(e => e.PhoneNumber);
            entity.Ignore(e => e.PhoneNumberConfirmed);
            entity.Ignore(e => e.TwoFactorEnabled);
            entity.Ignore(e => e.LockoutEnd);
            entity.Ignore(e => e.LockoutEnabled);
            entity.Ignore(e => e.AccessFailedCount);
        });
    }
}
