using Firebase.Auth;
using Firebase.Storage;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Milestone_Tracker.Models
{
    public class UploadAndDownload
    {
        public string RootFolder { get; }

        public UploadAndDownload()
        {
            RootFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }

        public async void UploadTOStorage()
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyCzSretU-4oxkfKSCSjfSYBRC8pGWz7oOI"));
            var auth = await authProvider.SignInWithEmailAndPasswordAsync("sreetej510@gmail.com", "dudecool");
            var uid = auth.User;

            var directories = Directory.GetDirectories(RootFolder);
            foreach (var dir in directories)
            {
                var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), dir);
                var dirName = Path.GetFileName(dir);
                var files = Directory.GetFiles(folder);
                foreach (var item in files)
                {
                    var itemName = Path.GetFileName(item);
                    var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), dir, item);
                    var stream = File.Open(path, FileMode.Open);
                    var fireStorage = new FirebaseStorage("milestone-tracker-728c6.appspot.com",
                        new FirebaseStorageOptions
                        {
                            AuthTokenAsyncFactory = () => Task.FromResult(auth.FirebaseToken)
                        });
                    await Task.Run(() => fireStorage.Child("Milestones").Child(uid.LocalId).Child(dirName).Child(itemName).PutAsync(stream));
                }
            }
        }
    }
}