using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NailsApp.Models;
using NailsApp.Services;

namespace NailsApp.ViewModels
{
    [QueryProperty(nameof(Post), "SelectedPost")]
    public class PostViewModel
    {
        private Post selectedPost;
        private NailsWebAPIProxy proxy;
        public PostViewModel(NailsWebAPIProxy proxy)
        {
            this.proxy = proxy;
            User u = ((App)Application.Current).LoggedInUser;//getting the current user using the app
            FirstName = u.FirstName;
            LastName = u.LastName;
            Name = FirstName + " " + LastName;
            SelectedPost = new Post();
            PhotoURL = proxy.GetImagesBaseAddress() + u.ProfilePic;
            PostURL = proxy.GetImagesBaseAddress() + SelectedPost.PostPicturePath;
            ProfileCommand = new Command(OnProfile);
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
        public Post SelectedPost
        {
            get => selectedPost;
            set
            {
                selectedPost = value;
                //OnPropertyChanged();
            }
        }
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
    }

}
