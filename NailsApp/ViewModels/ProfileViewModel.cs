﻿using NailsApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using static Java.Util.Jar.Attributes;
using NailsApp.Models;
using NailsApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls;
using NailsApp.Views;
using System.Windows.Input;
using System.Collections.ObjectModel;
//using Java.Time;

namespace NailsApp.ViewModels
{
    [QueryProperty(nameof(User), "selectedUser")]
    public class ProfileViewModel : ViewModelBase
    {
        private NailsWebAPIProxy proxy;
        private IServiceProvider serviceProvider;
        private User user;
        public User User
        {
            get => user;
            set
            {
                if (user != value)
                {
                    user = value;
                    InItFieldsDataAsync();
                    OnPropertyChanged(nameof(User));
                }
            }
        }

        public ProfileViewModel(NailsWebAPIProxy proxy)
        {
            //User u = ((App)Application.Current).LoggedInUser;//getting the current user using the app
 
            this.proxy = proxy;
           
            EditCommand = new Command(OnEdit);
           
            TreatmentsCommand = new Command(OnTreatments);
            FavoritesCommand = new Command(OnFavorites);
            
            ChatCommand = new Command(OnChat);
            SaveCommand = new Command(OnSave);
            
            UploadPhotoCommand = new Command(OnUploadPhoto);
            UploadTakePhotoCommand = new Command(OnUploadTakePhoto);
            
            
            ShowPasswordCommand = new Command(OnShowPassword);
            LocalPhotoPath = "";
            IsPassword = true;
            FirstNameError = "Name is required";
            LastNameError = "Last name is required";
            EmailError = "Email is required";
            DateError = "Age is required";
            PhoneNumberError = "Phone number is required";
            AddressError = "Address is required";
            DateError = "Age is required";
            PasswordError = "Password must be at least 4 characters long and contain letters and numbers";
            //RefreshCommand = new Command(Refresh);
            //ValidateManicurist();

            Posts = new ObservableCollection<Post>();
            // Subtract 10 years
            DateTime dateMinusTenYears = DateTime.Now.AddYears(-10);

           // this.Date = dateMinusTenYears.AddDays(-1);
            MaxDate = dateMinusTenYears;
            ReadPosts();

        }

        //Defiine properties for each field in the registration form including error messages and validation logic
        #region FirstName
        private bool showFirstNameError;

        public bool ShowFirstNameError
        {
            get => showFirstNameError;
            set
            {
                showFirstNameError = value;
                OnPropertyChanged("ShowFirstNameError");
            }
        }

        private string firstName;

        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                ValidateFirstName();
                OnPropertyChanged("FirstName");
            }
        }

        private string firstNameError;

        public string FirstNameError
        {
            get => firstNameError;
            set
            {
                firstNameError = value;
                OnPropertyChanged("FirstNameError");
            }
        }

        private void ValidateFirstName()
        {
            this.ShowFirstNameError = string.IsNullOrEmpty(FirstName);
        }
        #endregion

        #region LastName
        private bool showLastNameError;

        public bool ShowLastNameError
        {
            get => showLastNameError;
            set
            {
                showLastNameError = value;
                OnPropertyChanged("ShowLastNameError");
            }
        }

        private string lastName;

        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                ValidateLastName();
                OnPropertyChanged("LastName");
            }
        }

        private string lastNameError;

        public string LastNameError
        {
            get => lastNameError;
            set
            {
                lastNameError = value;
                OnPropertyChanged("LastNameError");
            }
        }

        private void ValidateLastName()
        {
            this.ShowLastNameError = string.IsNullOrEmpty(LastName);
        }
        #endregion

        #region Email
        private bool showEmailError;

        public bool ShowEmailError
        {
            get => showEmailError;
            set
            {
                showEmailError = value;
                OnPropertyChanged("ShowEmailError");
            }
        }

        private string email;

        public string Email
        {
            get => email;
            set
            {
                email = value;
                ValidateEmail();
                OnPropertyChanged("Email");
            }
        }

        private string emailError;

        public string EmailError
        {
            get => emailError;
            set
            {
                emailError = value;
                OnPropertyChanged("EmailError");
            }
        }

        private void ValidateEmail()
        {
            this.ShowEmailError = string.IsNullOrEmpty(Email);
            if (!ShowEmailError)
            {
                //check if email is in the correct format using regular expression
                if (!System.Text.RegularExpressions.Regex.IsMatch(Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                {
                    EmailError = "Email is not valid";
                    ShowEmailError = true;
                }
                else
                {
                    EmailError = "";
                    ShowEmailError = false;
                }
            }
            else
            {
                EmailError = "Email is required";
            }
        }
            #endregion

        #region DOB


        private bool showDateError;

        public bool ShowDateError
        {
            get => showDateError;
            set
            {
                showDateError = value;
                OnPropertyChanged("ShowDateError");
            }
        }

        private DateTime date;

        public DateTime Date
        {
            get => date;
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }

        private DateTime maxDate;

        public DateTime MaxDate
        {
            get => maxDate;
            set
            {
                maxDate = value;
                OnPropertyChanged("MaxDate");
            }
        }

        private string dateError;

        public string DateError
        {
            get => dateError;
            set
            {
                dateError = value;
                OnPropertyChanged("Date");
            }
        }


        #endregion

        #region Password
        private bool showPasswordError;

        public bool ShowPasswordError
        {
            get => showPasswordError;
            set
            {
                showPasswordError = value;
                OnPropertyChanged("ShowPasswordError");
            }
        }

        private string password;

        public string Password
        {
            get => password;
            set
            {
                password = value;
                ValidatePassword();
                OnPropertyChanged("Password");
            }
        }

        private string passwordError;

        public string PasswordError
        {
            get => passwordError;
            set
            {
                passwordError = value;
                OnPropertyChanged("PasswordError");
            }
        }

        private void ValidatePassword()
        {
            //Password must include characters and numbers and be longer than 4 characters
            if (string.IsNullOrEmpty(password) ||
                password.Length < 4 ||
                !password.Any(char.IsDigit) ||
                !password.Any(char.IsLetter))
            {
                this.ShowPasswordError = true;
            }
            else
                this.ShowPasswordError = false;
        }

        //This property will indicate if the password entry is a password
        private bool isPassword = true;
        public bool IsPassword
        {
            get => isPassword;
            set
            {
                isPassword = value;
                OnPropertyChanged("IsPassword");
            }
        }
        //This command will trigger on pressing the password eye icon
        public Command ShowPasswordCommand { get; }
        //This method will be called when the password eye icon is pressed
        public void OnShowPassword()
        {
            //Toggle the password visibility
            IsPassword = !IsPassword;
        }
        #endregion

        #region Photo

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

        public Command UploadPhotoCommand { get; }
        //This method open the file picker to select a photo
        private async void OnUploadPhoto()
        {
            try
            {
                var result = await MediaPicker.Default.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Please select a photo",
                });

                if (result != null)
                {
                    // The user picked a filex
                    this.LocalPhotoPath = result.FullPath;
                    this.PhotoURL = result.FullPath;
                }
            }
            catch (Exception ex)
            {

            }

        }

        public Command UploadTakePhotoCommand { get; }
        //This method open the file picker to select a photo
        private async void OnUploadTakePhoto()
        {
            try
            {
                var result = await MediaPicker.Default.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = "Please select a photo",
                });

                if (result != null)
                {
                    // The user picked a file
                    this.LocalPhotoPath = result.FullPath;
                    this.PhotoURL = result.FullPath;
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void UpdatePhotoURL(string virtualPath)
        {
            Random r = new Random();
            PhotoURL = proxy.GetImagesBaseAddress() + virtualPath + "?v=" + r.Next();
            LocalPhotoPath = "";
        }

        #endregion

        #region PhoneNumber

        private bool showPhoneNumberError;

        public bool ShowPhoneNumberError
        {
            get => showPhoneNumberError;
            set
            {
                showPhoneNumberError = value;
                OnPropertyChanged("ShowPhoneNumberError");
            }
        }

        private string phoneNumber;

        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                phoneNumber = value;
                ValidatePhoneNumber();
                OnPropertyChanged("PhoneNumber");
            }
        }

        private string phoneNumberError;

        public string PhoneNumberError
        {
            get => phoneNumberError;
            set
            {
                phoneNumberError = value;
                OnPropertyChanged("PhoneNumberError");
            }
        }

        private void ValidatePhoneNumber()
        {
            this.ShowPhoneNumberError = string.IsNullOrEmpty(PhoneNumber);
            if (this.PhoneNumber.Length != 10)
            {
                this.ShowPhoneNumberError = true;
            }
        }
        #endregion

        #region Address
        private bool showAddressError;

        public bool ShowAddressError
        {
            get => showAddressError;
            set
            {
                showAddressError = value;
                OnPropertyChanged("ShowAddressError");
            }
        }

        private string address;

        public string Address
        {
            get => address;
            set
            {
                address = value;
                ValidateAddress();
                OnPropertyChanged("Address");
            }
        }

        private string addressError;

        public string AddressError
        {
            get => addressError;
            set
            {
                addressError = value;
                OnPropertyChanged("AddressError");
            }
        }

        private void ValidateAddress()
        {
            this.ShowAddressError = string.IsNullOrEmpty(Address);
        }
        #endregion

        #region Gender

        private char gender;

        public char Gender
        {
            get => gender;
            set
            {
                gender = value;
                OnPropertyChanged("Gender");
            }
        }




        #endregion

        #region UserType
        private bool isManicurist;

        public bool IsManicurist
        {
            get => isManicurist;
            set
            {
                isManicurist = value;
                OnPropertyChanged("IsManicurist");
               
            }
        }
        #endregion

        #region Change
        private bool change;

        public bool Change
        {
            get => change;
            set
            {
                change = value;
                OnPropertyChanged("Change");
            }
        }
        #endregion

        #region Collection View of Posts
        private ObservableCollection<Post> posts;
        public ObservableCollection<Post> Posts
        {
            get
            {
                return this.posts;
            }
            set
            {
                this.posts = value;
                OnPropertyChanged();
            }
        }
       

        private async void ReadPosts()
        {
            List<Post> list = await proxy.GetPosts();
            foreach(Post p in list)
            {
                p.PostPicturePath= proxy.GetImagesBaseAddress()+ p.PostPicturePath;
            }
            this.Posts = new ObservableCollection<Post>(list);
        }


        #region Single Selection

       
        private Post selectedPost;
        public Post SelectedPost
        {
            get
            {
                return this.selectedPost;
            }
            set
            {
                this.selectedPost = value;
                OnSingleSelectPost(selectedPost);
                OnPropertyChanged();
            }
        }
       


        private async void OnSingleSelectPost(Post p)
        {
            if (p != null)
            {
                var navParam = new Dictionary<string, object>
                {
                    {"selectedPost",p }
                };
                await Shell.Current.GoToAsync("PostView", navParam);
                
                SelectedPost = null;
            
            }
        }

        #endregion
        #endregion

        #region In it Fields with data
        //Define a method to initialize the fields with data

        private async void InItFieldsDataAsync()
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Password = user.Pass;
            TimeOnly time = new TimeOnly();
            Date = user.DateOfBirth.ToDateTime(time);
            PhoneNumber = user.PhoneNumber;
            Address = user.UserAddress;

            PhotoURL = proxy.GetImagesBaseAddress() + user.ProfilePic;

            Gender = (char)user.Gender;
        }
        #endregion

        public Command EditCommand { get; }

        public void OnEdit()
        {
            Change = true;
        }

        //Define a command for the Save button
        public Command SaveCommand { get; }

        //Define a method that will be called when the register button is clicked
        public async void OnSave()
        {
            ValidateFirstName();
            ValidateLastName();
            ValidateEmail();
            ValidatePassword();
            ValidatePhoneNumber();
            ValidateAddress();
            //ValidateManicurist();
            
           
            if (!ShowFirstNameError && !ShowLastNameError && !ShowEmailError && !ShowPasswordError && !ShowPhoneNumberError && !ShowAddressError )
            {
                //Update AppUser object with the data from the Edit form
                User theUser = User;
                theUser.FirstName = FirstName;
                theUser.LastName = LastName;
                theUser.Email = Email;
                theUser.Pass = Password;
                theUser.DateOfBirth = new DateOnly(Date.Year, Date.Month, Date.Day);
                theUser.PhoneNumber = PhoneNumber;
                theUser.ProfilePic = PhotoURL;
                theUser.UserAddress = Address;
                theUser.Gender = Gender;
                theUser.IsManicurist= IsManicurist;
                  

                //Call the Register method on the proxy to register the new user
                InServerCall = true;
                bool success = await proxy.UpdateUser(theUser);

                Change = false;
                //If the save was successful, navigate to the login page
                if (success)
                {
                    //Upload profile imae if needed
                    if (!string.IsNullOrEmpty(LocalPhotoPath))
                    {
                        User? updatedUser = await proxy.UploadProfileImage(LocalPhotoPath);
                        if (updatedUser == null)
                        {
                            await Shell.Current.DisplayAlert("Save Profile", "User Data Was Saved BUT Profile image upload failed", "ok");
                        }
                        else
                        {
                            theUser.ProfilePic = updatedUser.ProfilePic;
                            UpdatePhotoURL(theUser.ProfilePic);
                        }

                    }
                    InServerCall = false;
                    await Shell.Current.DisplayAlert("Save Profile", "Profile saved successfully", "ok");
                }
                else
                {
                    InServerCall = false;
                    //If the registration failed, display an error message
                    string errorMsg = "Save Profile failed. Please try again.";
                    await Shell.Current.DisplayAlert("Save Profile", errorMsg, "ok");
                }
            }
        }
        
        public ICommand TreatmentsCommand { get; }
        public async void OnTreatments()
        {
            await Shell.Current.GoToAsync("TreatmentsView");

        }

        public ICommand ChatCommand { get; }

        public async void OnChat()
        {
            await Shell.Current.GoToAsync("ChatView");
        }

        public ICommand FavoritesCommand { get; }

        public async void OnFavorites()
        {
            await Shell.Current.GoToAsync("FavoritesView");
        }

    
    }
}
