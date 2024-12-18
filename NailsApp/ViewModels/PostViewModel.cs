using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using NailsApp.Models;
using NailsApp.Services;
//using static Android.App.ActivityManager;

namespace NailsApp.ViewModels
{
    [QueryProperty(nameof(Post), "selectedPost")]
    public class PostViewModel:ViewModelBase
    {
        private Post selectedPost;
        private NailsWebAPIProxy proxy;
        private IServiceProvider serviceProvider;


        private Post post;
        public Post Post
        {
            get => post;
            set
            {
                if (post != value)
                {
                    post = value;
                    InItFieldsData();
                    OnPropertyChanged(nameof(Post));
                }
            }
        }
        public PostViewModel(NailsWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            User u = ((App)Application.Current).LoggedInUser;//getting the current user using the app
            FirstName = u.FirstName;
            LastName = u.LastName;
            Name = FirstName + " " + LastName;
            //SelectedPost = new Post();
            PhotoURL = proxy.GetImagesBaseAddress() + u.ProfilePic;
            //PostURL = proxy.GetImagesBaseAddress() + Post.PostPicturePath;
            ProfileCommand = new Command(OnProfile);
            this.serviceProvider = serviceProvider;
            PostDescription = Post.PostText;
            //PostTime = Post.PostTime;
        }

        #region Name
        private string firstName;

        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
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
                //OnPropertyChanged("LastName");
            }
        }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
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
                //OnPropertyChanged("PhotoURL");
            }
        }

        private string localPhotoPath;

        public string LocalPhotoPath
        {
            get => localPhotoPath;
            set
            {
                localPhotoPath = value;
                // OnPropertyChanged("LocalPhotoPath");
            }
        }
        #endregion

        #region PostImage
        //public Post SelectedPost
        //{
        //    get => selectedPost;
        //    set
        //    {
        //        selectedPost = value;
        //        //OnPropertyChanged();
        //    }
        //}
        private string postURL;

        public string PostURL
        {
            get => postURL;
            set
            {
                postURL = value;
                //OnPropertyChanged("PhotoURL");
            }
        }

        public ICommand ProfileCommand { get; }
        public async void OnProfile()
        {
            await Shell.Current.GoToAsync("ProfileView");

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
        private void InItFieldsData()
        {
            PostTime = post.PostTime;
            PostDescription = post.PostText;
            PostURL = proxy.GetImagesBaseAddress() + post.PostPicturePath;
            PostData postData=new PostData();
            //Initialize the comments collection
            //PostComments = new ObservableCollection<Comment>(UserTask.TaskComments);
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

      
    }

}
