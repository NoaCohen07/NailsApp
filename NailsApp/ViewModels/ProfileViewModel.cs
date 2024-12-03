using NailsApp.Services;
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
    public class ProfileViewModel : ViewModelBase
    {
        private NailsWebAPIProxy proxy;
        private IServiceProvider serviceProvider;

        public ProfileViewModel(NailsWebAPIProxy proxy)
        {
            User u = ((App)Application.Current).LoggedInUser;//getting the current user using the game
 
            this.proxy = proxy;
            TimeOnly time = new TimeOnly();
            EditCommand = new Command(OnEdit);
            PostCommand = new Command(OnPost);
            TreatmentsCommand = new Command(OnTreatments);
            FavoritesCommand = new Command(OnFavorites);
            FirstName = u.FirstName;
            LastName = u.LastName;
            Email = u.Email;
            Password = u.Pass;
            Date = u.DateOfBirth.ToDateTime(time); 
            PhoneNumber = u.PhoneNumber;
            Address = u.UserAddress;
            PhotoURL = u.ProfilePic;
            Gender = (char)u.Gender;
            ChatCommand = new Command(OnChat);
            SaveCommand = new Command(OnSave);
            UploadPhotoCommand = new Command(OnUploadPhoto);
            UploadTakePhotoCommand = new Command(OnUploadTakePhoto);
            SelectPostCommand = new Command((Object obj) => SelectPost(obj));
            PhotoURL = proxy.GetDefaultProfilePhotoUrl();
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



            // Subtract 10 years
            DateTime dateMinusTenYears = DateTime.Now.AddYears(-10);

            this.Date = dateMinusTenYears.AddDays(-1);
            MaxDate = dateMinusTenYears;


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
                    // The user picked a file
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
            this.Posts = new ObservableCollection<Post>(list);
        }


        #region Single Selection
        private Object selectedPost;
        public Object SelectedPost
        {
            get
            {
                return this.selectedPost;
            }
            set
            {
                this.selectedPost = value;
                OnPropertyChanged();
            }
        }
        public ICommand SelectPostCommand { get;private set; }
        public async void SelectPost(Object obj)
        {

        }

        public ICommand SingleSelectCommand => new Command(OnSingleSelectPost);

        async void OnSingleSelectPost()
        {
            if (SelectedPost == null || !(SelectedPost is Post))
            {
               // SelectedPosts = "none";
            }
            else { }
                //SelectedNames = ((Student)SelectedStudent).FirstName;
        }

        #endregion
        #endregion

        #region Refresh View
        public ICommand RefreshCommand => new Command(Refresh);
        private async void Refresh()
        {
            Posts.Add(new Post
            {
                //FirstName = "Just",
                //LastName = "Added!",
                //BirthDate = DateTime.Now,
                //AverageScore = 100
            });


            IsRefreshing = false;
        }

        private bool isRefreshing;
        public bool IsRefreshing
        {
            get
            {
                return this.isRefreshing;
            }
            set
            {
                this.isRefreshing = value;
                OnPropertyChanged();
            }
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
            
           
            if (!ShowFirstNameError && !ShowLastNameError && !ShowEmailError && !ShowPasswordError && !ShowPhoneNumberError && !ShowAddressError)
            {
                //Update AppUser object with the data from the Edit form
                User theUser = ((App)App.Current).LoggedInUser;
                theUser.FirstName = FirstName;
                theUser.LastName = LastName;
                theUser.Email = Email;
                theUser.Pass = Password;
                theUser.DateOfBirth = new DateOnly(Date.Year, Date.Month, Date.Day);
                theUser.PhoneNumber = PhoneNumber;
                theUser.UserAddress = Address;
                theUser.Gender = Gender;
                  

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
        public ICommand PostCommand { get; }


        public ICommand TreatmentsCommand { get; }

       
        public async void OnPost()
        {
            await AppShell.Current.GoToAsync("PostView");
        }

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
