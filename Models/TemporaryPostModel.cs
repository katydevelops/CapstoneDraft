using Microsoft.AspNetCore.Components.Forms;

namespace CapstoneDraft.Models
{
    // An additional model was needed to hold the input from the user's post before it was stored permanently in the PostModel
    public class TemporaryPostModel
    {
        public string TempPostCategory { get; set; }
        public string TempPostNameOfPostAuthor { get; set;}
        public string TempPostUserLocation { get; set; }
        public string TempPostSubject { get; set; }
        public string TempPostMessageBody { get; set; }
        public string TempPostPhoto {  get; set; }
    }
}
