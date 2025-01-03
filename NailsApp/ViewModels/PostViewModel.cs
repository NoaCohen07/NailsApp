﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
//using Android.App;
using Microsoft.Extensions.DependencyInjection;
using NailsApp.Models;
using NailsApp.Services;
//using static Android.App.ActivityManager;

namespace NailsApp.ViewModels
{
    [QueryProperty(nameof(Post), "selectedPost")]
    public class PostViewModel:ViewModelBase
    {
        private NailsWebAPIProxy proxy;
        private IServiceProvider serviceProvider;
        private int likeCount = 0;
       
        private Post post;
        public Post Post
        {
            get => post;
            set
            {
                if (post != value)
                {
                    post = value;
                    InItFieldsDataAsync();
                    OnPropertyChanged(nameof(Post));
                }
            }
        }
        public PostViewModel(NailsWebAPIProxy proxy, IServiceProvider serviceProvider)
        {   
            this.proxy = proxy;
            
            //ProfileCommand = new Command(OnProfile);
            this.serviceProvider = serviceProvider;
            LikeCommand=new Command(OnLikeClicked);
            LikePic = "emptylike.png";
        }

        #region Name
        private string firstName;

        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        private string lastName;

        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                //ValidateLastName();
                OnPropertyChanged("LastName");
            }
        }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        #endregion

        #region ProfileImage

        private string photoURL;

        public string PhotoURL
        {
            get => photoURL;
            set
            {
                photoURL = value;
                OnPropertyChanged("PhotoURL");
            }
        }

        private string localPhotoPath;

        public string LocalPhotoPath
        {
            get => localPhotoPath;
            set
            {
                localPhotoPath = value;
                OnPropertyChanged("LocalPhotoPath");
            }
        }
        #endregion

        #region PostImage
        
        private string postURL;

        public string PostURL
        {
            get => postURL;
            set
            {
                postURL = value;
                OnPropertyChanged("PostURL");
            }
        }

        public ICommand ProfileCommand { get; }
        public async void OnProfile(User p)
        {
            if (p != null)
            {
                var navParam = new Dictionary<string, object>
                {
                    {"selectedUser",p }
                };
                await Shell.Current.GoToAsync("ProfileView", navParam);
                //SelectedPost = null;

            }


        }
        #endregion

        #region PostDescription
        private string postDescription;
        public string PostDescription
        {
            get => postDescription;
            set
            {
                postDescription = value;

                OnPropertyChanged(nameof(PostDescription));
            }
        }

        #endregion

        #region PostTime
        private DateTime postTime;
        public DateTime PostTime
        {
            get => postTime;
            set
            {
                postTime = value;
                OnPropertyChanged(nameof(PostTime));
            }
        }
        #endregion

        #region In it Fields with data
        //Define a method to initialize the fields with data

        private async void InItFieldsDataAsync()
        {
            int userId = (int)post.UserId;
            User u = await proxy.GetUser(userId);
            PostTime = post.PostTime;
            PostDescription = post.PostText;
            PostURL = post.PostPicturePath;
            PostData postData=new PostData();
            //Initialize the comments collection
            List<Comment> list = await proxy.GetComments(post.PostId);
            PostComments = new ObservableCollection<Comment>(list);
            PostLikes=await proxy.GetLikes(post.PostId);
            FirstName = u.FirstName;
            LastName = u.LastName;
            Name = FirstName + " " + LastName;
            if (u.ProfilePic == null)
            {
                PhotoURL = proxy.GetDefaultProfilePhotoUrl();
            }
            else
            {
                PhotoURL = proxy.GetImagesBaseAddress() + u.ProfilePic;
            }
            
        }
        #endregion

        #region Comments collection
        //Define an ObservableCollection of TaskComment to hold the comments for the task
        private ObservableCollection<Comment> postComments;
        public ObservableCollection<Comment> PostComments
        {
            get => postComments;
            set
            {
                postComments = value;
                OnPropertyChanged(nameof(PostComments));
            }
        }
        //Define a property to hold the new comment text
        private string newComment;
        public string NewComment
        {
            get => newComment;
            set
            {
                newComment = value;
                OnPropertyChanged(nameof(NewComment));
                OnPropertyChanged(nameof(EnableNewCommentButton));
            }
        }
        //define a boolean property to indiczte if the new comment field is not empty
        public bool EnableNewCommentButton
        {
            get => !string.IsNullOrEmpty(NewComment);
        }

        public Command CommentsCommand { get; }

        public async void OnComments()
        {
           // await Application.Current.MainPage.DisplayAlert("Comments", ,"close");

        }

        #endregion

        #region Add Comment
        //Define a command to add a new comment
        public Command AddCommentCommand { get; set; }
        //define a method to perform the add comment operation
        private void AddComment()
        {
            Comment newPostComment = new Comment()
            {
                PostId = Post.PostId,
                CommentTime = DateTime.Now,
                CommentText = NewComment,
                UserId = ((App)Application.Current).LoggedInUser.UserId
            };
            PostComments.Add(newPostComment);
            NewComment = "";
        }
        #endregion


        #region PostLikes
        private int postLikes;
        public int PostLikes
        {
            get => postLikes;
            set
            {
                postLikes = value;
                OnPropertyChanged(nameof(PostLikes));
            }
        }

   
        private string likePic;
        public string LikePic
        {
            get => likePic;
            set
            {
                likePic = value;
                OnPropertyChanged(nameof(LikePic));
            }
        }
        // Command for the "Like" button
        public ICommand LikeCommand { get; }


        // This method is invoked when the "Like" button is clicked
        // Command for the like button


      
        public void OnLikeClicked()
        {
            // Increment the click count
            likeCount++;
            // Switch between different images based on the click count
            if (likeCount % 2 == 0)
            {
                PostLikes--;
                LikePic = "emptylike.png"; // First image
            }
            else
            {
                PostLikes++;
                LikePic = "like.jpg"; // Second image
            }

        }
        #endregion



    }

}
