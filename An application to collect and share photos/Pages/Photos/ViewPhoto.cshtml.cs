using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace An_application_to_collect_and_share_photos.Pages.Photos
{
    public class ViewPhotoModel : PageModel
    {
        private readonly IPhotoService _photoService;
        private readonly ILikeService _likeService;
        private readonly ICommentService _commentService;
        private readonly UserManager<User> _userManager;

        public ViewPhotoModel(IPhotoService photoService, ILikeService likeService, ICommentService commentService, UserManager<User> userManager)
        {
            _photoService = photoService;
            _likeService = likeService;
            _commentService = commentService;
            _userManager = userManager;
        }

        public Photo photo;
        public User user;
        public User currentUser;
        public int likecount;
        public string commentText;
        public IEnumerable<Comment> comments;

        public bool isCurrentUserOwner;
        public async Task<IActionResult> OnGet(ObjectId photoId, string userId)
        {
            currentUser = await _userManager.GetUserAsync(User);
            
            user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }
            if (User.Identity.IsAuthenticated)
            {
                isCurrentUserOwner = (currentUser.Id == user.Id);
            }

            photo = await _photoService.GetPhotoByIdAsync(photoId);
            if (photo == null)
            {
                return NotFound();
            }
            likecount = await _likeService.GetLikeCountForPhotoAsync(photoId);
            comments = await _commentService.GetCommentsForPhotoAsync(photoId);

            return Page();
        }

        public async Task<IActionResult> OnPostLike(ObjectId photoId)
        {
            currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound();
            }
            photo = await _photoService.GetPhotoByIdAsync(photoId);
            if (photo == null)
            {
                return NotFound();
            }

            Like like = new Like
            {
                Id = ObjectId.GenerateNewId(),
                UserId = currentUser.Id,
                PhotoId = photoId,
                LikeCount = 0
            };
            await _likeService.LikePhotoAsync(currentUser.Id, photoId, like);
            comments = await _commentService.GetCommentsForPhotoAsync(photoId);
            likecount = await _likeService.GetLikeCountForPhotoAsync(photoId);
            return Page();
        }

        public async Task<IActionResult> OnPostComment(ObjectId photoId, string commentText)
        {
            
            currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound();
            }
            photo = await _photoService.GetPhotoByIdAsync(photoId);
            if (photo == null)
            {
                return NotFound();
            }
            if (commentText == null || commentText == string.Empty)
            {
                TempData["Error"] = "Write something!";
                comments = await _commentService.GetCommentsForPhotoAsync(photoId);
                likecount = await _likeService.GetLikeCountForPhotoAsync(photoId);
                return Page();
            }
            Comment comment = new Comment
            {
                Id = ObjectId.GenerateNewId(),
                UserId = currentUser.Id,
                UserName = currentUser.UserName,
                PhotoId = photoId,
                CommentText = commentText,
                Date = DateTime.Now
            };
            await _commentService.CreateCommentAsync(comment);
            comments = await _commentService.GetCommentsForPhotoAsync(photoId);
            likecount = await _likeService.GetLikeCountForPhotoAsync(photoId);
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteComment(ObjectId commentId, ObjectId photoId)
        {
            photo = await _photoService.GetPhotoByIdAsync(photoId);
            if (photo == null)
            {
                return NotFound();
            }
            currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound();
            }

            var comment = await _commentService.GetCommentByIdAsync(commentId);
            if (comment == null)
            {
                return NotFound();
            }
            await _commentService.DeleteCommentAsync(comment);
            comments = await _commentService.GetCommentsForPhotoAsync(photoId);
            likecount = await _likeService.GetLikeCountForPhotoAsync(photoId);
            return Page();
        }
    }
}
