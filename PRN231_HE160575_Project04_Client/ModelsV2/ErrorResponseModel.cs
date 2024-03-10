namespace PRN231_HE160575_Project04_Client.ModelsV2
{
    public class ErrorResponseModel
    {
        public int ErrorCode { get; set; }
        public Dictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();

        public void setSucessMessage(string message)
        {
            ErrorCode = 200;
            Errors = new Dictionary<string, string[]>()
                {
                    { "success", new string[] { message } }
                };
        }
        public void setFailMessage(string message)
        {
            ErrorCode = 400;
            Errors = new Dictionary<string, string[]>()
                {
                    { "success", new string[] { message } }
                };
        }

        public override string? ToString()
        {
            string errors = "";
            try
            {
                foreach (var error in Errors)
                {
                    errors = error.Value[0] + Environment.NewLine;
                }
            }
            catch (Exception ex) { 
            }
            return errors;
        }
    }
}
